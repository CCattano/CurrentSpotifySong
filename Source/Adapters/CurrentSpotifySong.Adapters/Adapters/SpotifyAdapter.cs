using Torty.Web.Apps.CurrentSpotifySong.Adapters.Exceptions.Spotify;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.Exceptions;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ResponseDtos;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;

public interface ISpotifyAdapter
{
    Task<string> GetAuthorizeUri(string unauthorizedUserId);
    Task<UserBE> GenerateAccessTokenForUser(string authorizationCode, string state);
    Task<string> GetCurrentlyPlayingTrack(string userId);
}

public class SpotifyAdapter : ISpotifyAdapter
{
    private readonly ISpotifyClient _client;
    private readonly IUserAdapter _userAdapter;

    public SpotifyAdapter(
        ISpotifyClient client,
        IUserAdapter userAdapter
    )
    {
        _client = client;
        _userAdapter = userAdapter;
    }

    public async Task<string> GetAuthorizeUri(string unauthorizedUserId)
    {
        UnauthenticatedUserBE user = await _userAdapter.GetUnauthenticatedUserById(unauthorizedUserId);
        if (DateTime.Now > user.ExpireDateTime)
        {
            await _userAdapter.DeleteExpiredUnauthenticatedUsers();
            throw new InvalidRegistrationTargetException();
        }
        string uri = _client.GetAuthorizeUri(user.Id);
        return uri;
    }

    public async Task<UserBE> GenerateAccessTokenForUser(string authorizationCode, string state)
    {
        // This method throws an exception if a unauthenticated user doesn't exist
        // We don't actually need any information about the unauthenticated user
        // As long as we get past this line without blowing up we're okay to continue
        await _userAdapter.GetUnauthenticatedUserById(state);
        AccessTokenDto accessToken = await _client.GetAccessToken(authorizationCode);
        // After fetching an access token for this user they're a fully authenticated user in our system
        // We need to create a row to represent them in our Users table
        UserBE userToCreate = new()
        {
            AccessToken = accessToken.AccessToken,
            RefreshToken = accessToken.RefreshToken
        };
        UserBE newUser = await _userAdapter.CreateUser(userToCreate);
        // Now that they're no longer a temporary unauthenticated user
        // let's remove their obsolete unauthenticated user row in our table
        await _userAdapter.DeleteUnauthenticatedUser(state);
        return newUser;
    }

    public async Task<string> GetCurrentlyPlayingTrack(string userId)
    {
        UserBE user = await _userAdapter.GetUserById(userId);
        try
        {
            string currentTrackInfo = await _GetCurrentlyPlayingTrack(user.AccessToken);
            return currentTrackInfo;
        }
        catch (BadOrExpiredTokenException)
        {
            // If our token has expired we'll attempt to refresh the token
            AccessTokenDto newAccessToken = await _client.RefreshAccessToken(user.RefreshToken);
            string refreshToken = string.IsNullOrWhiteSpace(newAccessToken.RefreshToken)
                ? user.RefreshToken
                : newAccessToken.RefreshToken;
            UserBE newUserDetails = user with
            {
                AccessToken = newAccessToken.AccessToken,
                RefreshToken = refreshToken
            };
            UserBE updateUser = await _userAdapter.UpdateUser(newUserDetails);
            // Then we'll try to make our request one more time
            string currentTrackInfo = await _GetCurrentlyPlayingTrack(updateUser.AccessToken);
            return currentTrackInfo; 
            // We won't catch any exceptions and will let them bubble at this point
        }
    }

    private async Task<string> _GetCurrentlyPlayingTrack(string accessToken)
    {
        CurrentlyPlayingResponseDto currentlyPlayingTrack = await _client.GetCurrentlyPlayingTrack(accessToken);
        string response = currentlyPlayingTrack?.CurrentlyPlayingType?.ToLower() switch
        {
            "track" => ParseTrack(currentlyPlayingTrack.Item as TrackDto),
            "episode" => ParseEpisode(currentlyPlayingTrack.Item as EpisodeDto),
            _ => "Could not determine currently playing track. Please try again."
        };
        return response;
    }

    private static string ParseTrack(TrackDto track)
    {
        if (track is null) 
            return "Could not determine currently playing track. Please try again.";

        string songName = track.Name;
        string[] artistNames = track.Artists.Select(artist => artist.Name).ToArray();
        string artists = artistNames.Length == 1
            ? artistNames[0]
            : string.Join(", ", artistNames[..^1]) + " and " + artistNames[^1];
        string albumName = track.Album.Name;
        string albumType = track.Album.AlbumType;
        string linkToTrack = track.ExternalUrls.Spotify;
        string response = $"\"{songName}\" by {artists} from the {albumType} \"{albumName}\". Listen here: {linkToTrack}";
        return response;
    }

    private static string ParseEpisode(EpisodeDto episode)
    {
        if (episode is null) 
            return "Could not determine currently playing track. Please try again.";

        string episodeName = episode.Name;
        string showName = episode.Show.Name;
        string linkToEpisode = episode.Uri;
        string response = $"Episode {episodeName} from the show {showName}. Listen here: {linkToEpisode}";
        return response;
    }
}
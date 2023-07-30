using Torty.Web.Apps.CurrentSpotifySong.Adapters.Exceptions.Spotify;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.Exceptions;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ResponseDtos;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;

/// <summary>
/// Adapter for handling all business logic related to working with Spotify data
/// </summary>
public interface ISpotifyAdapter
{
    /// <summary>
    /// Fetch a URI that can be used to give this app access to a user's Spotify information
    /// </summary>
    /// <param name="unauthorizedUserId"></param>
    /// <returns></returns>
    Task<string> GetAuthorizeUri(string unauthorizedUserId);
    /// <summary>
    /// Generate an access token for a user who has granted us permission to read the Spotify information
    /// </summary>
    /// <param name="authorizationCode"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    Task<UserBE> GenerateAccessTokenForUser(string authorizationCode, string state);
    /// <summary>
    /// Fetch information about what a user is currently listening to on Spotify
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> GetCurrentlyPlayingTrack(string userId);
}

/// <inheritdoc cref="ISpotifyAdapter"/>
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
        // We need to make sure this temp unauthenticated user hasn't expired
        if (DateTime.Now > user.ExpireDateTime)
        {
            /*
             * If the user has expired we'll fire off a request to prune our expired users
             *
             * Notice we don't delete the single expired user but go to prune all expired
             *
             * This has a added benefit of pruning out people who may have registered but
             * will never come back to authorize so we may have never identified they expired
             */
            await _userAdapter.DeleteExpiredUnauthenticatedUsers();
            // Throw an ex to our upstream handler notifying them we can't proceed b/c the temp user was expired
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
            /*
             * According to the Spotify docs a new RefreshToken can, "sometimes"
             * come back when making a request to refresh an access token
             *
             * So we'll check if our response contains a RefreshToken
             * it will be null if they did not give us a new one
             *
             * If it is null we'll continue to keep the one we already have
             * If it is not null we'll add it to our data payload so it gets updated on our user
             */
            string refreshToken = string.IsNullOrWhiteSpace(newAccessToken.RefreshToken)
                ? user.RefreshToken
                : newAccessToken.RefreshToken;
            UserBE newUserDetails = user with
            {
                AccessToken = newAccessToken.AccessToken,
                RefreshToken = refreshToken
            };
            UserBE updateUser = await _userAdapter.UpdateUser(newUserDetails);
            // Now we'll try to make our request one more time with our new AccessToken
            string currentTrackInfo = await _GetCurrentlyPlayingTrack(updateUser.AccessToken);
            return currentTrackInfo; 
            // We won't catch any exceptions and will let them bubble at this point
            // If something goes wrong again it's not going to be for access token reasons
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
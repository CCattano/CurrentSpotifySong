using System.Diagnostics;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.Exceptions;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ResponseDtos;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters;

public interface ISpotifyAdapter
{
    string GetAuthorizeUri();
    Task GetAccessToken(string authorizationCode);
    Task<string> RefreshAccessToken(string userId);
    Task<string> GetCurrentlyPlayingTrack(string userId);
}

public class SpotifyAdapter : ISpotifyAdapter
{
    private readonly ISpotifyClient _client;

    public SpotifyAdapter(ISpotifyClient client) => _client = client;

    public string GetAuthorizeUri()
    {
        string uri = _client.GetAuthorizeUri();
        return uri;
    }

    public async Task GetAccessToken(string authorizationCode)
    {
        AccessTokenDto accessToken = await _client.GetAccessToken(authorizationCode);
        Debugger.Break();
    }

    public async Task<string> GetCurrentlyPlayingTrack(string userId)
    {
        // TODO fetch token details by userId
        string accessToken = userId;
        try
        {
            string currentTrackInfo = await _GetCurrentlyPlayingTrack(accessToken);
            return currentTrackInfo;
        }
        catch (BadOrExpiredTokenException ex)
        {
            // If our token has expired we'll attempt to refresh the token
            string newAccessToken = await RefreshAccessToken("TODO: Get RefreshToken by userId");
            // Then we'll try to make our request one more time
            string currentTrackInfo = await _GetCurrentlyPlayingTrack(newAccessToken);
            return currentTrackInfo; 
            // We won't catch any exceptions and will let them bubble at this point
        }
    }
    
    // TODO: Make this private after testing
    public async Task<string> RefreshAccessToken(string userId)
    {
        // TODO fetch token details by userId
        string refreshToken = userId;
        // TODO: store this somewhere
        AccessTokenDto accessToken = await _client.RefreshAccessToken(refreshToken);
        return accessToken.AccessToken;
    }

    private async Task<string> _GetCurrentlyPlayingTrack(string accessToken)
    {
        CurrentlyPlayingResponseDto currentlyPlayingTrack = await _client.GetCurrentlyPlayingTrack(accessToken);
        string response = currentlyPlayingTrack.CurrentlyPlayingType.ToLower() switch
        {
            "track" => ParseSongTrack(currentlyPlayingTrack.Item as TrackDto),
            "episode" => ParseEpisodeTrack(currentlyPlayingTrack.Item as EpisodeDto),
            _ => "Could not determine currently playing track. Please try again."
        };
        return response;
    }

    private static string ParseSongTrack(TrackDto? track)
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
        string response = $"{songName} by {artists} from the {albumType} \"{albumName}\". Listen here: {linkToTrack}";
        return response;
    }

    private static string ParseEpisodeTrack(EpisodeDto? episode)
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
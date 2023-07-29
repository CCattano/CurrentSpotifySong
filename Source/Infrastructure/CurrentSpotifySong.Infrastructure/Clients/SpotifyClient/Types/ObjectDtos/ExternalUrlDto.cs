using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class ExternalUrlDto
{
    /// <summary>
    /// The Spotify URL for the object.
    /// </summary>
    [JsonPropertyName("spotify")]
    public string Spotify { get; init; }
}
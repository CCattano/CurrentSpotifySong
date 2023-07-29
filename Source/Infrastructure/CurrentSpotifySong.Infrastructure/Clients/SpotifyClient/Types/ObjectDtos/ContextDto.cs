using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class ContextDto
{
    /// <summary>
    /// The object type
    /// </summary>
    /// <example>
    /// "artist", "playlist", "album", "show".
    /// </example>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the track.
    /// </summary>
    [JsonPropertyName("href")]
    public string Href { get; init; }

    /// <summary>
    /// External URLs for this context.
    /// </summary>
    [JsonPropertyName("external_urls")]
    public ExternalUrlDto ExternalUrls { get; init; }

    /// <summary>
    /// The Spotify URI for the context.
    /// </summary>
    [JsonPropertyName("uri")]
    public string Uri { get; init; }
}
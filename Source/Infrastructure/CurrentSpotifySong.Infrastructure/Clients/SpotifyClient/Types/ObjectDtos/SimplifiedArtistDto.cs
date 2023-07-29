using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class SimplifiedArtistDto
{
    /// <summary>
    /// Known external URLs for this artist.
    /// </summary>
    [JsonPropertyName("external_urls")]
    public ExternalUrlDto ExternalUrls { get; init; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the artist.
    /// </summary>
    [JsonPropertyName("href")]
    public string Href { get; init; }

    /// <summary>
    /// The Spotify ID for the artist.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// The name of the artist.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// The object type.
    /// </summary>
    /// <remarks>
    /// Allowed values: "artist"
    /// </remarks>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// The Spotify URI for the artist.
    /// </summary>
    [JsonPropertyName("uri")]
    public string Uri { get; init; }
}
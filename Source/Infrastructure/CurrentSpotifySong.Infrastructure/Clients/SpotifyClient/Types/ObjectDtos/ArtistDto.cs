using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class ArtistDto
{
    /// <summary>
    /// Known external URLs for this artist.
    /// </summary>
    [JsonPropertyName("external_urls")]
    public ExternalUrlDto ExternalUrls { get; init; }

    /// <summary>
    /// Information about the followers of the artist.
    /// </summary>
    [JsonPropertyName("followers")]
    public FollowerDto Followers { get; init; }
    
    /// <summary>
    /// A list of the genres the artist is associated with.
    /// </summary>
    /// <remarks>
    /// If not yet classified, the array is empty.
    /// </remarks>
    [JsonPropertyName("genres")]
    public List<string> Genres { get; init; }
    
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
    /// Images of the artist in various sizes, widest first.
    /// </summary>
    [JsonPropertyName("images")]
    public ImageDto Images { get; init; }

    /// <summary>
    /// The name of the artist.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    /// <summary>
    /// The popularity of the artist.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The value will be between 0 and 100, with 100 being the most popular.
    ///     </para>
    ///     <para>
    ///         The artist's popularity is calculated from the popularity of all the artist's tracks.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("popularity")]
    public int Popularity { get; init; }

    /// <summary>
    /// The object type. Allowed values: "artist"
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// The Spotify URI for the artist.
    /// </summary>
    [JsonPropertyName("uri")]
    public string Uri { get; init; }
}
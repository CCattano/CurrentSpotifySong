using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class AlbumDto
{
    /// <summary>
    /// The type of the album.
    /// </summary>
    /// <remarks>
    /// Allowed values: "album", "single", "compilation"
    /// </remarks>
    /// <example>
    /// "compilation"
    /// </example>
    [JsonPropertyName("album_type")]
    public string AlbumType { get; init; }

    /// <summary>
    /// The number of tracks in the album.
    /// </summary>
    /// <example>
    /// 9
    /// </example>
    [JsonPropertyName("total_tracks")]
    public int TotalTracks { get; init; }

    /// <summary>
    /// The markets in which the album is available: ISO 3166-1 alpha-2 country codes.
    /// </summary>
    /// <remarks>
    /// An album is considered available in a market when at
    /// least 1 of its tracks is available in that market.
    /// </remarks>
    [JsonPropertyName("available_markets")]
    public List<string> AvailableMarkets { get; init; }

    /// <summary>
    /// Known external URLs for this album.
    /// </summary>
    [JsonPropertyName("external_urls")]
    public ExternalUrlDto ExternalUrls { get; init; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the album.
    /// </summary>
    [JsonPropertyName("href")]
    public string Href { get; init; }

    /// <summary>
    /// The Spotify ID for the album.
    /// </summary>
    /// <example>
    /// "2up3OPMp9Tb4dAKM2erWXQ"
    /// </example>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// The cover art for the album in various sizes, widest first. 
    /// </summary>
    [JsonPropertyName("images")]
    public List<ImageDto> Images { get; init; }

    /// <summary>
    /// The name of the album.
    /// </summary>
    /// <remarks>
    /// In case of an album takedown, the value may be an empty string.
    /// </remarks>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// The date the album was first released.
    /// </summary>
    /// <example>
    /// "1981-12"
    /// </example>
    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; init; }

    /// <summary>
    /// The precision with which release_date value is known. 
    /// </summary>
    /// <remarks>
    /// Allowed values: "year", "month", "day"
    /// </remarks>
    /// <example>
    /// "year"
    /// </example>
    [JsonPropertyName("release_date_precision")]
    public string ReleaseDatePrecision { get; init; }

    /// <summary>
    /// Included in the response when a content restriction is applied.
    /// </summary>
    [JsonPropertyName("restrictions")]
    public RestrictionDto Restrictions { get; init; }

    /// <summary>
    /// The object type. 
    /// </summary>
    /// <remarks>
    /// Allowed values: "album"
    /// </remarks>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// The Spotify URI for the album.
    /// </summary>
    /// <example>
    /// "spotify:album:2up3OPMp9Tb4dAKM2erWXQ"
    /// </example>
    [JsonPropertyName("uri")]
    public string Uri { get; init; }

    /// <summary>
    /// The copyright statements of the album.
    /// </summary>
    [JsonPropertyName("copyrights")]
    public List<CopyrightDto> Copyrights { get; init; }

    /// <summary>
    /// Known external IDs for the album.
    /// </summary>
    [JsonPropertyName("external_ids")]
    public ExternalIdDto ExternalIds { get; init; }

    /// <summary>
    /// A list of the genres the album is associated with.
    /// </summary>
    /// <remarks>
    /// If not yet classified, the array is empty.
    /// </remarks>
    [JsonPropertyName("genres")]
    public List<string> Genres { get; init; }

    /// <summary>
    /// The label associated with the album.
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; init; }

    /// <summary>
    /// The popularity of the album.
    /// </summary>
    /// <remarks>
    /// The value will be between 0 and 100, with 100 being the most popular.
    /// </remarks>
    [JsonPropertyName("popularity")]
    public int Popularity { get; init; }

    /// <summary>
    /// The field is present when getting an artist's albums.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Compared to album_type this field represents relationship between the artist and the album. 
    ///     </para>
    ///     <para>
    ///         Allowed values: "album", "single", "compilation", "appears_on"
    ///     </para>
    /// </remarks>
    /// <example>
    /// "compilation"
    /// </example>
    [JsonPropertyName("album_group")]
    public string AlbumGroup { get; init; }

    /// <summary>
    /// The artists of the album.
    /// </summary>
    /// <remarks>
    /// Each artist object includes a link in href to more detailed information about the artist.
    /// </remarks>
    [JsonPropertyName("artists")]
    public List<SimplifiedArtistDto> Artists { get; init; }
}
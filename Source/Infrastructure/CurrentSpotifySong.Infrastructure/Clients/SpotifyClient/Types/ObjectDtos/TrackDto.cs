using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class TrackDto
{
    /// <summary>
    /// The album on which the track appears.
    /// </summary>
    /// <remarks>
    /// The album object includes a link in href to full information about the album.
    /// </remarks>
    [JsonPropertyName("album")]
    public AlbumDto Album { get; init; }

    /// <summary>
    /// The artists who performed the track.
    /// </summary>
    /// <remarks>
    /// Each artist object includes a link in href to more detailed information about the artist.
    /// </remarks>
    [JsonPropertyName("artists")]
    public List<ArtistDto> Artists { get; init; }
    
    /// <summary>
    /// A list of the countries in which the track can be played
    /// </summary>
    /// <remarks>
    /// Identified by their ISO 3166-1 alpha-2 code.
    /// </remarks>
    [JsonPropertyName("available_markets")]
    public List<string> AvailableMarkets { get; init; }
    
    /// <summary>
    /// The disc number
    /// </summary>
    /// <remarks>
    /// Usually 1 unless the album consists of more than one disc
    /// </remarks>
    [JsonPropertyName("disc_number")]
    public int DiscNumber { get; init; }
    
    /// <summary>
    /// The track length in milliseconds.
    /// </summary>
    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; init; }
    
    /// <summary>
    /// Whether or not the track has explicit lyrics
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         true = yes it does
    ///     </para>
    ///     <para>
    ///         false = no it does not OR unknown
    ///     </para>
    /// </remarks>
    [JsonPropertyName("explicit")]
    public bool Explicit { get; init; }

    /// <summary>
    /// Known external IDs for the track.
    /// </summary>
    [JsonPropertyName("external_ids")]
    public ExternalIdDto ExternalIds { get; init; }

    /// <summary>
    /// Known external URLs for this track.
    /// </summary>
    [JsonPropertyName("external_urls")]
    public ExternalUrlDto ExternalUrls { get; init; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the track.
    /// </summary>
    [JsonPropertyName("href")]
    public string Href { get; init; }

    /// <summary>
    /// The Spotify ID for the track.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// Part of the response when Track Relinking is applied.
    /// </summary>
    /// <remarks>
    /// If true, the track is playable in the given market, otherwise false.
    /// </remarks>
    [JsonPropertyName("is_playable")]
    public bool IsPlayable { get; init; }
    
    /// <summary>
    /// Part of the response when Track Relinking is applied, and the requested track has been replaced with different track. 
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Object content was not defined in the API documentation hence the dynamic typing
    ///     </para>
    ///     <para>
    ///         The track in the linked_from object contains information about the originally requested track.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("linked_from")]
    public dynamic LinkedFrom { get; init; }

    /// <summary>
    /// Included in the response when a content restriction is applied.
    /// </summary>
    [JsonPropertyName("restrictions")]
    public RestrictionDto Restrictions { get; init; }

    /// <summary>
    /// The name of the track.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// The popularity of the track.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The popularity of a track is a value between 0 and 100, with 100 being the most popular.
    ///     </para>
    ///     <para>
    ///         The popularity is calculated by algorithm and is based, in the most part,
    ///         on the total number of plays the track has had and how recent those plays are.
    ///     </para>
    ///     <para>
    ///         Generally speaking, songs that are being played a lot now will have
    ///         a higher popularity than songs that were played a lot in the past.
    ///     </para>
    ///     <para>
    ///         Duplicate tracks(e.g.the same track from a single and an album) are rated independently.
    ///     </para>
    ///     <para>
    ///         Artist and album popularity is derived mathematically from track popularity.
    ///     </para>
    ///     <para>
    ///         The popularity value may lag actual popularity by a few days: the value is not updated in real time.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("popularity")]
    public int Popularity { get; init; }

    /// <summary>
    /// A link to a 30 second preview in MP3 format of the track.
    /// </summary>
    /// <remarks>
    /// Nullable field
    /// </remarks>
    [JsonPropertyName("preview_url")]
    public string? PreviewUrl { get; init; }

    /// <summary>
    /// The number of the track.
    /// </summary>
    /// <remarks>
    /// If an album has several discs, the track number is the number on the specified disc.
    /// </remarks>
    [JsonPropertyName("track_number")]
    public int TrackNumber { get; init; }

    /// <summary>
    /// The object type: "track".
    /// </summary>
    /// <remarks>
    /// Allowed values: "track"
    /// </remarks>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// The Spotify URI for the track.
    /// </summary>
    [JsonPropertyName("uri")]
    public string Uri { get; init; }

    /// <summary>
    /// Whether or not the track is from a local file.
    /// </summary>
    [JsonPropertyName("is_local")]
    public bool IsLocal { get; init; }
}
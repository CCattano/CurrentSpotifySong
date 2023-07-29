using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class EpisodeDto

{
    /// <summary>
    /// A URL to a 30 second preview (MP3 format) of the episode.
    /// </summary>
    /// <remarks>
    /// Nullable field
    /// </remarks>
    /// <example>
    /// "https://p.scdn.co/mp3-preview/2f37da1d4221f40b9d1a98cd191f4d6f1646ad17"
    /// </example>
    [JsonPropertyName("audio_preview_url")]
    public string? AudioPreviewUrl { get; init; }

    /// <summary>
    /// A description of the episode.
    /// </summary>
    /// <remarks>
    /// HTML tags are stripped away from this field, use html_description field in case HTML tags are needed.
    /// </remarks>
    /// <example>
    /// "A Spotify podcast sharing fresh insights on important topics of the moment—in a way only Spotify can.
    /// You’ll hear from experts in the music, podcast and tech industries as we discover and uncover stories
    /// about our work and the world around us."
    /// </example>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// A description of the episode.
    /// </summary>
    /// <remarks>
    /// This field may contain HTML tags.
    /// </remarks>
    /// <example>
    /// "<p>A Spotify podcast sharing fresh insights on important topics of
    /// the moment—in a way only Spotify can. You’ll hear from experts in the
    /// music, podcast and tech industries as we discover and uncover stories
    /// about our work and the world around us.</p>"
    /// </example>
    [JsonPropertyName("html_description")]
    public string HtmlDescription { get; init; }

    /// <summary>
    /// The episode length in milliseconds.
    /// </summary>
    /// <example>
    /// 1686230
    /// </example>
    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; init; }

    /// <summary>
    /// Whether or not the episode has explicit content
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
    /// External URLs for this episode.
    /// </summary>
    [JsonPropertyName("external_urls")]
    public ExternalUrlDto ExternalUrls { get; init; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the episode.
    /// </summary>
    /// <example>
    /// "https://api.spotify.com/v1/episodes/5Xt5DXGzch68nYYamXrNxZ"
    /// </example>
    [JsonPropertyName("href")]
    public string Href { get; init; }

    /// <summary>
    /// The Spotify ID for the episode.
    /// </summary>
    /// <example>
    /// "5Xt5DXGzch68nYYamXrNxZ"
    /// </example>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// The cover art for the episode in various sizes, widest first.
    /// </summary>
    [JsonPropertyName("images")]
    public List<ImageDto> Images { get; init; }

    /// <summary>
    /// Whether the episode is hosted outside of Spotify's CDN.
    /// </summary>
    [JsonPropertyName("is_externally_hosted")]
    public bool IsExternallyHosted { get; init; }

    /// <summary>
    /// Whether the episode is playable in the given market.
    /// </summary>
    [JsonPropertyName("is_playable")]
    public bool IsPlayable { get; init; }

    /// <summary>
    /// The language used in the episode
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Deprecated field
    ///     </para>
    ///     <para>
    ///         Identified by a ISO 639 code
    ///     </para>
    ///     <para>
    ///         This field is deprecated and might be removed in the future. Please use the languages field instead.
    ///     </para>
    /// </remarks>
    /// <example>
    /// "en"
    /// </example>
    [JsonPropertyName("language")]
    public string Language { get; init; }

    /// <summary>
    /// A list of the languages used in the episode
    /// </summary>
    /// <remarks>
    /// Identified by their ISO 639-1 code.
    /// </remarks>
    [JsonPropertyName("languages")]
    public List<string> Languages { get; init; }

    /// <summary>
    /// The name of the episode.
    /// </summary>
    /// <example>
    /// "Starting Your Own Podcast: Tips, Tricks, and Advice From Anchor Creators"
    /// </example>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// The date the episode was first released.
    /// </summary>
    /// <remarks>
    /// Depending on the precision, it might be shown as "1981" or "1981-12"
    /// </remarks>
    /// <example>
    /// "1981-12-15"
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
    /// "day"
    /// </example>
    [JsonPropertyName("release_date_precision")]
    public string ReleaseDatePrecision { get; init; }

    /// <summary>
    /// The user's most recent position in the episode.
    /// </summary>
    /// <remarks>
    /// Set if the supplied access token is a user token and has the scope 'user-read-playback-position'.
    /// </remarks>
    [JsonPropertyName("resume_point")]
    public ResumePointDto ResumePoint { get; init; }

    /// <summary>
    /// The object type.
    /// </summary>
    /// <remarks>
    /// Allowed values: "episode"
    /// </remarks>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// The Spotify URI for the episode.
    /// </summary>
    /// <example>
    /// "spotify:episode:0zLhl3WsOCQHbe1BPTiHgr"
    /// </example>
    [JsonPropertyName("uri")]
    public string Uri { get; init; }

    /// <summary>
    /// Included in the response when a content restriction is applied.
    /// </summary>
    [JsonPropertyName("restrictions")]
    public RestrictionDto Restrictions { get; init; }

    /// <summary>
    /// The show on which the episode belongs.
    /// </summary>
    [JsonPropertyName("show")]
    public ShowDto Show { get; init; }
}
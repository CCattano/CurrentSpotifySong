using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class ShowDto
{
    /// <summary>
    /// A list of the countries in which the show can be played
    /// </summary>
    /// <remarks>
    /// Identified by their ISO 3166-1 alpha-2 code.
    /// </remarks>
    [JsonPropertyName("available_markets")]
    public List<string> AvailableMarkets { get; init; }

    /// <summary>
    /// The copyright statements of the show.
    /// </summary>
    [JsonPropertyName("copyrights")]
    public List<CopyrightDto> Copyrights { get; init; }

    /// <summary>
    /// A description of the show.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         HTML tags are stripped away from this field
    ///     </para>
    ///     <para>
    ///         Use html_description field in case HTML tags are needed
    ///     .</para>
    /// </remarks>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// A description of the show.
    /// </summary>
    /// <remarks>
    /// This field may contain HTML tags.
    /// </remarks>
    [JsonPropertyName("html_description")]
    public string HtmlDescription { get; init; }

    /// <summary>
    /// Whether or not the show has explicit content
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
    /// External URLs for this show.
    /// </summary>
    [JsonPropertyName("external_urls")]
    public ExternalUrlDto ExternalUrls { get; init; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the show.
    /// </summary>
    [JsonPropertyName("href")]
    public string Href { get; init; }

    /// <summary>
    /// The Spotify ID for the show.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// The cover art for the show in various sizes, widest first.
    /// </summary>
    [JsonPropertyName("images")]
    public List<ImageDto> Images { get; init; }

    /// <summary>
    /// True if all of the shows episodes are hosted outside of Spotify's CDN.
    /// </summary>
    /// <remarks>
    /// Nullable field
    /// </remarks>
    [JsonPropertyName("is_externally_hosted")]
    public bool? IsExternallyHosted { get; init; }
    
    /// <summary>
    /// A list of the languages used in the show
    /// </summary>
    /// <remarks>
    /// Identified by their ISO 639 code.
    /// </remarks>
    [JsonPropertyName("languages")]
    public List<string> Languages { get; init; }
    
    /// <summary>
    /// The media type of the show.
    /// </summary>
    [JsonPropertyName("media_type")]
    public string MediaType { get; init; }

    /// <summary>
    /// The name of the episode.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// The publisher of the show.
    /// </summary>
    [JsonPropertyName("publisher")]
    public string Publisher { get; init; }

    /// <summary>
    /// The object type.
    /// </summary>
    /// <remarks>
    /// Allowed values: "show"
    /// </remarks>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// The Spotify URI for the show.
    /// </summary>    
    [JsonPropertyName("uri")]
    public string Uri { get; init; }

    /// <summary>
    /// The total number of episodes in the show.
    /// </summary>
    [JsonPropertyName("total_episodes")]
    public int TotalEpisodes { get; init; }
}
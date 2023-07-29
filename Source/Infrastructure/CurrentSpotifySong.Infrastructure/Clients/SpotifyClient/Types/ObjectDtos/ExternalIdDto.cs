using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class ExternalIdDto
{
    /// <summary>
    /// International Standard Recording Code
    /// </summary>
    [JsonPropertyName("isrc")]
    public string ISRC { get; init; }
    
    /// <summary>
    /// International Article Number
    /// </summary>
    [JsonPropertyName("ean")]
    public string EAN { get; init; }

    /// <summary>
    /// Universal Product Code
    /// </summary>
    [JsonPropertyName("upc")]
    public string UPC { get; init; }
}
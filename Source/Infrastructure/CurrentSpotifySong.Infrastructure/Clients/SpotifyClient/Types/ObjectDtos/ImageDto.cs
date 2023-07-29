using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class ImageDto
{
    /// <summary>
    /// The source URL of the image.
    /// </summary>
    /// <example>
    /// "https://i.scdn.co/image/ab67616d00001e02ff9ca10b55ce82ae553c8228"
    /// </example>
    [JsonPropertyName("url")]
    public string Url { get; init; }

    /// <summary>
    /// The image height in pixels.
    /// </summary>
    /// <remarks>
    /// Nullable field
    /// </remarks>
    /// <example>
    /// 300
    ///</example>
    [JsonPropertyName("height")]
    public int? Height { get; init; }

    /// <summary>
    /// The image width in pixels.
    /// </summary>
    /// <remarks>
    /// Nullable field
    /// </remarks>
    /// <example>
    /// 300
    /// </example>
    [JsonPropertyName("width")]
    public int? Width { get; init; }
}
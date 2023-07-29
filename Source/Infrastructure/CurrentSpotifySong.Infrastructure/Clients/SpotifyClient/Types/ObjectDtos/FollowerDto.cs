using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class FollowerDto
{
    /// <summary>
    ///     Not defined in the docs
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Nullable field
    ///     </para>
    ///     <para>
    ///         This will always be set to null, as the Web API does not support it at the moment.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("href")]
    public string Href { get; init; }

    /// <summary>
    /// The total number of followers.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }
}
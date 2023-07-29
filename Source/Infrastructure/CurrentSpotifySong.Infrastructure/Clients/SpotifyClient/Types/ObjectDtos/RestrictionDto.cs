using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class RestrictionDto
{
    /// <summary>
    /// The reason for the restriction.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Albums may be restricted if the content is not available in a given market,
    ///         to the user's subscription type,
    ///         or when the user's account is set to not play explicit content.
    ///     </para>
    ///     <para>
    ///         Additional reasons may be added in the future.
    ///     </para>
    ///     <para>
    ///         Allowed values: "market", "product", "explicit"
    ///     </para>
    /// </remarks>
    [JsonPropertyName("reason")]
    public string Reason { get; init; }
}
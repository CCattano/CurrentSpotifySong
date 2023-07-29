using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.RequestDtos;

public class AccessTokenRequestDto
{
    /// <summary>
    /// This field must contain the value "authorization_code".
    /// </summary>
    /// <remarks>
    /// Required field.
    /// </remarks>
    [JsonPropertyName("grant_type")]
    public readonly string GrantType = "authorization_code";

    /// <summary>
    /// The authorization code returned from the previous request.
    /// </summary>
    /// <remarks>
    /// Required field.
    /// </remarks>
    [JsonPropertyName("code")]
    public string Code { get; init; }

    /// <summary>
    /// The value of the redirect_uri supplied when requesting the authorization code
    /// </summary>
    /// <remarks>
    /// <para>
    ///     Required field.
    /// </para>
    /// <para>
    ///     This parameter is used for validation only (there is no actual redirection).
    /// </para>
    /// <para>
    ///     The value of this parameter must exactly match the value of
    ///     redirect_uri supplied when requesting the authorization code.
    /// </para>
    /// </remarks>
    [JsonPropertyName("redirect_uri")]
    public string RedirectUri { get; init; }
}
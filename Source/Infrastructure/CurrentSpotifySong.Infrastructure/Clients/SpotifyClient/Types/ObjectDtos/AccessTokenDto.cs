using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class AccessTokenDto
{
    /// <remarks>
    /// An Access Token that can be provided in subsequent calls, for example to Spotify Web API services.
    /// </remarks>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }

    /// <remarks>
    /// How the Access Token may be used: always "Bearer".
    /// </remarks>
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; }

    /// <remarks>
    /// A space-separated list of scopes which have been granted for this access_token
    /// </remarks>
    [JsonPropertyName("scope")]
    public string Scope { get; init; }

    /// <remarks>
    /// The time period (in seconds) for which the Access Token is valid.
    /// </remarks>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    /// <remarks>
    /// A token that can be sent to the Spotify Accounts service in place of an authorization code.
    /// </remarks>
    /// <remarks>
    /// <para>
    ///     When the access code expires, send a POST request to the Accounts service
    ///     /api/token endpoint, but use this code in place of an authorization code.
    /// </para>
    /// <para>
    ///     A new Access Token will be returned. A new refresh token might be returned too.
    /// </para>
    /// </remarks>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; }
}
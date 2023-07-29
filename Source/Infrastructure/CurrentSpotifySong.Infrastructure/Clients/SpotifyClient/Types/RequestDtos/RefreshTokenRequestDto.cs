using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.RequestDtos;

public class RefreshTokenRequestDto
{
    /// <summary>
    /// Required Set it to refresh_token.
    /// </summary>
    [JsonPropertyName("grant_type")]
    public readonly string GrantType = "refresh_token";

    /// <summary>
    /// Required The refresh token returned from the authorization code exchange.
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; }

    public RefreshTokenRequestDto(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
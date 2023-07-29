using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.RequestDtos;

public class AuthorizeRequestDto
{
    /// <summary> 
    /// The Client ID generated after registering your application.
    /// </summary>
    /// <remarks>
    /// Required field.
    ///</remarks>
    [JsonPropertyName("client_id")]
    public string ClientId { get; init; }

    /// <summary> 
    /// Set to code.
    /// </summary>
    /// <remarks>
    /// Required field.
    ///</remarks>
    [JsonPropertyName("response_type")]
    public readonly string ResponseType = "code";

    /// <summary> 
    /// The URI to redirect to after the user grants or denies permission.
    /// This URI needs to have been entered in the Redirect URI allowlist
    /// that you specified when you registered your application (See the app guide).
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Required field.
    ///     </para>
    ///     <para>
    ///         The value of redirect_uri here must exactly match one of
    ///         the values you entered when you registered your application,
    ///         including upper or lowercase, terminating slashes, and such.
    ///     </para>
    ///</remarks>
    [JsonPropertyName("redirect_uri")]
    public string RedirectUri { get; init; }

    /// <summary> 
    /// This provides protection against attacks such as cross-site request forgery. See RFC-6749.
    /// </summary>
    /// <remarks>
    /// Optional field. Strongly recommended you provide this field.
    /// </remarks>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary> 
    /// A space-separated list of scopes.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Optional field.
    ///     </para>
    ///     <para>
    ///         If no scopes are specified, authorization will be granted only to access publicly available information:
    ///         that is, only information normally visible in the Spotify desktop, web, and mobile players.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("scope")]
    public readonly string Scope = "user-read-currently-playing";

    /// <summary> 
    /// Whether to force the user to approve the app again if they’ve already done so.
    /// </summary>
    /// <remarks>
    /// Optional field.
    ///     <para>
    ///         If false (default), a user who has already approved the application
    ///         may be automatically redirected to the URI specified by redirect_uri.
    ///     </para>
    ///     <para>
    ///         If true, the user will not be automatically redirected and will have to approve the app again.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("show_dialog")]
    public bool? ShowDialog { get; init; }
}
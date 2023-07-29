using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class DeviceDto
{
    /// <summary>
    /// The device ID.
    /// </summary>
    /// <remarks>
    /// Nullable field
    /// </remarks>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    ///     If this device is the currently active device.
    /// </summary>
    [JsonPropertyName("is_active")]
    public bool IsActive { get; init; }

    /// <summary>
    /// If this device is currently in a private session.
    /// </summary>
    [JsonPropertyName("is_private_session")]
    public bool IsPrivateSession { get; init; }

    /// <summary>
    /// Whether controlling this device is restricted.
    /// </summary>
    /// <remarks>
    /// At present if this is "true" then no Web API commands will be accepted by this device.
    /// </remarks>
    [JsonPropertyName("is_restricted")]
    public bool IsRestricted { get; init; }

    /// <summary>
    ///  A human-readable name for the device.
    /// </summary>
    /// <remarks>
    /// Some devices have a name that the user can configure(e.g. "Loudest speaker") and
    /// some devices have a generic name associated with the manufacturer or device model.
    /// </remarks>
    /// <example>
    /// "Kitchen speaker"
    /// </example>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Device type, such as "computer", "smartphone" or "speaker".
    /// </summary>
    /// <example>
    /// "computer"
    /// </example>
    [JsonPropertyName("type")]
    public string Type { get; init; }

    /// <summary>
    /// The current volume in percent
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Nullable field
    ///     </para>
    ///     <para>
    ///         Value will be an integer between 0 and 100
    ///     </para>
    /// </remarks>
    /// <example>
    /// 59
    /// </example>
    [JsonPropertyName("volume_percent")]
    public int VolumePercent { get; init; }
}
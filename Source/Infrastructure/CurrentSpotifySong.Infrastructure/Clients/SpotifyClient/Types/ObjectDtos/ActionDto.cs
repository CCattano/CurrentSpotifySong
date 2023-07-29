using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class ActionDto
{
    /// <summary>
    /// Interrupting playback.
    /// </summary>
    [JsonPropertyName("interrupting_playback")]
    public bool InterruptingPlayback { get; init; }

    /// <summary>
    /// Pausing.
    /// </summary>
    [JsonPropertyName("pausing")]
    public bool Pausing { get; init; }

    /// <summary>
    /// Resuming.
    /// </summary>
    [JsonPropertyName("resuming")]
    public bool Resuming { get; init; }

    /// <summary>
    /// Seeking playback location.
    /// </summary>
    [JsonPropertyName("seeking")]
    public bool Seeking { get; init; }

    /// <summary>
    /// Skipping to the next context.
    /// </summary>
    [JsonPropertyName("skipping_next")]
    public bool SkippingNext { get; init; }

    /// <summary>
    /// Skipping to the previous context.
    /// </summary>
    [JsonPropertyName("skipping_prev")]
    public bool SkippingPrev { get; init; }

    /// <summary>
    /// Toggling repeat context flag.
    /// </summary>
    [JsonPropertyName("toggling_repeat_context")]
    public bool TogglingRepeatContext { get; init; }

    /// <summary>
    /// Toggling shuffle flag.
    /// </summary>
    [JsonPropertyName("toggling_shuffle")]
    public bool TogglingShuffle { get; init; }

    /// <summary>
    /// Toggling repeat track flag.
    /// </summary>
    [JsonPropertyName("toggling_repeat_track")]
    public bool TogglingRepeatTrack { get; init; }

    /// <summary>
    /// Transferring playback between devices.
    /// </summary>
    [JsonPropertyName("transferring_playback")]
    public bool TransferringPlayback { get; init; }
}
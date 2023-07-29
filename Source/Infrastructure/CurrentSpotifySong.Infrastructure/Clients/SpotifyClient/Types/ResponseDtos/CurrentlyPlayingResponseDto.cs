using System.Text.Json.Serialization;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ResponseDtos;

public class CurrentlyPlayingResponseDto
{
    /// <summary>
    /// The device that is currently active.
    /// </summary>
    [JsonPropertyName("device")]
    public DeviceDto Device { get; init; }

    /// <summary>
    /// off, track, context
    ///</summary>
    [JsonPropertyName("repeat_state")]
    public string RepeatState { get; init; }

    /// <summary>
    /// If shuffle is on or off.
    ///</summary>
    [JsonPropertyName("shuffle_state")]
    public bool ShuffleState { get; init; }

    /// <summary>
    /// A Context Object.
    /// </summary>
    /// <remarks>
    /// Nullable Field
    /// </remarks>
    [JsonPropertyName("context")]
    public ContextDto? Context { get; init; }

    /// <summary>
    /// Unix Millisecond Timestamp when data was fetched.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; init; }

    /// <summary>
    /// Progress into the currently playing track or episode.
    /// </summary>
    /// <remarks>
    /// Nullable field
    /// </remarks>
    [JsonPropertyName("progress_ms")]
    public int? ProgressMilliseconds { get; init; }

    /// <summary>
    /// If something is currently playing, return true.
    /// </summary>
    [JsonPropertyName("is_playing")]
    public bool IsPlaying { get; init; }

    /// <summary>
    /// The currently playing track or episode.
    /// </summary>
    /// <remarks>
    /// Nullable field
    /// </remarks>
    [JsonIgnore]
    public object? Item;

    /// <summary>
    /// The object type of the currently playing item.
    /// </summary>
    /// <remarks>
    /// Can be one of track, episode, ad or unknown.
    /// </remarks>
    [JsonPropertyName("currently_playing_type")]
    public string CurrentlyPlayingType { get; init; }

    /// <summary>
    /// Allows to update the user interface based on which playback actions are available within the current context.
    /// </summary>
    [JsonPropertyName("actions")]
    public ActionDto Actions { get; init; }
}
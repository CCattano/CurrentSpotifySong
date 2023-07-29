using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class ResumePointDto
{
    /// <summary>
    /// Whether or not the episode has been fully played by the user.
    /// </summary>
    [JsonPropertyName("fully_played")]
    public bool FullyPlayed { get; init; }

    /// <summary>
    /// The user's most recent position in the episode in milliseconds.
    /// </summary>
    [JsonPropertyName("resume_position_ms")]
    public int ResumePositionMilliseconds { get; init; }
}
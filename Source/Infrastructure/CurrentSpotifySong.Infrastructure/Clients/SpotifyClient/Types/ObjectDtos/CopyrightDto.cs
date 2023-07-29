using System.Text.Json.Serialization;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;

public class CopyrightDto
{
    /// <summary>
    /// The copyright text for this content.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; }

    /// <summary>
    /// The type of copyright.
    /// </summary>
    /// <remarks>
    /// C = the copyright
    /// <br />
    /// P = the sound recording (performance) copyright.
    /// </remarks>
    [JsonPropertyName("type")]
    public string Type { get; init; }
}
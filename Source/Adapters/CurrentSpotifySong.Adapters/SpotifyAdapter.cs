using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters;

public interface ISpotifyAdapter
{
    Task Temp();
}

public class SpotifyAdapter : ISpotifyAdapter
{
    private readonly ISpotifyClient _client;

    public SpotifyAdapter(ISpotifyClient client) => _client = client;

    public async Task Temp()
    {
        await _client.Temp();
    }
}
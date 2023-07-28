using Microsoft.Extensions.DependencyInjection;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient
{
    public static class SpotifyClientExtensions
    {
        public static void AddSpotifyClient(this IServiceCollection services)
        {
            services.AddHttpClient<ISpotifyClient, SpotifyClient>();
        }
    }
}
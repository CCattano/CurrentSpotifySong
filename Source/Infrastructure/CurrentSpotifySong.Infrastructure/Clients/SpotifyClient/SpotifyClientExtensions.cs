using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient
{
    public static class SpotifyClientExtensions
    {
        public static void AddSpotifyClient(this IServiceCollection services, string clientId, string clientSecret)
        {
            services.AddHttpClient<ISpotifyClient, SpotifyClient>((httpClient, svcProvider) =>
            {
                IHttpContextAccessor httpCtx = svcProvider.GetRequiredService<IHttpContextAccessor>();
                return new SpotifyClient(httpClient, httpCtx, clientId, clientSecret);
            });
        }
    }
}
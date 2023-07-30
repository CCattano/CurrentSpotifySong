using Microsoft.Extensions.DependencyInjection;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient
{
    public static class SpotifyClientExtensions
    {
        public static void AddSpotifyClient(this IServiceCollection services, string clientId, string clientSecret)
        {
            services.AddHttpClient<ISpotifyClient, SpotifyClient>((httpClient, svcProvider) =>
            {
                ApiContextUtility apiCtxUtil = svcProvider.GetRequiredService<ApiContextUtility>();
                return new SpotifyClient(httpClient, apiCtxUtil, clientId, clientSecret);
            });
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient
{
    public static class SpotifyClientExtensions
    {
        /// <summary>
        /// Adds an instance of the SpotifyClient to the DI bag for injection in the runtime
        /// </summary>
        /// <param name="services"></param>
        /// <param name="clientId">The clientID the client should use when making requests to the Spotify API</param>
        /// <param name="clientSecret">The clientSecret the client should use when making requests to the Spotify API</param>
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
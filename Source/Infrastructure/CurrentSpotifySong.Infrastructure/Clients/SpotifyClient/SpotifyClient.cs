using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient
{
    public interface ISpotifyClient
    {
        Task Temp();
    }

    public class SpotifyClient : ISpotifyClient
    {
        private readonly HttpClient _httpClient;

        public SpotifyClient(HttpClient httpClient) => _httpClient = httpClient;

        public Task Temp()
        {
            Console.WriteLine("DI Works!");
            return Task.CompletedTask;
        }
    }
}
namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure;

public struct SystemConstants
{
    public struct AppSettings
    {
        public struct ConnStrings
        {
            public const string MongoDb = "MongoDb";
        }
        public struct APIs
        {
            public struct Spotify
            {
                public const string ClientId = "ClientId"; 
                public const string ClientSecret = "ClientSecret";
            }
        }
    }
}
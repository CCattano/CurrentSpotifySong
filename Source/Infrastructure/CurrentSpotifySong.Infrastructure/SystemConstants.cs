namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure;

public struct SystemConstants
{
    /// <summary>
    /// A struct structure that mirrors the layout of our appsettings.json files
    /// </summary>
    /// <remarks>
    /// This gives us a structured/centralized way to navigate our Configuration
    /// object without using hardcoded sporadically around the application 
    /// </remarks>
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
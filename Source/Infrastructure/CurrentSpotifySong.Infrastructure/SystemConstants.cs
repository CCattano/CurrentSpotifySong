namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure;

public struct SystemConstants
{
    public struct AppSettings
    {
        public struct APIs
        {
            public const string SectionName = "APIs";
            public struct Spotify
            {
                public const string SectionName = "Spotify";
                public const string ClientId = "ClientId"; 
                public const string ClientSecret = "ClientSecret";
            }
        }
    }
}
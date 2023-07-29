using System;
using Microsoft.Extensions.Configuration;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public enum ApiType
    {
        Spotify
    }
    public static string GetApiConfig(this IConfiguration configuration, ApiType api, string configName)
    {
        string apiName = Enum.GetName(typeof(ApiType), api);
        string configValue = configuration.GetSection("APIs").GetSection(apiName)[configName];
        return configValue;
    }
}
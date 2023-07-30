using System;
using Microsoft.Extensions.Configuration;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public enum ApiType
    {
        Spotify
    }
    /// <summary>
    /// Extension that access values in the APIs section of our Configuration object
    /// </summary>
    /// <remarks>
    /// This is meant to replicate the functionality observed in
    /// <see cref="Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString"/>
    /// </remarks>
    /// <param name="configuration"></param>
    /// <param name="api">Which API in the APIs section we are fetching a configuration property for</param>
    /// <param name="configName">The name of the configuration property whose value we should fetch</param>
    public static string GetApiConfig(this IConfiguration configuration, ApiType api, string configName)
    {
        string apiName = Enum.GetName(typeof(ApiType), api);
        string configValue = configuration.GetSection("APIs").GetSection(apiName)[configName];
        return configValue;
    }
}
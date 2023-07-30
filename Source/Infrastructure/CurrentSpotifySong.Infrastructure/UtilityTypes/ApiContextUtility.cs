namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

public class ApiContextUtility
{
    public readonly string BaseUri;
    public ApiContextUtility(string baseUri) => BaseUri = baseUri;
}
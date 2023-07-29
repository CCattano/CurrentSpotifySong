using Torty.Web.Apps.CurrentSpotifySong.Data;

namespace Torty.Web.Apps.CurrentSpotifySong.Facades;

public class BaseFacade
{
    protected readonly IDataService DataService;

    protected BaseFacade(IDataService dataService) => DataService = dataService;
}
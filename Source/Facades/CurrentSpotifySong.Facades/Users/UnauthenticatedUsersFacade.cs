using Torty.Web.Apps.CurrentSpotifySong.Data;

namespace Torty.Web.Apps.CurrentSpotifySong.Facades.Users;

public interface IUnauthenticatedUsersFacade
{
}

public class UnauthenticatedUsersFacade : BaseFacade, IUnauthenticatedUsersFacade
{
    public UnauthenticatedUsersFacade(IDataService dataSvc) : base(dataSvc)
    {
    }
}
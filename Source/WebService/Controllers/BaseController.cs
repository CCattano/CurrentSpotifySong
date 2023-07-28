using Microsoft.AspNetCore.Mvc;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers;

public class BaseController<TAdapter>: Controller
{
    protected TAdapter Adapter;

    public BaseController(TAdapter adapter) => Adapter = adapter;
}
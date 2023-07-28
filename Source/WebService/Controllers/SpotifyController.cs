using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Torty.Web.Apps.CurrentSpotifySong.Adapters;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers;

[Route("[controller]/[action]")]
public class SpotifyController : BaseController<ISpotifyAdapter>
{
    public SpotifyController(ISpotifyAdapter adapter) : base(adapter)
    {
    }

    [HttpGet]
    public async Task Temp()
    {
        await base.Adapter.Temp();
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers;

[Route("[controller]/[action]")]
public class UserController : BaseController<IUserAdapter>
{
    private readonly IHttpContextAccessor _httpCtx;
    public UserController(
        IHttpContextAccessor httpCtx,
        IUserAdapter adapter
    ) : base(adapter)
    {
        _httpCtx = httpCtx;
    }

    [HttpGet]
    public async Task<ActionResult> Register()
    {
        UnauthenticatedUserBE newUser = await base.Adapter.CreateUnauthenticatedUser();
        string redirectUrl = $"https://{_httpCtx.HttpContext!.Request.Host}/Spotify/Authorize?uuid={newUser.Id}";
        return RedirectPermanentPreserveMethod(redirectUrl);
    }
}
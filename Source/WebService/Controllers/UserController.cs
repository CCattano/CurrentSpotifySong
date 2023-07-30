using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers;

[Route("[controller]/[action]")]
public class UserController : BaseController<IUserAdapter>
{
    private readonly string _apiBaseUri;
    public UserController(
        ApiContextUtility apiCtxUtil,
        IUserAdapter adapter
    ) : base(adapter)
    {
        _apiBaseUri = apiCtxUtil.BaseUri;
    }

    /// <summary>
    /// Register a new temporary user with the system
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This temp user will have 24 hours to authorize the app access to their Spotify information
    ///     </para>
    ///     <para>
    ///         After 24 hours the user will have to restart the registration process
    ///     </para>
    /// </remarks>
    /// <returns></returns>
    [HttpGet]
    [ResponseCache(NoStore = true, Duration = 0)]
    public async Task<ActionResult> Register()
    {
        UnauthenticatedUserBE newUser = await base.Adapter.CreateUnauthenticatedUser();
        string redirectUrl = $"{_apiBaseUri}/Spotify/Authorize?uuid={newUser.Id}";
        return RedirectPermanentPreserveMethod(redirectUrl);
    }
}
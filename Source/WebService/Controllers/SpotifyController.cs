using System.Net;
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
    public ActionResult AuthorizeUri()
    {
        string uri = base.Adapter.GetAuthorizeUri();
        return RedirectPermanentPreserveMethod(uri);
    }

    [HttpGet]
    public async Task<ActionResult> AccessCode(
        [FromQuery] string code,
        [FromQuery] string? error,
        [FromQuery] string? state // TODO: Process and validate state
    )
    {
        if (!string.IsNullOrWhiteSpace(error) || string.IsNullOrWhiteSpace(code))
            return Problem("Could not authenticate. Please try again.", statusCode: (int)HttpStatusCode.InternalServerError);

        await base.Adapter.GetAccessToken(code);
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<string>> CurrentlyPlayingTrackDetails([FromQuery] string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return Problem("Must provide user identification", statusCode: (int)HttpStatusCode.BadRequest);
        string currentTrackInfo = await base.Adapter.GetCurrentlyPlayingTrack(userId);
        return Ok(currentTrackInfo);
    }

    [HttpPut]
    public async Task<ActionResult> RefreshAccessCode([FromQuery] string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return Problem("Must provide user identification", statusCode: (int)HttpStatusCode.BadRequest);
        
        await base.Adapter.RefreshAccessToken(userId);
        return Ok();
    }
}
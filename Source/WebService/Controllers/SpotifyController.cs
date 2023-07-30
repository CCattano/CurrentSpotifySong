using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Exceptions.Spotify;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Exceptions.User;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers;

[Route("[controller]/[action]")]
public class SpotifyController : BaseController<ISpotifyAdapter>
{
    private readonly string _requestHost;
    public SpotifyController(
        ISpotifyAdapter adapter,
        IHttpContextAccessor httpCtx
    ) : base(adapter)
    {
        _requestHost = httpCtx.HttpContext!.Request.Host.ToString();
    }

    [HttpGet]
    public async Task<ActionResult> Authorize([FromQuery] string uuid)
    {
        if (string.IsNullOrWhiteSpace(uuid))
        {
            string response = $"""
                Looks like you skipped a step.

                Go here to get started:

                https://{_requestHost}/User/Register
            """;
            return Ok(response);
        }

        try
        {
            string uri = await base.Adapter.GetAuthorizeUri(uuid);
            return RedirectPermanentPreserveMethod(uri);
        }
        catch (UnauthenticatedUserNotFoundException _)
        {
            string response = $"""
                The unique code you provided was not recognized.

                Go here to get started:

                https://{_requestHost}/User/Register
            """;
            return Ok(response);
        }
        catch (InvalidRegistrationTargetException _)
        {
            string response = $"""
                Your registration link has expired.

                Go here to get started again:

                https://{_requestHost}/User/Register
            """;
            return Ok(response);
        }
    }

    [HttpGet]
    public async Task<ActionResult<string>> AccessCode(
        [FromQuery] string code,
        [FromQuery] string error,
        [FromQuery] string state
    )
    {
        if (string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(error))
            return Ok("Could not authenticate. Please try again.");

        try
        {
            UserBE user = await base.Adapter.GenerateAccessTokenForUser(code, state);
        
            string bespokeUrl = $"https://{_requestHost}/Spotify/CurrentlyPlayingTrack?id={user.Id}";
            string response = $"""
                You're all set!
                
                Use this unique URL to fetch information about what you're listening to on Spotify!
            
                {bespokeUrl}
            """;
            return Ok(response);
        }
        catch (UnauthenticatedUserNotFoundException ex)
        {
            string response = $"""
                The unique code you provided was not recognized.

                Go here to get started:

                https://{_requestHost}/User/Register
            """;
            return Ok(response);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<string>> CurrentlyPlayingTrack([FromQuery] string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return Ok("You must provide your unique ID to fetch your currently playing information");
        string currentTrackInfo = await base.Adapter.GetCurrentlyPlayingTrack(id);
        return Ok(currentTrackInfo);
    }

    [HttpPut]
    public async Task<ActionResult> RefreshAccessCode([FromQuery] string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return Ok("Must provide user identification");
        
        await base.Adapter.RefreshAccessToken(userId);
        return Ok();
    }
}
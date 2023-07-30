﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Exceptions.Spotify;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Exceptions.User;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers;

[Route("[controller]/[action]")]
public class SpotifyController : BaseController<ISpotifyAdapter>
{
    private readonly string _apiBaseUri;

    public SpotifyController(
        ApiContextUtility apiCtxUtil,
        ISpotifyAdapter adapter
    ) : base(adapter)
    {
        _apiBaseUri = apiCtxUtil.BaseUri;
    }

    [HttpGet]
    [ResponseCache(NoStore = true, Duration = 0)]
    public async Task<ActionResult> Authorize([FromQuery] string uuid)
    {
        if (string.IsNullOrWhiteSpace(uuid))
        {
            string response = $"""
                Looks like you skipped a step.

                Go here to get started:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }

        try
        {
            string uri = await base.Adapter.GetAuthorizeUri(uuid);
            return RedirectPermanentPreserveMethod(uri);
        }
        catch (UnauthenticatedUserNotFoundException)
        {
            string response = $"""
                The unique code you provided was not recognized.

                Go here to get started:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }
        catch (InvalidRegistrationTargetException)
        {
            string response = $"""
                Your registration link has expired.

                Go here to get started again:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }
    }

    [HttpGet]
    [ResponseCache(NoStore = true, Duration = 0)]
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
        
            string bespokeUrl = $"{_apiBaseUri}/Spotify/CurrentlyPlayingTrack?id={user.Id}";
            string response = $"""
                You're all set!
                
                Use this unique URL to fetch information about what you're listening to on Spotify!
            
                {bespokeUrl}
            """;
            return Ok(response);
        }
        catch (UnauthenticatedUserNotFoundException)
        {
            string response = $"""
                The unique code you provided was not recognized.

                Go here to get started:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }
    }
    
    [HttpGet]
    [ResponseCache(NoStore = true, Duration = 0)]
    public async Task<ActionResult<string>> CurrentlyPlayingTrack([FromQuery] string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                return Ok("You must provide your unique ID to fetch your currently playing information");
            string currentTrackInfo = await base.Adapter.GetCurrentlyPlayingTrack(id);
            return Ok(currentTrackInfo);
        }
        catch (UserNotFoundException)
        {
            string response = $"""
                The unique code you provided was not recognized.

                Go here to get started:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }
    }
}
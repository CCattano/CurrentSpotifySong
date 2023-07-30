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

    /// <summary>
    /// Fetches the Spotify URI the user will use to
    /// grant this app access to their Spotify information
    /// </summary>
    /// <param name="uuid">Unauthenticated User ID</param>
    /// <returns></returns>
    [HttpGet]
    [ResponseCache(NoStore = true, Duration = 0)]
    public async Task<ActionResult> Authorize([FromQuery] string uuid)
    {
        // A uuid should come from Users/Register
        // If it hasn't the user is trying to come straight to this endpoint
        // We'll let them know to go through the proper onboarding flow
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
            // If we couldn't find an unauthenticated user for the uuid provided
            // the user is trying to spoof the uuid
            // We'll let them know to go through the proper onboarding flow
            string response = $"""
                The unique code you provided was not recognized.

                Go here to get started:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }
        catch (InvalidRegistrationTargetException)
        {
            // If the unauthenticated user has expired for the uuid provided
            // the user cannot authorize anymore, and needs to onboard again
            // We'll let them know to go through the onboarding flow again
            string response = $"""
                Your registration link has expired.

                Go here to get started again:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }
    }

    /// <summary>
    /// Endpoint used by Spotify when a user has
    /// granted/denied us access to their Spotify information
    /// </summary>
    /// <param name="code">
    ///     <para>
    ///         The access code Spotify has generated for this
    ///         user who has granted us access to their information.
    ///     </para>
    ///     <para>
    ///         This value will be used to generate an AccessToken
    ///         that we can use to fetch the user's Spotify info.
    ///     </para>
    /// </param>
    /// <param name="error">
    ///     <para>
    ///         A string provided when we have not been granted access to the user's information.
    ///     </para>
    ///     <para>
    ///         The string will explain why we have not been granted access.
    ///     </para>
    /// </param>
    /// <param name="state">
    ///     <para>
    ///         A unique generated by us, provided to Spotify
    ///         previously that only we and Spotify know.
    ///     </para>
    ///     <para>
    ///         We verify this value is known to us and if it is not
    ///         we know this is not an authentic request made from Spotify
    ///     </para>
    /// </param>
    /// <returns></returns>
    [HttpGet]
    [ResponseCache(NoStore = true, Duration = 0)]
    public async Task<ActionResult<string>> AccessCode(
        [FromQuery] string code,
        [FromQuery] string error,
        [FromQuery] string state
    )
    {
        /*
         * If we weren't granted access to a user's information b/c code is
         * null/empty we'll notify the user that we could not authenticate
         * 
         * 99% of the time this should be no surprise to the
         * user b/c the reason we have no code should be b/c
         * they decided to deny us access to their information
         *
         * So we should just be reiterating what they already know 
         */
        if (string.IsNullOrWhiteSpace(code))
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
            // If we couldn't find an unauthenticated user for the state provided
            // the user is trying to spoof the uuid
            // We'll let them know to go through the proper onboarding flow
            string response = $"""
                The unique code you provided was not recognized.

                Go here to get started:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }
    }
    
    /// <summary>
    /// Fetches information about what a user is currently listening to on Spotify
    /// </summary>
    /// <param name="id">The id of the user to fetch information for</param>
    /// <returns></returns>
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
            // If we couldn't find a user for the id provided
            // the user is trying to spoof the id
            // We'll let them know to go through the proper onboarding flow
            string response = $"""
                The unique code you provided was not recognized.

                Go here to get started:

                {_apiBaseUri}/User/Register
            """;
            return Ok(response);
        }
    }
}
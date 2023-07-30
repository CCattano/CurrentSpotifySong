using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.Exceptions;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ObjectDtos;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.RequestDtos;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.ResponseDtos;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient;

public interface ISpotifyClient
{
    /// <summary>
    /// Returns the URI a user should use to be navigated to Spotify's
    /// authorization UI to give this API access to their Spotify information
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    string GetAuthorizeUri(string state);
    
    /// <summary>
    /// Generate an access token for a user who has
    /// granted us access to their Spotify information
    /// </summary>
    /// <param name="authorizationCode"></param>
    /// <returns></returns>
    Task<AccessTokenDto> GetAccessToken(string authorizationCode);
    /// <summary>
    /// Generate a new access token for a user whose current AccessToken has expired
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    Task<AccessTokenDto> RefreshAccessToken(string refreshToken);
    /// <summary>
    /// Fetch information about what a user is currently listening to
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<CurrentlyPlayingResponseDto> GetCurrentlyPlayingTrack(string accessToken);
}

/// <inheritdoc cref="ISpotifyClient"/>
public class SpotifyClient : ISpotifyClient
{
    private readonly HttpClient _client;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _apiBaseUri;

    public SpotifyClient(
        HttpClient client,
        ApiContextUtility apiCtxUtil,
        string clientId,
        string clientSecret
    )
    {
        _client = client;
        _clientId = clientId;
        _clientSecret = clientSecret;
        _apiBaseUri = apiCtxUtil.BaseUri;
    }

    public string GetAuthorizeUri(string state)
    {
        AuthorizeRequestDto requestDetails = new()
        {
            ClientId = _clientId,
            RedirectUri = _GetRedirectUri(),
            ShowDialog = false,
            State = state
        };

        IEnumerable<string> queryParams = JsonNode
            .Parse(JsonSerializer.Serialize(requestDetails, new JsonSerializerOptions() { IncludeFields = true }))
            !.AsObject()
            .Where(prop => prop.Value is not null)
            .Select(prop => $"{prop.Key}={prop.Value}");
        string queryParamStr = string.Join('&', queryParams);
        string uri = $"{ApiEndpoints.Authorize}?{queryParamStr}";

        return uri;
    }

    public async Task<AccessTokenDto> GetAccessToken(string authorizationCode)
    {
        string authHeaderValue = _GetAuthorizationHeader();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        FormUrlEncodedContent httpRequestContent = _GetHttpFormContent(new AccessTokenRequestDto()
        {
            Code = authorizationCode,
            RedirectUri = _GetRedirectUri(),
        });

        HttpResponseMessage httpResponse = await _client.PostAsync(ApiEndpoints.Token, httpRequestContent);
        AccessTokenDto response = null;
        if (httpResponse.IsSuccessStatusCode)
            response = await httpResponse.Content.ReadFromJsonAsync<AccessTokenDto>();
        return response;
    }

    public async Task<AccessTokenDto> RefreshAccessToken(string refreshToken)
    {
        string authHeaderValue = _GetAuthorizationHeader();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        FormUrlEncodedContent httpRequestContent = _GetHttpFormContent(new RefreshTokenRequestDto(refreshToken));

        HttpResponseMessage httpResponse = await _client.PostAsync(ApiEndpoints.Token, httpRequestContent);
        AccessTokenDto response = null;
        if (httpResponse.IsSuccessStatusCode)
            response = await httpResponse.Content.ReadFromJsonAsync<AccessTokenDto>();
        return response;
    }

    public async Task<CurrentlyPlayingResponseDto> GetCurrentlyPlayingTrack(string accessToken)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        HttpResponseMessage httpResponse = await _client.GetAsync(ApiEndpoints.GetCurrentlyPlayingTrack);
        CurrentlyPlayingResponseDto response = null;
        if (httpResponse.IsSuccessStatusCode)
        {
            string responseString = await httpResponse.Content.ReadAsStringAsync();
            response = _ProcessCurrentlyPlayingResponse(responseString);
        }
        else if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new BadOrExpiredTokenException();
        }
        return response;
    }

    private string _GetRedirectUri() => $"{_apiBaseUri}/Spotify/AccessCode";

    private string _GetAuthorizationHeader()
    {
        string authHeaderValue =
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
        return authHeaderValue;
    }

    private static FormUrlEncodedContent _GetHttpFormContent<T>(T objToConvertToForm)
    {
        Dictionary<string, string> formContent = JsonNode
            .Parse(
                JsonSerializer.Serialize(
                    objToConvertToForm,
                    new JsonSerializerOptions() { IncludeFields = true }
                )
            )
            !.AsObject()
            .Where(prop => prop.Value is not null)
            .ToDictionary(prop => prop.Key, prop => prop.Value!.ToString());

        FormUrlEncodedContent httpRequestContent = new(formContent);

        return httpRequestContent;
    }

    private static CurrentlyPlayingResponseDto _ProcessCurrentlyPlayingResponse(string responseString)
    {
        if (string.IsNullOrWhiteSpace(responseString)) return null;
        
        JsonNode jsonDoc = JsonNode.Parse(responseString);
        
        if (jsonDoc is null) return null;

        string trackType = jsonDoc["currently_playing_type"]?.GetValue<string>();
        if (string.IsNullOrWhiteSpace(trackType)) return null;

        CurrentlyPlayingResponseDto responseDto = 
            jsonDoc.Deserialize<CurrentlyPlayingResponseDto>();

        if (trackType.ToLower() == "track")
        {
            TrackDto item = jsonDoc["item"].Deserialize<TrackDto>();
            responseDto!.Item = item;
        } 
        else if (trackType.ToLower() == "episode")
        {
            EpisodeDto item = jsonDoc["item"].Deserialize<EpisodeDto>();
            responseDto!.Item = item;
        }

        return responseDto;
    }
    private struct ApiEndpoints
    {
        public const string Authorize = "https://accounts.spotify.com/authorize";
        public const string Token = "https://accounts.spotify.com/api/token";
        public const string GetCurrentlyPlayingTrack = "https://api.spotify.com/v1/me/player/currently-playing";
    }
}
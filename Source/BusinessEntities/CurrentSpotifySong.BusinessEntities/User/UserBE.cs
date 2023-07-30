namespace Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;

public record UserBE
{
    /// <summary>
    /// The unique MongoDb ID for this User
    /// </summary>
    public string Id;

    /// <summary>
    /// An Access Token that should be provided in API calls
    /// made to Spotify when accessing data about this user
    /// </summary>
    public string AccessToken;

    /// <summary>
    /// The token that should be sent to the Spotify Api to
    /// acquire a new AccessToken when the current one expired
    /// </summary>
    public string RefreshToken;
}
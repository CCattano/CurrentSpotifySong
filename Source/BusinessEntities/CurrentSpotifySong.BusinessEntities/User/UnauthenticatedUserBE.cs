namespace Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;

public record UnauthenticatedUserBE
{
    /// <summary>
    /// The unique ID for this Unauthenticated user
    /// </summary>
    public string Id;

    /// <summary>
    /// How long this Unauthenticated User entry is valid for
    /// </summary>
    /// <remarks>
    /// If the current date time exceeds this expiry date
    /// time this user cannot authorize themselves with
    /// our app without restarting the registration workflow
    /// </remarks>
    public DateTime ExpireDateTime;
}
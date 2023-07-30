namespace Torty.Web.Apps.CurrentSpotifySong.BusinessModels.User;

public record UnauthenticatedUserBM(string Id)
{
    /// <summary>
    /// The unique ID for this Unauthenticated user
    /// </summary>
    public readonly string Id = Id;
}
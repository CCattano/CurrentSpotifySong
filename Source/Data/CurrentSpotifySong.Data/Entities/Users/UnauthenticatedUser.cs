using MongoDB.Bson;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

public record UnauthenticatedUser
{
    public ObjectId Id;
}
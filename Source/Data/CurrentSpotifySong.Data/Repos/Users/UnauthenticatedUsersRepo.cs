using MongoDB.Driver;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users;

public interface IUnauthenticatedUsersRepo : IBaseRepo
{
}

public class UnauthenticatedUsersRepo : BaseRepo<UnauthenticatedUser>, IUnauthenticatedUsersRepo
{
    public static string CollectionName => "users.UnauthenticatedUsers";

    public UnauthenticatedUsersRepo(IMongoCollection<UnauthenticatedUser> collection) : base(collection)
    {
    }
}
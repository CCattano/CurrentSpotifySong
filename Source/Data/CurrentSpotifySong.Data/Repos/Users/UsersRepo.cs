using MongoDB.Driver;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users;

public interface IUsersRepo : IBaseRepo
{
    Task<User> Create(User userToCreate);
}

public class UsersRepo : BaseRepo<User>, IUsersRepo
{
    public static string CollectionName => "users.Users";

    public UsersRepo(IMongoCollection<User> collection) : base(collection)
    {
    }

    public async Task<User> Create(User userToCreate)
    {
        await base.Collection.InsertOneAsync(userToCreate);
        return userToCreate;
    }
}
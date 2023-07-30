using MongoDB.Bson;
using MongoDB.Driver;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users;

public interface IUsersRepo : IBaseRepo
{
    Task<User> Create(User userToCreate);
    Task<User> GetById(string id);
    Task<User> Update(User newUserDetails);
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

    public async Task<User> GetById(string id)
    {
        FilterDefinition<User> query =
            Builders<User>.Filter.Eq(nameof(User.Id), new ObjectId(id));
        IAsyncCursor<User> cursor = await base.Collection.FindAsync(query);
        User user = await cursor.SingleOrDefaultAsync();
        return user;
    }

    public async Task<User> Update(User user)
    {
        FilterDefinition<User> query = Builders<User>.Filter.Eq(nameof(User.Id), user.Id);
        User updatedUser = await base.Collection.FindOneAndReplaceAsync(query, user);
        return updatedUser;
    }
}
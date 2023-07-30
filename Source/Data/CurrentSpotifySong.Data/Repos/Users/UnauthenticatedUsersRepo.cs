using MongoDB.Bson;
using MongoDB.Driver;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users;

public interface IUnauthenticatedUsersRepo : IBaseRepo
{
    Task<UnauthenticatedUser> Create();
    Task<UnauthenticatedUser> Get(string id);
    Task Delete(string id);
    Task DeleteExpiredUsers();
}

public class UnauthenticatedUsersRepo : BaseRepo<UnauthenticatedUser>, IUnauthenticatedUsersRepo
{
    public static string CollectionName => "users.UnauthenticatedUsers";

    public UnauthenticatedUsersRepo(IMongoCollection<UnauthenticatedUser> collection) : base(collection)
    {
    }

    public async Task<UnauthenticatedUser> Create()
    {
        UnauthenticatedUser newUser = new()
        {
            ExpireDateTime = DateTime.Now.AddDays(1)
        };
        await base.Collection.InsertOneAsync(newUser);
        return newUser;
    }

    public async Task<UnauthenticatedUser> Get(string id)
    {
        FilterDefinition<UnauthenticatedUser> query =
            Builders<UnauthenticatedUser>.Filter.Eq(nameof(UnauthenticatedUser.Id), new ObjectId(id));
        IAsyncCursor<UnauthenticatedUser> cursor = await Collection.FindAsync(query);
        UnauthenticatedUser user = await cursor.SingleOrDefaultAsync();
        return user;
    }

    public async Task Delete(string id)
    {
        FilterDefinition<UnauthenticatedUser> query =
            Builders<UnauthenticatedUser>.Filter.Eq(nameof(UnauthenticatedUser.Id), new ObjectId(id));
        await Collection.FindOneAndDeleteAsync(query);
    }

    public async Task DeleteExpiredUsers()
    {
        FilterDefinition<UnauthenticatedUser> query =
            Builders<UnauthenticatedUser>.Filter.Lte(nameof(UnauthenticatedUser.ExpireDateTime), DateTime.Now);
        await Collection.DeleteManyAsync(query);
    }
}
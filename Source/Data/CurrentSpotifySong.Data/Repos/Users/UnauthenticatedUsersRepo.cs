using MongoDB.Bson;
using MongoDB.Driver;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users;

/// <summary>
/// A repository for interacting with the data contained in the users.UnauthenticatedUsers table
/// </summary>
public interface IUnauthenticatedUsersRepo : IBaseRepo
{
    /// <summary>
    /// Create a new Unauthenticated User in the users.UnauthenticatedUsers table
    /// </summary>
    /// <returns></returns>
    Task<UnauthenticatedUser> Create();
    /// <summary>
    /// Fetch a specific Unauthenticated User from the users.UnauthenticatedUsers table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UnauthenticatedUser> Get(string id);
    /// <summary>
    /// Delete a specific Unauthenticated User from the users.UnauthenticatedUsers table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(string id);
    /// <summary>
    /// Delete all Unauthenticated Users from the users.UnauthenticatedUsers
    /// table whose ExpireDateTime is in the past compared to DateTime.Now
    /// </summary>
    /// <returns></returns>
    Task DeleteExpiredUsers();
}

/// <inheritdoc cref="IUnauthenticatedUsersRepo"/>
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
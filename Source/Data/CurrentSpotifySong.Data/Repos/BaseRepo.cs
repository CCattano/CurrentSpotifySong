using MongoDB.Driver;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Repos;

public interface IBaseRepo
{
    public static abstract string CollectionName { get; }
}

public abstract class BaseRepo<TModel>
{
    protected readonly IMongoCollection<TModel> Collection;

    protected BaseRepo(IMongoCollection<TModel> collection) => Collection = collection;
}
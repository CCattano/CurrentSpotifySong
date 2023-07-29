using MongoDB.Driver;

namespace Torty.Web.Apps.CurrentSpotifySong.Data;

public class BaseDataService
{
    private readonly string _connStr;

    private IMongoDatabase _db;
    private IMongoDatabase Database => _db ??= _InitializeDb();

    protected BaseDataService(string connStr) => _connStr = connStr;

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);

        // If the collection doesn't exist it's about to
        if (collection is null)
            Database.CreateCollection(collectionName);

        collection = Database.GetCollection<T>(collectionName);

        return collection;
    }

    private IMongoDatabase _InitializeDb()
    {
        MongoClientSettings settings = MongoClientSettings.FromConnectionString(_connStr);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        MongoClient client = new(settings);

        // This app only has access to the Spotify Database kept on the Database Server
        IMongoDatabase database = client.GetDatabase("Spotify");

        return database;
    }
}
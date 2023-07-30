using Microsoft.Extensions.DependencyInjection;
using Torty.Web.Apps.CurrentSpotifySong.Data.Schemas;

namespace Torty.Web.Apps.CurrentSpotifySong.Data;

public interface IDataService
{
    /// <inheritdoc cref="IUsersSchema"/>
    IUsersSchema Users { get; }
}

/// <inheritdoc cref="IDataService"/>
public class DataService : BaseDataService, IDataService
{
    private IUsersSchema _users;

    public DataService(string connStr) : base(connStr)
    {
    }

    public IUsersSchema Users => _users ??= new UsersSchema(this);
}

public static class DataServiceExtensions
{
    /// <summary>
    /// Registers the DataService with the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connStr">
    ///     The connection string used by the
    ///     data service to connect to the database
    /// </param>
    public static void AddDataService(this IServiceCollection services, string connStr) =>
        services.AddScoped<IDataService, DataService>(_ => new DataService(connStr));
}
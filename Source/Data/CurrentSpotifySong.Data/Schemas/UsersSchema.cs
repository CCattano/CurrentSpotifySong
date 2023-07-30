using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;
using Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users;
using UnauthenticatedUsersRepository = Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users.UnauthenticatedUsersRepo;
using UsersRepository = Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users.UsersRepo;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Schemas;

/// <summary>
/// An object representing a specific Database schema containing all
/// Repositories that would write data to tables belonging to that schema
/// </summary>
public interface IUsersSchema
{
    /// <inheritdoc cref="IUnauthenticatedUsersRepo"/>
    IUnauthenticatedUsersRepo UnauthenticatedUsersRepo { get; }
    
    /// <inheritdoc cref="IUsersRepo"/>
    IUsersRepo UsersRepo { get; }
}

/// <inheritdoc cref="IUsersSchema"/>
public class UsersSchema : BaseSchema, IUsersSchema
{
    private IUnauthenticatedUsersRepo _unauthenticatedUsersRepo;
    private IUsersRepo _usersRepo;

    public UsersSchema(BaseDataService dataSvc) : base(dataSvc)
    {
    }

    public IUnauthenticatedUsersRepo UnauthenticatedUsersRepo => _unauthenticatedUsersRepo
        ??= new UnauthenticatedUsersRepo(base.DataSvc.GetCollection<UnauthenticatedUser>(UnauthenticatedUsersRepository.CollectionName));

    public IUsersRepo UsersRepo => _usersRepo ??= new UsersRepo(base.DataSvc.GetCollection<User>(UsersRepository.CollectionName));
}
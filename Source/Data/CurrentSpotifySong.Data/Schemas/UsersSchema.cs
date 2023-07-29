using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;
using Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users;
using UnauthenticatedUsersRepository = Torty.Web.Apps.CurrentSpotifySong.Data.Repos.Users.UnauthenticatedUsersRepo;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Schemas;

public interface IUsersSchema
{
    IUnauthenticatedUsersRepo UnauthenticatedUsersRepo { get; }
}

public class UsersSchema : BaseSchema, IUsersSchema
{
    private IUnauthenticatedUsersRepo _unauthenticatedUsersRepo;

    public UsersSchema(BaseDataService dataSvc) : base(dataSvc)
    {
    }

    public IUnauthenticatedUsersRepo UnauthenticatedUsersRepo => _unauthenticatedUsersRepo
        ??= new UnauthenticatedUsersRepo(base.DataSvc.GetCollection<UnauthenticatedUser>(UnauthenticatedUsersRepository.CollectionName));
}
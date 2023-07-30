using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Data;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Facades.Users;

public interface IUnauthenticatedUsersFacade
{
    Task<UnauthenticatedUserBE> Create();
    Task<UnauthenticatedUserBE> GetById(string id);
    Task Delete(string id);
    Task DeleteExpired();
}

public class UnauthenticatedUsersFacade : BaseFacade, IUnauthenticatedUsersFacade
{
    public UnauthenticatedUsersFacade(IDataService dataSvc, IMapper mapper) : base(dataSvc, mapper)
    {
    }

    public async Task<UnauthenticatedUserBE> Create()
    {
        UnauthenticatedUser newUserEntity =
            await base.DataService.Users.UnauthenticatedUsersRepo.Create();

        UnauthenticatedUserBE newUserBE = base.Mapper.Map<UnauthenticatedUserBE>(newUserEntity);
        return newUserBE;
    }

    public async Task<UnauthenticatedUserBE> GetById(string id)
    {
        UnauthenticatedUser userEntity =
            await base.DataService.Users.UnauthenticatedUsersRepo.Get(id);
        if (userEntity is null) return null;
        UnauthenticatedUserBE userBE = base.Mapper.Map<UnauthenticatedUserBE>(userEntity);
        return userBE;
    }
    
    public async Task Delete(string id)
    {
        await base.DataService.Users.UnauthenticatedUsersRepo.Delete(id);
    }

    public async Task DeleteExpired()
    {
        await base.DataService.Users.UnauthenticatedUsersRepo.DeleteExpiredUsers();
    }
}
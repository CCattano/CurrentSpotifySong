using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Data;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Facades.Users;

public interface IUnauthenticatedUsersFacade
{
    /// <summary>
    /// Create a new Unauthenticated User 
    /// </summary>
    /// <returns></returns>
    Task<UnauthenticatedUserBE> Create();
    /// <summary>
    /// Get a Unauthenticated User by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UnauthenticatedUserBE> GetById(string id);
    /// <summary>
    /// Delete a specific Unauthenticated User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(string id);
    /// <summary>
    /// Delete any Unauthenticated Users whose ExpireDateTime
    /// is in the past compared to DateTime.Now
    /// </summary>
    /// <returns></returns>
    Task DeleteExpired();
}

/// <inheritdoc cref="IUnauthenticatedUsersFacade"/>
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
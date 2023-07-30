using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Exceptions.User;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Facades.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;

public interface IUserAdapter
{
    #region UNAUTHENTICATED USERS

    Task<UnauthenticatedUserBE> CreateUnauthenticatedUser();
    Task<UnauthenticatedUserBE> GetUnauthenticatedUserById(string unauthenticatedUserId);
    Task DeleteUnauthenticatedUser(string id);
    Task DeleteExpiredUnauthenticatedUsers();
    
    #endregion

    #region USERS
    
    Task<UserBE> CreateUser(UserBE user);
    Task<UserBE> GetUserById(string id);
    Task<UserBE> UpdateUser(UserBE user);

    #endregion
}

public class UserAdapter : BaseAdapter, IUserAdapter
{
    private readonly IUnauthenticatedUsersFacade _unauthenticatedUsersFacade;
    private readonly IUsersFacade _usersFacade;

    public UserAdapter(
        IMapper mapper,
        IUnauthenticatedUsersFacade unauthenticatedUsersFacade,
        IUsersFacade usersFacade
    ) : base(mapper)
    {
        _unauthenticatedUsersFacade = unauthenticatedUsersFacade;
        _usersFacade = usersFacade;
    }

    #region UNAUTHENTICATED USERS

    public async Task<UnauthenticatedUserBE> CreateUnauthenticatedUser()
    {
        UnauthenticatedUserBE newUser = await _unauthenticatedUsersFacade.Create();
        return newUser;
    }

    public async Task<UnauthenticatedUserBE> GetUnauthenticatedUserById(string unauthenticatedUserId)
    {
        UnauthenticatedUserBE user = await _unauthenticatedUsersFacade.GetById(unauthenticatedUserId);
        if (user is null) throw new UnauthenticatedUserNotFoundException();
        return user;
    }

    public async Task DeleteExpiredUnauthenticatedUsers()
    {
        await _unauthenticatedUsersFacade.DeleteExpired();
    }

    public async Task DeleteUnauthenticatedUser(string id)
    {
        await _unauthenticatedUsersFacade.Delete(id);
    }
    
    #endregion

    #region Users

    public async Task<UserBE> CreateUser(UserBE user)
    {
        UserBE newUser = await _usersFacade.Create(user);
        return newUser;
    }

    public async Task<UserBE> GetUserById(string id)
    {
        UserBE user = await _usersFacade.GetById(id);
        if (user is null) throw new UserNotFoundException();
        return user;
    }

    public async Task<UserBE> UpdateUser(UserBE user)
    {
        await GetUserById(user.Id);
        UserBE updatedUser = await _usersFacade.Update(user);
        return updatedUser;
    }

    #endregion

}
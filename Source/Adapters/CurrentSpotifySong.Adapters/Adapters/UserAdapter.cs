using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Exceptions.User;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Facades.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;

/// <summary>
/// Adapter for handling all business logic related to working with User data
/// </summary>
public interface IUserAdapter
{
    #region UNAUTHENTICATED USERS

    /// <summary>
    /// Create a new Unauthenticated User
    /// </summary>
    /// <returns></returns>
    Task<UnauthenticatedUserBE> CreateUnauthenticatedUser();
    /// <summary>
    /// Fetch a specific Unauthenticated User
    /// </summary>
    /// <param name="unauthenticatedUserId"></param>
    /// <returns></returns>
    Task<UnauthenticatedUserBE> GetUnauthenticatedUserById(string unauthenticatedUserId);
    /// <summary>
    /// Delete a specific Unauthenticated User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteUnauthenticatedUser(string id);
    /// <summary>
    /// Delete all Unauthenticated Users whose ExpireDateTime
    /// is in the past compared to DateTime.Now
    /// </summary>
    /// <returns></returns>
    Task DeleteExpiredUnauthenticatedUsers();
    
    #endregion

    #region USERS
    
    /// <summary>
    /// Create a new User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<UserBE> CreateUser(UserBE user);
    /// <summary>
    /// Fetch a specific User
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserBE> GetUserById(string id);
    /// <summary>
    /// Update a user with new information
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<UserBE> UpdateUser(UserBE user);

    #endregion
}

/// <inheritdoc cref="IUserAdapter"/>
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
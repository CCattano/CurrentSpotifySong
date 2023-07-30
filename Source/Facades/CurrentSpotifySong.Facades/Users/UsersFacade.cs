using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Data;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

namespace Torty.Web.Apps.CurrentSpotifySong.Facades.Users;

public interface IUsersFacade
{
    /// <summary>
    /// Create a new User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<UserBE> Create(UserBE user);
    /// <summary>
    /// Fetch a User by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserBE> GetById(string id);
    /// <summary>
    /// Update an existing User with new information
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<UserBE> Update(UserBE user);
}

/// <inheritdoc cref="IUsersFacade"/>
public class UsersFacade: BaseFacade, IUsersFacade
{
    public UsersFacade(IDataService dataService, IMapper mapper) : base(dataService, mapper)
    {
    }

    public async Task<UserBE> Create(UserBE user)
    {
        User userToCreate = base.Mapper.Map<User>(user);
        User newUser = await base.DataService.Users.UsersRepo.Create(userToCreate);
        UserBE newUserBE = base.Mapper.Map<UserBE>(newUser);
        return newUserBE;
    }
    
    public async Task<UserBE> GetById(string id)
    {
        User userEntity = await base.DataService.Users.UsersRepo.GetById(id);
        if (userEntity is null) return null;
        UserBE userBE = base.Mapper.Map<UserBE>(userEntity);
        return userBE;
    }

    public async Task<UserBE> Update(UserBE user)
    {
        User userEntity = base.Mapper.Map<User>(user);
        User updatedUserEntity = await base.DataService.Users.UsersRepo.Update(userEntity);
        UserBE updatedUserBE = base.Mapper.Map<UserBE>(updatedUserEntity);
        return updatedUserBE;
    }
}
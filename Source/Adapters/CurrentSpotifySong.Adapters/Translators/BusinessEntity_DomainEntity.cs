using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Translators.User;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Translators;

/// <summary>
/// Manifest of all translators that deal with translating business entities to domain entities and vice-versa
/// </summary>
public class BusinessEntity_DomainEntity: Profile
{
    public BusinessEntity_DomainEntity()
    {
        this.RegisterTranslator<UnauthenticatedUserBE, UnauthenticatedUser, UnauthenticatedUserBE_UnauthenticatedUser>();
        this.RegisterTranslator<UserBE, Data.Entities.Users.User, UserBE_User>();
    }
}
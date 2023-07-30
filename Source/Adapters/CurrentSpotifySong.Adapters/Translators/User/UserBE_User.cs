using AutoMapper;
using MongoDB.Bson;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Translators.User;

/// <summary>
/// Translator that converts a UserBE to a User and vice-versa
/// </summary>
public class UserBE_User: BaseTranslator<UserBE, Data.Entities.Users.User>
{
    /// <summary>
    /// Converts a User Entity to a UserBE
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override UserBE Convert(Data.Entities.Users.User source, UserBE destination, ResolutionContext context)
    {
        UserBE result = new()
        {
            Id = source.Id.ToString(),
            AccessToken = source.AccessToken,
            RefreshToken = source.RefreshToken
        };
        return result;
    }

    /// <summary>
    /// Converts a UserBE to a User Entity 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Data.Entities.Users.User Convert(UserBE source, Data.Entities.Users.User destination, ResolutionContext context)
    {
        Data.Entities.Users.User result = new()
        {
            Id = string.IsNullOrWhiteSpace(source.Id) ? ObjectId.Empty : new ObjectId(source.Id),
            AccessToken = source.AccessToken,
            RefreshToken = source.RefreshToken,
            LastAccessedDateTime = DateTime.Now
        };
        return result;
    }
}
using AutoMapper;
using MongoDB.Bson;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Translators.User;

public class UserBE_User: BaseTranslator<UserBE, Data.Entities.Users.User>
{
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

    public override Data.Entities.Users.User Convert(UserBE source, Data.Entities.Users.User destination, ResolutionContext context)
    {
        Data.Entities.Users.User result = new()
        {
            Id = string.IsNullOrWhiteSpace(source.Id) ? ObjectId.Empty : new ObjectId(source.Id),
            AccessToken = source.AccessToken,
            RefreshToken = source.RefreshToken
        };
        return result;
    }
}
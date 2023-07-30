using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Translators.User;

/// <summary>
/// Translator that converts a UnauthenticatedUserBE to a UnauthenticatedUser and vice-versa
/// </summary>
public class UnauthenticatedUserBE_UnauthenticatedUser: BaseTranslator<UnauthenticatedUserBE, UnauthenticatedUser>
{
    /// <summary>
    /// Converts a UnauthenticatedUserBE to a UnauthenticatedUser Entity
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override UnauthenticatedUser Convert(UnauthenticatedUserBE source, UnauthenticatedUser destination, ResolutionContext context)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Converts a UnauthenticatedUser Entity to a UnauthenticatedUserBE
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override UnauthenticatedUserBE Convert(UnauthenticatedUser source, UnauthenticatedUserBE destination, ResolutionContext context)
    {
        // Will always map source content directly to a new result obj
        // Do not support the idea of receiving a pre-translated object
        // and "updating" it with new data point from a source object
        UnauthenticatedUserBE result = new()
        {
            Id = source.Id.ToString(),
            ExpireDateTime = source.ExpireDateTime
        };
        return result;
    }
}
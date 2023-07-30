using System;
using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.BusinessModels.User;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers.BusinessModel_BusinessEntity.User;

public class UnauthenticatedUserBM_UnauthenticatedUserBE: BaseTranslator<UnauthenticatedUserBM, UnauthenticatedUserBE>
{
    public override UnauthenticatedUserBE Convert(
        UnauthenticatedUserBM source,
        UnauthenticatedUserBE destination,
        ResolutionContext context
    )
    {
        throw new NotImplementedException();
    }
    
    public override UnauthenticatedUserBM Convert(
        UnauthenticatedUserBE source,
        UnauthenticatedUserBM destination,
        ResolutionContext context
    )
    {
        // Will always map source content directly to a new result obj
        // Do not support the idea of receiving a pre-translated object
        // and "updating" it with new data point from a source object
        UnauthenticatedUserBM result = new(source.Id);
        return result;
    }
}
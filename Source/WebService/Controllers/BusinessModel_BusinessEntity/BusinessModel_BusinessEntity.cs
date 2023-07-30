using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.BusinessEntities.User;
using Torty.Web.Apps.CurrentSpotifySong.BusinessModels.User;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions;
using Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers.BusinessModel_BusinessEntity.User;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers.BusinessModel_BusinessEntity;

public class BusinessModel_BusinessEntity: Profile
{
    public BusinessModel_BusinessEntity()
    {
        this.RegisterTranslator<UnauthenticatedUserBM, UnauthenticatedUserBE, UnauthenticatedUserBM_UnauthenticatedUserBE>();
    }
}
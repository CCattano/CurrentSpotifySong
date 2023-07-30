using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.Data;

namespace Torty.Web.Apps.CurrentSpotifySong.Facades;

public class BaseFacade
{
    protected readonly IDataService DataService;
    protected readonly IMapper Mapper;

    protected BaseFacade(IDataService dataService, IMapper mapper) => (DataService, Mapper) = (dataService, mapper);
}
using AutoMapper;

namespace Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;

public abstract class BaseAdapter
{
    protected readonly IMapper Mapper;

    protected BaseAdapter(IMapper mapper) => Mapper = mapper;
}
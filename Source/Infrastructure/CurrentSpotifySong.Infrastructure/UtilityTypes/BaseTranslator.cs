using AutoMapper;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

public abstract class BaseTranslator<TType1, TType2>: TranslateSourceToDestination<TType1, TType2>
{
}

public abstract class TranslateSourceToDestination<TType1, TType2> : TranslateDestinationToSource<TType1, TType2>, ITypeConverter<TType1, TType2>
{
    public abstract TType2 Convert(TType1 source, TType2 destination, ResolutionContext context);
}

public abstract class TranslateDestinationToSource<TType1, TType2> : ITypeConverter<TType2, TType1>
{
    public abstract TType1 Convert(TType2 source, TType1 destination, ResolutionContext context);
}
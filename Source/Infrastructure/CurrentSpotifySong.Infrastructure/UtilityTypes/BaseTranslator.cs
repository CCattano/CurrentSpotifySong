using AutoMapper;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

/// <summary>
/// Base class all translators extend
/// </summary>
/// <remarks>
/// Extending this guarantees the child class defines how to translate from both Source->Destination and Destination->Source
/// </remarks>
/// <typeparam name="TType1">Source type</typeparam>
/// <typeparam name="TType2">Destination type</typeparam>
public abstract class BaseTranslator<TType1, TType2>: TranslateSourceToDestination<TType1, TType2>
{
}

/// <summary>
/// Class requiring a translation of Source->Destination be defined
/// </summary>
/// <typeparam name="TType1">Source type</typeparam>
/// <typeparam name="TType2">Destination type</typeparam>
public abstract class TranslateSourceToDestination<TType1, TType2> : TranslateDestinationToSource<TType1, TType2>, ITypeConverter<TType1, TType2>
{
    public abstract TType2 Convert(TType1 source, TType2 destination, ResolutionContext context);
}

/// <summary>
/// Class requiring a translation of Destination->Source be defined
/// </summary>
/// <typeparam name="TType1">Source type</typeparam>
/// <typeparam name="TType2">Destination type</typeparam>
public abstract class TranslateDestinationToSource<TType1, TType2> : ITypeConverter<TType2, TType1>
{
    public abstract TType1 Convert(TType2 source, TType1 destination, ResolutionContext context);
}
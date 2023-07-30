using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions;

public static class TranslatorExtensions
{
    /// <summary>
    /// Utility Method that takes a SourceType, DestinationType, and ConverterType
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This utility then registers a AutoMapper mapping in the
    ///         direction Source->Destination and Destination->Source
    ///         using the types and converter specified
    ///     </para>
    ///     <para>
    ///         Because the Converter has to extend BaseTranslator to be valid
    ///         we know the converter defines translations in both directions
    ///     </para>
    /// </remarks>
    /// <param name="mapperProfile"></param>
    /// <typeparam name="TType1"></typeparam>
    /// <typeparam name="TType2"></typeparam>
    /// <typeparam name="TConverter"></typeparam>
    public static void RegisterTranslator<TType1, TType2, TConverter>(this Profile mapperProfile) where TConverter : BaseTranslator<TType1, TType2>
    {
        mapperProfile.CreateMap<TType1, TType2>().ConvertUsing<TConverter>();
        mapperProfile.CreateMap<TType2, TType1>().ConvertUsing<TConverter>();
    }
}
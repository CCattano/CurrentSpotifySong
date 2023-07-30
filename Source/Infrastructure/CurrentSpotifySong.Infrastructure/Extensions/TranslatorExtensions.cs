using AutoMapper;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions;

public static class TranslatorExtensions
{
    public static void RegisterTranslator<TType1, TType2, TConverter>(this Profile mapperProfile) where TConverter : BaseTranslator<TType1, TType2>
    {
        mapperProfile.CreateMap<TType1, TType2>().ConvertUsing<TConverter>();
        mapperProfile.CreateMap<TType2, TType1>().ConvertUsing<TConverter>();
    }
}
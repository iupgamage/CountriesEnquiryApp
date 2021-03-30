using AutoMapper;
using CountriesEnquiryApp.Common.DTOs;
using CountriesEnquiryApp.Common.Models;

namespace CountriesEnquiryApp.Common.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Country, CountryDto>() // Map Country to CountryDto
                .ForMember(dest => dest.Name, source => source.MapFrom(source => source.Translations.NL)); //Specific Mapping for Dutch name

            CreateMap<RegionalBloc, RegionalBlocDto>(); // Map RegionalBloc to RegionalBlocDto
        }
    }
}

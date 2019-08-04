namespace Footballize.Web.MappingProfiles
{
    using AutoMapper;
    using Footballize.Models;
    using ViewModels.Countries;

    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryInputModel>();
            CreateMap<Country, CountryInputModel>().ReverseMap();
            CreateMap<Country, CountriesIndexViewModel>();
        }
    }
}

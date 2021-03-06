﻿namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class PitchIndexViewModel : IMapFrom<Pitch>, IMapTo<Pitch>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string TownName { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public double AddressLocationLatitude { get; set; }
        public double AddressLocationLongitude { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pitch, PitchIndexViewModel>()
                .ForMember(x => x.CountryName, opt => opt.MapFrom(y => y.Address.Town.Province.Country.Name))
                .ForMember(x => x.TownName, opt => opt.MapFrom(y => y.Address.Town.Name));
        }
    }
}
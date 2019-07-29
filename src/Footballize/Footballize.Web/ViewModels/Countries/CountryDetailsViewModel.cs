namespace Footballize.Web.ViewModels.Countries
{
    using System.Collections.Generic;
    using Footballize.Models;
    using Models.Towns;
    using Provinces;
    using Services.Mapping;

    public class CountryDetailsViewModel : IMapFrom<Country>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProvinceWithTownsViewModel> Provinces { get; set; }
    }
}
namespace Footballize.Web.Areas.Administration.ViewModels.Countries
{
    using System.Collections.Generic;
    using Models;
    using Provinces;
    using Services.Mapping;

    public class CountryDetailsViewModel : IMapFrom<Country>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProvinceWithTownsViewModel> Provinces { get; set; }
    }
}
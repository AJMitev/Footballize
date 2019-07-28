namespace Footballize.Web.ViewModels.Provinces
{
    using System.Collections.Generic;
    using Footballize.Models;
    using Models.Towns;
    using Services.Mapping;

    public class ProvinceDetailsViewModel : IMapFrom<Province>
    {
        public string Name { get; set; }
        public string CountryName { get; set; }
        public ICollection<TownNameViewModel> Towns{ get; set; }
    }
}
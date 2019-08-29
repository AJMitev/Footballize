namespace Footballize.Web.Areas.Administration.ViewModels.Provinces
{
    using System.Collections.Generic;
    using Models;
    using Services.Mapping;
    using Towns;

    public class ProvinceDetailsViewModel : IMapFrom<Province>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public ICollection<TownNameAndIdViewModel> Towns{ get; set; }
    }
}
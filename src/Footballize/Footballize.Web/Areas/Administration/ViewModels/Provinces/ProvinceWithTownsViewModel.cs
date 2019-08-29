namespace Footballize.Web.Areas.Administration.ViewModels.Provinces
{
    using System.Collections.Generic;
    using Models;
    using Services.Mapping;
    using Towns;

    public class ProvinceWithTownsViewModel : IMapFrom<Province>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<TownNameAndIdViewModel> Towns { get; set; }
    }
}
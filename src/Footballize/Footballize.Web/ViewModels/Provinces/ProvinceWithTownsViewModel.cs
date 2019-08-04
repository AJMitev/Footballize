namespace Footballize.Web.ViewModels.Provinces
{
    using System.Collections.Generic;
    using Footballize.Models;
    using ViewModels.Towns;
    using Services.Mapping;

    public class ProvinceWithTownsViewModel : IMapFrom<Province>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<TownNameAndIdViewModel> Towns { get; set; }
    }
}
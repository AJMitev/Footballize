namespace Footballize.Web.ViewModels.Provinces
{
    using Footballize.Models;
    using Services.Mapping;

    public class ProvinceNameAndIdViewModel :IMapFrom<Province>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
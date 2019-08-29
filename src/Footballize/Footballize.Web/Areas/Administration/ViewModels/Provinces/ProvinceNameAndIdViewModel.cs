namespace Footballize.Web.Areas.Administration.ViewModels.Provinces
{
    using Models;
    using Services.Mapping;

    public class ProvinceNameAndIdViewModel :IMapFrom<Province>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
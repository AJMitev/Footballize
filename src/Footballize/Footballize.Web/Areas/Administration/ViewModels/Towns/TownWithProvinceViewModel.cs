namespace Footballize.Web.Areas.Administration.ViewModels.Towns
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class TownWithProvinceViewModel : IMapFrom<Town>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Town, TownWithProvinceViewModel>()
                .ForMember(x => x.Name,
                    cfg => cfg.MapFrom(y => string.Concat(y.Name, ", ", y.Province.Name)));
        }

        public override bool Equals(object obj)
        {
            var other = obj as TownWithProvinceViewModel;

            return this.Id.Equals(other?.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
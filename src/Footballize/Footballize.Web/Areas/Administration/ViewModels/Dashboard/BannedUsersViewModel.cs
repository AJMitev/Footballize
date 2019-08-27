namespace Footballize.Web.Areas.Administration.ViewModels.Dashboard
{
    using System;
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class BannedUsersViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime BanUntil { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, BannedUsersViewModel>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(y => y.FirstName + " " + y.LastName));
        }
    }
}
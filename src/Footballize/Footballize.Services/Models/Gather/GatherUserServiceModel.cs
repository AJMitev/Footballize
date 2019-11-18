namespace Footballize.Services.Models.Gather
{
    using Footballize.Models;
    using Mapping;

    public class GatherUserServiceModel : IMapFrom<GatherUser>
    {
        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}
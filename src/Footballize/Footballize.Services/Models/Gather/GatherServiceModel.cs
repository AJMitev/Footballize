namespace Footballize.Services.Models.Gather
{
    using System.Collections.Generic;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Mapping;

    public class GatherServiceModel : IMapFrom<Gather>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaximumPlayers { get; set; }
        public TeamFormat TeamFormat { get; set; }
        public GameStatus Status { get; set; }
        public ICollection<GatherUserServiceModel> Players { get; set; }
        public string CreatorId { get; set; }
    }
}
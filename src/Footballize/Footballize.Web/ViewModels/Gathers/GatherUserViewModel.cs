namespace Footballize.Web.ViewModels.Gathers
{
    using System;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class GatherUserViewModel : IMapFrom<GatherUser>
    {
        public string GatherId { get; set; }
        public string GatherTitle { get; set; }
        public TeamFormat GatherTeamFormat { get; set; }
        public GameStatus GatherStatus { get; set; }
        public DateTime GatherStartingAt { get; set; }
            
    }
}
namespace Footballize.Models
{
    using Abstracts;

    public class Versus : BaseMatchModel
    {
        public string AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public string HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
    }
}
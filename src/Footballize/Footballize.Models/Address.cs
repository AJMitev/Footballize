namespace Footballize.Models
{
    using Abstracts;

    public class Address : BaseDeletableModel<string>
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public virtual Town Town { get; set; }
        public string TownId { get; set; }
        public string LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
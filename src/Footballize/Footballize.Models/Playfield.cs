namespace Footballize.Models
{
    using Abstracts;

    public class Playfield : BaseDeletableModel<string>
    {
        public string Name { get; set; }
        public virtual Address Address { get; set; }
        public string AddressId { get; set; }
    }
}
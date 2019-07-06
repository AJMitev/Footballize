namespace Footballize.Models
{
    using Abstracts;

    public class Pitch : BaseDeletableModel<string>
    {
        public string Name { get; set; }
        public virtual Address Address { get; set; }
        public string AddresId { get; set; }
    }
}
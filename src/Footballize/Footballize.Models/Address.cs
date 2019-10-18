namespace Footballize.Models
{
    using System;
    using Abstracts;

    public class Address : BaseDeletableModel<string>
    {
        public Address()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Street { get; set; }
        public int Number { get; set; }
        public virtual Town Town { get; set; }
        public string TownId { get; set; }
        public string LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
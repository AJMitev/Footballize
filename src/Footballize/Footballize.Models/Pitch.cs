namespace Footballize.Models
{
    using System;
    using Abstracts;

    public class Pitch : BaseDeletableModel<string>
    {
        public Pitch()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public virtual Address Address { get; set; }
        public string AddressId { get; set; }
    }
}
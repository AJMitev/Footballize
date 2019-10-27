namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Abstracts;

    public class Province : BaseDeletableModel<string>
    {
        public Province()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Towns = new HashSet<Town>();
        }

        public string Name { get; set; }
        public Country Country { get; set; }
        public string CountryId { get; set; }
        public ICollection<Town> Towns { get; set; }
    }
}
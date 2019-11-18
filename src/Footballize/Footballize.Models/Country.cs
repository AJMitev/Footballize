namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Abstracts;

    public class Country : BaseDeletableModel<string>
    {
        public Country()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Provinces = new HashSet<Province>();
        }

        public string Name { get; set; }
        public string IsoCode { get; set; }
        public ICollection<Province> Provinces { get; set; }
    }
}
namespace Footballize.Models
{
    using System.Collections.Generic;
    using Abstracts;

    public class Country : BaseDeletableModel<string>
    {
        public Country()
        {
            this.Provinces = new HashSet<Province>();
        }

        public string Name { get; set; }
        public string IsoCode { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}
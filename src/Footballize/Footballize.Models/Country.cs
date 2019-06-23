namespace Footballize.Models
{
    using System.Collections.Generic;

    public class Country
    {
        public Country()
        {
            this.Provinces = new HashSet<Province>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Province> Provinces { get; set; }
    }
}
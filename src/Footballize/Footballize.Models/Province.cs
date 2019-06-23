namespace Footballize.Models
{
    using System.Collections.Generic;

    public class Province
    {
        public Province()
        {
            this.Municipalities = new HashSet<Municipality>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public string CountryId { get; set; }
        public ICollection<Municipality> Municipalities { get; set; }
    }
}
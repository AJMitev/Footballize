namespace Footballize.Models
{
    using System.Collections.Generic;
    using Abstracts;

    public class Province : BaseDeletableModel<string>
    {
        public Province()
        {
            this.Municipalities = new HashSet<Municipality>();
        }

        public string Name { get; set; }
        public virtual Country Country { get; set; }
        public string CountryId { get; set; }
        public virtual ICollection<Municipality> Municipalities { get; set; }
    }
}
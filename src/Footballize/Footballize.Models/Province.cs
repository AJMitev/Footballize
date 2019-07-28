namespace Footballize.Models
{
    using System.Collections.Generic;
    using Abstracts;

    public class Province : BaseDeletableModel<string>
    {
        public Province()
        {
            this.Towns = new HashSet<Town>();
        }

        public string Name { get; set; }
        public virtual Country Country { get; set; }
        public string CountryId { get; set; }
        public virtual ICollection<Town> Towns { get; set; }
    }
}
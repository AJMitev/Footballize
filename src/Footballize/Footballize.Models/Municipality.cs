namespace Footballize.Models
{
    using System.Collections.Generic;
    using Abstracts;

    public class Municipality : BaseDeletableModel<string>
    {
        public Municipality()
        {
            this.Towns = new HashSet<Town>();
        }
        public string Name { get; set; }
        public virtual Province Province { get; set; }
        public string ProvinceId { get; set; }
        public virtual ICollection<Town> Towns { get; set; }
    }
}
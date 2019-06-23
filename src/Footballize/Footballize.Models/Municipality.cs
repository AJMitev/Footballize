namespace Footballize.Models
{
    using System.Collections.Generic;

    public class Municipality
    {
        public Municipality()
        {
            this.Towns = new HashSet<Town>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Province Province { get; set; }
        public string ProvinceId { get; set; }
        public ICollection<Town> Towns { get; set; }
    }
}
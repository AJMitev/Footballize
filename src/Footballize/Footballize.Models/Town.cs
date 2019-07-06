namespace Footballize.Models
{
    using System.Collections.Generic;
    using Abstracts;

    public class Town : BaseDeletableModel<string>
    {
        public Town()
        {
            this.Addresses = new HashSet<Address>();
        }
        public string Name { get; set; }
        public virtual Municipality Municipality { get; set; }
        public string MunicipalityId { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
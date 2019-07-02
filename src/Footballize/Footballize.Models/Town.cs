﻿namespace Footballize.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.Addresses = new HashSet<Address>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Municipality Municipality { get; set; }
        public string MunicipalityId { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
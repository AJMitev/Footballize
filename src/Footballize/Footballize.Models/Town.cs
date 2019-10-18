﻿namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Abstracts;

    public class Town : BaseDeletableModel<string>
    {
        public Town()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Addresses = new HashSet<Address>();
        }
        public string Name { get; set; }
        public virtual Province Province { get; set; }
        public string ProvinceId { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
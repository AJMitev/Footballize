namespace Footballize.Models
{
    using System;
    using Abstracts;

    public class Location : BaseDeletableModel<string>
    {
        public Location()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
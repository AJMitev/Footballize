namespace Footballize.Models
{
    using Abstracts;

    public class Location : BaseDeletableModel<string>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
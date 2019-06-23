namespace Footballize.Models
{
    public class Address
    {
        public string Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public Town Town { get; set; }
        public string TownId { get; set; }

        public Playfield Playfield { get; set; }
        public string PlayfieldId { get; set; }
    }

    public class Playfield
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
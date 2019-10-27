namespace Footballize.Services.Models.Address
{
    using Footballize.Models;
    using Mapping;

    public class AddressServiceModel : IMapFrom<Address>
    {
        public string Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
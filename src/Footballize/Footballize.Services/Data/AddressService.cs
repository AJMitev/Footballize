namespace Footballize.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Mapping;
    using Models.Address;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;

        public AddressService(IDeletableEntityRepository<Address> addressRepository)
            => this.addressRepository = addressRepository;

        public async Task<string> Create(string street, int number, double latitude, double longitude)
        {
            var address = new Address
            {
                Street = street,
                Number = number,
                Location = new Location
                {
                    Latitude = latitude,
                    Longitude = longitude
                }
            };
            
            await this.addressRepository.AddAsync(address);
            await this.addressRepository.SaveChangesAsync();

            return address.Id;
        }

        public bool Exists(string street, int number)
            => this.addressRepository
                .All()
                .Any(x => x.Street.Equals(street)
                          && x.Number.Equals(number));

        public TViewModel GetById<TViewModel>(string id)
            => this.addressRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .SingleOrDefault();

        public AddressServiceModel GetByName(string street, int number)
            => this.addressRepository
                .All()
                .Where(x => x.Street == street && x.Number == number)
                .To<AddressServiceModel>()
                .SingleOrDefault();
    }
}
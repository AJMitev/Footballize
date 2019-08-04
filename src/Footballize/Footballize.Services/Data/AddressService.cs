using Footballize.Models;

namespace Footballize.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;

    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> addressRepository;

        public AddressService(IRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<string> CreateOrGetAddress(Address address)
        {
            var currentAddress =  this.addressRepository
                .All()
                .SingleOrDefault(x=>x.Street.Equals(address.Street) && x.Number.Equals(address.Number));

            if (currentAddress != null)
            {
                return currentAddress.Id;
            }

            string newAddressId = await this.CreateNewAddress(address);
            return newAddressId;
        }

        private async Task<string> CreateNewAddress(Address address)
        {
            await this.addressRepository.AddAsync(address);
            await this.addressRepository.SaveChangesAsync();

            return address.Id;
        }
    }
}
using Footballize.Models;

namespace Footballize.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Footballize.Data.Repositories;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;

        public AddressService(IDeletableEntityRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<string> CreateOrGetAddress(Address address)
        {
            if (address == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Address)));
            }

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
            if (address == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Address)));
            }


            await this.addressRepository.AddAsync(address);
            await this.addressRepository.SaveChangesAsync();

            return address.Id;
        }
    }
}
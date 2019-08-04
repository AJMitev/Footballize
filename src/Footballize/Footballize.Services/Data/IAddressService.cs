namespace Footballize.Services.Data
{
    using System.Threading.Tasks;
    using Models;

    public interface IAddressService
    {
        Task<string> CreateOrGetAddress(Address address);
    }
}
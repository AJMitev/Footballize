namespace Footballize.Services.Data
{
    using System.Threading.Tasks;
    using Models;

    public interface IAddressService : IService
    {
        Task<string> CreateOrGetAddress(Address address);
    }
}
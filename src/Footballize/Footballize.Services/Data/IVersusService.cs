namespace Footballize.Services.Data
{
    using System.Threading.Tasks;
    using Models;

    public interface IVersusService
    {
        Task AddVersusAsync(Versus versus);
    }
}
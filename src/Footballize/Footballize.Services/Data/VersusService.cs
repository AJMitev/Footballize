using System.Threading.Tasks;
using Footballize.Models;

namespace Footballize.Services.Data
{
    using System;
    using Exceptions;
    using Footballize.Data.Repositories;

    public class VersusService : IVersusService
    {
        private readonly IDeletableEntityRepository<Versus> versusRepository;

        public VersusService(IDeletableEntityRepository<Versus> versusRepository)
        {
            this.versusRepository = versusRepository;
        }

        public async Task AddVersusAsync(Versus versus)
        {
            if (versus == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

           await this.versusRepository.AddAsync(versus);
           await this.versusRepository.SaveChangesAsync();
        }
    }
}
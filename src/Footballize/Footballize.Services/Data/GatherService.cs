namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class GatherService : IGatherServices
    {
        private readonly IRepository<Gather> eventRepository;

        public GatherService(IRepository<Gather> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public ICollection<TViewModel> GetGathers<TViewModel>()
        {
            return this.eventRepository
                .All()
                .To<TViewModel>()
                .ToList();
        }
    }
}
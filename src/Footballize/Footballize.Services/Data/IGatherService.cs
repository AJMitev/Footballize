namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Models.Gather;

    public interface IGatherService : IService
    {
        IEnumerable<TViewModel> GetAll<TViewModel>();
        IEnumerable<TViewModel> GetAll<TViewModel>(Expression<Func<Gather, bool>> expression);
        TViewModel GetById<TViewModel>(string id);
        Task<GatherServiceModel> GetByIdAsync(string id);
        Task<string> AddAsync(string title, string description, DateTime startingAt, TeamFormat teamFormat, string pitchId);
        Task LeaveAsync(string gatherId, string userId);
        Task EnrollAsync(string gatherId, string userId);
        Task StartAsync(string id);
        Task CompleteAsync(string id);
        Task DeleteAsync(string id);
        int GetCount();
    }
}
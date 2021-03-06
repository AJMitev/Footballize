﻿namespace Footballize.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITownService : IService
    {
        Task<string> AddAsync(string name, string provinceId);
        TViewModel GetById<TViewModel>(string id);
        IEnumerable<TViewModel> GetByCountryId<TViewModel>(string countryId);
        Task DeleteAsync(string id);
        Task UpdateAsync(string townId, string name, string provinceId);
        bool Exists(string id);
        Task<string> GetProvinceId(string id);
    }
}
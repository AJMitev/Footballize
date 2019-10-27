﻿namespace Footballize.Services.Data
{
    using System.Threading.Tasks;
    using Models.Address;

    public interface IAddressService : IService
    {
        TViewModel GetById<TViewModel>(string id);
        AddressServiceModel GetByName(string street, int number);
        Task<string> Create(string street, int number, double latitude, double longitude);
        bool Exists(string street, int number);
    }
}
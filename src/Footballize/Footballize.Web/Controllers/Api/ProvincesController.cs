﻿namespace Footballize.Web.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Data.Repositories;
    using Footballize.Models;
    using Services.Data;
    using ViewModels.Provinces;

    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IProvinceServices provinceServices;

        public ProvincesController(IProvinceServices provinceServices)
        {
            this.provinceServices = provinceServices;
        }

        // GET: api/Provinces/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<ProvinceNameAndIdViewModel> Get(string id)
        {
            var provinces = this.provinceServices.GetProvincesByCountry<ProvinceNameAndIdViewModel>(id);

            return provinces;
        }
    }
}
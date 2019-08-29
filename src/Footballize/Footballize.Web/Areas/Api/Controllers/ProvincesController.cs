namespace Footballize.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using Administration.ViewModels.Provinces;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;

    public class ProvincesController : ApiController
    {
        private readonly IProvinceServices provinceServices;

        public ProvincesController(IProvinceServices provinceServices)
        {
            this.provinceServices = provinceServices;
        }

        // GET: api/Provinces/5
        [HttpGet("{id}")]
        public IEnumerable<ProvinceNameAndIdViewModel> Get(string id)
        {
            var provinces = this.provinceServices.GetProvincesByCountry<ProvinceNameAndIdViewModel>(id);

            return provinces;
        }
    }
}

namespace Footballize.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using Administration.ViewModels.Provinces;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;

    public class ProvincesController : ApiController
    {
        private readonly IProvinceService provinceService;

        public ProvincesController(IProvinceService provinceService)
        {
            this.provinceService = provinceService;
        }

        // GET: api/Provinces/5
        [HttpGet("{id}")]
        public IEnumerable<ProvinceNameAndIdViewModel> Get(string id)
        {
            var provinces = this.provinceService.GetAllByCountry<ProvinceNameAndIdViewModel>(id);

            return provinces;
        }
    }
}

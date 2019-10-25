namespace Footballize.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Services.Data;
    using ViewModels.Countries;
    using ViewModels.Provinces;

    public class ProvincesController : AdminController
    {
        private readonly IProvinceService provinceService;
        private readonly ICountryService countryService;
        private readonly IMapper mapper;

        public ProvincesController(IProvinceService provinceService, ICountryService countryService, IMapper mapper)
        {
            this.provinceService = provinceService;
            this.countryService = countryService;
            this.mapper = mapper;
        }

        
        [HttpGet]
        public IActionResult Add(string countryId)
        {
            var country = this.countryService.GetById<CountryNameAndIdViewModel>(countryId);

            if (country ==  null)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<ProvinceAddViewModel>(country);


            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProvinceAddInputModel model)
        {
            if (!ModelState.IsValid)
                return this.View();

            await this.provinceService.CreateProvinceAsync(this.mapper.Map<Province>(model));

            return this.RedirectToAction("Details", "Countries", new { id = model.CountryId});
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            var province = provinceService.GetProvince<ProvinceEditViewModel>(id);

            if (province == null)
            {
                return this.NotFound();
            }

            return this.View(province);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProvinceEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.NotFound();
            }

            await this.provinceService.UpdateProvinceAsync(this.mapper.Map<Province>(model));

            return this.RedirectToAction("Details", "Countries", new {id = model.CountryId});
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.provinceService.RemoveProvinceAsync(id);

            return this.RedirectToAction("Index","Countries");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var province = this.provinceService.GetProvince<ProvinceDetailsViewModel>(id);


            if (province == null)
            {
                return this.NotFound();
            }

            return this.View(province);
        }
    }
}

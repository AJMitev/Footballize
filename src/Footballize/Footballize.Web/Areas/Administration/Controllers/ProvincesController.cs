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
        private readonly IProvinceServices provinceServices;
        private readonly ICountryService countryService;

        public ProvincesController(IProvinceServices provinceServices, ICountryService countryService)
        {
            this.provinceServices = provinceServices;
            this.countryService = countryService;
        }

        
        [HttpGet]
        public IActionResult Add(string countryId)
        {
            var country = this.countryService.GetCountry<CountryNameAndIdViewModel>(countryId);

            if (country ==  null)
            {
                return this.NotFound();
            }

            var model = Mapper.Map<ProvinceAddViewModel>(country);


            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProvinceAddInputModel model)
        {
            if (!ModelState.IsValid)
                return this.View();

            await this.provinceServices.CreateProvinceAsync(Mapper.Map<Province>(model));

            return this.RedirectToAction("Details", "Countries", new { id = model.CountryId});
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            var province = provinceServices.GetProvince<ProvinceEditViewModel>(id);

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

            await this.provinceServices.UpdateProvinceAsync(Mapper.Map<Province>(model));

            return this.RedirectToAction("Details", "Countries", new {id = model.CountryId});
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.provinceServices.RemoveProvinceAsync(id);

            return this.RedirectToAction("Index","Countries");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var province = this.provinceServices.GetProvince<ProvinceDetailsViewModel>(id);


            if (province == null)
            {
                return this.NotFound();
            }

            return this.View(province);
        }
    }
}

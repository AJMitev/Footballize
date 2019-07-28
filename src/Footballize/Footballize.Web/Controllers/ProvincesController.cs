namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Footballize.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Data;
    using ViewModels.Countries;
    using ViewModels.Provinces;

    public class ProvincesController : Controller
    {
        private readonly IProvinceServices provinceServices;
        private readonly ICountryService countryService;

        public ProvincesController(IProvinceServices provinceServices, ICountryService countryService)
        {
            this.provinceServices = provinceServices;
            this.countryService = countryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var provinces = this.provinceServices.GetProvinces<ProvincesIndexViewModel>();

            return this.View(provinces);
        }

        [HttpGet]

        public IActionResult Add()
        {
            var countriesAvailable = this.countryService.GetCountries<CountryNameAndIdViewModel>();
            var model = new ProvinceAddViewModel
            {
                Countries = new SelectList(countriesAvailable, "Id", "Name")
            };


            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProvinceAddInputModel model)
        {
            if (!ModelState.IsValid)
                return this.View();

            await this.provinceServices.CreateProvince(Mapper.Map<Province>(model));

            return this.RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            var province = provinceServices.GetProvince<ProvinceEditViewModel>(id);
            var countriesAvailable = this.countryService.GetCountries<CountryNameAndIdViewModel>();
            province.Countries = new SelectList(countriesAvailable, "Id", "Name");

            return this.View(province);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProvinceEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.NotFound();
            }

            var province = this.provinceServices.GetProvince<Province>(model.Id);

            province.Name = model.Name;
            province.CountryId = model.CountryId;

            await this.provinceServices.UpdateProvince(province);

            return this.RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.provinceServices.RemoveProvince(id);

            return this.RedirectToAction("Index");
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

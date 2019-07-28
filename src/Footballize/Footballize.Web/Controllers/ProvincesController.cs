namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
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
                Countries = new SelectList(countriesAvailable,"Id", "Name")
            };


            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProvinceInputModel model)
        {
            if (!ModelState.IsValid)
              return this.View();

            var newProvince = new Province
            {
                Name = model.Name,
                CountryId = model.SelectedCountryId
            };

            await this.provinceServices.CreateProvince(newProvince);

           return this.RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            return this.View();
        }
    }
}

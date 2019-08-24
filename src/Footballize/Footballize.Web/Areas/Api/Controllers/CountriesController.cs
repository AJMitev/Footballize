namespace Footballize.Web.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using ViewModels.Countries;

    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        // GET: api/Countries
        [HttpGet]
        public IEnumerable<CountryNameAndIdViewModel> Get()
        {
            return this.countryService.GetCountries<CountryNameAndIdViewModel>();
        }
    }
}

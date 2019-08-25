namespace Footballize.Web.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using ViewModels.Countries;

   
    public class CountriesController : ApiController
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

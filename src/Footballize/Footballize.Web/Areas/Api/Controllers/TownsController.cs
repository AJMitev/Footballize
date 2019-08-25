namespace Footballize.Web.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Towns;
    using Services.Data;

   public class TownsController : ApiController
    {
        private readonly ITownService townService;

        public TownsController(ITownService townService)
        {
            this.townService = townService;
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        public IEnumerable<TownWithProvinceViewModel> Get(string id)
        {
            return this.townService.GetTownsByCountry<TownWithProvinceViewModel>(id);
        }
    }
}

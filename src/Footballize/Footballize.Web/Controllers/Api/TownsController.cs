namespace Footballize.Web.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Towns;
    using Services.Data;

    [Route("api/[controller]")]
    [ApiController]
    public class TownsController : ControllerBase
    {
        private readonly ITownService townService;

        public TownsController(ITownService townService)
        {
            this.townService = townService;
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        public IEnumerable<TownNameAndIdViewModel> Get(string id)
        {
            return this.townService.GetTownsByProvince<TownNameAndIdViewModel>(id);
        }
    }
}

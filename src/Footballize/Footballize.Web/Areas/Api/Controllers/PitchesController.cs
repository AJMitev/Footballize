namespace Footballize.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using Administration.ViewModels.Pitches;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;

    public class PitchesController : ApiController
    {
        private readonly IPitchService pitchService;

        public PitchesController(IPitchService pitchService)
        {
            this.pitchService = pitchService;
        }

        // GET: api/Pitches/5
        [HttpGet("{id}")]
        public IEnumerable<PitchNameAndIdViewModel> Get(string id)
        {
            return this.pitchService.GetByTownId<PitchNameAndIdViewModel>(id);
        }
    }
}

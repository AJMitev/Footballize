namespace Footballize.Web.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using ViewModels.Pitches;

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
            return this.pitchService.GetPitchesByTownId<PitchNameAndIdViewModel>(id);
        }
    }
}

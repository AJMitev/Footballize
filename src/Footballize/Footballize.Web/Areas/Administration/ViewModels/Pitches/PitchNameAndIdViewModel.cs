﻿namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using Models;
    using Services.Mapping;

    public class PitchNameAndIdViewModel : IMapFrom<Pitch>, IMapTo<Pitch>
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
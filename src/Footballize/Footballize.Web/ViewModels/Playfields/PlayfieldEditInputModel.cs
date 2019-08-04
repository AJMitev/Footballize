﻿namespace Footballize.Web.ViewModels.Playfields
{
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Services.Mapping;

    public class PlayfieldEditInputModel : IMapTo<Playfield>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AddressId { get; set; }
    }
}
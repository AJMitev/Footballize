namespace Footballize.Services.Tests.TestViewModels
{
    using System;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Mapping;

    public class GatherTestViewModel : IMapFrom<Gather>
    {
        public string Id { get; set; }
        public GameStatus Status { get; set; }
        public DateTime StartingAt { get; set; }
    }
}
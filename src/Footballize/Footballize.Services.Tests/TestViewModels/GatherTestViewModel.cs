namespace Footballize.Services.Tests.TestViewModels
{
    using System;
    using Mapping;
    using Models;
    using Models.Enums;

    public class GatherTestViewModel : IMapFrom<Gather>
    {
        public string Id { get; set; }
        public GameStatus Status { get; set; }
        public DateTime StartingAt { get; set; }
    }
}
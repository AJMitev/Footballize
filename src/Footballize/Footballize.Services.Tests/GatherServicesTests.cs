namespace Footballize.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Moq;
    using Xunit;

    public class GatherServicesTests
    {

        [Fact]
        public void GetCountShouldReturnFive()
        {

            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.All())
                .Returns(GetAllGather().AsQueryable());

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepository = new Mock<IDeletableEntityRepository<User>>();

            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepository.Object);

            //Act

            var expected = 5;
            var result = service.GetCount();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void StartGameShouldSetStatusToStarted()
        {
            //Arrange

            var game = new Gather
            {
                Status = GameStatus.Registration
            };

            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(game));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepository = new Mock<IDeletableEntityRepository<User>>();

            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepository.Object);

            //Act
            service.StartAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            //Assert
            Assert.Equal(GameStatus.Started, game.Status);
        }


        [Fact]
        public void CompleteShouldChangeGameStatus()
        {
            //Arrange
            var gather = new Gather
            {
                Status = GameStatus.Started
            };
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<Gather>(gather));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepository = new Mock<IDeletableEntityRepository<User>>();

            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepository.Object);

            //Act
            service.CompleteAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            //Assert
            Assert.Equal(GameStatus.Finished, gather.Status);
        }

        private IEnumerable<Gather> GetAllGather()
        {
            var gathers = new List<Gather>
            {
                new Gather
                {
                    Id = "1",
                    StartingAt = DateTime.Now.AddMinutes(15),
                    Status = GameStatus.Registration
                },
                new Gather
                {
                    Id = "2",
                    StartingAt = DateTime.Now.AddMinutes(18),
                    Status = GameStatus.Registration
                },
                new Gather
                {
                    Id = "3",
                    StartingAt = DateTime.Now.AddMinutes(35),
                    Status = GameStatus.Registration
                },
                new Gather
                {
                    Id = "4",
                    StartingAt = DateTime.Now.AddMinutes(-15),
                    Status = GameStatus.Registration
                },
                new Gather
                {
                    Id = "5",
                    StartingAt = DateTime.Now.AddMinutes(-15),
                    Status = GameStatus.Registration
                },
            };


            return gathers;
        }
    }
}
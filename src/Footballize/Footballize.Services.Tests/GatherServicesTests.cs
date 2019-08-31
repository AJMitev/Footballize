namespace Footballize.Services.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Footballize.Services.Data;
    using Models;
    using Models.Enums;
    using Moq;
    using Xunit;

    public class GatherServicesTests
    {


        [Fact]
        public void AddGatherShouldThrowExceptionIfObjectIsNull()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);


            //Assert

            Assert.Throws<ServiceException>(() => service.AddGatherAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void LeavingGatherThatDosentExistShouldThrowException()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Assert

            Assert.Throws<ServiceException>(() => service.LeaveGatherAsync(null, Guid.NewGuid().ToString()).GetAwaiter().GetResult());
        }


        [Theory]
        [InlineData(GameStatus.Started)]
        [InlineData(GameStatus.Finished)]
        public void LeavingStartedOrFinishedGatherShouldThrowsException(GameStatus status)
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            // Act

            var gather = new Gather
            {
                Status = status
            };

            // Assert
            Assert.Throws<ServiceException>(() => service.LeaveGatherAsync(gather, Guid.NewGuid().ToString()).GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(GameStatus.Started)]
        [InlineData(GameStatus.Finished)]
        public void JoinStartedOrFinishedGatherShouldThrowException(GameStatus status)
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);


            var gather = new Gather
            {
                Status = status
            };

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "johnny"
            };

            // Assert
            Assert.Throws<ServiceException>(() => service.EnrollGatherAsync(gather, user).GetAwaiter().GetResult());
        }

        [Fact]
        public void GetCountShouldReturnFive()
        {

            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.All()).Returns(GetAllGather().AsQueryable());

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Act

            var expected = 5;
            var result = service.GetGatherCount();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetGatherShouldReturnRightOne()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.All()).Returns(GetAllGather().AsQueryable());

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Act

            var expected = GetAllGather().SingleOrDefault(x => x.Id == "5");
            var actual = service.GetGatherWithPlayers("5");

            //Assert
            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public void JoinGameThatDosentExistShouldThrowException()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.All()).Returns(GetAllGather().AsQueryable());

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Act

            Assert.Throws<ServiceException>(() => service.EnrollGatherAsync(null, null).GetAwaiter().GetResult());
        }

        [Fact]
        public void JoinGameWhenUserIsBannedShouldThrowException()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.All()).Returns(GetAllGather().AsQueryable());

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            var game = new Gather();
            var user = new User{IsBanned = true};

            //Act, Assert
            Assert.Throws<ServiceException>(() => service.EnrollGatherAsync(game, user).GetAwaiter().GetResult());
        }

        [Fact]
        public void JoinGameThatIsAlreadyJoinedShouldThrowException()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.All()).Returns(GetAllGather().AsQueryable());

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Act
            var user = new User {Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f"};
            var game = new Gather
            {
                Players = new List<GatherUser>
                {
                    new GatherUser
                    {
                        UserId = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f"
                    }
                }
            };

            //Assert
            Assert.Throws<ServiceException>(() => service.EnrollGatherAsync(game, user).GetAwaiter().GetResult());
        }

        [Fact]
        public void StartGatherShouldThrowWhenGameIdIsInvalid()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Gather>(null));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Act
            Assert.Throws<ServiceException>(() => service.StartGatherAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());

        }

        [Fact]
        public void StartGameShouldThrowWhenNotEnoughtPlayersAreJoined()
        {
            //Arrange
            var gather = new Gather
            {
                MaximumPlayers = 12,
                Players = new List<GatherUser>
                {
                    new GatherUser(),
                    new GatherUser(),
                    new GatherUser(),
                }
            };
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Gather>(gather));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);


            Assert.Throws<ServiceException>(() => service.StartGatherAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(GameStatus.Pending)]
        [InlineData(GameStatus.Finished)]
        [InlineData(GameStatus.Started)]
        public void StartGameShouldThrowWhenGameIsAlreadyStarted(GameStatus stats)
        {
            //Arrange
            var gather = new Gather
            {
                Status = stats
            };
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Gather>(gather));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);


            Assert.Throws<ServiceException>(() => service.StartGatherAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
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
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Act
            service.StartGatherAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            //Assert
            Assert.Equal(GameStatus.Started, game.Status);
        }

        [Fact]
        public void CompleteGameShouldThrowsExceptionIfGameIsNotFound()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Gather>(null));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Act
            Assert.Throws<ServiceException>(() =>
                service.CompleteGatherAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(GameStatus.Finished)]
        [InlineData(GameStatus.Registration)]
        [InlineData(GameStatus.Pending)]
        public void CompleteShouldThrowWhenStatusIsDifferentFromStarted(GameStatus status)
        {
            //Arrange
            var gather = new Gather
            {
                Status = status
            };
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Gather>(gather));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);


            Assert.Throws<ServiceException>(() => service.CompleteGatherAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());

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
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Gather>(gather));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);

            //Act
            service.CompleteGatherAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            //Assert
            Assert.Equal(GameStatus.Finished,gather.Status);
        }

        [Fact]
        public void DeleteGatherShouldThrowIfTheGameIsNotfound()
        {
            //Arrange
            var gatherRepositoryMoq = new Mock<IDeletableEntityRepository<Gather>>();
            gatherRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Gather>(null));

            var gatherUserRepositoryMoq = new Mock<IDeletableEntityRepository<GatherUser>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var service = new GatherService(gatherRepositoryMoq.Object, gatherUserRepositoryMoq.Object, userRepositoryMoq.Object);


            Assert.Throws<ServiceException>(() => service.DeleteGatherAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());

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
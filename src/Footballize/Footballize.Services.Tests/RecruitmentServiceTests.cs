namespace Footballize.Services.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Models;
    using Models.Enums;
    using Moq;
    using Xunit;

    public class RecruitmentServiceTests
    {
        [Fact]
        public void AddRecruitmentShouldThrowsIfParameterIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            Assert.Throws<ServiceException>(() => service.AddRecruitmentAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void AddRecruitmentInvokesRepositoryOnce()
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            var entity = new Recruitment();

            service.AddRecruitmentAsync(entity).GetAwaiter().GetResult();

            repo.Verify(x => x.AddAsync(entity), Times.Once);
        }

        [Fact]
        public void LeaveRecruitmentThrowIfUserIdIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            var entity = new Recruitment();

            Assert.Throws<ServiceException>(() => service.LeaveRecruitmentAsync(entity, null).GetAwaiter().GetResult());
        }

        [Fact]
        public void LeaveRecruitmentThrowsIfRecruitmentIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            var entity = new Recruitment();

            Assert.Throws<ServiceException>(() => service.LeaveRecruitmentAsync(null, "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(GameStatus.Finished)]
        [InlineData(GameStatus.Pending)]
        [InlineData(GameStatus.Started)]
        public void LeaveRecruitmentShouldThrowsIfGameStatusIsNotRegister(GameStatus status)
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            var entity = new Recruitment
            {
                Status = status
            };


            Assert.Throws<ServiceException>(() =>
                service.LeaveRecruitmentAsync(entity, "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        
        [Fact]
        public void EnrollRecruitmentShouldThrowIfEntityIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            var user = new User();

            Assert.Throws<ServiceException>(() => service.EnrollRecruitmentAsync(null, user).GetAwaiter().GetResult());
        }


        [Theory]
        [InlineData(GameStatus.Finished)]
        [InlineData(GameStatus.Started)]
        [InlineData(GameStatus.Pending)]
        public void StartRecruitmentShouldThrowIfStatusIsInvalid(GameStatus gameStatus)
        {
            var game = new Recruitment { Status = gameStatus };
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            Assert.Throws<ServiceException>(() =>
                service.StartRecruitmentAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Fact]
        public void StartRecruitmentShouldThrowIfNotAllSlotsAreCompleted()
        {
            var game = new Recruitment
            {
                MaximumPlayers = 2,
                Players = new List<RecruitmentUser>
                {
                    new RecruitmentUser()
                }
            };

            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            Assert.Throws<ServiceException>(() =>
                service.StartRecruitmentAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Fact]
        public void StartRecruitmentShouldThrowsIfRecruitmentIsNotFound()
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Recruitment>(null));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            Assert.Throws<ServiceException>(() =>
                service.StartRecruitmentAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Fact]
        public void StartRecuitmentShouldChangeGameStatusToStarted()
        {
            var game = new Recruitment { Status = GameStatus.Registration };
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            service.StartRecruitmentAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            Assert.Equal(GameStatus.Started, game.Status);
        }

        [Theory]
        [InlineData(GameStatus.Finished)]
        [InlineData(GameStatus.Registration)]
        [InlineData(GameStatus.Pending)]
        public void CompleteRecruitmentShouldThrowIfGameStatusIsInvalid(GameStatus status)
        {
            var game = new Recruitment { Status = status };
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            Assert.Throws<ServiceException>(() =>
                service.CompleteRecruitmentAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Fact]
        public void CompleteRecruitmentShouldThrowIfGameIsNotFound()
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Recruitment>(null));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            Assert.Throws<ServiceException>(() =>
                service.CompleteRecruitmentAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Fact]
        public void CompleteRecuitmentShouldChangeGameStatusToFinished()
        {
            var game = new Recruitment { Status = GameStatus.Started };
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            service.CompleteRecruitmentAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            Assert.Equal(GameStatus.Finished, game.Status);
        }

        [Fact]
        public void DeleteRecruitmentThrowsIfGameWasNotFound()
        {
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Recruitment>(null));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            Assert.Throws<ServiceException>(() =>
                service.DeleteRecruitmentAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Fact]
        public void GetRecuitmentShouldReturnValidEntity()
        {
            var game = new Recruitment { Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f" };
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object, userRepo.Object);

            var expected = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f";
            var actual = service.GetRecruitmentAsync(expected).GetAwaiter().GetResult().Id;

            Assert.Equal(expected, actual);
        }
    }
}
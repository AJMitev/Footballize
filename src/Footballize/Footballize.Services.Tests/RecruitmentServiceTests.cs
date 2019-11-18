namespace Footballize.Services.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Moq;
    using Xunit;

    public class RecruitmentServiceTests
    {


        [Fact]
        public void StartRecuitmentShouldChangeGameStatusToStarted()
        {
            var game = new Recruitment { Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f", Status = GameStatus.Registration };
            var gameList = new List<Recruitment> { game };
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.All()).Returns(gameList.AsQueryable());
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object,userRepo.Object);

            service.StartAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            Assert.Equal(GameStatus.Started, game.Status);
        }

      
        [Fact]
        public void CompleteRecuitmentShouldChangeGameStatusToFinished()
        {
            var game = new Recruitment { Status = GameStatus.Started };
            var repo = new Mock<IDeletableEntityRepository<Recruitment>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(game));
            var userRecruitRepo = new Mock<IDeletableEntityRepository<RecruitmentUser>>();
            var userRepo = new Mock<IDeletableEntityRepository<User>>();
            var service = new RecruitmentService(repo.Object, userRecruitRepo.Object,userRepo.Object);

            service.CompleteAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            Assert.Equal(GameStatus.Finished, game.Status);
        }
       
    }
}
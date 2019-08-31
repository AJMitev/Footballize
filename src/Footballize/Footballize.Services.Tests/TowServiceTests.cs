namespace Footballize.Services.Tests
{
    using System.Threading.Tasks;
    using Data;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Models;
    using Moq;
    using Xunit;

    public class TowServiceTests
    {
        [Fact]
        public void AddTownShouldThrowIfParameterIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Town>>().Object;
            var service = new TownService(repo);

            Assert.Throws<ServiceException>(() => service.AddTownAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void AddTownShouldInvokeRepositoryMethodOnce()
        {
            var town = new Town();
            var repo = new Mock<IDeletableEntityRepository<Town>>();
            var service = new TownService(repo.Object);

            service.AddTownAsync(town).GetAwaiter().GetResult();

            repo.Verify(x=>x.AddAsync(town), Times.Once);
        }

        [Fact]
        public void DeleteTownShouldThrowIfNotFound()
        {
            var repo = new Mock<IDeletableEntityRepository<Town>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Town>(null));
            var service = new TownService(repo.Object);

            Assert.Throws<ServiceException>(() => service.DeleteTownAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());
        }

        [Fact]
        public void UpdateTownShouldThrowIfNotFound()
        {
            var repo = new Mock<IDeletableEntityRepository<Town>>();
            var service = new TownService(repo.Object);

            Assert.Throws<ServiceException>(() => service.UpdateTownAsync(null).GetAwaiter().GetResult());
        }
    }
}
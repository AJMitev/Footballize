namespace Footballize.Services.Tests
{
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Moq;
    using Xunit;

    public class CountryServicesTests
    {
        [Fact]
        public void DeleteCountryShouldInvokeDeleteRepositoryMethodOnce()
        {
            //Arrange
            var country = new Country { Name = "Bulgaria" };
            var repo = new Mock<IDeletableEntityRepository<Country>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(country));
            var service = new CountryService(repo.Object);

            //Act
            service.DeleteAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            //Assert
            repo.Verify(x => x.Delete(country), Times.Once);
        }
    }
}
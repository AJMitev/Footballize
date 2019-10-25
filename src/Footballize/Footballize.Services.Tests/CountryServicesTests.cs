namespace Footballize.Services.Tests
{
    using System.Threading.Tasks;
    using Data;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Models;
    using Moq;
    using Xunit;

    public class CountryServicesTests
    {
        [Fact]
        public void AddMethodShouldThrowIfParameterIsNull()
        {
            //Arrange
            var repo = new Mock<IDeletableEntityRepository<Country>>();
            var service = new CountryService(repo.Object);

            //Assert
            Assert.Throws<ServiceException>(() => service.AddAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void UpdateCountryShouldThrowIfParameterIsNull()
        {
            //Arrange
            var repo = new Mock<IDeletableEntityRepository<Country>>();
            var service = new CountryService(repo.Object);

            //Assert
            Assert.Throws<ServiceException>(() => service.UpdateAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void UpdateCountryShouldInvokeRepositoryUpdateOnce()
        {
            //Arrange
            var repo = new Mock<IDeletableEntityRepository<Country>>();
            var service = new CountryService(repo.Object);

            //Act
            var country = new Country { Name = "Bulgaria" };

            service.UpdateAsync(country).GetAwaiter().GetResult();

            //Assert
            repo.Verify(x=>x.Update(country),Times.Once);
        }

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
            repo.Verify(x=>x.Delete(country),Times.Once);
        }

        

    }
}
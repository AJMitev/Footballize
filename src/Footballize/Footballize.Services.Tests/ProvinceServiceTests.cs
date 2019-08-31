namespace Footballize.Services.Tests
{
    using System.Threading.Tasks;
    using Data;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Models;
    using Moq;
    using Xunit;

    public class ProvinceServiceTests
    {
        [Fact]
        public void CreateProvinceThrowsIfModelIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Province>>();
            var service = new ProvinceService(repo.Object);

            Assert.Throws<ServiceException>(() => service.CreateProvinceAsync(null).GetAwaiter().GetResult());
        }


        [Fact]
        public void CreateProvinceInvokeServiceOnce()
        {
            var repo = new Mock<IDeletableEntityRepository<Province>>();
            var service = new ProvinceService(repo.Object);

            var province = new Province
            {
                Name = "Province"
            };

            service.CreateProvinceAsync(province).GetAwaiter().GetResult();

            repo.Verify(x => x.AddAsync(province), Times.Once);
        }


        [Fact]
        public void RemoveProvinceShouldDeleteEntity()
        {
            var province = new Province
            {
                Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f",
                Name = "Province"
            };

            var repo = new Mock<IDeletableEntityRepository<Province>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(province));
            var service = new ProvinceService(repo.Object);


            service.RemoveProvinceAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            repo.Verify(x => x.Delete(province), Times.Once);
        }

        [Fact]
        public void RemoveProvinceShouldThrowIfEntityNotFound()
        {
            var repo = new Mock<IDeletableEntityRepository<Province>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Province>(null));
            var service = new ProvinceService(repo.Object);


            Assert.Throws<ServiceException>(() => service.RemoveProvinceAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult());

        }

        [Fact]
        public void UpdateProvinceShouldThrowIfEntityIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Province>>();
            repo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Province>(null));
            var service = new ProvinceService(repo.Object);

            Assert.Throws<ServiceException>(() => service.UpdateProvinceAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void UpdateProvinceShouldInvokeRepositoryOnce()
        {
            var province = new Province
            {
                Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f",
                Name = "Province"
            };

            var repo = new Mock<IDeletableEntityRepository<Province>>();
            var service = new ProvinceService(repo.Object);

            service.UpdateProvinceAsync(province).GetAwaiter().GetResult();

            repo.Verify(x => x.Update(province), Times.Once);
        }
    }
}
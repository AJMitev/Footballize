namespace Footballize.Services.Tests
{
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Moq;
    using Xunit;

    public class ProvinceServiceTests
    {
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


            service.RemoveAsync("70400fb3-aed2-4876-aa9a-bcf8ba49ca9f").GetAwaiter().GetResult();

            repo.Verify(x => x.Delete(province), Times.Once);

        }
    }
}
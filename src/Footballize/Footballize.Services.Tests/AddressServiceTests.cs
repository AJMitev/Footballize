namespace Footballize.Services.Tests
{
    using Data;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Models;
    using Moq;
    using Xunit;

    public class AddressServiceTests
    {
        [Fact]
        public void CreateNewAddressShouldThrowIfPassedParameterIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Address>>();
            var service = new AddressService(repo.Object);

            Assert.Throws<ServiceException>(() => service.CreateOrGetAddress(null).GetAwaiter().GetResult());
        }
        
    }
}
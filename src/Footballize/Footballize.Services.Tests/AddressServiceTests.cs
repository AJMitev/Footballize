namespace Footballize.Services.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Moq;
    using Xunit;

    public class AddressServiceTests
    {
        [Fact]
        public void AddAsyncShouldReturnCreatedAddressId()
        {
            var repo = new Mock<IDeletableEntityRepository<Address>>();
            var service = new AddressService(repo.Object);

            var id = service.AddAsync("Test", 1, 25.14, 12.12)
                .GetAwaiter()
                .GetResult();

            Assert.NotNull(id);
        }

        [Fact]
        public void ExistsShouldReturnTrueIfRecordExists()
        {
            var addresses = new List<Address>
            {
                new Address
                {
                    Id = "5fb7a413-2d93-4d0d-b6d5-160c03fc19c1",
                    Street = "Vasil Levski blvd.",
                    Number = 37
                }
            };

            var repo = new Mock<IDeletableEntityRepository<Address>>();

            repo.Setup(x => x.All())
                .Returns(addresses.AsQueryable());

            var service = new AddressService(repo.Object);

            Assert.True(service.Exists("5fb7a413-2d93-4d0d-b6d5-160c03fc19c1"));
        }

        [Fact]
        public void ExistsShouldReturnFalseIfRecordDidNotExists()
        {
            var repo = new Mock<IDeletableEntityRepository<Address>>();
            repo.Setup(x => x.All())
                .Returns(new List<Address>().AsQueryable());

            var service = new AddressService(repo.Object);

            Assert.False(service.Exists("5fb7a413-2d93-4d0d-b6d5-160c03fc19c1"));
        }
    }
}
namespace Footballize.Services.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Moq;
    using Xunit;


    public class UserServicesTests
    {
        [Fact]
        public void CountUsersShouldReturnFour()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            userRepositoryMoq.Setup(x => x.All()).Returns(GetUsers().AsQueryable());
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            // Act
            var expected = 4;
            var actual = service.GetUsersCount();

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetUsersShouldReturnFourUsers()
        {

            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            userRepositoryMoq.Setup(x => x.All()).Returns(GetUsers().AsQueryable());
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            var expected = 4;
            var actual = service.GetUsersCount();


            Assert.Equal(expected, actual);
        }

        private ICollection<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = "2acd1bdb-ccaa-4dac-b872-082e63aa79fd",
                    IsBanned = false,
                    UserName = "James"
                },
                new User
                {
                    Id = "5862f54f-f1a2-49d1-b453-daf7ffd97b5c",
                },
                new User
                {
                    Id = "f7d797ca-b9e5-4caa-94cb-aa8f56e3d4f2",
                },
                new User
                {
                    Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f",
                }
            };
        }
    }
}
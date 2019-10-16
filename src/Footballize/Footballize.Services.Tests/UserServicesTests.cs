namespace Footballize.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Threading.Tasks;
    using Data;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;
    using Moq;
    using TestViewModels;
    using Xunit;


    public class UserServicesTests
    {

        [Fact]
        public void AddSameUserAsPlaypalShouldThrowException()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            //Act
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Adolf"
            };

            // Assert
            Assert.Throws<ServiceException>(() => service.AddPlaypal(user, user).GetAwaiter().GetResult());
        }

        [Fact]
        public void AddPlaypalShouldThrowIfOneOfParametersAreNull()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            //Act
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Adolf"
            };

            //Assert
            Assert.Throws<ServiceException>(() => service.AddPlaypal(user, null).GetAwaiter().GetResult());
            Assert.Throws<ServiceException>(() => service.AddPlaypal(null, user).GetAwaiter().GetResult());
        }

        [Fact]
        public void RemoveSameUserAsPlaypalShouldThrowException()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            //Act
            var id = Guid.NewGuid().ToString();

            // Assert
            Assert.Throws<ServiceException>(() => service.RemovePlaypal(id, id).GetAwaiter().GetResult());
        }

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
        public void BanPlayerShouldSetIsBannedToTrue()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            //Act
            var player = new User
            {
                Id = "2acd1bdb-ccaa-4dac-b872-082e63aa79fd",
                IsBanned = false
            };

            service.BanPlayer(player, 15).GetAwaiter().GetResult();

            //Assert
            Assert.True(player.IsBanned);
        }

        [Fact]
        public void BanPlayerShouldThrowExceptionIfPlayerIsNull()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            //Assert
            Assert.Throws<ServiceException>(() => service.BanPlayer(null, 15).GetAwaiter().GetResult());
        }

        [Fact]
        public void RemoveBanMethodShouldSetIsBannedToFalse()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            //Act
            var player = new User
            {
                Id = "2acd1bdb-ccaa-4dac-b872-082e63aa79fd",
                IsBanned = true
            };

            service.RemoveBan(player).GetAwaiter().GetResult();

            //Assert
            Assert.False(player.IsBanned);
        }

        [Fact]
        public void RemoveBanMethodShouldThrowExceptionIfUserIsNull()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            //Assert
            Assert.Throws<ServiceException>(() => service.RemoveBan(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void GetUserAsyncShoudReturnUser()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            userRepositoryMoq.Setup(x => x.GetByIdAsync("2acd1bdb-ccaa-4dac-b872-082e63aa79fd")).Returns(GetUser("2acd1bdb-ccaa-4dac-b872-082e63aa79fd"));
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            var expected = "James";
            var actuial = service.GetUserAsync("2acd1bdb-ccaa-4dac-b872-082e63aa79fd").GetAwaiter().GetResult().UserName;

            Assert.Equal(expected, actuial);
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
            var actual = service.GetUsers<UserTestViewModel>().Count;


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateReportShouldThrowIfReportIsNull()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            Assert.Throws<ServiceException>(() => service.CreateReport(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void GetUserReportsShouldReturnFourRecords()
        {
            //Arrange
            var playpalRepositoryMoq = new Mock<IDeletableEntityRepository<Playpal>>();
            var userRepositoryMoq = new Mock<IDeletableEntityRepository<User>>();
            var userReportsRepositoryMoq = new Mock<IDeletableEntityRepository<UserReport>>();
            userReportsRepositoryMoq.Setup(x => x.All()).Returns(GetReports().AsQueryable());
            var service = new UserService(userRepositoryMoq.Object, playpalRepositoryMoq.Object, userReportsRepositoryMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(UserServicesTests).GetTypeInfo().Assembly);

            var expected = 4;
            var actual = service.GetUserReports<UserReportTestViewModel>().Count;

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

        private Task<User> GetUser(string id)
        {
            var user = this.GetUsers().SingleOrDefault(x => x.Id == id);

            return Task.FromResult(user);
        }

        private ICollection<UserReport> GetReports()
        {
            return new List<UserReport>
            {
                new UserReport {Text = "Report 1"},
                new UserReport {Text = "Report 2"},
                new UserReport {Text = "Report 3"},
                new UserReport {Text = "Report 4"}
            };
        }
    }
}
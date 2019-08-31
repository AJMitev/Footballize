namespace Footballize.Services.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Data;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;
    using Moq;
    using TestViewModels;
    using Xunit;

    public class PitchServiceTests
    {
        [Fact]
        public void AddPitchShouldThrowIfParameterIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Pitch>>();
            var gatherRepo = new Mock<IRepository<Gather>>();
            var recruitmentRepo = new Mock<IRepository<Recruitment>>();
            var service = new PitchService(repo.Object, gatherRepo.Object, recruitmentRepo.Object);

            Assert.Throws<ServiceException>(() => service.AddPitchAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void RemovePitchShouldThrowIfParameterIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Pitch>>();
            var gatherRepo = new Mock<IRepository<Gather>>();
            var recruitmentRepo = new Mock<IRepository<Recruitment>>();
            var service = new PitchService(repo.Object, gatherRepo.Object, recruitmentRepo.Object);

            Assert.Throws<ServiceException>(() => service.RemovePitchAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void RemovePitchShouldInvokeRepositoryOnce()
        {
            var repo = new Mock<IDeletableEntityRepository<Pitch>>();
            var gatherRepo = new Mock<IRepository<Gather>>();
            var recruitmentRepo = new Mock<IRepository<Recruitment>>();
            var service = new PitchService(repo.Object, gatherRepo.Object, recruitmentRepo.Object);

            var pitch = new Pitch
            {
                Name = "Test 1",
                Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f"
            };


            service.RemovePitchAsync(pitch).GetAwaiter().GetResult();

            repo.Verify(x=>x.Delete(pitch), Times.Once);
        }

        [Fact]
        public void UpdatePitchShouldThrowIfParameterIsNull()
        {
            var repo = new Mock<IDeletableEntityRepository<Pitch>>();
            var gatherRepo = new Mock<IRepository<Gather>>();
            var recruitmentRepo = new Mock<IRepository<Recruitment>>();
            var service = new PitchService(repo.Object, gatherRepo.Object, recruitmentRepo.Object);

            Assert.Throws<ServiceException>(() => service.UpdatePitchAsync(null).GetAwaiter().GetResult());
        }

        [Fact]
        public void UpdatePitchShouldInvokeRepositoryOnce()
        {
            var repo = new Mock<IDeletableEntityRepository<Pitch>>();
            var gatherRepo = new Mock<IRepository<Gather>>();
            var recruitmentRepo = new Mock<IRepository<Recruitment>>();
            var service = new PitchService(repo.Object, gatherRepo.Object, recruitmentRepo.Object);

            var pitch = new Pitch
            {
                Name = "Test 1",
                Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f"
            };


            service.UpdatePitchAsync(pitch).GetAwaiter().GetResult();

            repo.Verify(x=>x.Update(pitch), Times.Once);
        }


        private ICollection<Pitch> GetPitches()
        {
            return new List<Pitch>
            {
                new Pitch
                {
                    Name = "Test 1",
                    Id = "70400fb3-aed2-4876-aa9a-bcf8ba49ca9f"
                },new Pitch
                {
                    Name = "Test 2"
                },new Pitch
                {
                    Name = "Test 3"
                },new Pitch
                {
                    Name = "Test 4"
                },new Pitch
                {
                    Name = "Test 5"
                },
            };
        }
    }
}
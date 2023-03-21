using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class SalesPointServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Return_Collection()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            var expected = 3;

            //Act
            var result = await service.GetAllAsync();

            //Assert
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public async Task GetAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            int salesPointId = 1;
            var expected = 1;

            //Act
            var result = await service.GetAsync(salesPointId);

            //Assert
            Assert.Equal(expected, result.Id);
        }

        [Fact]
        public async Task GetAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            int salesPointId = 9999;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(salesPointId));
        }

        [Fact]
        public async Task InsertAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            var salesPoint = new SalesPoint
            {
                Id = 3546,
                Name = "StoreTest"
            };
            var expected = "StoreTest";

            //Act
            var result = await service.InsertAsync(salesPoint);

            //Assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task InsertAsync_Null_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.InsertAsync(null));
        }

        [Fact]
        public async Task UpdateAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            var salesPoint = new SalesPoint
            {
                Id = 1,
                Name = "StoreTest"
            };
            var expected = "StoreTest";

            //Act
            var result = await service.UpdateAsync(salesPoint);

            //Assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task UpdateAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            var salesPoint = new SalesPoint
            {
                Id = 1234,
                Name = "StoreTest"
            };
            var expected = "StoreTest";

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(salesPoint));
        }

        [Fact]
        public async Task DeleteAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            int salesPointId = 1;

            //Act
            //Assert
            await service.DeleteAsync(salesPointId);
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(salesPointId));
        }

        [Fact]
        public async Task DeleteAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesPointService(context);

            int salesPointId = 9999;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(salesPointId));
        }
    }
}
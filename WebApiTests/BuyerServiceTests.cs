using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class BuyerServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Return_Collection()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new BuyerService(context);

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
            var service = new BuyerService(context);

            int buyerId = 1;
            var expected = 1;

            //Act
            var result = await service.GetAsync(buyerId);

            //Assert
            Assert.Equal(expected, result.Id);
        }

        [Fact]
        public async Task GetAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new BuyerService(context);

            int buyerId = 9999;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(buyerId));
        }

        [Fact]
        public async Task InsertAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new BuyerService(context);

            var buyer = new Buyer
            {
                Id = 3546,
                Name = "John"
            };
            var expected = "John";

            //Act
            var result = await service.InsertAsync(buyer);

            //Assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task InsertAsync_Null_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new BuyerService(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.InsertAsync(null));
        }

        [Fact]
        public async Task UpdateAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new BuyerService(context);

            var buyer = new Buyer
            {
                Id = 1,
                Name = "John"
            };
            var expected = "John";

            //Act
            var result = await service.UpdateAsync(buyer);

            //Assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task UpdateAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new BuyerService(context);

            var buyer = new Buyer
            {
                Id = 1234,
                Name = "John"
            };
            var expected = "John";

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(buyer));
        }

        [Fact]
        public async Task DeleteAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new BuyerService(context);

            int buyerId = 1;

            //Act
            //Assert
            await service.DeleteAsync(buyerId);
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(buyerId));
        }

        [Fact]
        public async Task DeleteAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new BuyerService(context);

            int buyerId = 9999;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(buyerId));
        }
    }
}
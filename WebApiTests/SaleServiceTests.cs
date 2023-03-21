using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class SaleServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Return_Collection()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SaleService(context);

            var expected = 1;

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
            var service = new SaleService(context);

            int saleId = 1;
            var expected = 1;

            //Act
            var result = await service.GetAsync(saleId);

            //Assert
            Assert.Equal(expected, result.Id);
        }

        [Fact]
        public async Task GetAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SaleService(context);

            int saleId = 9999;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(saleId));
        }

        [Fact]
        public async Task InsertAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SaleService(context);

            var sale = new Sale
            {
                Id = 3546,
                BuyerId = 1,
                SalesPointId = 1,
                DateTime = DateTime.UtcNow,
                TotalAmount = 500
            };
            var expected = 500;

            //Act
            var result = await service.InsertAsync(sale);

            //Assert
            Assert.Equal(expected, result.TotalAmount);
        }

        [Fact]
        public async Task InsertAsync_Null_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SaleService(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.InsertAsync(null));
        }

        [Fact]
        public async Task UpdateAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SaleService(context);

            var sale = new Sale
            {
                Id = 1,
                BuyerId = 1,
                SalesPointId = 1,
                DateTime = DateTime.UtcNow,
                TotalAmount = 500
            };
            var expected = 500;

            //Act
            var result = await service.UpdateAsync(sale);

            //Assert
            Assert.Equal(expected, result.TotalAmount);
        }

        [Fact]
        public async Task UpdateAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SaleService(context);

            var sale = new Sale
            {
                Id = 3546,
                BuyerId = 1555,
                SalesPointId = 132,
                DateTime = DateTime.UtcNow,
                TotalAmount = 500
            };

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(sale));
        }

        [Fact]
        public async Task DeleteAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SaleService(context);

            int saleId = 1;

            //Act
            //Assert
            await service.DeleteAsync(saleId);
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(saleId));
        }

        [Fact]
        public async Task DeleteAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SaleService(context);

            int saleId = 9999;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(saleId));
        }
    }
}
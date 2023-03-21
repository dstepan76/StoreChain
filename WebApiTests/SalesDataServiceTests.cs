using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class SalesDataServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Return_Collection()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesDataService(context);

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
            var service = new SalesDataService(context);

            int productId = 2;
            int saleId = 1;
            var expected = 6;

            //Act
            var result = await service.GetAsync(productId, saleId);

            //Assert
            Assert.Equal(expected, result.ProductQuantity);
        }

        [Fact]
        public async Task GetAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesDataService(context);

            int productId = 5555;
            int saleId = 1;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(productId, saleId));
        }

        [Fact]
        public async Task InsertAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesDataService(context);

            var salesData = new SalesData
            {
                ProductId = 1,
                SaleId = 1,
                ProductQuantity = 333
            };
            var expected = 333;

            //Act
            var result = await service.InsertAsync(salesData);

            //Assert
            Assert.Equal(expected, result.ProductQuantity);
        }

        [Fact]
        public async Task InsertAsync_Null_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesDataService(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.InsertAsync(null));
        }

        [Fact]
        public async Task UpdateAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesDataService(context);

            var salesData = new SalesData
            {
                ProductId = 2,
                SaleId = 1,
                ProductQuantity = 333
            };
            var expected = 333;

            //Act
            var result = await service.UpdateAsync(salesData);

            //Assert
            Assert.Equal(expected, result.ProductQuantity);
        }

        [Fact]
        public async Task UpdateAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesDataService(context);

            var salesData = new SalesData
            {
                ProductId = 54,
                SaleId = 1,
                ProductQuantity = 333
            };

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(salesData));
        }

        [Fact]
        public async Task DeleteAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesDataService(context);

            int productId = 2;
            int salesPointId = 1;

            //Act
            //Assert
            await service.DeleteAsync(productId, salesPointId);
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(productId, salesPointId));
        }

        [Fact]
        public async Task DeleteAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new SalesDataService(context);

            int productId = 134;
            int salesPointId = 111;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(productId, salesPointId));
        }
    }
}
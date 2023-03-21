using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class ProvidedProductServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Return_Collection()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProvidedProductService(context);

            var expected = 9;

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
            var service = new ProvidedProductService(context);

            int productId = 1;
            int salesPointId = 1;
            var expected = 50;

            //Act
            var result = await service.GetAsync(productId, salesPointId);

            //Assert
            Assert.Equal(expected, result.ProductQuantity);
        }

        [Fact]
        public async Task GetAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProvidedProductService(context);

            int productId = 5555;
            int salesPointId = 1;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(productId, salesPointId));
        }

        [Fact]
        public async Task InsertAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProvidedProductService(context);

            var providedProduct = new ProvidedProduct
            {
                ProductId = 1,
                SalesPointId = 3,
                ProductQuantity = 333
            };
            var expected = 333;

            //Act
            var result = await service.InsertAsync(providedProduct);

            //Assert
            Assert.Equal(expected, result.ProductQuantity);
        }

        [Fact]
        public async Task InsertAsync_Null_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProvidedProductService(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.InsertAsync(null));
        }

        [Fact]
        public async Task UpdateAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProvidedProductService(context);

            var providedProduct = new ProvidedProduct
            {
                ProductId = 1,
                SalesPointId = 1,
                ProductQuantity = 333
            };
            var expected = 333;

            //Act
            var result = await service.UpdateAsync(providedProduct);

            //Assert
            Assert.Equal(expected, result.ProductQuantity);
        }

        [Fact]
        public async Task UpdateAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProvidedProductService(context);

            var providedProduct = new ProvidedProduct
            {
                ProductId = 54,
                SalesPointId = 1,
                ProductQuantity = 333
            };

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(providedProduct));
        }

        [Fact]
        public async Task DeleteAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProvidedProductService(context);

            int productId = 1;
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
            var service = new ProvidedProductService(context);

            int productId = 134;
            int salesPointId = 111;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(productId, salesPointId));
        }
    }
}
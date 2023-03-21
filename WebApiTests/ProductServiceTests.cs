using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Return_Collection()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProductService(context);

            var expected = 4;

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
            var service = new ProductService(context);

            int productId = 1;
            var expected = 1;

            //Act
            var result = await service.GetAsync(productId);

            //Assert
            Assert.Equal(expected, result.Id);
        }

        [Fact]
        public async Task GetAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProductService(context);

            int productId = 9999;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(productId));
        }

        [Fact]
        public async Task InsertAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProductService(context);

            var product = new Product
            {
                Id = 3546,
                Name = "Oil"
            };
            var expected = "Oil";

            //Act
            var result = await service.InsertAsync(product);

            //Assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task InsertAsync_Null_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProductService(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.InsertAsync(null));
        }

        [Fact]
        public async Task UpdateAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProductService(context);

            var product = new Product
            {
                Id = 1,
                Name = "Oil"
            };
            var expected = "Oil";

            //Act
            var result = await service.UpdateAsync(product);

            //Assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task UpdateAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProductService(context);

            var product = new Product
            {
                Id = 1234,
                Name = "Oil"
            };
            var expected = "Oil";

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(product));
        }

        [Fact]
        public async Task DeleteAsync_Return_Ok()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProductService(context);

            int productId = 1;

            //Act
            //Assert
            await service.DeleteAsync(productId);
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAsync(productId));
        }

        [Fact]
        public async Task DeleteAsync_WrongId_Throw_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var service = new ProductService(context);

            int productId = 9999;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(productId));
        }
    }
}
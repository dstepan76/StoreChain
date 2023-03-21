using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class SaleOperationServiceTests
    {
        [Fact]
        public async Task Sale_EnoughQuantity_TotalAmount_OK()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var saleOperationService = new SaleOperationService(context);
            var providedProductService = new ProvidedProductService(context);
            
            var salesPointId = 1;
            var buyerId = 1;
            var productId = 1;
            var quantity = 20;

            var providedProduct = await providedProductService.GetAsync(productId, salesPointId);

            var expected = providedProduct.ProductQuantity - quantity;

            //Act
            await saleOperationService.SaleAsync(salesPointId, buyerId, productId, quantity);
            providedProduct = await providedProductService.GetAsync(productId, salesPointId);

            //Assert
            Assert.Equal(expected, providedProduct.ProductQuantity);
        }

        [Fact]
        public async Task Sale_BuyerIdIsNull_SalesDataBuyersIdIsNull()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var saleOperationService = new SaleOperationService(context);
            
            var salesPointId = 1;
            int? buyerId = null;
            var productId = 1;
            var quantity = 20;

            //Act
            await saleOperationService.SaleAsync(salesPointId, buyerId, productId, quantity);
            var sale = await context.Sale.FirstAsync(p => p.SalesPointId == salesPointId);

            //Assert
            Assert.Null(sale.BuyerId);
        }

        [Fact]
        public async Task Sale_Buyer_Has_SalesId()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var saleOperationService = new SaleOperationService(context);

            var salesPointId = 1;
            var buyerId = 1;
            var productId = 1;
            var quantity = 20;

            var expected = 2;

            //Act
            await saleOperationService.SaleAsync(salesPointId, buyerId, productId, quantity);
            var buyer = await context.Buyer.Include(p => p.Sales).FirstAsync(p => p.Id == buyerId);

            //Assert
            Assert.Contains(buyer.SalesIds, p => p == expected);
        }

        [Fact]
        public async Task Sale_Sales_Has_SalesData_With_Quantity()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var saleOperationService = new SaleOperationService(context);

            var salesPointId = 1;
            var buyerId = 1;
            var productId = 1;
            var quantity = 20;

            var expected = quantity;

            //Act
            await saleOperationService.SaleAsync(salesPointId, buyerId, productId, quantity);
            var sale = await context.Sale.Include(p => p.SalesDataItems).FirstAsync(p => p.SalesPointId == salesPointId);

            //Assert
            Assert.Contains(sale.SalesDataItems, p => p.ProductQuantity == expected);
        }

        [Fact]
        public async Task Sale_NotEnoughQuantity_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var saleOperationService = new SaleOperationService(context);
            var providedProductService = new ProvidedProductService(context);

            var salesPointId = 1;
            var buyerId = 1;
            var productId = 1;
            var quantity = 100;

            var providedProduct = await providedProductService.GetAsync(productId, salesPointId);


            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await saleOperationService.SaleAsync(salesPointId, buyerId, productId, quantity));
        }

        [Fact]
        public async Task Sale_WrongIds_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var saleOperationService = new SaleOperationService(context);

            var salesPointId = 654654;
            var buyerId = 1;
            var productId = 4565;
            var quantity = 1;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await saleOperationService.SaleAsync(salesPointId, buyerId, productId, quantity));
        }

        [Fact]
        public async Task Sale_WrongBuyerId_Exception()
        {
            //Arrange
            await using var context = Database.GetInstance();
            var saleOperationService = new SaleOperationService(context);

            var salesPointId = 1;
            var buyerId = 4565;
            var productId = 1;
            var quantity = 1;

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await saleOperationService.SaleAsync(salesPointId, buyerId, productId, quantity));
        }
    }
}
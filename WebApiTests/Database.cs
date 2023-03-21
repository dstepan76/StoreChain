using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Entities;
using WebApi.Persistence;

namespace WebApiTests
{
    public static class Database
    {
        public static StoreChainDbContext GetInstance()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreChainDbContext>();

            var options = optionsBuilder
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new StoreChainDbContext(options);
            Initialize(context);
            return context;
        }

        /// <summary>
        /// Заполняет базу данных начальными значениями
        /// </summary>
        private static void Initialize(StoreChainDbContext context)
        {
            context.Product.AddRange(
                new Product { Id = 1, Name = "Bread", Price = 50 },
                new Product { Id = 2, Name = "Milk", Price = 100 },
                new Product { Id = 3, Name = "Meat", Price = 350 },
                new Product { Id = 4, Name = "Fish", Price = 470 });

            context.SalesPoint.AddRange(
                new SalesPoint { Id = 1, Name = "Store1", },
                new SalesPoint { Id = 2, Name = "Store2", },
                new SalesPoint { Id = 3, Name = "Store3", });

            context.Buyer.AddRange(
                new Buyer { Id = 1, Name = "Alex" },
                new Buyer { Id = 2, Name = "Bob" },
                new Buyer { Id = 3, Name = "Paul" });

            context.ProvidedProduct.AddRange(
                new ProvidedProduct { ProductId = 1, SalesPointId = 1, ProductQuantity = 50 },
                new ProvidedProduct { ProductId = 2, SalesPointId = 1, ProductQuantity = 60 },
                new ProvidedProduct { ProductId = 3, SalesPointId = 1, ProductQuantity = 70 },

                new ProvidedProduct { ProductId = 1, SalesPointId = 2, ProductQuantity = 30 },
                new ProvidedProduct { ProductId = 2, SalesPointId = 2, ProductQuantity = 40 },
                new ProvidedProduct { ProductId = 4, SalesPointId = 2, ProductQuantity = 50 },

                new ProvidedProduct { ProductId = 2, SalesPointId = 3, ProductQuantity = 90 },
                new ProvidedProduct { ProductId = 3, SalesPointId = 3, ProductQuantity = 80 },
                new ProvidedProduct { ProductId = 4, SalesPointId = 3, ProductQuantity = 20 });

            context.Sale.AddRange(
                new Sale { Id = 1, BuyerId = 3, SalesPointId = 3, DateTime = DateTime.UtcNow, TotalAmount = 600 }
                );

            context.SalesData.AddRange(
                new SalesData { SaleId = 1, ProductId = 2, ProductQuantity = 6, ProductIdAmount = 600 }
                );

            context.SaveChanges();
        }
    }
}
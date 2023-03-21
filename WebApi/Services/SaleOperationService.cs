using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Persistence;

namespace WebApi.Services
{
    public class SaleOperationService : ISaleOperationService
    {
        private readonly StoreChainDbContext _context;

        public SaleOperationService(StoreChainDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Продажа товара
        /// </summary>
        public async Task SaleAsync(int salesPointId, int? buyerId, int productId, int quantity)
        {
            //Проверяем покупателя
            if(buyerId.HasValue && !await _context.Buyer.AnyAsync(p => p.Id == buyerId))
                throw new Exception($"Buyer not found. BuyerId={buyerId}");

            //Проверяем наличие товара
            var providedProduct = await _context.ProvidedProduct
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.SalesPointId == salesPointId);

            if (providedProduct == null)
                throw new Exception($"ProvidedProduct not found. ProductId={productId}, SalesPointId={salesPointId}");

            if (providedProduct.ProductQuantity < quantity)
                throw new Exception($"Not enough goods. Total:{providedProduct.ProductQuantity}");

            //Меняем количество товаров на складе
            providedProduct.ProductQuantity -= quantity;

            var amount = providedProduct.Product.Price * quantity;

            var sale = new Sale
            {
                SalesPointId = salesPointId,
                BuyerId = buyerId,
                DateTime = DateTime.UtcNow,
                TotalAmount = amount,
                SalesDataItems = new List<SalesData>
                {
                    new()
                    {
                        ProductId = productId,
                        ProductQuantity = quantity,
                        ProductIdAmount = amount
                    }
                }
            };

            await _context.Sale.AddAsync(sale);

            await _context.SaveChangesAsync();
        }
    }
}
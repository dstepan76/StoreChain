using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Persistence;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class ProvidedProductService : IProvidedProductService
    {
        private readonly StoreChainDbContext _context;

        public ProvidedProductService(StoreChainDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает все точки
        /// </summary>
        public async Task<ICollection<ProvidedProduct>> GetAllAsync()
        {
            return await _context.ProvidedProduct.ToListAsync();
        }

        /// <summary>
        /// Возвращает точку по Id
        /// </summary>
        public async Task<ProvidedProduct> GetAsync(int productId, int salesPointId)
        {
            var providedProduct = await _context.ProvidedProduct.FirstOrDefaultAsync(p => p.ProductId == productId && p.SalesPointId == salesPointId);

            if (providedProduct == null)
                throw new Exception($"ProvidedProduct not found. ProductId={productId}, SalesPointId={salesPointId}");

            return providedProduct;
        }

        /// <summary>
        /// Добавляет новую точку в БД
        /// </summary>
        public async Task<ProvidedProduct> InsertAsync(ProvidedProduct providedProduct)
        {
            if (providedProduct == null)
                throw new Exception("ProvidedProduct is null");

            if (!await _context.Product.AnyAsync(p => p.Id == providedProduct.ProductId))
                throw new Exception($"Product not exists. Id={providedProduct.ProductId}");

            if (!await _context.SalesPoint.AnyAsync(p => p.Id == providedProduct.SalesPointId))
                throw new Exception($"SalesPoint not exists. Id={providedProduct.SalesPointId}");

            await _context.ProvidedProduct.AddAsync(providedProduct);
            await _context.SaveChangesAsync();

            return providedProduct;
        }

        /// <summary>
        /// Обновляет точку в БД
        /// </summary>
        public async Task<ProvidedProduct> UpdateAsync(ProvidedProduct providedProduct)
        {
            if (providedProduct == null)
                throw new Exception("ProvidedProduct is null");

            var old = await _context.ProvidedProduct.FirstOrDefaultAsync(p =>
                p.ProductId == providedProduct.ProductId && p.SalesPointId == providedProduct.SalesPointId);

            if(old == null)
                throw new Exception($"ProvidedProduct not found. ProductId={providedProduct.ProductId}, SalesPointId={providedProduct.SalesPointId}");

            old.ProductQuantity = providedProduct.ProductQuantity;
            
            _context.ProvidedProduct.Update(old);
            await _context.SaveChangesAsync();

            return providedProduct;
        }

        /// <summary>
        /// Удаляет точку из БД
        /// </summary>
        public async Task DeleteAsync(int productId, int salesPointId)
        {
            var providedProduct = await _context.ProvidedProduct.FirstOrDefaultAsync(p => p.ProductId == productId && p.SalesPointId == salesPointId);

            if (providedProduct == null)
                throw new Exception($"ProvidedProduct not found. ProductId={productId}, SalesPointId={salesPointId}");

            _context.ProvidedProduct.Remove(providedProduct);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает товар с описанием
        /// </summary>
        public async Task<ProvidedProduct> GetWithProductAsync(int productId, int salesPointId)
        {
            var providedProduct = await _context.ProvidedProduct
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.SalesPointId == salesPointId);

            if (providedProduct == null)
                throw new Exception($"ProvidedProduct not found. ProductId={productId}, SalesPointId={salesPointId}");

            return providedProduct;
        }
    }
}
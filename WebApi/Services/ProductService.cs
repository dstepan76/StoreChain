using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Persistence;

namespace WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly StoreChainDbContext _context;

        public ProductService(StoreChainDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает все продукты
        /// </summary>
        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

        /// <summary>
        /// Возвращает продукт по Id
        /// </summary>
        public async Task<Product> GetAsync(int productId)
        {
            var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new Exception($"Product not found. Id={productId}");

            return product;
        }

        /// <summary>
        /// Добавляет новый продукт в БД
        /// </summary>
        public async Task<Product> InsertAsync(Product product)
        {
            if (product == null)
                throw new Exception("Product is null");

            product.Id = default;
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        /// <summary>
        /// Обновляет продукт в БД
        /// </summary>
        public async Task<Product> UpdateAsync(Product product)
        {
            if (product == null)
                throw new Exception("Product is null");

            var old = await _context.Product.FirstOrDefaultAsync(p => p.Id == product.Id);
            if(old == null)
                throw new Exception($"Product not found. Id={product.Id}");

            old.Name = product.Name;
            old.Price = product.Price;

            _context.Product.Update(old);
            await _context.SaveChangesAsync();

            return product;
        }

        /// <summary>
        /// Удаляет продукт из БД
        /// </summary>
        public async Task DeleteAsync(int productId)
        {
            var product = await _context.Product
                .Include(p => p.ProvidedProducts)
                .Include(p => p.SalesDataItems)
                .Include(p => p.SalesPoints)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new Exception($"Product not found. Id={productId}");

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
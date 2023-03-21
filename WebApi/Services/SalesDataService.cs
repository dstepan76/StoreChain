using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Persistence;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class SalesDataService : ISalesDataService
    {
        private readonly StoreChainDbContext _context;

        public SalesDataService(StoreChainDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает все точки
        /// </summary>
        public async Task<ICollection<SalesData>> GetAllAsync()
        {
            return await _context.SalesData.ToListAsync();
        }

        /// <summary>
        /// Возвращает точку по Id
        /// </summary>
        public async Task<SalesData> GetAsync(int productId, int saleId)
        {
            var salesData = await _context.SalesData.FirstOrDefaultAsync(p => p.ProductId == productId && p.SaleId == saleId);

            if (salesData == null)
                throw new Exception($"SalesData not found. ProductId={productId}, SaleId={saleId}");

            return salesData;
        }

        /// <summary>
        /// Добавляет новую точку в БД
        /// </summary>
        public async Task<SalesData> InsertAsync(SalesData salesData)
        {
            if (salesData == null)
                throw new Exception("SalesData is null");

            if (!await _context.Product.AnyAsync(p => p.Id == salesData.ProductId))
                throw new Exception($"Product not exists. Id={salesData.ProductId}");

            if (!await _context.Sale.AnyAsync(p => p.Id == salesData.SaleId))
                throw new Exception($"SaleAsync not exists. Id={salesData.SaleId}");

            await _context.SalesData.AddAsync(salesData);
            await _context.SaveChangesAsync();

            return salesData;
        }

        /// <summary>
        /// Обновляет точку в БД
        /// </summary>
        public async Task<SalesData> UpdateAsync(SalesData salesData)
        {
            if (salesData == null)
                throw new Exception("SalesData is null");

            var old = await _context.SalesData.FirstOrDefaultAsync(p =>
                p.ProductId == salesData.ProductId && p.SaleId == salesData.SaleId);
            if (old == null)
                throw new Exception($"SalesData not found. ProductId={salesData.ProductId}, SaleId={salesData.SaleId}");

            old.ProductQuantity = salesData.ProductQuantity;
            old.ProductIdAmount = salesData.ProductIdAmount;

            _context.SalesData.Update(old);
            await _context.SaveChangesAsync();

            return salesData;
        }

        /// <summary>
        /// Удаляет точку из БД
        /// </summary>
        public async Task DeleteAsync(int productId, int saleId)
        {
            var salesData = await _context.SalesData.FirstOrDefaultAsync(p => p.ProductId == productId && p.SaleId == saleId);

            if (salesData == null)
                throw new Exception($"SalesData not found. ProductId={productId}, SaleId={saleId}");

            _context.SalesData.Remove(salesData);
            await _context.SaveChangesAsync();
        }
    }
}
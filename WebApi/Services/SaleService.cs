using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Persistence;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class SaleService : ISaleService
    {
        private readonly StoreChainDbContext _context;

        public SaleService(StoreChainDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает все продажи
        /// </summary>
        public async Task<ICollection<Sale>> GetAllAsync()
        {
            return await _context.Sale.ToListAsync();
        }

        /// <summary>
        /// Возвращает продажи по Id
        /// </summary>
        public async Task<Sale> GetAsync(int saleId)
        {
            var sale = await _context.Sale.FirstOrDefaultAsync(p => p.Id == saleId);

            if (sale == null)
                throw new Exception($"SaleAsync not found. SaleId={saleId}");

            return sale;
        }

        /// <summary>
        /// Добавляет новую продажу
        /// </summary>
        public async Task<Sale> InsertAsync(Sale sale)
        {
            if (sale == null)
                throw new Exception("SaleAsync is null");

            if (sale.BuyerId.HasValue && !await _context.Buyer.AnyAsync(p => p.Id == sale.BuyerId))
                throw new Exception($"Buyer not exists. BuyerId={sale.BuyerId}");

            if (!await _context.SalesPoint.AnyAsync(p => p.Id == sale.SalesPointId))
                throw new Exception($"SalesPoint not exists. SalesPointId={sale.SalesPointId}");

            sale.Id = default;

            await _context.Sale.AddAsync(sale);
            await _context.SaveChangesAsync();

            return sale;
        }

        /// <summary>
        /// Обновляет точку в БД
        /// </summary>
        public async Task<Sale> UpdateAsync(Sale sale)
        {
            if (sale == null)
                throw new Exception("SaleAsync is null");

            if (sale.BuyerId.HasValue && !await _context.Buyer.AnyAsync(p => p.Id == sale.BuyerId))
                throw new Exception($"Buyer not exists. BuyerId={sale.BuyerId}");

            if (!await _context.SalesPoint.AnyAsync(p => p.Id == sale.SalesPointId))
                throw new Exception($"SalesPoint not exists. SalesPointId={sale.SalesPointId}");

            var old = await _context.Sale.FirstOrDefaultAsync(p => p.Id == sale.Id);
            if(old == null)
                throw new Exception($"SaleAsync not found. Id={sale.Id}");

            old.DateTime = sale.DateTime;
            old.TotalAmount = sale.TotalAmount;

            _context.Sale.Update(old);
            await _context.SaveChangesAsync();

            return sale;
        }

        /// <summary>
        /// Удаляет точку из БД
        /// </summary>
        public async Task DeleteAsync(int saleId)
        {
            var sale = await _context.Sale
                .Include(p => p.SalesDataItems)
                .FirstOrDefaultAsync(p => p.Id == saleId);

            if (sale == null)
                throw new Exception($"SaleAsync not found. Id={saleId}");

            _context.Sale.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}
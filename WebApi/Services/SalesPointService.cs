using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Persistence;

namespace WebApi.Services
{
    public class SalesPointService : ISalesPointService
    {
        private readonly StoreChainDbContext _context;

        public SalesPointService(StoreChainDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает все точки
        /// </summary>
        public async Task<ICollection<SalesPoint>> GetAllAsync()
        {
            return await _context.SalesPoint.Include(p => p.ProvidedProducts).ToListAsync();
        }

        /// <summary>
        /// Возвращает точку по Id
        /// </summary>
        public async Task<SalesPoint> GetAsync(int salesPointId)
        {
            var salesPoint = await _context.SalesPoint.Include(p => p.ProvidedProducts).FirstOrDefaultAsync(p => p.Id == salesPointId);

            if (salesPoint == null)
                throw new Exception($"SalesPoint not found. Id={salesPointId}");

            return salesPoint;
        }

        /// <summary>
        /// Добавляет новую точку в БД
        /// </summary>
        public async Task<SalesPoint> InsertAsync(SalesPoint salesPoint)
        {
            if (salesPoint == null)
                throw new Exception("SalesPoint is null");

            salesPoint.Id = default;
            await _context.SalesPoint.AddAsync(salesPoint);
            await _context.SaveChangesAsync();

            return salesPoint;
        }

        /// <summary>
        /// Обновляет точку в БД
        /// </summary>
        public async Task<SalesPoint> UpdateAsync(SalesPoint salesPoint)
        {
            if (salesPoint == null)
                throw new Exception("SalesPoint is null");
            
            var old = await _context.SalesPoint.FirstOrDefaultAsync(p => p.Id == salesPoint.Id);
            if(old == null)
                throw new Exception($"SalesPoint not found. Id={salesPoint.Id}");

            old.Name = salesPoint.Name;

            _context.SalesPoint.Update(old);
            await _context.SaveChangesAsync();

            return salesPoint;
        }

        /// <summary>
        /// Удаляет точку из БД
        /// </summary>
        public async Task DeleteAsync(int salesPointId)
        {
            var salesPoint = await _context.SalesPoint
                .Include(p => p.ProvidedProducts)
                .Include(p => p.Sales)
                .FirstOrDefaultAsync(p => p.Id == salesPointId);
            
            if (salesPoint == null)
                throw new Exception($"SalesPoint not found. Id={salesPointId}");

            _context.SalesPoint.Remove(salesPoint);
            await _context.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Persistence;

namespace WebApi.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly StoreChainDbContext _context;

        public BuyerService(StoreChainDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает всех покупателей
        /// </summary>
        public async Task<ICollection<Buyer>> GetAllAsync()
        {
            return await _context.Buyer.ToListAsync();
        }

        /// <summary>
        /// Возвращает покупателя по Id
        /// </summary>
        public async Task<Buyer> GetAsync(int buyerId)
        {
            var buyer = await _context.Buyer.FirstOrDefaultAsync(p => p.Id == buyerId);

            if (buyer == null)
                throw new Exception($"Buyer not found. Id={buyerId}");

            return buyer;
        }

        /// <summary>
        /// Добавляет нового покупателя
        /// </summary>
        public async Task<Buyer> InsertAsync(Buyer buyer)
        {
            if (buyer == null)
                throw new Exception("Buyer is null");

            buyer.Id = default;
            await _context.Buyer.AddAsync(buyer);
            await _context.SaveChangesAsync();

            return buyer;
        }

        /// <summary>
        /// Обновляет покупателя
        /// </summary>
        public async Task<Buyer> UpdateAsync(Buyer buyer)
        {
            if (buyer == null)
                throw new Exception("Buyer is null");
            
            var old = await _context.Buyer.FirstOrDefaultAsync(p => p.Id == buyer.Id);
           
            if (old == null)
                throw new Exception($"Buyer not found. Id={buyer.Id}");

            old.Name = buyer.Name;

            _context.Buyer.Update(old);
            await _context.SaveChangesAsync();

            return buyer;
        }

        /// <summary>
        /// Удаляет покупателя
        /// </summary>
        public async Task DeleteAsync(int buyerId)
        {
            var buyer = await _context.Buyer
                .Include(p => p.Sales)
                .Include(p => p.SalesPoints)
                .FirstOrDefaultAsync(p => p.Id == buyerId);

            if (buyer == null)
                throw new Exception($"Buyer not found. Id={buyerId}");

            _context.Buyer.Remove(buyer);
            await _context.SaveChangesAsync();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IBuyerService
    {
        /// <summary>
        /// Возвращает всех покупателей
        /// </summary>
        Task<ICollection<Buyer>> GetAllAsync();

        /// <summary>
        /// Возвращает покупателя по Id
        /// </summary>
        Task<Buyer> GetAsync(int buyerId);

        /// <summary>
        /// Добавляет нового покупателя
        /// </summary>
        Task<Buyer> InsertAsync(Buyer buyer);

        /// <summary>
        /// Обновляет покупателя
        /// </summary>
        Task<Buyer> UpdateAsync(Buyer buyer);

        /// <summary>
        /// Удаляет покупателя
        /// </summary>
        Task DeleteAsync(int buyerId);
    }
}
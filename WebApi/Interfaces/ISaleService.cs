using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface ISaleService
    {
        /// <summary>
        /// Возвращает все продажи
        /// </summary>
        Task<ICollection<Sale>> GetAllAsync();

        /// <summary>
        /// Возвращает продажи по Id
        /// </summary>
        Task<Sale> GetAsync(int saleId);

        /// <summary>
        /// Добавляет новую продажу
        /// </summary>
        Task<Sale> InsertAsync(Sale sale);

        /// <summary>
        /// Обновляет точку в БД
        /// </summary>
        Task<Sale> UpdateAsync(Sale sale);

        /// <summary>
        /// Удаляет точку из БД
        /// </summary>
        Task DeleteAsync(int saleId);
    }
}
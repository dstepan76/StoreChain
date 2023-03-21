using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface ISalesDataService
    {
        /// <summary>
        /// Возвращает все точки
        /// </summary>
        Task<ICollection<SalesData>> GetAllAsync();

        /// <summary>
        /// Возвращает точку по Id
        /// </summary>
        Task<SalesData> GetAsync(int productId, int saleId);

        /// <summary>
        /// Добавляет новую точку в БД
        /// </summary>
        Task<SalesData> InsertAsync(SalesData salesData);

        /// <summary>
        /// Обновляет точку в БД
        /// </summary>
        Task<SalesData> UpdateAsync(SalesData salesData);

        /// <summary>
        /// Удаляет точку из БД
        /// </summary>
        Task DeleteAsync(int productId, int saleId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface ISalesPointService
    {
        /// <summary>
        /// Возвращает все точки
        /// </summary>
        Task<ICollection<SalesPoint>> GetAllAsync();

        /// <summary>
        /// Возвращает точку по Id
        /// </summary>
        Task<SalesPoint> GetAsync(int salesPointId);

        /// <summary>
        /// Добавляет новую точку в БД
        /// </summary>
        Task<SalesPoint> InsertAsync(SalesPoint salesPoint);

        /// <summary>
        /// Обновляет точку в БД
        /// </summary>
        Task<SalesPoint> UpdateAsync(SalesPoint salesPoint);

        /// <summary>
        /// Удаляет точку из БД
        /// </summary>
        Task DeleteAsync(int salesPointId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IProvidedProductService
    {
        /// <summary>
        /// Возвращает все точки
        /// </summary>
        Task<ICollection<ProvidedProduct>> GetAllAsync();

        /// <summary>
        /// Возвращает точку по Id
        /// </summary>
        Task<ProvidedProduct> GetAsync(int productId, int salesPointId);

        /// <summary>
        /// Добавляет новую точку в БД
        /// </summary>
        Task<ProvidedProduct> InsertAsync(ProvidedProduct providedProduct);

        /// <summary>
        /// Обновляет точку в БД
        /// </summary>
        Task<ProvidedProduct> UpdateAsync(ProvidedProduct providedProduct);

        /// <summary>
        /// Удаляет точку из БД
        /// </summary>
        Task DeleteAsync(int productId, int salesPointId);

        /// <summary>
        /// Возвращает товар с описанием
        /// </summary>
        Task<ProvidedProduct> GetWithProductAsync(int productId, int salesPointId);
    }
}
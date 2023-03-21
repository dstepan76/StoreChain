using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Возвращает все продукты
        /// </summary>
        Task<ICollection<Product>> GetAllAsync();

        /// <summary>
        /// Возвращает продукт по Id
        /// </summary>
        Task<Product> GetAsync(int productId);

        /// <summary>
        /// Добавляет новый продукт в БД
        /// </summary>
        Task<Product> InsertAsync(Product product);

        /// <summary>
        /// Обновляет продукт в БД
        /// </summary>
        Task<Product> UpdateAsync(Product product);

        /// <summary>
        /// Удаляет продукт из БД
        /// </summary>
        Task DeleteAsync(int productId);
    }
}
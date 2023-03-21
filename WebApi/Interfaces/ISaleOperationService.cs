using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface ISaleOperationService
    {
        /// <summary>
        /// Продажа товара
        /// </summary>
        Task SaleAsync(int salesPointId, int? buyerId, int productId, int quantity);
    }
}
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    /// <summary>
    /// Данные о продаже
    /// </summary>
    public class SalesData
    {
        /// <summary>
        /// Идентификатор купленного продукта
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Идентификатор продажи
        /// </summary>
        public int SaleId { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        [JsonIgnore]
        public Product Product { get; set; }

        /// <summary>
        /// Акт продажи
        /// </summary>
        [JsonIgnore]
        public Sale Sale { get; set; }

        /// <summary>
        /// Количество штук купленных продуктов
        /// </summary>
        public int ProductQuantity { get; set; }

        /// <summary>
        /// Общая стоимость купленного количества товаров
        /// </summary>
        public decimal ProductIdAmount { get; set; }

        
    }
}
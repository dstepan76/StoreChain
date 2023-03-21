using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    /// <summary>
    /// Товар, доступный к продаже
    /// </summary>
    public class ProvidedProduct
    {
        /// <summary>
        /// Id товара
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Идентификатор точки продажи
        /// </summary>
        public int SalesPointId { get; set; }

        /// <summary>
        /// Точка продажи
        /// </summary>
        [JsonIgnore]
        public SalesPoint SalesPoint { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        [JsonIgnore]
        public Product Product { get; set; }


        /// <summary>
        /// Количество товара
        /// </summary>
        public int ProductQuantity { get; set; }

        
    }
}
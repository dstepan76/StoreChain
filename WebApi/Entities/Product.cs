using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Price { get; set; }

        ///// <summary>
        ///// Список доступных к продаже товаров
        ///// </summary>
        [JsonIgnore]
        public ICollection<ProvidedProduct> ProvidedProducts { get; set; }

        /// <summary>
        /// Список точек продаж
        /// </summary>
        [JsonIgnore]
        public ICollection<SalesPoint> SalesPoints { get; set; }

        /// <summary>
        /// Список продаж
        /// </summary>
        [JsonIgnore]
        public ICollection<SalesData> SalesDataItems { get; set; }

        /// <summary>
        /// Список продаж
        /// </summary>
        [JsonIgnore]
        public ICollection<Sale> Sales { get; set; }
    }
}
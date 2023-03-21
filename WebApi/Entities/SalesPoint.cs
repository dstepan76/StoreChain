using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    /// <summary>
    /// Точка продажи товаров
    /// </summary>
    public class SalesPoint
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        ///// <summary>
        ///// Список доступных к продаже товаров
        ///// </summary>
        [JsonIgnore]
        public ICollection<ProvidedProduct> ProvidedProducts { get; set; } = new List<ProvidedProduct>();

        /// <summary>
        /// Список продуктов
        /// </summary>
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Коллекция всех покупок
        /// </summary>
        [JsonIgnore]
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();


        /// <summary>
        /// Коллекция всех покупателей
        /// </summary>
        [JsonIgnore]
        public ICollection<Buyer> Buyers { get; set; } = new List<Buyer>();


    }
}
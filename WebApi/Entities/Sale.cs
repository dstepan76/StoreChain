using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    /// <summary>
    /// Акт продажи
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

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
        /// Идентификатор покупателя
        /// </summary>
        public int? BuyerId { get; set; }

        /// <summary>
        /// Покупатель
        /// </summary>
        [JsonIgnore]
        public Buyer Buyer { get; set; }

        /// <summary>
        /// Данные о продажах
        /// </summary>
        [JsonIgnore]
        public ICollection<SalesData> SalesDataItems { get; set; } = new List<SalesData>();

        // <summary>
        /// Продукты
        /// </summary>
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Дата осуществления продажи
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Общая сумма всей покупки
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
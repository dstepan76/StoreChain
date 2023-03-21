using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    /// <summary>
    /// Покупатель
    /// </summary>
    public class Buyer
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция всех идентификаторов покупок
        /// </summary>
        public IEnumerable<int> SalesIds => Sales.Select(p => p.Id);

        /// <summary>
        /// Коллекция всех покупок
        /// </summary>
        [JsonIgnore]
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

        // <summary>
        /// Коллекция всех точек продаж
        /// </summary>
        [JsonIgnore]
        public ICollection<SalesPoint> SalesPoints { get; set; } = new List<SalesPoint>();
    }
}
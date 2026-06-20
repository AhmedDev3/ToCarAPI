using System.Text.Json.Serialization;

namespace ToCarAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        [JsonIgnore] 
        public virtual Order Order { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public int Quantity { get; set; }

        public decimal PriceAtOrder { get; set; }
    }
}

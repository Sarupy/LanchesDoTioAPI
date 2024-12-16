using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LanchesDoTioAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public Meal? Meal { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesDoTioAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public Meal? Meal { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}

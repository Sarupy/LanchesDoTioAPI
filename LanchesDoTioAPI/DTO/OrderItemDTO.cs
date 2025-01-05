using LanchesDoTioAPI.Models;

namespace LanchesDoTioAPI.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public MealDTO? Meal { get; set; }
        public int Quantity { get; set; }
    }
}

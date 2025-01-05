using LanchesDoTioAPI.Models;
using LanchesDoTioAPI.Models.Enums;

namespace LanchesDoTioAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public IEnumerable<OrderItemDTO>? Items { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderType Type { get; set; } = OrderType.Purchase;
    }
}

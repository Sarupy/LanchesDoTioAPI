using LanchesDoTioAPI.Models;

namespace LanchesDoTioAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<OrderItem>? Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

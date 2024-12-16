using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesDoTioAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Customer? Customer { get; set; }
        public List<OrderItem>? Items { get; set; }
        [NotMapped]
        public decimal TotalPrice
        {
            get {
                return Items.Sum(x => x.Quantity * x.Meal.getPriceAtDateTime(CreatedDate));
            }
        }
    }
}

using LanchesDoTioAPI.Models.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesDoTioAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public OrderType Type { get; set; } = OrderType.Purchase;
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Customer Customer { get; set; }
        public List<OrderItem>? Items { get; set; }
        public decimal? PaymentAmount { get; set; }

        public Order()
        {
        }

        public Order(decimal paymentAmount)
        {
            this.Type = OrderType.Payment;
            this.PaymentAmount = paymentAmount;
        }
        public decimal getTotalPrice()
        {
            return Items.Sum(x => x.Quantity * x.Meal.GetPriceAtDateTime(CreatedDate));           
        }
    }
}

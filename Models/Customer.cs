using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesDoTioAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order>? Orders { get; set; }

        [NotMapped]
        public decimal Debt 
        {
            get
            {
                return Orders.Sum(x => x.TotalPrice);
            }
        }

    }
}

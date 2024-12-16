using System.Text.Json.Serialization;

namespace LanchesDoTioAPI.Models
{
    public class PriceHistory
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public int MealId { get; set; }
        [JsonIgnore]
        public Meal? Meal { get; set; }

        public PriceHistory (decimal price)
        {
            this.Price = price;
            this.StartDate = DateTime.Now;
        }
    }
}

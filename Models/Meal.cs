using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesDoTioAPI.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PriceHistory> PriceHistoryList { get; set; }
        [NotMapped]
        public decimal CurrentPrice 
        { 
            get { return getPriceAtDateTime(DateTime.Now); }
        }
        
        public decimal getPriceAtDateTime(DateTime datetime)
        {
            if (PriceHistoryList?.Count > 0)
            {
                var pastPrices = PriceHistoryList.Where(x => x.StartDate <= datetime);
                if (pastPrices.Count() > 0)
                {
                    return pastPrices.MaxBy(x => x.StartDate).Price;
                }
                
            }
            throw new Exception($"Meal had no price at {datetime.ToShortDateString()}.");
        }

        public void updatePrice(decimal price)
        {
            PriceHistoryList.Add(new PriceHistory(price));
        }

    }
}

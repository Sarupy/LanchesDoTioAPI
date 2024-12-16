using LanchesDoTioAPI.Data;
using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;
using LanchesDoTioAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesDoTioAPI.Services.Implemetations
{
    public class MealService : IMealService
    {
        private readonly LanchesContext _context;
        public MealService(LanchesContext context)
        {
            _context = context;
        }

        public MealDTO ModelToDto(Meal meal)
        {
            return new MealDTO
            {
                Id = meal.Id,
                Name = meal.Name,
                Price = meal.CurrentPrice
            };
        }
        public Meal DtoToModel(MealDTO mealDTO)
        {
            return new Meal
            {
                Id = mealDTO.Id,
                Name = mealDTO.Name,
                PriceHistoryList = [new PriceHistory(mealDTO.Price)]
            };
        }
        public async Task<Meal> Rename(int mealId, string newName)
        {
            var meal = EnsureMealExists(mealId);

            if (string.IsNullOrWhiteSpace(newName))
                throw new BadHttpRequestException("New name cannot be empty.");

            if (newName == meal.Name)
                return meal;

            meal.Name = newName;
            _context.Entry(meal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return meal;
        }

        public async Task<Meal> UpdatePrice(int mealId, decimal newPrice)
        {
            var meal = EnsureMealExists(mealId);

            if (newPrice == meal.CurrentPrice)
                return meal;

            meal.updatePrice(newPrice);
            _context.Entry(meal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return meal;
        }

        private Meal EnsureMealExists(int mealId)
        {
            var meal = _context.Meal.Include(x => x.PriceHistoryList).FirstOrDefault(x=> x.Id == mealId);

            if (meal == null)
                throw new KeyNotFoundException($"Meal with ID {mealId} was not found.");

            return meal;
        }
    }
}

using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;

namespace LanchesDoTioAPI.Services.Interfaces
{
    public interface IMealService
    {
        public MealDTO ModelToDto(Meal meal);
        public Meal DtoToModel(MealDTO mealDTO);
        public Task<Meal> Rename(int mealId, string newName);
        public Task<Meal> UpdatePrice(int mealId, decimal newPrice);

    }
}

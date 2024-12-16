using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;

namespace LanchesDoTioAPI.Services.Interfaces
{
    public interface IMealService
    {
        public Task<MealDTO> Rename(int mealId, string newName);
        public Task<MealDTO> UpdatePrice(int mealId, decimal newPrice);
        public Task<MealDTO> GetById(int mealId);
        public Task<IEnumerable<MealDTO>> GetAll();
        public Task<MealDTO> Create(MealDTO mealDTO);
        public Task Delete(int mealId);

    }
}

using LanchesDoTioAPI.DTO;

namespace LanchesDoTioAPI.Services.Interfaces
{
    public interface IMealService
    {
        public Task<MealDTO> GetById(int mealId);
        public Task<IEnumerable<MealDTO>> GetAll();
        public Task<MealDTO> Create(MealDTO mealDTO);
        public Task<MealDTO> Update(int mealId, MealDTO mealDTO);
        public Task Delete(int mealId);
    }
}

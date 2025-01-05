using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;

namespace LanchesDoTioAPI.Mappers
{
    public static class MealMapper
    {
        public static MealDTO? ModelToDto(Meal meal)
        {
            if (meal == null)
            {
                return null;
            }

            return new MealDTO
            {
                Id = meal.Id,
                Name = meal.Name,
                Price = meal.CurrentPrice
            };
        }
        public static Meal DtoToModel(MealDTO mealDTO)
        {
            return new Meal
            {
                Id = mealDTO.Id,
                Name = mealDTO.Name,
                PriceHistoryList = [new PriceHistory(mealDTO.Price)]
            };
        }
    }
}

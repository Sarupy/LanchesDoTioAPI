using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;

namespace LanchesDoTioAPI.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItemDTO ModelToDto(OrderItem orderItem)
        {
            return new OrderItemDTO
            {
                Id = orderItem.Id,
                Meal = MealMapper.ModelToDto(orderItem.Meal),
                Quantity = orderItem.Quantity,
            };
        }

        public static OrderItem DtoToModel(OrderItemDTO orderItemDTO)
        {
            return new OrderItem
            {
                Id = orderItemDTO.Id,
                MealId = orderItemDTO.Meal.Id,
                Meal = MealMapper.DtoToModel(orderItemDTO.Meal),               
                Quantity = orderItemDTO.Quantity,
            };
        }
    }
}

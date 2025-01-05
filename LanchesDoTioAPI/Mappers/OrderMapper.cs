using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;

namespace LanchesDoTioAPI.Mappers
{
    public static class OrderMapper
    {
        public static OrderDTO ModelToDto(Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                Customer = new CustomerDTO()
                {
                    Id = order.CustomerId,
                    Name = order.Customer.Name
                },
                Type = order.Type,
                TotalPrice = order.getTotalCost(),
                CreatedDate = order.CreatedDate,
                Items = order.Items.Select( x=> OrderItemMapper.ModelToDto(x))
            };
        }
        public static Order DtoToModel(OrderDTO orderDTO)
        {
            return new Order
            {
                Id = orderDTO.Id,
                CustomerId = orderDTO.Customer.Id,
                Items = orderDTO.Items.Select(x => OrderItemMapper.DtoToModel(x)).ToList(),
                CreatedDate = orderDTO.CreatedDate,
            };
        }
    }
}

using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;

namespace LanchesDoTioAPI.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDTO ModelToDto(Customer customer)
        {
            decimal debt = 0;

            if (customer.Orders?.Count > 0)
            {
                foreach (var order in customer.Orders)
                {
                    if (order.Type == Models.Enums.OrderType.Payment)
                        debt -= order.PaymentAmount;
                    else
                        debt += order.getTotalCost();
                }
            }

            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Debt = debt
            };
        }
        public static Customer DtoToModel(CustomerDTO customerDTO)
        {
            return new Customer
            {
                Id = customerDTO.Id,
                Name = customerDTO.Name,
            };
        }
    }
}

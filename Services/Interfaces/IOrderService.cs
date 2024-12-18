using LanchesDoTioAPI.DTO;

namespace LanchesDoTioAPI.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderDTO> GetById(int orderId);
        public Task<IEnumerable<OrderDTO>> GetAll();
        public Task<OrderDTO> Create(OrderDTO orderDTO);
        public Task<OrderDTO> GetByCustomer(int customerId);
    }
}


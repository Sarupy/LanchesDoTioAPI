using LanchesDoTioAPI.Data;
using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;
using LanchesDoTioAPI.Models.Enums;
using LanchesDoTioAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesDoTioAPI.Services.Implemetations
{
    public class OrderService: IOrderService
    {
        private readonly LanchesContext _context;
        private readonly ICustomerService _customerService;
        public OrderService(LanchesContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        public async Task<OrderDTO> GetById(int orderId)
        {
            var order = await EnsureOrderExists(orderId);

            return ModelToDto(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetByCustomer(int customerId)
        {
            var orders = await _context.Order.Include(x => x.Customer).Include(x => x.Items).AsNoTracking()
                .Where(x => x.CustomerId == customerId).Select(x => ModelToDto(x)).ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            var ordersQuery = _context.Order.Include(x => x.Customer).Include(x => x.Items).Select(x => ModelToDto(x));
            return await ordersQuery.AsNoTracking().ToListAsync();
        }

        public async Task<OrderDTO> Create(OrderDTO OrderDTO)
        {
            var order = DtoToModel(OrderDTO);
            _context.Order.Add(order);
            var mealId = await _context.SaveChangesAsync();
            OrderDTO.Id = mealId;
            return OrderDTO;
        }

        public static OrderDTO ModelToDto(Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                Customer = order.Customer,
                TotalPrice = order.getTotalPrice(),
                CreatedDate = order.CreatedDate,
                Items = order.Items
            };
        }
        private static Order DtoToModel(OrderDTO orderDTO)
        {
            return new Order
            {
                Id = orderDTO.Id,
                CustomerId = orderDTO.Customer.Id,
                Items = orderDTO.Items,
                CreatedDate = orderDTO.CreatedDate,
            };
        }

        private async Task<Order> EnsureOrderExists(int orderId)
        {
            var order = await _context.Order.AsNoTracking().FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} was not found.");

            return order;
        }

    }
}

using LanchesDoTioAPI.Adapters;
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

            return OrderAdapter.ModelToDto(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetByCustomer(int customerId)
        {
            var orders = await _context.Order.Include(x => x.Customer).Include(x => x.Items).AsNoTracking()
                .Where(x => x.CustomerId == customerId).Select(x => OrderAdapter.ModelToDto(x)).ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            var ordersQuery = _context.Order.Include(x=> x.Customer).Include(x => x.Items).ThenInclude(x=> x.Meal)
                .ThenInclude(x => x.PriceHistoryList).Select(x => OrderAdapter.ModelToDto(x));
            return await ordersQuery.AsNoTracking().ToListAsync();
        }

        public async Task<OrderDTO> Create(OrderDTO OrderDTO)
        {
            var order = OrderAdapter.DtoToModel(OrderDTO);
            _context.Order.Add(order);
            var mealId = await _context.SaveChangesAsync();
            OrderDTO.Id = mealId;
            return OrderDTO;
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

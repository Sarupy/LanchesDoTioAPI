using LanchesDoTioAPI.Data;
using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesDoTioAPI.Services.Implemetations
{
    public class CustomerService
    {
        private readonly LanchesContext _context;
        public CustomerService(LanchesContext context)
        {
            _context = context;
        }

        public async Task<CustomerDTO> GetById(int customerId)
        {
            var customer = await EnsureCustomerExists(customerId);
            return ModelToDto(customer);
        }
        public async Task<IEnumerable<CustomerDTO>> GetAll()
        {
            var allCustomersQuery = _context.Customer.Include(x => x.Orders).Select(x => ModelToDto(x));
            return await allCustomersQuery.AsNoTracking().ToListAsync();
        }

        public async Task<CustomerDTO> Create(CustomerDTO customerDTO)
        {
            var customer = DtoToModel(customerDTO);
            _context.Customer.Add(customer);
            var mealId = await _context.SaveChangesAsync();
            customerDTO.Id = mealId;
            return customerDTO;
        }
        public async Task<CustomerDTO> Pay(int customerId, decimal value)
        {
            var customer = await EnsureCustomerExists(customerId);

            customer.Orders.Add(new Order(value));

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return ModelToDto(customer);
        }


        private static CustomerDTO ModelToDto(Customer customer)
        {
            decimal debt = 0;

            if (customer.Orders?.Count > 0)
                debt = customer.Orders.Sum(x => x.getTotalPrice());

            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Debt = debt
            };
        }
        private static Customer DtoToModel(CustomerDTO customerDTO)
        {
            return new Customer
            {
                Id = customerDTO.Id,
                Name = customerDTO.Name,
            };
        }

        public async Task Delete(int customerId)
        {
            var customer = await EnsureCustomerExists(customerId);

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
        }
        private async Task<Customer> EnsureCustomerExists(int customerId)
        {
            var customer = await _context.Customer.FindAsync(customerId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {customerId} was not found.");

            return customer;
        }

    }
}

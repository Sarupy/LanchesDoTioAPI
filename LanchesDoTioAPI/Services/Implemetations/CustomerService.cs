using LanchesDoTioAPI.Data;
using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Mappers;
using LanchesDoTioAPI.Models;
using LanchesDoTioAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesDoTioAPI.Services.Implemetations
{
    public class CustomerService : ICustomerService
    {
        private readonly LanchesContext _context;
        public CustomerService(LanchesContext context)
        {
            _context = context;
        }

        public async Task<CustomerDTO> GetById(int customerId)
        {
            var customer = await EnsureCustomerExists(customerId);
            return CustomerMapper.ModelToDto(customer);
        }
        public async Task<IEnumerable<CustomerDTO>> GetAll()
        {
            //TODO: This does not performs well, maybe add static property OrderPrice to Order, to avoid those includes to get the actual prices
            var allCustomersQuery = _context.Customer.Include(x => x.Orders).ThenInclude(x=> x.Items)
                .ThenInclude(x=> x.Meal).ThenInclude(x=>x.PriceHistoryList);
            return (await allCustomersQuery.AsNoTracking().ToListAsync()).Select(x => CustomerMapper.ModelToDto(x));
        }

        public async Task<CustomerDTO> Create(CustomerDTO customerDTO)
        {
            var customer = CustomerMapper.DtoToModel(customerDTO);
            _context.Customer.Add(customer);
            var customerId = await _context.SaveChangesAsync();
            customerDTO.Id = customerId;
            return customerDTO;
        }
        public async Task<CustomerDTO> Pay(int customerId, decimal value)
        {
            var customer = await EnsureCustomerExists(customerId);

            customer.Orders.Add(new Order(value));

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CustomerMapper.ModelToDto(customer);
        }
        public async Task<CustomerDTO> Rename(int customerId, string newName)
        {
            var customer = await EnsureCustomerExists(customerId);

            if (string.IsNullOrWhiteSpace(newName))
                throw new BadHttpRequestException("New name cannot be empty.");

            if (newName != customer.Name)
            {
                customer.Name = newName;
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return CustomerMapper.ModelToDto(customer);
        }

        public async Task Delete(int customerId)
        {
            var customer = await EnsureCustomerExists(customerId);

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
        }
        private async Task<Customer> EnsureCustomerExists(int customerId)
        {
            var customer = _context.Customer.Include(x => x.Orders).ThenInclude(x => x.Items)
                .ThenInclude(x => x.Meal).ThenInclude(x => x.PriceHistoryList).FirstOrDefault(x=> x.Id == customerId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {customerId} was not found.");

            return customer;
        }

    }
}

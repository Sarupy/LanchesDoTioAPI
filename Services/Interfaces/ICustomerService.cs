using LanchesDoTioAPI.DTO;

namespace LanchesDoTioAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomerDTO> Pay(int customerId, decimal value);
        public Task<CustomerDTO> GetById(int customerId);
        public Task<IEnumerable<CustomerDTO>> GetAll();
        public Task<CustomerDTO> Rename(int customerId, string newName);
        public Task<CustomerDTO> Create(CustomerDTO customerDTO);
        public Task Delete(int customerId);
    }
}

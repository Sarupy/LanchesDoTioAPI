using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanchesDoTioAPI.Data;
using LanchesDoTioAPI.Models;
using LanchesDoTioAPI.Services.Interfaces;
using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Services.Implemetations;

namespace LanchesDoTioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly LanchesContext _context;
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomersController(LanchesContext context, ICustomerService customerService,  ILogger<CustomersController> logger)
        {
            _context = context;
            _customerService = customerService;
            _logger = logger;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            try
            {
                var allCustomers = await _customerService.GetAll();
                return Ok(allCustomers);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message,
                DateTime.UtcNow.ToLongTimeString());
                return Ok(new List<CustomerDTO>());
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetById(id);

            return Ok(customer);
        }

        // PUT: api/Customers/rename/5?newName={newName}
        [HttpPut("Rename/{id}")]
        public async Task<ActionResult<CustomerDTO>> RenameCustomer(int id, [FromQuery] string newName)
        {
            var customer = await _customerService.Rename(id, newName);

            return Ok(customer);
        }


        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CustomerDTO customerDTO)
        {
            var customer = await _customerService.Create(customerDTO);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.Delete(id);

            return NoContent();
        }
    }
}

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

namespace LanchesDoTioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly LanchesContext _context;
        private readonly IOrderService _orderService;
        public OrdersController(LanchesContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetAll();

            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderService.GetById(id);

            return Ok(order);
        }

        // GET: api/Orders/ByCustomer/5
        [HttpGet("ByCustomer/{id}")]
        public async Task<ActionResult<Order>> GetOrdersByCustomerId(int id)
        {
            var order = await _orderService.GetByCustomer(id);

            return Ok(order);
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDTO orderDTO)
        {
            var order = await _orderService.Create(orderDTO);

            return CreatedAtAction("GetOrderById", new { id = order.Id }, order);
        }

        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            var orders = await _context.Order.AsNoTracking().ToListAsync();
            _context.Order.RemoveRange(orders);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

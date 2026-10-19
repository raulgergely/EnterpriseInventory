using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventoryApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrdersById(int id)
        {
            var orderResult = await _context.Orders.FindAsync(id);
            if (orderResult == null)
            {
                return NotFound();
            }
            return orderResult;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> createOrder([FromBody] Order order)
        {

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrdersById), new { id = order.Id }, order);
        }
    }
}

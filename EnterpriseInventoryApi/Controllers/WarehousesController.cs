using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Models;

namespace EnterpriseInventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WarehousesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            return await _context.Warehouses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehousesById(int id) {
            var warehouses = await _context.Warehouses.FindAsync(id);

            if (warehouses == null) {
                return NotFound();
            }
            
            return warehouses;
        }
       
        [HttpPost]
        public async Task<ActionResult<Warehouse>> CreateWarehouse([FromBody] Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWarehousesById), new { id = warehouse.Id }, warehouse);
        }
    }
}
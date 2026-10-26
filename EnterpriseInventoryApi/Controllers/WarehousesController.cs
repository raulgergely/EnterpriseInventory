using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.DTO;
using EnterpriseInventoryApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<WarehouseDTO>>> GetWarehouses()
        {
            var warehouses = await _context.Warehouses
                .Select(w => new WarehouseDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    Location = w.Location
                })
                .ToListAsync();

            return Ok(warehouses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseDTO>> GetWarehousesById(int id)
        {
            var warehouseDto = await _context.Warehouses
                .Where(w => w.Id == id)
                .Select(w => new WarehouseDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    Location = w.Location
                })
                .FirstOrDefaultAsync();

            if (warehouseDto == null)
            {
                return NotFound();
            }

            return Ok(warehouseDto);
        }

        [HttpPost]
        public async Task<ActionResult<WarehouseDTO>> CreateWarehouse([FromBody] WarehouseDTO warehouseDto)
        {
            var warehouse = new Warehouse
            {
                Name = warehouseDto.Name,
                Location = warehouseDto.Location
            };

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();

            warehouseDto.Id = warehouse.Id;

            return CreatedAtAction(nameof(GetWarehousesById), new { id = warehouse.Id }, warehouseDto);
        }
    }
}
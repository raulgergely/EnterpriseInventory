using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductWarehouse>>> getStocks() {
            return await _context.ProductWarehouses
                .Include(pw => pw.Product)
                .Include(pw => pw.Warehouse)
                .ToListAsync();            
        }

        [HttpGet("{productId}/{warehouseId}")]
        public async Task<ActionResult<ProductWarehouse>> getStocksById(int productId,int warehouseId) { 
            var productWarehouse = await _context.ProductWarehouses.FindAsync(productId,warehouseId);
            if (productWarehouse == null) {
                return NotFound();
            }
            return productWarehouse;
        }

        [HttpPost]
        public async Task<ActionResult<ProductWarehouse>> createStock([FromBody] ProductWarehouse productWarehouse) {
            bool stockExists = await _context.ProductWarehouses.AnyAsync(pw => pw.ProductId == productWarehouse.ProductId && pw.WarehouseId == productWarehouse.WarehouseId);
            if (stockExists) {
                return BadRequest("Stock already exists for this product and warehouse.");
            }
            _context.ProductWarehouses.Add(productWarehouse);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(getStocksById), new { productId = productWarehouse.ProductId, warehouseId = productWarehouse.WarehouseId }, productWarehouse);
        }
        [HttpPatch]
        public async Task<ActionResult<ProductWarehouse>> UpdateStockQuantity([FromBody] ProductWarehouse productWarehouse) {
            var ProdWar = await _context.ProductWarehouses.FindAsync(productWarehouse.ProductId, productWarehouse.WarehouseId);
            if (ProdWar == null)
            {
                return NotFound("Stock not found for this product and warehouse.");
            }
           
            ProdWar.StockQuantity = productWarehouse.StockQuantity;
            await _context.SaveChangesAsync();
            return Ok(ProdWar);
        }
     } 
}

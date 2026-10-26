using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.DTO;
using EnterpriseInventoryApi.Entity;
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
        public async Task<ActionResult<IEnumerable<ProductWarehouseDTO>>> GetStocks() {
            var stocks = await _context.ProductWarehouses
                .Include(pw => pw.Product)
                .Include(pw => pw.Warehouse)
                .Select(pw => new ProductWarehouseDTO
                {
                    ProductId = pw.ProductId,
                    ProductName = pw.Product.Name,
                    WarehouseId = pw.WarehouseId,
                    WarehouseName = pw.Warehouse.Name,
                    StockQuantity = pw.StockQuantity
                })
                .ToListAsync();

            return Ok(stocks);
        }

        [HttpGet("{productId}/{warehouseId}")]
        public async Task<ActionResult<ProductWarehouseDTO>> GetStocksById(int productId,int warehouseId) {
            var stockDto = await _context.ProductWarehouses
                .Include(pw => pw.Product)
                .Include(pw => pw.Warehouse)
                .Where(pw => pw.ProductId == productId && pw.WarehouseId == warehouseId)
                .Select(pw => new ProductWarehouseDTO
                {
                    ProductId = pw.ProductId,
                    ProductName = pw.Product.Name,
                    WarehouseId = pw.WarehouseId,
                    WarehouseName = pw.Warehouse.Name,
                    StockQuantity = pw.StockQuantity
                })
                .FirstOrDefaultAsync();

            if (stockDto == null)
            {
                return NotFound();
            }

            return Ok(stockDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductWarehouseDTO>> CreateStock([FromBody] ProductWarehouseDTO productWarehouseDTO) {
            var productExists = await _context.Products.AnyAsync(p => p.Id == productWarehouseDTO.ProductId);
            if (!productExists)
            {
                return BadRequest(new { message = "The specified product does not exist!" });
            }

            var warehouseExists = await _context.Warehouses.AnyAsync(w => w.Id == productWarehouseDTO.WarehouseId);
            if (!warehouseExists)
            {
                return BadRequest(new { message = "The specified warehouse does not exist!" });
            }

            bool stockExists = await _context.ProductWarehouses
                .AnyAsync(pw => pw.ProductId == productWarehouseDTO.ProductId && pw.WarehouseId == productWarehouseDTO.WarehouseId);
            if (stockExists)
            {
                return BadRequest(new { message = "Stock already exists for this product and warehouse." });
            }

            var productWarehouse = new ProductWarehouse
            {
                ProductId = productWarehouseDTO.ProductId,
                WarehouseId = productWarehouseDTO.WarehouseId,
                StockQuantity = productWarehouseDTO.StockQuantity
            };

            _context.ProductWarehouses.Add(productWarehouse);
            await _context.SaveChangesAsync();

            var createdStockDto = await _context.ProductWarehouses
                .Include(pw => pw.Product)
                .Include(pw => pw.Warehouse)
                .Where(pw => pw.ProductId == productWarehouseDTO.ProductId && pw.WarehouseId == productWarehouseDTO.WarehouseId)
                .Select(pw => new ProductWarehouseDTO
                {
                    ProductId = pw.ProductId,
                    ProductName = pw.Product.Name,
                    WarehouseId = pw.WarehouseId,
                    WarehouseName = pw.Warehouse.Name,
                    StockQuantity = pw.StockQuantity
                })
                .FirstAsync();

            return CreatedAtAction(nameof(GetStocksById), new { productId = productWarehouseDTO.ProductId, warehouseId = productWarehouseDTO.WarehouseId }, createdStockDto);
        }
        [HttpPatch]
        public async Task<ActionResult<ProductWarehouseDTO>> UpdateStockQuantity([FromBody] ProductWarehouseDTO productWarehouseDTO)
        {
            var prodWar = await _context.ProductWarehouses
                .Include(pw => pw.Product)
                .Include(pw => pw.Warehouse)
                .FirstOrDefaultAsync(pw => pw.ProductId == productWarehouseDTO.ProductId && pw.WarehouseId == productWarehouseDTO.WarehouseId);

            if (prodWar == null)
            {
                return NotFound(new { message = "Stock not found for this product and warehouse." });
            }

            prodWar.StockQuantity = productWarehouseDTO.StockQuantity;
            await _context.SaveChangesAsync();

            var updatedStockDto = new ProductWarehouseDTO
            {
                ProductId = prodWar.ProductId,
                ProductName = prodWar.Product?.Name,
                WarehouseId = prodWar.WarehouseId,
                WarehouseName = prodWar.Warehouse?.Name,
                StockQuantity = prodWar.StockQuantity
            };

            return Ok(updatedStockDto);
        }
    } 
}

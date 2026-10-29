using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Entity;
using EnterpriseInventoryApi.DTO;

namespace EnterpriseInventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _context.Products
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    SKU = p.SKU,
                    UnitPrice = p.UnitPrice,
                    MinimumStockLevel = p.MinimumStockLevel,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier.Name
                })
                .ToListAsync();

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductsById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.Id == id)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    SKU = p.SKU,
                    UnitPrice = p.UnitPrice,
                    MinimumStockLevel = p.MinimumStockLevel,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier.Name
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] ProductDTO productDto)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == productDto.CategoryId);
            if (!categoryExists)
            {
                return BadRequest(new { message = "The specified category does not exist!" });
            }

            var supplierExists = await _context.Suppliers.AnyAsync(s => s.Id == productDto.SupplierId);
            if (!supplierExists)
            {
                return BadRequest(new { message = "The specified supplier does not exist!" });
            }
            var product = new Product
            {
                Name = productDto.Name,
                SKU = productDto.SKU,
                UnitPrice = productDto.UnitPrice,
                MinimumStockLevel = productDto.MinimumStockLevel,
                CategoryId = productDto.CategoryId,
                SupplierId = productDto.SupplierId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();


            var createdProductDto = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.Id == product.Id)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    SKU = p.SKU,
                    UnitPrice = p.UnitPrice,
                    MinimumStockLevel = p.MinimumStockLevel,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier.Name
                })
                .FirstAsync();

            return CreatedAtAction(nameof(GetProductsById), new { id = createdProductDto.Id }, createdProductDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
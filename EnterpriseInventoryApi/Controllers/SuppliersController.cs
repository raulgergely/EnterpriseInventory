using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.DTO;
using EnterpriseInventoryApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
        {
            var suppliers = await _context.Suppliers
                .Select(s => new SupplierDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    ContactEmail = s.ContactEmail,
                    Phone = s.Phone
                })
                .ToListAsync();

            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTO>> GetSuppliersById(int id)
        {
            var supplierDto = await _context.Suppliers
                .Where(s => s.Id == id)
                .Select(s => new SupplierDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    ContactEmail = s.ContactEmail,
                    Phone = s.Phone
                })
                .FirstOrDefaultAsync();

            if (supplierDto == null)
            {
                return NotFound();
            }

            return Ok(supplierDto);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> CreateSupplier([FromBody] SupplierDTO supplierDTO)
        {
            var supplier = new Supplier
            {
                Name = supplierDTO.Name,
                ContactEmail = supplierDTO.ContactEmail,
                Phone = supplierDTO.Phone
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            var createdSupplierDto = new SupplierDTO
            {
                Id = supplier.Id,
                Name = supplier.Name,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone
            };

            return CreatedAtAction(nameof(GetSuppliersById), new { id = createdSupplierDto.Id }, createdSupplierDto);
        }
    }
}
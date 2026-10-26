using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Entity;
using EnterpriseInventoryApi.DTO;

namespace EnterpriseInventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _context.Customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                Email = c.Email,
                Address = c.Address
            }).ToListAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomersById(int id)
        {
            var customerResult = await _context.Customers.Where(c => c.Id == id)
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    Email = c.Email,
                    Address = c.Address
                })
                .FirstOrDefaultAsync();
            if (customerResult == null)
            {
                return NotFound();
            }
            return Ok(customerResult);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CustomerDTO customerDto)
        {
            var customer = new Customer
            {
                CompanyName = customerDto.CompanyName,
                Email = customerDto.Email,
                Address = customerDto.Address
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customerDto.Id = customer.Id;

            return CreatedAtAction(nameof(GetCustomersById), new { id = customer.Id }, customer);
        }
    }
}
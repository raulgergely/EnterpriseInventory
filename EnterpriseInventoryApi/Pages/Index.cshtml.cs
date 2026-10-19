using EnterpriseInventoryApi.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventoryApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public int TotalProductsCount { get; set; }
        public decimal AverageUnitPrice { get; set; }

        public async Task OnGetAsync()
        {
            TotalProductsCount = await _context.Products.CountAsync();

            AverageUnitPrice = TotalProductsCount > 0
                ? await _context.Products.AverageAsync(p => p.UnitPrice)
                : 0m;
        }
    }
}
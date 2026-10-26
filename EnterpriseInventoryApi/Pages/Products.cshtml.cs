using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventoryApi.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProductsModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public int TotalProductsCount { get; set; }
        public decimal AverageUnitPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        public async Task OnGetAsync()
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                query = query.Where(p => p.Name.Contains(SearchQuery) || p.SKU.Contains(SearchQuery));
            }

            TotalProductsCount = await query.CountAsync();

            AverageUnitPrice = TotalProductsCount > 0
                ? await query.AverageAsync(p => p.UnitPrice)
                : 0;

            TotalPages = (int)Math.Ceiling(TotalProductsCount / (double)PageSize);

            if (CurrentPage < 1) CurrentPage = 1;
            if (TotalPages > 0 && CurrentPage > TotalPages) CurrentPage = TotalPages;

            query = SortOrder switch
            {
                "price_asc" => query.OrderBy(p => p.UnitPrice),
                "price_desc" => query.OrderByDescending(p => p.UnitPrice),
                _ => query.OrderBy(p => p.Id)
            };

            Products = await query
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
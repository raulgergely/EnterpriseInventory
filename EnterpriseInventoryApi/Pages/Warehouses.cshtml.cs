using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventoryApi.Pages
{
    public class WarehouseModel : PageModel
    {
        private readonly AppDbContext _context; 

        public WarehouseModel(AppDbContext context)
        {
            _context = context;
        }
        public IList<ProductWarehouse> ProductWarehouses { get; set; } = default!;

        public IList<Warehouse> Warehouses { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedWarehouseId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public int TotalProductsCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 15;
        public async Task OnGetAsync()
        {
            Warehouses = await _context.Warehouses.ToListAsync();

            var query = _context.ProductWarehouses
                .Include(p => p.Product)
                .Include(p => p.Warehouse)
                .AsQueryable();

            query = query.Where(pw => pw.StockQuantity > 0);

            //selected warehouse
            if (SelectedWarehouseId.HasValue && SelectedWarehouseId.Value > 0)
            {
                query = query.Where(pw => pw.WarehouseId == SelectedWarehouseId.Value);
            }

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                query = query.Where(pw => pw.Product.Name.Contains(SearchQuery)
                                       || pw.Product.SKU.Contains(SearchQuery)
                                       || pw.Warehouse.Name.Contains(SearchQuery));
            }

            TotalProductsCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(TotalProductsCount / (double)PageSize);
            if (TotalPages == 0) TotalPages = 1;

            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages) CurrentPage = TotalPages;

            ProductWarehouses = await query
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}

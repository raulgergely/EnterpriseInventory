using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Entity;

namespace EnterpriseInventoryApi.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly AppDbContext _context;

        public OrdersModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }

        public async Task OnGetAsync()
        {
            int pageSize = 10;

            IQueryable<Order> query = _context.Orders
                .Include(o => o.Customer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SelectedStatus))
            {
                query = query.Where(o => o.Status == SelectedStatus);
            }

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                query = query.Where(o => o.Id.ToString().Contains(SearchQuery) ||
                                         (o.Customer != null && o.Customer.CompanyName.Contains(SearchQuery)));
            }

            int totalCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (CurrentPage < 1) CurrentPage = 1;
            if (TotalPages > 0 && CurrentPage > TotalPages) CurrentPage = TotalPages;

            Orders = await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
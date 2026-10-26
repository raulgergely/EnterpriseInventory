using EnterpriseInventoryApi.Data;
using EnterpriseInventoryApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventoryApi.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CustomersModel> _logger;

        public CustomersModel(AppDbContext context, ILogger<CustomersModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Customer> Customers { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public int TotalCustomersCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        public async Task OnGetAsync()
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                query = query.Where(c => c.CompanyName.Contains(SearchQuery)
                                      || c.Email.Contains(SearchQuery)
                                      || c.Address.Contains(SearchQuery));
            }

            TotalCustomersCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(TotalCustomersCount / (double)PageSize);
            if (TotalPages == 0) TotalPages = 1;

            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages) CurrentPage = TotalPages;

            Customers = await query
                .OrderByDescending(c => c.Id)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
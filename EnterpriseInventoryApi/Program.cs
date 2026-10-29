using EnterpriseInventoryApi.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Globalization;
//Initialize the builder
var builder = WebApplication.CreateBuilder(args);

//Register services & configure the JSON serializer
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
//razor pages
builder.Services.AddRazorPages();

//set support for different languages
var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ro") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en"); 
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

//database ocntext  using sql server and connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//support for API
builder.Services.AddOpenApi();
//service for accessing the current HTTP context from anywhere in the application
builder.Services.AddHttpContextAccessor();
// set an localization service
builder.Services.AddTransient<JsonLocalizer>();

//build web application
var app = builder.Build();

//set for using static files /resources (js ,css,images)
app.UseStaticFiles();
//set for using localization
app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
//Automatic redirection from HTTP to HTTPS
app.UseHttpsRedirection();
app.UseAuthorization();

//default route for MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
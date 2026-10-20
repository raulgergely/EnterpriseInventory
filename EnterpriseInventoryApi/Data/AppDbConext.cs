using Microsoft.EntityFrameworkCore;
using EnterpriseInventoryApi.Models;

namespace EnterpriseInventoryApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductWarehouse>()
                .HasKey(pw => new { pw.ProductId, pw.WarehouseId });

            modelBuilder.Entity<ProductWarehouse>()
                .HasOne(pw => pw.Product)
                .WithMany(p => p.ProductWarehouses)
                .HasForeignKey(pw => pw.ProductId);

            modelBuilder.Entity<ProductWarehouse>()
                .HasOne(pw => pw.Warehouse)
                .WithMany(w => w.ProductWarehouses)
                .HasForeignKey(pw => pw.WarehouseId);


             
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Scaun de Birou Ergonomic", SKU = "FURN-CHAIR-01", UnitPrice = 850.00m, MinimumStockLevel = 4, CategoryId = 3, SupplierId = 2 },
                new Product { Id = 2, Name = "Laptop Business 15 inch", SKU = "IT-LAP-001", UnitPrice = 4200.50m, MinimumStockLevel = 5, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 3, Name = "Monitor LED 27 4K", SKU = "PER-MON-002", UnitPrice = 1500.00m, MinimumStockLevel = 3, CategoryId = 2, SupplierId = 1 },
                new Product { Id = 4, Name = "Tastatură Mecanică RGB", SKU = "PER-KEY-003", UnitPrice = 350.00m, MinimumStockLevel = 10, CategoryId = 2, SupplierId = 2 },
                new Product { Id = 5, Name = "Scaun Directorial Piele", SKU = "MOB-CHAIR-004", UnitPrice = 1200.00m, MinimumStockLevel = 2, CategoryId = 4, SupplierId = 2 },
                new Product { Id = 6, Name = "Aparat de Cafea Espresso", SKU = "ELC-COF-005", UnitPrice = 2800.00m, MinimumStockLevel = 3, CategoryId = 3, SupplierId = 3 },
                new Product { Id = 7, Name = "Mouse Wireless Ergonomic", SKU = "PER-MOU-006", UnitPrice = 120.50m, MinimumStockLevel = 15, CategoryId = 2, SupplierId = 2 },
                new Product { Id = 8, Name = "Căști Gaming Wireless", SKU = "PER-HP-007", UnitPrice = 450.00m, MinimumStockLevel = 8, CategoryId = 2, SupplierId = 1 },
                new Product { Id = 9, Name = "Hub USB-C 7-în-1", SKU = "IT-ACC-008", UnitPrice = 180.00m, MinimumStockLevel = 12, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 10, Name = "Geantă Laptop 15.6 inch", SKU = "IT-BAG-009", UnitPrice = 160.00m, MinimumStockLevel = 10, CategoryId = 1, SupplierId = 2 },
                new Product { Id = 11, Name = "Stand Reglabil Laptop", SKU = "IT-STD-010", UnitPrice = 130.00m, MinimumStockLevel = 7, CategoryId = 1, SupplierId = 2 },
                new Product { Id = 12, Name = "Proiector LED Full HD", SKU = "IT-PROJ-011", UnitPrice = 1950.00m, MinimumStockLevel = 3, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 13, Name = "Imprimantă Multifuncțională Laser", SKU = "PER-PRN-012", UnitPrice = 920.00m, MinimumStockLevel = 4, CategoryId = 2, SupplierId = 2 },
                new Product { Id = 14, Name = "Router Wireless Wi-Fi 6", SKU = "IT-NET-013", UnitPrice = 540.00m, MinimumStockLevel = 6, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 15, Name = "Hard Disk Extern 2TB", SKU = "IT-HDD-014", UnitPrice = 340.00m, MinimumStockLevel = 10, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 16, Name = "SSD Extern Portabil 1TB", SKU = "IT-SSD-015", UnitPrice = 480.00m, MinimumStockLevel = 10, CategoryId = 1, SupplierId = 1 },
                new Product { Id = 17, Name = "Cuptor cu Microunde Digital", SKU = "ELC-MIC-016", UnitPrice = 650.00m, MinimumStockLevel = 4, CategoryId = 3, SupplierId = 3 },
                new Product { Id = 18, Name = "Fierbător Electric Inox", SKU = "ELC-KET-017", UnitPrice = 150.00m, MinimumStockLevel = 8, CategoryId = 6, SupplierId = 2 },
                new Product { Id = 19, Name = "Lampă de Birou LED cu Încărcător Wireless", SKU = "MOB-LMP-019", UnitPrice = 220.00m, MinimumStockLevel = 6, CategoryId = 4, SupplierId = 2 },
                new Product { Id = 20, Name = "Dulap Arhivă Metalic", SKU = "MOB-CAB-020", UnitPrice = 1100.00m, MinimumStockLevel = 2, CategoryId = 4, SupplierId = 2 }
            );
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Office Solutions SA", ContactEmail = "sales@officesolutions.ro", Phone = "0722334455" },
                new Supplier { Id = 2, Name = "Home & Kitchen Distribution", ContactEmail = "orders@homekitchen.ro", Phone = "0733445566" },
                new Supplier { Id = 3, Name = "Tech Distribution SRL", ContactEmail = "comenzi@techdistrib.ro", Phone = "0721112233" },
                new Supplier { Id = 4, Name = "Global Office Supply", ContactEmail = "office@globaloffice.ro", Phone = "0733445566" },
                new Supplier { Id = 5, Name = "Home & Kitchen Pro", ContactEmail = "b2b@homekitchen.ro", Phone = "0744556677" }
            );

            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { Id = 1, Name = "Depozit Central Timișoara", Location = "Calea Aradului Km 4, Timișoara" },
                new Warehouse { Id = 2, Name = "Hub Logistic Regional București", Location = "Autostrada Militari Km 13, București" },
                new Warehouse { Id = 3, Name = "Depozit Regional Transilvania", Location = "Bulevardul Muncii Nr. 12, Cluj-Napoca" },
                new Warehouse { Id = 4, Name = "Centru Logistic Nord-Est", Location = "Calea Chișinăului Nr. 24, Iași" },
                new Warehouse { Id = 5, Name = "Depozit Local Fratelia Sud", Location = "Strada Ana Ipătescu Nr. 10, Timișoara" },
                new Warehouse { Id = 6, Name = "Hub Logistic Port Constanța", Location = "Incinta Port, Poarta 7, Constanța" },
                new Warehouse { Id = 7, Name = "Depozit Regional Centru", Location = "Strada Zizinului Nr. 115, Brașov" },
                new Warehouse { Id = 8, Name = "Centru Distribuție Oltenia", Location = "Calea București Nr. 150, Craiova" }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronice" ,Description = "Gadgeturi și dispozitive" },
                new Category { Id = 2, Name = "Electrocasnice" ,Description = "Aparate pentru uz casnic și bucătărie" },
                new Category { Id = 3, Name = "Mobilier" ,Description = "Mobilă de birou și scaune ergonomice" },
                new Category { Id = 4, Name = "IT & Laptopuri" ,Description = "Sisteme portabile și componente PC" },
                new Category { Id = 5, Name = "Periferice" ,Description = "Mouse-uri, tastaturi și monitoare" },
                new Category { Id = 6, Name = "Electrocasnice" ,Description = "Echipamente pentru casă și bucătărie" },
                new Category { Id = 7, Name = "Mobilier Office" ,Description = "Scaune, birouri și dulapuri de arhivă" }
            );
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, CompanyName = "Alpha Corp SRL", Email = "achizitii@alphacorp.ro", Address = "Bulevardul Victoriei 12, Timișoara" },
                new Customer { Id = 2, CompanyName = "Beta Retail SA", Email = "contact@betaretail.ro", Address = "Strada Republicii 45, Arad" },
                new Customer { Id = 3, CompanyName = "Gamma Logistica SRL", Email = "dispecerat@gammalogistica.ro", Address = "Calea Aradului 115, Timișoara" },
                new Customer { Id = 4, CompanyName = "Delta Construct Impex", Email = "office@deltaconstruct.ro", Address = "Strada Mureșului 8, Timișoara" },
                new Customer { Id = 5, CompanyName = "Epsilon Tech Solutions", Email = "contact@epsilontech.ro", Address = "Bulevardul Take Ionescu 22, Timișoara" },
                new Customer { Id = 6, CompanyName = "Zeta Distribution Group", Email = "comenzi@zetagroup.ro", Address = "Calea Lugojului 45, Timișoara" },
                new Customer { Id = 7, CompanyName = "Omega Servicii Industriale", Email = "suport@omegasev.ro", Address = "Strada Aurel Vlaicu 14, Arad" },
                new Customer { Id = 8, CompanyName = "Transilvania Agro SRL", Email = "office@transilvaniaagro.ro", Address = "Piața Libertății 3, Timișoara" },
                new Customer { Id = 9, CompanyName = "Nordic Pharma Distribution", Email = "achizitii@nordicpharma.ro", Address = "Strada Telegrafului 19, Timișoara" },
                new Customer { Id = 10, CompanyName = "Apex Media Production", Email = "hello@apexmedia.ro", Address = "Bulevardul Revoluției 10, Arad" },
                new Customer { Id = 11, CompanyName = "Balkan Express Transport", Email = "logistica@balkanexpress.ro", Address = "Calea Torontalului 60, Timișoara" },
                new Customer { Id = 12, CompanyName = "Crisana Food Prod", Email = "vanzari@crisanafood.ro", Address = "Strada Crișan 5, Arad" },
                new Customer { Id = 13, CompanyName = "Danubius Software House", Email = "dev@danubius-soft.ro", Address = "Splaiul Nicolae Titulescu 4, Timișoara" },
                new Customer { Id = 14, CompanyName = "Eco Clean Timișoara", Email = "contact@ecoclean-tm.ro", Address = "Strada Fratelia 28, Timișoara" },
                new Customer { Id = 15, CompanyName = "Frontera Impex SRL", Email = "office@frontera-impex.ro", Address = "Calea Timișoarei 88, Arad" },
                new Customer { Id = 16, CompanyName = "Global Auto Parts", Email = "piese@globalauto.ro", Address = "Bulevardul Eroilor de la Tisa 35, Timișoara" },
                new Customer { Id = 17, CompanyName = "Hestia Trading", Email = "comenzi@hestiatrading.ro", Address = "Strada Ștefan cel Mare 12, Timișoara" },
                new Customer { Id = 18, CompanyName = "Imperial Print Shop", Email = "print@imperialshop.ro", Address = "Piața Unirii 7, Timișoara" },
                new Customer { Id = 19, CompanyName = "Jupiter Security Systems", Email = "dispecerat@jupitersystems.ro", Address = "Strada Ioan Slavici 45, Arad" },
                new Customer { Id = 20, CompanyName = "Krono Wood Industry", Email = "office@kronowood.ro", Address = "Calea Buziașului 11, Timișoara" },
                new Customer { Id = 21, CompanyName = "Luna Cosmetics Distribution", Email = "comenzi@lunacosmetics.ro", Address = "Strada Alba Iulia 6, Timișoara" },
                new Customer { Id = 22, CompanyName = "Metropolis Real Estate", Email = "contact@metropolis-re.ro", Address = "Bulevardul General Dragalina 9, Timișoara" }
            );
            modelBuilder.Entity<ProductWarehouse>().HasData(

                new ProductWarehouse { ProductId = 1, WarehouseId = 1, StockQuantity = 0 },
                new ProductWarehouse { ProductId = 1, WarehouseId = 2, StockQuantity = 250 },
                new ProductWarehouse { ProductId = 1, WarehouseId = 3, StockQuantity = 112 },
                new ProductWarehouse { ProductId = 1, WarehouseId = 4, StockQuantity = 30 },
                new ProductWarehouse { ProductId = 1, WarehouseId = 5, StockQuantity = 75 },
                new ProductWarehouse { ProductId = 1, WarehouseId = 6, StockQuantity = 120 },
                new ProductWarehouse { ProductId = 1, WarehouseId = 7, StockQuantity = 20 },
                new ProductWarehouse { ProductId = 1, WarehouseId = 8, StockQuantity = 100 },

                new ProductWarehouse { ProductId = 2, WarehouseId = 1, StockQuantity = 10 },
                new ProductWarehouse { ProductId = 2, WarehouseId = 2, StockQuantity = 180 },
                new ProductWarehouse { ProductId = 2, WarehouseId = 3, StockQuantity = 350 },
                new ProductWarehouse { ProductId = 2, WarehouseId = 4, StockQuantity = 120 },
                new ProductWarehouse { ProductId = 2, WarehouseId = 5, StockQuantity = 200 },
                new ProductWarehouse { ProductId = 2, WarehouseId = 6, StockQuantity = 150 },
                new ProductWarehouse { ProductId = 2, WarehouseId = 7, StockQuantity = 10 },
                new ProductWarehouse { ProductId = 2, WarehouseId = 8, StockQuantity = 50 },

                new ProductWarehouse { ProductId = 3, WarehouseId = 1, StockQuantity = 40 },
                new ProductWarehouse { ProductId = 3, WarehouseId = 2, StockQuantity = 35 },
                new ProductWarehouse { ProductId = 3, WarehouseId = 3, StockQuantity = 90 },
                new ProductWarehouse { ProductId = 3, WarehouseId = 4, StockQuantity = 130 },
                new ProductWarehouse { ProductId = 3, WarehouseId = 5, StockQuantity = 270 },
                new ProductWarehouse { ProductId = 3, WarehouseId = 6, StockQuantity = 170 },
                new ProductWarehouse { ProductId = 3, WarehouseId = 7, StockQuantity = 90 },
                new ProductWarehouse { ProductId = 3, WarehouseId = 8, StockQuantity = 25 },

                new ProductWarehouse { ProductId = 4, WarehouseId = 1, StockQuantity = 600 },
                new ProductWarehouse { ProductId = 4, WarehouseId = 2, StockQuantity = 330 },
                new ProductWarehouse { ProductId = 4, WarehouseId = 3, StockQuantity = 250 },
                new ProductWarehouse { ProductId = 4, WarehouseId = 4, StockQuantity = 190 },
                new ProductWarehouse { ProductId = 4, WarehouseId = 5, StockQuantity = 390 },
                new ProductWarehouse { ProductId = 4, WarehouseId = 6, StockQuantity = 420 },
                new ProductWarehouse { ProductId = 4, WarehouseId = 7, StockQuantity = 140 },
                new ProductWarehouse { ProductId = 4, WarehouseId = 8, StockQuantity = 500 },

                new ProductWarehouse { ProductId = 5, WarehouseId = 1, StockQuantity = 20 },
                new ProductWarehouse { ProductId = 5, WarehouseId = 2, StockQuantity = 30 },
                new ProductWarehouse { ProductId = 5, WarehouseId = 3, StockQuantity = 50 },
                new ProductWarehouse { ProductId = 5, WarehouseId = 4, StockQuantity = 65 },
                new ProductWarehouse { ProductId = 5, WarehouseId = 5, StockQuantity = 85 },
                new ProductWarehouse { ProductId = 5, WarehouseId = 6, StockQuantity = 35 },
                new ProductWarehouse { ProductId = 5, WarehouseId = 7, StockQuantity = 60 },
                new ProductWarehouse { ProductId = 5, WarehouseId = 8, StockQuantity = 20 },

                new ProductWarehouse { ProductId = 6, WarehouseId = 1, StockQuantity = 20 },
                new ProductWarehouse { ProductId = 6, WarehouseId = 2, StockQuantity = 15 },
                new ProductWarehouse { ProductId = 6, WarehouseId = 3, StockQuantity = 82 },
                new ProductWarehouse { ProductId = 6, WarehouseId = 4, StockQuantity = 90 },
                new ProductWarehouse { ProductId = 6, WarehouseId = 6, StockQuantity = 55 },
                new ProductWarehouse { ProductId = 6, WarehouseId = 7, StockQuantity = 40 },
                new ProductWarehouse { ProductId = 6, WarehouseId = 8, StockQuantity = 70 },

                new ProductWarehouse { ProductId = 7, WarehouseId = 1, StockQuantity = 200 },
                new ProductWarehouse { ProductId = 7, WarehouseId = 2, StockQuantity = 120 },
                new ProductWarehouse { ProductId = 7, WarehouseId = 3, StockQuantity = 85 },
                new ProductWarehouse { ProductId = 7, WarehouseId = 4, StockQuantity = 120 },
                new ProductWarehouse { ProductId = 7, WarehouseId = 5, StockQuantity = 322 },
                new ProductWarehouse { ProductId = 7, WarehouseId = 6, StockQuantity = 280 },
                new ProductWarehouse { ProductId = 7, WarehouseId = 7, StockQuantity = 160 },
                new ProductWarehouse { ProductId = 7, WarehouseId = 8, StockQuantity = 190 },

                new ProductWarehouse { ProductId = 8, WarehouseId = 1, StockQuantity = 500 },
                new ProductWarehouse { ProductId = 8, WarehouseId = 2, StockQuantity = 100 },
                new ProductWarehouse { ProductId = 8, WarehouseId = 3, StockQuantity = 220 },
                new ProductWarehouse { ProductId = 8, WarehouseId = 4, StockQuantity = 40 },
                new ProductWarehouse { ProductId = 8, WarehouseId = 5, StockQuantity = 110 },
                new ProductWarehouse { ProductId = 8, WarehouseId = 6, StockQuantity = 90 },
                new ProductWarehouse { ProductId = 8, WarehouseId = 7, StockQuantity = 0 },
                new ProductWarehouse { ProductId = 8, WarehouseId = 8, StockQuantity = 40 }
            );

        }
    }
}
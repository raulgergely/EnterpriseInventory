using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnterpriseInventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumStockLevel = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPriceAtOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductWarehouses",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWarehouses", x => new { x.ProductId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_ProductWarehouses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Gadgeturi și dispozitive", "Electronice" },
                    { 2, "Aparate pentru uz casnic și bucătărie", "Electrocasnice" },
                    { 3, "Mobilă de birou și scaune ergonomice", "Mobilier" },
                    { 4, "Sisteme portabile și componente PC", "IT & Laptopuri" },
                    { 5, "Mouse-uri, tastaturi și monitoare", "Periferice" },
                    { 6, "Echipamente pentru casă și bucătărie", "Electrocasnice" },
                    { 7, "Scaune, birouri și dulapuri de arhivă", "Mobilier Office" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CompanyName", "Email" },
                values: new object[,]
                {
                    { 1, "Bulevardul Victoriei 12, Timișoara", "Alpha Corp SRL", "achizitii@alphacorp.ro" },
                    { 2, "Strada Republicii 45, Arad", "Beta Retail SA", "contact@betaretail.ro" },
                    { 3, "Calea Aradului 115, Timișoara", "Gamma Logistica SRL", "dispecerat@gammalogistica.ro" },
                    { 4, "Strada Mureșului 8, Timișoara", "Delta Construct Impex", "office@deltaconstruct.ro" },
                    { 5, "Bulevardul Take Ionescu 22, Timișoara", "Epsilon Tech Solutions", "contact@epsilontech.ro" },
                    { 6, "Calea Lugojului 45, Timișoara", "Zeta Distribution Group", "comenzi@zetagroup.ro" },
                    { 7, "Strada Aurel Vlaicu 14, Arad", "Omega Servicii Industriale", "suport@omegasev.ro" },
                    { 8, "Piața Libertății 3, Timișoara", "Transilvania Agro SRL", "office@transilvaniaagro.ro" },
                    { 9, "Strada Telegrafului 19, Timișoara", "Nordic Pharma Distribution", "achizitii@nordicpharma.ro" },
                    { 10, "Bulevardul Revoluției 10, Arad", "Apex Media Production", "hello@apexmedia.ro" },
                    { 11, "Calea Torontalului 60, Timișoara", "Balkan Express Transport", "logistica@balkanexpress.ro" },
                    { 12, "Strada Crișan 5, Arad", "Crisana Food Prod", "vanzari@crisanafood.ro" },
                    { 13, "Splaiul Nicolae Titulescu 4, Timișoara", "Danubius Software House", "dev@danubius-soft.ro" },
                    { 14, "Strada Fratelia 28, Timișoara", "Eco Clean Timișoara", "contact@ecoclean-tm.ro" },
                    { 15, "Calea Timișoarei 88, Arad", "Frontera Impex SRL", "office@frontera-impex.ro" },
                    { 16, "Bulevardul Eroilor de la Tisa 35, Timișoara", "Global Auto Parts", "piese@globalauto.ro" },
                    { 17, "Strada Ștefan cel Mare 12, Timișoara", "Hestia Trading", "comenzi@hestiatrading.ro" },
                    { 18, "Piața Unirii 7, Timișoara", "Imperial Print Shop", "print@imperialshop.ro" },
                    { 19, "Strada Ioan Slavici 45, Arad", "Jupiter Security Systems", "dispecerat@jupitersystems.ro" },
                    { 20, "Calea Buziașului 11, Timișoara", "Krono Wood Industry", "office@kronowood.ro" },
                    { 21, "Strada Alba Iulia 6, Timișoara", "Luna Cosmetics Distribution", "comenzi@lunacosmetics.ro" },
                    { 22, "Bulevardul General Dragalina 9, Timișoara", "Metropolis Real Estate", "contact@metropolis-re.ro" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "ContactEmail", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "sales@officesolutions.ro", "Office Solutions SA", "0722334455" },
                    { 2, "orders@homekitchen.ro", "Home & Kitchen Distribution", "0733445566" },
                    { 3, "comenzi@techdistrib.ro", "Tech Distribution SRL", "0721112233" },
                    { 4, "office@globaloffice.ro", "Global Office Supply", "0733445566" },
                    { 5, "b2b@homekitchen.ro", "Home & Kitchen Pro", "0744556677" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Calea Aradului Km 4, Timișoara", "Depozit Central Timișoara" },
                    { 2, "Autostrada Militari Km 13, București", "Hub Logistic Regional București" },
                    { 3, "Bulevardul Muncii Nr. 12, Cluj-Napoca", "Depozit Regional Transilvania" },
                    { 4, "Calea Chișinăului Nr. 24, Iași", "Centru Logistic Nord-Est" },
                    { 5, "Strada Ana Ipătescu Nr. 10, Timișoara", "Depozit Local Fratelia Sud" },
                    { 6, "Incinta Port, Poarta 7, Constanța", "Hub Logistic Port Constanța" },
                    { 7, "Strada Zizinului Nr. 115, Brașov", "Depozit Regional Centru" },
                    { 8, "Calea București Nr. 150, Craiova", "Centru Distribuție Oltenia" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "MinimumStockLevel", "Name", "SKU", "SupplierId", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 3, 4, "Scaun de Birou Ergonomic", "FURN-CHAIR-01", 2, 850.00m },
                    { 2, 1, 5, "Laptop Business 15 inch", "IT-LAP-001", 1, 4200.50m },
                    { 3, 2, 3, "Monitor LED 27 4K", "PER-MON-002", 1, 1500.00m },
                    { 4, 2, 10, "Tastatură Mecanică RGB", "PER-KEY-003", 2, 350.00m },
                    { 5, 4, 2, "Scaun Directorial Piele", "MOB-CHAIR-004", 2, 1200.00m },
                    { 6, 3, 3, "Aparat de Cafea Espresso", "ELC-COF-005", 3, 2800.00m },
                    { 7, 2, 15, "Mouse Wireless Ergonomic", "PER-MOU-006", 2, 120.50m },
                    { 8, 2, 8, "Căști Gaming Wireless", "PER-HP-007", 1, 450.00m },
                    { 9, 1, 12, "Hub USB-C 7-în-1", "IT-ACC-008", 1, 180.00m },
                    { 10, 1, 10, "Geantă Laptop 15.6 inch", "IT-BAG-009", 2, 160.00m },
                    { 11, 1, 7, "Stand Reglabil Laptop", "IT-STD-010", 2, 130.00m },
                    { 12, 1, 3, "Proiector LED Full HD", "IT-PROJ-011", 1, 1950.00m },
                    { 13, 2, 4, "Imprimantă Multifuncțională Laser", "PER-PRN-012", 2, 920.00m },
                    { 14, 1, 6, "Router Wireless Wi-Fi 6", "IT-NET-013", 1, 540.00m },
                    { 15, 1, 10, "Hard Disk Extern 2TB", "IT-HDD-014", 1, 340.00m },
                    { 16, 1, 10, "SSD Extern Portabil 1TB", "IT-SSD-015", 1, 480.00m },
                    { 17, 3, 4, "Cuptor cu Microunde Digital", "ELC-MIC-016", 3, 650.00m },
                    { 18, 6, 8, "Fierbător Electric Inox", "ELC-KET-017", 2, 150.00m },
                    { 19, 4, 6, "Lampă de Birou LED cu Încărcător Wireless", "MOB-LMP-019", 2, 220.00m },
                    { 20, 4, 2, "Dulap Arhivă Metalic", "MOB-CAB-020", 2, 1100.00m }
                });

            migrationBuilder.InsertData(
                table: "ProductWarehouses",
                columns: new[] { "ProductId", "WarehouseId", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 1, 2, 250 },
                    { 1, 3, 112 },
                    { 1, 4, 30 },
                    { 1, 5, 75 },
                    { 1, 6, 120 },
                    { 1, 7, 20 },
                    { 1, 8, 100 },
                    { 2, 1, 10 },
                    { 2, 2, 180 },
                    { 2, 3, 350 },
                    { 2, 4, 120 },
                    { 2, 5, 200 },
                    { 2, 6, 150 },
                    { 2, 7, 10 },
                    { 2, 8, 50 },
                    { 3, 1, 40 },
                    { 3, 2, 35 },
                    { 3, 3, 90 },
                    { 3, 4, 130 },
                    { 3, 5, 270 },
                    { 3, 6, 170 },
                    { 3, 7, 90 },
                    { 3, 8, 25 },
                    { 4, 1, 600 },
                    { 4, 2, 330 },
                    { 4, 3, 250 },
                    { 4, 4, 190 },
                    { 4, 5, 390 },
                    { 4, 6, 420 },
                    { 4, 7, 140 },
                    { 4, 8, 500 },
                    { 5, 1, 20 },
                    { 5, 2, 30 },
                    { 5, 3, 50 },
                    { 5, 4, 65 },
                    { 5, 5, 85 },
                    { 5, 6, 35 },
                    { 5, 7, 60 },
                    { 5, 8, 20 },
                    { 6, 1, 20 },
                    { 6, 2, 15 },
                    { 6, 3, 82 },
                    { 6, 4, 90 },
                    { 6, 6, 55 },
                    { 6, 7, 40 },
                    { 6, 8, 70 },
                    { 7, 1, 200 },
                    { 7, 2, 120 },
                    { 7, 3, 85 },
                    { 7, 4, 120 },
                    { 7, 5, 322 },
                    { 7, 6, 280 },
                    { 7, 7, 160 },
                    { 7, 8, 190 },
                    { 8, 1, 500 },
                    { 8, 2, 100 },
                    { 8, 3, 220 },
                    { 8, 4, 40 },
                    { 8, 5, 110 },
                    { 8, 6, 90 },
                    { 8, 7, 0 },
                    { 8, 8, 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouses_WarehouseId",
                table: "ProductWarehouses",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductWarehouses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}

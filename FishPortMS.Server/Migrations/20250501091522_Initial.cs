using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishPortMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsignacionOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsignacionOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotifSubscriptions",
                columns: table => new
                {
                    SubsriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P256dh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Auth = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifSubscriptions", x => x.SubsriptionId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RefreshTokenCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshTokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consignacions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsignacionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignacionAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignacionLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartOfContract = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndOfContract = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActiveSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConsignacionOwnerId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consignacions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consignacions_ConsignacionOwners_ConsignacionOwnerId",
                        column: x => x.ConsignacionOwnerId,
                        principalTable: "ConsignacionOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterProducts_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AnnouncementRole = table.Column<int>(type: "int", nullable: false),
                    AnnouncementType = table.Column<int>(type: "int", nullable: false),
                    ConsignacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_Consignacions_ConsignacionId",
                        column: x => x.ConsignacionId,
                        principalTable: "Consignacions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConsignacionEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConsignacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsignacionEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsignacionEmployees_Consignacions_ConsignacionId",
                        column: x => x.ConsignacionId,
                        principalTable: "Consignacions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpenseCategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsignacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Consignacions_ConsignacionId",
                        column: x => x.ConsignacionId,
                        principalTable: "Consignacions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterProductId = table.Column<int>(type: "int", nullable: false),
                    ConsignacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProducts_Consignacions_ConsignacionId",
                        column: x => x.ConsignacionId,
                        principalTable: "Consignacions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientProducts_MasterProducts_MasterProductId",
                        column: x => x.MasterProductId,
                        principalTable: "MasterProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    SRP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MasterProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterInventories_MasterProducts_MasterProductId",
                        column: x => x.MasterProductId,
                        principalTable: "MasterProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCarousels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCarousels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCarousels_MasterProducts_MasterProductId",
                        column: x => x.MasterProductId,
                        principalTable: "MasterProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsignacionSessions",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartSession = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartingCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EndSession = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsignacionEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ConsignacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsignacionSessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_ConsignacionSessions_ConsignacionEmployees_ConsignacionEmployeeId",
                        column: x => x.ConsignacionEmployeeId,
                        principalTable: "ConsignacionEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendancePin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsignacionOwnerId = table.Column<int>(type: "int", nullable: true),
                    ConsignacionEmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_ConsignacionEmployees_ConsignacionEmployeeId",
                        column: x => x.ConsignacionEmployeeId,
                        principalTable: "ConsignacionEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_ConsignacionOwners_ConsignacionOwnerId",
                        column: x => x.ConsignacionOwnerId,
                        principalTable: "ConsignacionOwners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    MaxQty = table.Column<int>(type: "int", nullable: false),
                    ClientProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientInventories_ClientProducts_ClientProductId",
                        column: x => x.ClientProductId,
                        principalTable: "ClientProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ref_No = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrders_ConsignacionSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "ConsignacionSessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsignacionAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendanceState = table.Column<int>(type: "int", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LunchIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LunchOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeInImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOutImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    ConsignacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsignacionAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsignacionAttendances_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PettyCashes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsignacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PettyCashes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PettyCashes_Consignacions_ConsignacionId",
                        column: x => x.ConsignacionId,
                        principalTable: "Consignacions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PettyCashes_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_ConsignacionId",
                table: "Announcements",
                column: "ConsignacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInventories_ClientProductId",
                table: "ClientInventories",
                column: "ClientProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientProducts_ConsignacionId",
                table: "ClientProducts",
                column: "ConsignacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProducts_MasterProductId",
                table: "ClientProducts",
                column: "MasterProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsignacionAttendances_UserProfileId",
                table: "ConsignacionAttendances",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsignacionEmployees_ConsignacionId",
                table: "ConsignacionEmployees",
                column: "ConsignacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Consignacions_ConsignacionOwnerId",
                table: "Consignacions",
                column: "ConsignacionOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsignacionSessions_ConsignacionEmployeeId",
                table: "ConsignacionSessions",
                column: "ConsignacionEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ConsignacionId",
                table: "Expenses",
                column: "ConsignacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseCategoryId",
                table: "Expenses",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterInventories_MasterProductId",
                table: "MasterInventories",
                column: "MasterProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterProducts_ProductCategoryId",
                table: "MasterProducts",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SalesOrderId",
                table: "OrderItems",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PettyCashes_ConsignacionId",
                table: "PettyCashes",
                column: "ConsignacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PettyCashes_UserProfileId",
                table: "PettyCashes",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCarousels_MasterProductId",
                table: "ProductCarousels",
                column: "MasterProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_SessionId",
                table: "SalesOrders",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ConsignacionEmployeeId",
                table: "UserProfiles",
                column: "ConsignacionEmployeeId",
                unique: true,
                filter: "[ConsignacionEmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ConsignacionOwnerId",
                table: "UserProfiles",
                column: "ConsignacionOwnerId",
                unique: true,
                filter: "[ConsignacionOwnerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "ClientInventories");

            migrationBuilder.DropTable(
                name: "ConsignacionAttendances");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "MasterInventories");

            migrationBuilder.DropTable(
                name: "NotifSubscriptions");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PettyCashes");

            migrationBuilder.DropTable(
                name: "ProductCarousels");

            migrationBuilder.DropTable(
                name: "ClientProducts");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "MasterProducts");

            migrationBuilder.DropTable(
                name: "ConsignacionSessions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ConsignacionEmployees");

            migrationBuilder.DropTable(
                name: "Consignacions");

            migrationBuilder.DropTable(
                name: "ConsignacionOwners");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dispatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DispatchCode = table.Column<string>(type: "text", nullable: true),
                    SourceBranch = table.Column<string>(type: "text", nullable: false),
                    VehicleNumber = table.Column<string>(type: "text", nullable: false),
                    Driver = table.Column<string>(type: "text", nullable: false),
                    ParcelIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    DispatchTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Initials = table.Column<string>(type: "text", nullable: false),
                    LicenseNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    BodyType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchDeposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Branch = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CodTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    RunningDebt = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchDeposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchDeposits_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CODCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DispatchCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DispatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VehicleNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BranchName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    TotalCODAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    DepositedAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false, defaultValue: 0m),
                    Shortfall = table.Column<decimal>(type: "numeric(12,2)", nullable: false, defaultValue: 0m),
                    CollectionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "collected"),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    UpdatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CODCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CODCollections_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CODCollections_Dispatches_DispatchId",
                        column: x => x.DispatchId,
                        principalTable: "Dispatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CODCollections_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CODCollections_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ContractCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContactPerson = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContractNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PaymentTerms = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TaxRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractCustomers_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Vendor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ReceiptNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "pending"),
                    ApprovalNotes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RejectionReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    BranchName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ClerkId = table.Column<int>(type: "integer", nullable: true),
                    ClerkName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    CreatedByName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApprovedById = table.Column<int>(type: "integer", nullable: true),
                    ApprovedByName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyExpenses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DailyExpenses_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DailyExpenses_Users_ClerkId",
                        column: x => x.ClerkId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DailyExpenses_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WaybillNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatorLastNameSnapshot = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserBranches",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBranches", x => new { x.UserId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_UserBranches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBranches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false, defaultValue: 0m),
                    ContractCustomerId = table.Column<int>(type: "integer", nullable: false),
                    BillingPeriodStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BillingPeriodEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_ContractCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "ContractCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WaybillNumber = table.Column<string>(type: "text", nullable: true),
                    QRCode = table.Column<string>(type: "text", nullable: true),
                    DispatchedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DispatchTrackingCode = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatorLastNameSnapshot = table.Column<string>(type: "text", nullable: true),
                    ShipmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Sender = table.Column<string>(type: "text", nullable: false),
                    SenderTelephone = table.Column<string>(type: "text", nullable: false),
                    Receiver = table.Column<string>(type: "text", nullable: false),
                    ReceiverTelephone = table.Column<string>(type: "text", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Rate = table.Column<decimal>(type: "numeric", nullable: true),
                    PaymentMethods = table.Column<string>(type: "text", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalRate = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "numeric", nullable: true),
                    TransactionCode = table.Column<string>(type: "text", nullable: true),
                    PaymentUpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentUpdatedById = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcels_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcels_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Parcels_Users_PaymentUpdatedById",
                        column: x => x.PaymentUpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChequeDeposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChequeNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DrawerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BankName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ClearanceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "deposited"),
                    RelatedInvoiceNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RelatedInvoiceId = table.Column<int>(type: "integer", nullable: true),
                    ContractCustomerId = table.Column<int>(type: "integer", nullable: true),
                    CustomerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    BranchName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    BounceReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    UpdatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeDeposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChequeDeposits_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ChequeDeposits_ContractCustomers_ContractCustomerId",
                        column: x => x.ContractCustomerId,
                        principalTable: "ContractCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ChequeDeposits_Invoices_RelatedInvoiceId",
                        column: x => x.RelatedInvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ChequeDeposits_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChequeDeposits_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    ParcelId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WaybillNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DispatchParcels",
                columns: table => new
                {
                    DispatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParcelsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispatchParcels", x => new { x.DispatchId, x.ParcelsId });
                    table.ForeignKey(
                        name: "FK_DispatchParcels_Dispatches_DispatchId",
                        column: x => x.DispatchId,
                        principalTable: "Dispatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DispatchParcels_Parcels_ParcelsId",
                        column: x => x.ParcelsId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParcelDeposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParcelId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepositedAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m),
                    Expenses = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    UpdatedById = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelDeposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcelDeposits_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParcelDeposits_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ParcelDeposits_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Mombasa Address", "mombasa@email.com", "Mombasa", "111" },
                    { 2, "Nairobi Address", "nairobi@email.com", "Nairobi", "222" },
                    { 3, "Kisumu Address", "kisumu@email.com", "Kisumu", "333" },
                    { 4, "Eldoret Address", "eldoret@email.com", "Eldoret", "444" },
                    { 5, "Nakuru Address", "nakuru@email.com", "Nakuru", "555" },
                    { 6, "Kericho Address", "kericho@email.com", "Kericho", "666" },
                    { 7, "Kakamega Address", "kakamega@email.com", "Kakamega", "777" },
                    { 8, "Kapsabet Address", "kapsabet@email.com", "Kapsabet", "888" },
                    { 9, "Kitale Address", "kitale@email.com", "Kitale", "999" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("095232fb-d3df-4604-888e-63160d083194"), "Charles", "CM", "Maina", "DL123456" },
                    { new Guid("79c0695e-2faf-407e-be48-9bc1b72c8a62"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("7ee49add-8f7f-4c89-917e-23ab18495f0c"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("8233b66a-d0ca-4392-85fe-4de96bff5e06"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("85834e40-92a3-41e4-82fd-29fdd9ca0d13"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("87457561-de53-443a-8be5-0d99d2ab6383"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("923f64da-8416-4780-b0fe-03b90eae2b5c"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("990dfca5-f619-46aa-ad73-c0e071e74711"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("a6e1837b-f42f-4134-b29d-d939d48edcf7"), "James", "JG", "Gichohi", "DL123456" },
                    { new Guid("ea569014-1c38-4494-aa2a-7d9c34b8c9ad"), "Erastus", "EK", "Kagwa", "DL123456" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator", "Admin" },
                    { 2, "Manager", "Manager" },
                    { 3, "Regular User", "Clerk" },
                    { 4, "Client User", "Client" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("58c70873-85e4-4ab8-8720-6a7921a5c666"), "Truck", "KDE 228S" },
                    { new Guid("7571df6f-7d50-4042-bb72-c8884eca6628"), "Truck", "KCY 067A" },
                    { new Guid("9e5d2c5f-0c93-4853-a6ec-5a12bb431777"), "Van", "KBF 462A" },
                    { new Guid("b9798a67-f89e-4564-8992-97e2227009fb"), "Van", "KAY 215H" },
                    { new Guid("cb3f3cb6-fa0d-4e1f-8cef-d7edb12938b5"), "Truck", "KCZ 595L" },
                    { new Guid("f4627c1d-1c8b-46ae-8d0e-a71d485c03ad"), "Truck", "KDL 085M" },
                    { new Guid("fd79e494-2623-4455-8c1f-d66965918a42"), "Truck", "KDB 387Q" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BranchId", "CreatedAt", "Email", "FirstName", "IsActive", "LastName", "PasswordHash", "PasswordSalt", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "admin@example.com", "Admin", true, "User", new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 }, 1, "admin" },
                    { 2, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "manager@example.com", "Manager", true, "Person", new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 }, 2, "manager" },
                    { 3, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "clerk1@example.com", "Clerk", true, "One", new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 }, 3, "clerk1" },
                    { 4, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "clerk2@example.com", "Clerk", true, "Two", new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 }, 3, "clerk2" },
                    { 5, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "client@example.com", "Client", true, "User", new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 }, 4, "client" },
                    { 6, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "client2@example.com", "Client", true, "UserTwo", new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 }, 4, "client2" }
                });

            migrationBuilder.InsertData(
                table: "UserBranches",
                columns: new[] { "BranchId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchDeposit_Branch",
                table: "BranchDeposits",
                column: "Branch");

            migrationBuilder.CreateIndex(
                name: "IX_BranchDeposit_Branch_Date",
                table: "BranchDeposits",
                columns: new[] { "Branch", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchDeposit_Date",
                table: "BranchDeposits",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_BranchDeposits_CreatedById",
                table: "BranchDeposits",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeDeposits_BranchId",
                table: "ChequeDeposits",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeDeposits_ChequeNumber_Unique",
                table: "ChequeDeposits",
                column: "ChequeNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChequeDeposits_ContractCustomerId",
                table: "ChequeDeposits",
                column: "ContractCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeDeposits_CreatedById",
                table: "ChequeDeposits",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeDeposits_RelatedInvoiceId",
                table: "ChequeDeposits",
                column: "RelatedInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeDeposits_UpdatedById",
                table: "ChequeDeposits",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CODCollections_BranchId",
                table: "CODCollections",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CODCollections_CreatedById",
                table: "CODCollections",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CODCollections_DispatchCode_Unique",
                table: "CODCollections",
                column: "DispatchCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CODCollections_DispatchId",
                table: "CODCollections",
                column: "DispatchId");

            migrationBuilder.CreateIndex(
                name: "IX_CODCollections_UpdatedById",
                table: "CODCollections",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCustomers_CreatedById",
                table: "ContractCustomers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenses_ApprovedById",
                table: "DailyExpenses",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenses_BranchId",
                table: "DailyExpenses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenses_ClerkId",
                table: "DailyExpenses",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyExpenses_CreatedById",
                table: "DailyExpenses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchParcels_ParcelsId",
                table: "DispatchParcels",
                column: "ParcelsId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CreatedById",
                table: "Invoices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber_Unique",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParcelDeposits_CreatedById",
                table: "ParcelDeposits",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelDeposits_ParcelId_Unique",
                table: "ParcelDeposits",
                column: "ParcelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParcelDeposits_UpdatedById",
                table: "ParcelDeposits",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_CreatedById",
                table: "Parcels",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_PaymentUpdatedById",
                table: "Parcels",
                column: "PaymentUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ShipmentId",
                table: "Parcels",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CreatedById",
                table: "Shipments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_WaybillNumber",
                table: "Shipments",
                column: "WaybillNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBranches_BranchId",
                table: "UserBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchDeposits");

            migrationBuilder.DropTable(
                name: "ChequeDeposits");

            migrationBuilder.DropTable(
                name: "CODCollections");

            migrationBuilder.DropTable(
                name: "DailyExpenses");

            migrationBuilder.DropTable(
                name: "DispatchParcels");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "ParcelDeposits");

            migrationBuilder.DropTable(
                name: "UserBranches");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Dispatches");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "ContractCustomers");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

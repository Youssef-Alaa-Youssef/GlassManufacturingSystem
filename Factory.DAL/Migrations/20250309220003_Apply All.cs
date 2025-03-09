using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Factory.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ApplyAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMFAEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDarkModeEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LastBackupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteRequestedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documentation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExportImportSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnableExport = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    EnableImport = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SupportedExportFormats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportedImportFormats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxExportRows = table.Column<int>(type: "int", nullable: false, defaultValue: 10000),
                    MaxImportFileSize = table.Column<int>(type: "int", nullable: false, defaultValue: 10485760),
                    IncludeHeaders = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DateFormat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "yyyy-MM-dd"),
                    DefaultExportFormat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "XLSX"),
                    CsvDelimiter = table.Column<string>(type: "nvarchar(1)", nullable: false, defaultValue: ","),
                    AllowNullValues = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ValidateImportData = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportImportSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressAr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainWarehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnableEmailNotifications = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EnableSmsNotifications = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EnablePushNotifications = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnboardingProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnboardingProcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SelectedMachines = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSQM = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    TotalLM = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rank = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionTyepe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTyepe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SquareFootage = table.Column<int>(type: "int", nullable: false),
                    Bedrooms = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    PropertyType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ListedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    HasGarage = table.Column<bool>(type: "bit", nullable: false),
                    HasPool = table.Column<bool>(type: "bit", nullable: false),
                    IsFurnished = table.Column<bool>(type: "bit", nullable: false),
                    AgentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AgentEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AgentPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FacebookLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TwitterLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LinkedInLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InstagramLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    YouTubeLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMember", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAQS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ArchivedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    HelpfulVotes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UnhelpfulVotes = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQS_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportTicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Open"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Medium"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "General"),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedToUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportTicket_AspNetUsers_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressAr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MainWarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubWarehouses_MainWarehouses_MainWarehouseId",
                        column: x => x.MainWarehouseId,
                        principalTable: "MainWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITSetupModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnboardingProcessId = table.Column<int>(type: "int", nullable: false),
                    EmailSetup = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HardwareProvisioned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SoftwareInstalled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AccessGranted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITSetupModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITSetupModule_OnboardingProcess_OnboardingProcessId",
                        column: x => x.OnboardingProcessId,
                        principalTable: "OnboardingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrientationModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnboardingProcessId = table.Column<int>(type: "int", nullable: false),
                    CompanyOrientationCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DepartmentOrientationCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MentorAssigned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FirstWeekCheckInCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrientationModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrientationModule_OnboardingProcess_OnboardingProcessId",
                        column: x => x.OnboardingProcessId,
                        principalTable: "OnboardingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreboardingModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnboardingProcessId = table.Column<int>(type: "int", nullable: false),
                    DocumentsReceived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractSigned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BackgroundCheckCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    WelcomeEmailSent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreboardingModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreboardingModule_OnboardingProcess_OnboardingProcessId",
                        column: x => x.OnboardingProcessId,
                        principalTable: "OnboardingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnboardingProcessId = table.Column<int>(type: "int", nullable: false),
                    ComplianceTraining = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SkillsTraining = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SystemsTraining = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SecurityTraining = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingModule_OnboardingProcess_OnboardingProcessId",
                        column: x => x.OnboardingProcessId,
                        principalTable: "OnboardingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Width = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    Height = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    StepWidth = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: true),
                    StepHeight = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SQM = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    TotalLM = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    CustomerReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rank = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeliveredBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_PermissionTyepe_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PermissionTyepe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponseText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SupportTicketId = table.Column<int>(type: "int", nullable: false),
                    RespondedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportResponse_AspNetUsers_RespondedByUserId",
                        column: x => x.RespondedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportResponse_SupportTicket_SupportTicketId",
                        column: x => x.SupportTicketId,
                        principalTable: "SupportTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Thickness = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dimensions = table.Column<string>(type: "nvarchar(max)", precision: 10, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    SubWarehouseId = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFragile = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_MainWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "MainWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_SubWarehouses_SubWarehouseId",
                        column: x => x.SubWarehouseId,
                        principalTable: "SubWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SubmoduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_SubModules_SubmoduleId",
                        column: x => x.SubmoduleId,
                        principalTable: "SubModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MainWarehouses",
                columns: new[] { "Id", "AddressAr", "AddressEn", "NameAr", "NameEn" },
                values: new object[] { 1, "الطابق الأول، المصنع أ", "Factory A, Floor 1", "المستودع الرئيسي", "Main Warehouse" });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "IconClass", "IsActive", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "bi-people", true, "User Management", "" },
                    { 2, "bi-person-badge", true, "Role Management", "" },
                    { 3, "bi-box-seam", true, "Inventory Management", "" },
                    { 4, "bi-cart", true, "Order Management", "" },
                    { 5, "bi-cash-stack", true, "Payroll Management", "" },
                    { 6, "bi-speedometer", true, "System Management", "" },
                    { 7, "bi-headset", true, "Support Management", "" },
                    { 8, "bi-file-earmark-bar-graph", true, "Reports", "" },
                    { 9, "bi-gear", true, "Settings", "" }
                });

            migrationBuilder.InsertData(
                table: "PermissionTyepe",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Create" },
                    { 2, "Read" },
                    { 3, "Update" },
                    { 4, "Delete" }
                });

            migrationBuilder.InsertData(
                table: "SubModules",
                columns: new[] { "Id", "Action", "Controller", "IconClass", "ModuleId", "Name", "Title" },
                values: new object[,]
                {
                    { 1, "", "", "bi-people", 1, "Users", "" },
                    { 2, "", "", "bi-shield-lock", 1, "Permissions", "" },
                    { 3, "", "", "bi-person-badge", 2, "Roles", "" },
                    { 4, "", "", "bi-person-check", 2, "Assign Roles", "" },
                    { 5, "", "", "bi-box-seam", 3, "Inventory", "" },
                    { 6, "", "", "bi-boxes", 3, "Stock Levels", "" },
                    { 7, "", "", "bi-cart-plus", 4, "New Order", "" },
                    { 8, "", "", "bi-cart-check", 4, "Order History", "" },
                    { 9, "", "", "bi-cash-stack", 5, "Payroll Dashboard", "" },
                    { 10, "", "", "bi-file-earmark-person", 5, "Employee Records", "" },
                    { 11, "", "", "bi-ticket-detailed", 6, "Tickets", "" },
                    { 12, "", "", "bi-speedometer", 6, "System Monitoring", "" },
                    { 13, "", "", "bi-headset", 7, "Support Tickets", "" },
                    { 14, "", "", "bi-chat-dots", 7, "Live Chat", "" },
                    { 15, "", "", "bi-file-earmark-bar-graph", 8, "Reports", "" },
                    { 16, "", "", "bi-speedometer2", 8, "Dashboards", "" },
                    { 17, "", "", "bi-gear", 9, "General Settings", "" },
                    { 18, "", "", "bi-shield", 9, "Security", "" }
                });

            migrationBuilder.InsertData(
                table: "SubWarehouses",
                columns: new[] { "Id", "AddressAr", "AddressEn", "MainWarehouseId", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "القسم الأول، المستودع الرئيسي", "Main Warehouse, Section 1", 1, "المستودع الفرعي أ", "Sub-Warehouse A" },
                    { 2, "القسم الثاني، المستودع الرئيسي", "Main Warehouse, Section 2", 1, "المستودع الفرعي ب", "Sub-Warehouse B" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "Action", "Controller", "IsActive", "Name", "SubmoduleId" },
                values: new object[,]
                {
                    { 1, "Index", "PermissionManagement", true, "Permission List", 2 },
                    { 2, "AssignPermissions", "PermissionManagement", true, "Assign Permission", 2 },
                    { 3, "Index", "Module", true, "Module List", 3 },
                    { 4, "Create", "Module", true, "Add Module", 3 },
                    { 5, "Index", "SubModule", true, "Submodule List", 4 },
                    { 6, "Create", "SubModule", true, "Add Submodule", 4 },
                    { 7, "Index", "Auth", true, "User List", 1 },
                    { 8, "Create", "Auth", true, "Add User", 1 },
                    { 9, "Index", "Role", true, "Role List", 3 },
                    { 10, "Create", "Role", true, "Add Role", 3 },
                    { 11, "Index", "Warehouse", true, "Warehouse List", 5 },
                    { 12, "Create", "Warehouse", true, "Add Warehouse", 5 },
                    { 13, "Index", "Item", true, "Item List", 6 },
                    { 14, "Create", "Item", true, "Add Item", 6 },
                    { 15, "Create", "Order", true, "Create Order", 7 },
                    { 16, "Index", "Order", true, "Order List", 8 },
                    { 17, "General", "Settings", true, "General Settings", 17 },
                    { 18, "Security", "Settings", true, "Security Settings", 18 },
                    { 19, "Index", "Payroll", true, "Payroll Dashboard", 9 },
                    { 20, "EmployeeSalaries", "Payroll", true, "Employee Salaries", 10 },
                    { 21, "ProcessSalaries", "Payroll", true, "Salary Processing", 9 },
                    { 22, "Reports", "Payroll", true, "Payroll Reports", 15 },
                    { 23, "Bonuses", "Payroll", true, "Bonuses Management", 9 },
                    { 24, "Deductions", "Payroll", true, "Deductions", 9 },
                    { 25, "Tax", "Payroll", true, "Tax Calculations", 9 },
                    { 26, "GeneratePayslip", "Payroll", true, "Payslip Generation", 9 },
                    { 27, "Overtime", "Payroll", true, "Overtime Payments", 9 },
                    { 28, "History", "Payroll", true, "Payroll History", 9 },
                    { 29, "Index", "Accountant", true, "Financial Orders", 15 },
                    { 30, "PreOnboarding", "Onboarding", true, "Pre-Onboarding", 10 },
                    { 31, "ITSetup", "Onboarding", true, "IT Setup", 10 },
                    { 32, "Training", "Onboarding", true, "Training & Orientation", 10 },
                    { 33, "Clearance", "Offboarding", true, "Exit Clearance", 10 },
                    { 34, "RevokeAccess", "Offboarding", true, "Access Revocation", 10 },
                    { 35, "FinalPayroll", "Offboarding", true, "Final Payroll & Documents", 10 },
                    { 36, "Records", "HR", true, "Employee Records", 10 },
                    { 37, "Leave", "HR", true, "Leave Management", 10 },
                    { 38, "Payroll", "HR", true, "Payroll Processing", 9 },
                    { 39, "Reviews", "Performance", true, "Performance Reviews", 10 },
                    { 40, "KPIs", "Performance", true, "KPI Tracking", 10 },
                    { 41, "Feedback", "Performance", true, "Feedback & Recognition", 10 },
                    { 42, "Tickets", "ITService", true, "Ticket Management", 11 },
                    { 43, "Monitoring", "ITService", true, "System Monitoring", 12 },
                    { 44, "Inventory", "ITService", true, "Hardware Inventory", 5 },
                    { 45, "Tickets", "Support", true, "Support Tickets", 13 },
                    { 46, "Chat", "Support", true, "Live Chat", 14 },
                    { 47, "FAQ", "Support", true, "FAQ & Help Center", 13 },
                    { 48, "Finance", "Reports", true, "Financial Reports", 15 },
                    { 49, "Employees", "Reports", true, "Employee Insights", 15 },
                    { 50, "Sales", "Reports", true, "Sales & Revenue", 15 },
                    { 51, "Index", "Support", true, "Support Dashboard", 13 },
                    { 52, "Index", "OrderReport", true, "Orders Dashboard", 8 },
                    { 53, "Settings", "ExportImport", true, "Data Management", 17 },
                    { 54, "Export", "ExportImport", true, "Data Export", 17 },
                    { 55, "Import", "ExportImport", true, "Data Import", 17 },
                    { 56, "Index", "OrderReport", true, "Order Dashboard", 16 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FAQS_UserId",
                table: "FAQS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecords_Date",
                table: "FinancialRecords",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubWarehouseId",
                table: "Items",
                column: "SubWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_WarehouseId",
                table: "Items",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ITSetupModule_OnboardingProcessId",
                table: "ITSetupModule",
                column: "OnboardingProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainWarehouses_NameAr",
                table: "MainWarehouses",
                column: "NameAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainWarehouses_NameEn",
                table: "MainWarehouses",
                column: "NameEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrientationModule_OnboardingProcessId",
                table: "OrientationModule",
                column: "OnboardingProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_SubmoduleId",
                table: "Pages",
                column: "SubmoduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_Name",
                table: "Partners",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreboardingModule_OnboardingProcessId",
                table: "PreboardingModule",
                column: "OnboardingProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ModuleId",
                table: "RolePermissions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubModules_ModuleId",
                table: "SubModules",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubWarehouses_MainWarehouseId",
                table: "SubWarehouses",
                column: "MainWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubWarehouses_NameAr",
                table: "SubWarehouses",
                column: "NameAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubWarehouses_NameEn",
                table: "SubWarehouses",
                column: "NameEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportResponse_RespondedByUserId",
                table: "SupportResponse",
                column: "RespondedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportResponse_SupportTicketId",
                table: "SupportResponse",
                column: "SupportTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicket_AssignedToUserId",
                table: "SupportTicket",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingModule_OnboardingProcessId",
                table: "TrainingModule",
                column: "OnboardingProcessId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "ContractSettings");

            migrationBuilder.DropTable(
                name: "Documentation");

            migrationBuilder.DropTable(
                name: "ExportImportSettings");

            migrationBuilder.DropTable(
                name: "FAQS");

            migrationBuilder.DropTable(
                name: "FinancialRecords");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ITSetupModule");

            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "OrientationModule");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "PreboardingModule");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SupportResponse");

            migrationBuilder.DropTable(
                name: "TeamMember");

            migrationBuilder.DropTable(
                name: "TrainingModule");

            migrationBuilder.DropTable(
                name: "SubWarehouses");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SubModules");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PermissionTyepe");

            migrationBuilder.DropTable(
                name: "SupportTicket");

            migrationBuilder.DropTable(
                name: "OnboardingProcess");

            migrationBuilder.DropTable(
                name: "MainWarehouses");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

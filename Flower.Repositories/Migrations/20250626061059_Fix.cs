﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flower.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessages_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageURLs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flowers_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FlowerID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Flowers_FlowerID",
                        column: x => x.FlowerID,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    FlowerID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Flowers_FlowerID",
                        column: x => x.FlowerID,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "LastUpdatedBy", "LastUpdatedTime", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(340), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(341), new TimeSpan(0, 7, 0, 0, 0)), "Admin", "ADMIN" },
                    { 2, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(343), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(344), new TimeSpan(0, 7, 0, 0, 0)), "User", "USER" },
                    { 3, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(409), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(409), new TimeSpan(0, 7, 0, 0, 0)), "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "AvatarUrl", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "FullName", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, null, "53c91d51-85aa-4ff6-8ae2-aff12827246b", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(429), new TimeSpan(0, 7, 0, 0, 0)), null, null, "admin@fpt.edu.vn", true, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(430), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ADMIN@FPT.EDU.VN", "ADMIN", "AQAAAAIAAYagAAAAELably8FNVxBM8rdfI1vO1fOUchxLXROuMKtYluqhovHsJLUzuG0DGS6OE/MTZ7VWw==", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "9baf276c-0fb4-4bf1-80f3-a9de30aa3392", 1, false, "admin" },
                    { 2, 0, null, null, "722c402e-9a0a-42b5-a5b6-0cf128416a3e", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 737, DateTimeKind.Unspecified).AddTicks(7068), new TimeSpan(0, 7, 0, 0, 0)), null, null, "user@fpt.edu.vn", true, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 737, DateTimeKind.Unspecified).AddTicks(7105), new TimeSpan(0, 7, 0, 0, 0)), false, null, "USER@FPT.EDU.VN", "USER", "AQAAAAIAAYagAAAAEKC4NGUvyxZ0G8RgWMV2Y5yhoSKrUkXitNpFL5Q4dndwEWNjSOOyVcrmyTqA1gPeeg==", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "4cedb78a-aff9-4310-95cf-6898b77dfefb", 1, false, "user" },
                    { 3, 0, null, null, "2593bdab-8b9e-45fa-80ed-d961102f0027", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 785, DateTimeKind.Unspecified).AddTicks(4414), new TimeSpan(0, 7, 0, 0, 0)), null, null, "staff@fpt.edu.vn", true, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 785, DateTimeKind.Unspecified).AddTicks(4448), new TimeSpan(0, 7, 0, 0, 0)), false, null, "STAFF@FPT.EDU.VN", "STAFF", "AQAAAAIAAYagAAAAEMuRFsmqKixkp0DciofXCEENH6j07qs4ozQe2MsTcLpVCg7wB1TOXzf8lvJbjKZOLw==", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "a8f141d2-5f50-48eb-9873-935546c9d200", 1, false, "staff" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime" },
                values: new object[,]
                {
                    { 1, "Roses", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2931), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2939), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, "Lilies", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2943), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2945), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, "Tulips", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2949), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2949), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, "Sunflowers", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2953), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2953), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, "Orchids", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2956), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(2956), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "Latitude", "Longitude", "StoreName" },
                values: new object[] { 1, "Lô E2a-7, Đường D1, Khu Công nghệ cao, P. Long Thạnh Mỹ, TP. Thủ Đức, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(195), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 690, DateTimeKind.Unspecified).AddTicks(204), new TimeSpan(0, 7, 0, 0, 0)), 10.841100000000001, 106.8095, "FPT University HCM Store" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Flowers",
                columns: new[] { "Id", "CategoryID", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "ImageURLs", "LastUpdatedBy", "LastUpdatedTime", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3015), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Classic red rose symbolizing love", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3015), new TimeSpan(0, 7, 0, 0, 0)), "Red Rose", 100000m, 100 },
                    { 2, 2, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3020), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Elegant white lily for sympathy and peace", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3020), new TimeSpan(0, 7, 0, 0, 0)), "White Lily", 150000m, 80 },
                    { 3, 3, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3024), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Bright and colorful tulips for any occasion", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3024), new TimeSpan(0, 7, 0, 0, 0)), "Pink Tulip", 100000m, 150 },
                    { 4, 4, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3028), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Cheerful sunflower for happy vibes", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3028), new TimeSpan(0, 7, 0, 0, 0)), "Sunflower", 100000m, 120 },
                    { 5, 5, null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3032), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Delicate and exotic orchid", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 26, 13, 10, 58, 834, DateTimeKind.Unspecified).AddTicks(3032), new TimeSpan(0, 7, 0, 0, 0)), "Purple Orchid", 100000m, 70 }
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
                name: "IX_CartItems_FlowerID",
                table: "CartItems",
                column: "FlowerID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserID",
                table: "CartItems",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_CategoryID",
                table: "Flowers",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_FlowerID",
                table: "OrderDetails",
                column: "FlowerID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderID",
                table: "Payments",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_RecipientId",
                table: "UserMessages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_SenderId",
                table: "UserMessages",
                column: "SenderId");
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
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Flowers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenericBrand.Data.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gb_security");

            migrationBuilder.CreateTable(
                name: "AspNetRole",
                schema: "gb_security",
                columns: table => new
                {
                    AspNetRoleId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    RoleLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRole", x => x.AspNetRoleId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUser",
                schema: "gb_security",
                columns: table => new
                {
                    AspNetUserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUser", x => x.AspNetUserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaim",
                schema: "gb_security",
                columns: table => new
                {
                    AspNetRoleClaimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspNetRoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaim", x => x.AspNetRoleClaimId);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaim_AspNetRole_AspNetRoleId",
                        column: x => x.AspNetRoleId,
                        principalSchema: "gb_security",
                        principalTable: "AspNetRole",
                        principalColumn: "AspNetRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaim",
                schema: "gb_security",
                columns: table => new
                {
                    AspNetUserClaimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspNetUserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaim", x => x.AspNetUserClaimId);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaim_AspNetUser_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalSchema: "gb_security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogin",
                schema: "gb_security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    AspNetUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogin_AspNetUser_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalSchema: "gb_security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRole",
                schema: "gb_security",
                columns: table => new
                {
                    AspNetUserId = table.Column<Guid>(nullable: false),
                    AspNetRoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRole", x => new { x.AspNetUserId, x.AspNetRoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRole_AspNetRole_AspNetRoleId",
                        column: x => x.AspNetRoleId,
                        principalSchema: "gb_security",
                        principalTable: "AspNetRole",
                        principalColumn: "AspNetRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRole_AspNetUser_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalSchema: "gb_security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserToken",
                schema: "gb_security",
                columns: table => new
                {
                    AspNetUserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserToken", x => new { x.AspNetUserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserToken_AspNetUser_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalSchema: "gb_security",
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "gb_security",
                table: "AspNetRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaim_AspNetRoleId",
                schema: "gb_security",
                table: "AspNetRoleClaim",
                column: "AspNetRoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "gb_security",
                table: "AspNetUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "gb_security",
                table: "AspNetUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaim_AspNetUserId",
                schema: "gb_security",
                table: "AspNetUserClaim",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogin_AspNetUserId",
                schema: "gb_security",
                table: "AspNetUserLogin",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRole_AspNetRoleId",
                schema: "gb_security",
                table: "AspNetUserRole",
                column: "AspNetRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaim",
                schema: "gb_security");

            migrationBuilder.DropTable(
                name: "AspNetUserClaim",
                schema: "gb_security");

            migrationBuilder.DropTable(
                name: "AspNetUserLogin",
                schema: "gb_security");

            migrationBuilder.DropTable(
                name: "AspNetUserRole",
                schema: "gb_security");

            migrationBuilder.DropTable(
                name: "AspNetUserToken",
                schema: "gb_security");

            migrationBuilder.DropTable(
                name: "AspNetRole",
                schema: "gb_security");

            migrationBuilder.DropTable(
                name: "AspNetUser",
                schema: "gb_security");
        }
    }
}

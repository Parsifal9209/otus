using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Migrations
{
    public partial class crate_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "preference",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CountAppliedPromocodes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employees_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "promoCode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ServiceInfo = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    BeginDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PartnerName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    PartnerManagerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreferenceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promoCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_promoCode_employees_PartnerManagerId",
                        column: x => x.PartnerManagerId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_promoCode_preference_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "preference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    PromoCodeId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_promoCode_PromoCodeId",
                        column: x => x.PromoCodeId,
                        principalTable: "promoCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_preference",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreferenceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_preference", x => new { x.CustomerId, x.PreferenceId });
                    table.ForeignKey(
                        name: "FK_customer_preference_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customer_preference_preference_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "preference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "preference",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), "Театр" });

            migrationBuilder.InsertData(
                table: "preference",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"), "Семья" });

            migrationBuilder.InsertData(
                table: "preference",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"), "Дети" });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"), "Администратор", "Admin" });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"), "Партнерский менеджер", "PartnerManager" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "CountAppliedPromocodes", "Email", "FirstName", "LastName", "RoleId" },
                values: new object[] { new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"), 5, "owner@somemail.ru", "Иван", "Сергеев", new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02") });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "CountAppliedPromocodes", "Email", "FirstName", "LastName", "RoleId" },
                values: new object[] { new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"), 10, "andreev@somemail.ru", "Петр", "Андреев", new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665") });

            migrationBuilder.InsertData(
                table: "promoCode",
                columns: new[] { "Id", "BeginDate", "Code", "EndDate", "PartnerManagerId", "PartnerName", "PreferenceId", "ServiceInfo" },
                values: new object[] { new Guid("ef7f299f-92d7-459f-896e-078ed53ef11c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Промокод на покупку билета в Театр", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"), null, new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), "test" });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PromoCodeId" },
                values: new object[] { new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), "ivan_sergeev@mail.ru", "Иван", "Петров", new Guid("ef7f299f-92d7-459f-896e-078ed53ef11c") });

            migrationBuilder.CreateIndex(
                name: "IX_customer_preference_PreferenceId",
                table: "customer_preference",
                column: "PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_PromoCodeId",
                table: "customers",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_RoleId",
                table: "employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_promoCode_PartnerManagerId",
                table: "promoCode",
                column: "PartnerManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_promoCode_PreferenceId",
                table: "promoCode",
                column: "PreferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_preference");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "promoCode");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "preference");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Migrations
{
    public partial class fix_relationships_promocode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_promoCode_PromoCodeId",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_PromoCodeId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "customers");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "promoCode",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "promoCode",
                keyColumn: "Id",
                keyValue: new Guid("ef7f299f-92d7-459f-896e-078ed53ef11c"),
                column: "CustomerId",
                value: new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"));

            migrationBuilder.CreateIndex(
                name: "IX_promoCode_CustomerId",
                table: "promoCode",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_promoCode_customers_CustomerId",
                table: "promoCode",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promoCode_customers_CustomerId",
                table: "promoCode");

            migrationBuilder.DropIndex(
                name: "IX_promoCode_CustomerId",
                table: "promoCode");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "promoCode");

            migrationBuilder.AddColumn<Guid>(
                name: "PromoCodeId",
                table: "customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "customers",
                keyColumn: "Id",
                keyValue: new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                column: "PromoCodeId",
                value: new Guid("ef7f299f-92d7-459f-896e-078ed53ef11c"));

            migrationBuilder.CreateIndex(
                name: "IX_customers_PromoCodeId",
                table: "customers",
                column: "PromoCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_promoCode_PromoCodeId",
                table: "customers",
                column: "PromoCodeId",
                principalTable: "promoCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

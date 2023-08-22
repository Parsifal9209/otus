using Microsoft.EntityFrameworkCore.Migrations;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Migrations
{
    public partial class eidt_foreaignkey_employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_promoCode_PartnerManagerId",
                table: "promoCode");

            migrationBuilder.CreateIndex(
                name: "IX_promoCode_PartnerManagerId",
                table: "promoCode",
                column: "PartnerManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_promoCode_PartnerManagerId",
                table: "promoCode");

            migrationBuilder.CreateIndex(
                name: "IX_promoCode_PartnerManagerId",
                table: "promoCode",
                column: "PartnerManagerId",
                unique: true);
        }
    }
}

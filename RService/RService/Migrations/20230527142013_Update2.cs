using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RService.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Record_ClientId",
                table: "Record",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_OfficeId",
                table: "Record",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_ServiceId",
                table: "Record",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Office_OfficeTypeId",
                table: "Office",
                column: "OfficeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Office_OfficeType_OfficeTypeId",
                table: "Office",
                column: "OfficeTypeId",
                principalTable: "OfficeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Client_ClientId",
                table: "Record",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Office_OfficeId",
                table: "Record",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Service_ServiceId",
                table: "Record",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Office_OfficeType_OfficeTypeId",
                table: "Office");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Client_ClientId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Office_OfficeId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Service_ServiceId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_ClientId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_OfficeId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_ServiceId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Office_OfficeTypeId",
                table: "Office");
        }
    }
}

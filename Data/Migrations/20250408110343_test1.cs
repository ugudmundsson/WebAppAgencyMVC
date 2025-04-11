using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Clients_ClientEntityId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ClientEntityId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ClientEntityId",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientEntityId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientEntityId",
                table: "Projects",
                column: "ClientEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Clients_ClientEntityId",
                table: "Projects",
                column: "ClientEntityId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Auth.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Roles",
                newName: "ModifiedUserId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserId",
                table: "Roles",
                newName: "CreatedByUserId");
        }
    }
}

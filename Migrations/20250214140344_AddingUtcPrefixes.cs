using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logbook.Migrations
{
    /// <inheritdoc />
    public partial class AddingUtcPrefixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastSeenAt",
                table: "Users",
                newName: "UtcLastSeenAt");

            migrationBuilder.RenameColumn(
                name: "BornAt",
                table: "Users",
                newName: "UtcBornAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Groups",
                newName: "UtcCreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UtcLastSeenAt",
                table: "Users",
                newName: "LastSeenAt");

            migrationBuilder.RenameColumn(
                name: "UtcBornAt",
                table: "Users",
                newName: "BornAt");

            migrationBuilder.RenameColumn(
                name: "UtcCreatedAt",
                table: "Groups",
                newName: "CreatedAt");
        }
    }
}

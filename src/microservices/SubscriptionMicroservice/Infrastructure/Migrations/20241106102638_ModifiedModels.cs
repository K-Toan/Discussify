using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubscriptionMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Communities",
                newName: "CreatorName");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Communities",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Communities");

            migrationBuilder.RenameColumn(
                name: "CreatorName",
                table: "Communities",
                newName: "AuthorId");
        }
    }
}

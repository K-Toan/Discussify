using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubscriptionMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCommunityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatorName",
                table: "Communities",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreatorName",
                table: "Communities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}

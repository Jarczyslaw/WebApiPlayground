using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiPlayground.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantCreatedById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CreatedUserId",
                table: "Restaurants",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Users_CreatedUserId",
                table: "Restaurants",
                column: "CreatedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Users_CreatedUserId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_CreatedUserId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Restaurants");
        }
    }
}

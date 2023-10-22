using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_SOCIALNETWORK.Migrations
{
    /// <inheritdoc />
    public partial class Update_Like_Type_Add_Logo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "LikeType",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "LikeType");
        }
    }
}

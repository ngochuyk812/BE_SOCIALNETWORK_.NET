using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_SOCIALNETWORK.Migrations
{
    /// <inheritdoc />
    public partial class Update_Like_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_Type",
                table: "Like",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_LikeType_Type",
                table: "Like",
                column: "Type",
                principalTable: "LikeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_LikeType_Type",
                table: "Like");

            migrationBuilder.DropTable(
                name: "LikeType");

            migrationBuilder.DropIndex(
                name: "IX_Like_Type",
                table: "Like");
        }
    }
}

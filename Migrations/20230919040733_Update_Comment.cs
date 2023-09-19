using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_SOCIALNETWORK.Migrations
{
    /// <inheritdoc />
    public partial class Update_Comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "Src",
                table: "MediaComment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Src",
                table: "MediaComment");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comment");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

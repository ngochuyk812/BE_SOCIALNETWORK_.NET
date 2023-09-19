using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_SOCIALNETWORK.Migrations
{
    /// <inheritdoc />
    public partial class RemoteMediaType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaComment_MediaType_MediaTypeId",
                table: "MediaComment");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaMessage_MediaType_MediaTypeId",
                table: "MediaMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaPost_MediaType_MediaTypeId",
                table: "MediaPost");

            migrationBuilder.DropTable(
                name: "MediaType");

            migrationBuilder.DropIndex(
                name: "IX_MediaPost_MediaTypeId",
                table: "MediaPost");

            migrationBuilder.DropIndex(
                name: "IX_MediaMessage_MediaTypeId",
                table: "MediaMessage");

            migrationBuilder.DropIndex(
                name: "IX_MediaComment_MediaTypeId",
                table: "MediaComment");

            migrationBuilder.DropColumn(
                name: "MediaTypeId",
                table: "MediaPost");

            migrationBuilder.DropColumn(
                name: "Src",
                table: "MediaPost");

            migrationBuilder.DropColumn(
                name: "MediaTypeId",
                table: "MediaMessage");

            migrationBuilder.DropColumn(
                name: "Src",
                table: "MediaMessage");

            migrationBuilder.DropColumn(
                name: "MediaTypeId",
                table: "MediaComment");

            migrationBuilder.DropColumn(
                name: "Src",
                table: "MediaComment");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Room",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MediaPost",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MediaMessage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MediaComment",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "MediaPost");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MediaMessage");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MediaComment");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediaTypeId",
                table: "MediaPost",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Src",
                table: "MediaPost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MediaTypeId",
                table: "MediaMessage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Src",
                table: "MediaMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MediaTypeId",
                table: "MediaComment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Src",
                table: "MediaComment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MediaType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaPost_MediaTypeId",
                table: "MediaPost",
                column: "MediaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaMessage_MediaTypeId",
                table: "MediaMessage",
                column: "MediaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaComment_MediaTypeId",
                table: "MediaComment",
                column: "MediaTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaComment_MediaType_MediaTypeId",
                table: "MediaComment",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaMessage_MediaType_MediaTypeId",
                table: "MediaMessage",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPost_MediaType_MediaTypeId",
                table: "MediaPost",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

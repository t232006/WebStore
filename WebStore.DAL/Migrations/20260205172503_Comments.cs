using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Visitors_AuthorID",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Visitors_AuthorID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorID",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommentID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorID",
                table: "Blogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentID",
                table: "Comments",
                column: "CommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_AuthorID",
                table: "Blogs",
                column: "AuthorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorID",
                table: "Comments",
                column: "AuthorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentID",
                table: "Comments",
                column: "CommentID",
                principalTable: "Comments",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_SectionId",
                table: "Products",
                column: "SectionId",
                principalTable: "Brands",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_AuthorID",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_SectionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PublicDate",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "Blogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Visitors_AuthorID",
                table: "Blogs",
                column: "AuthorID",
                principalTable: "Visitors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Visitors_AuthorID",
                table: "Comments",
                column: "AuthorID",
                principalTable: "Visitors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "ID");
        }
    }
}

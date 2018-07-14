using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreaPost.Migrations
{
    public partial class ArticleAuthorRelationshipSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Autors_AuthorId",
                table: "Articles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Autors",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Articles",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Autors_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Autors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Autors_AuthorId",
                table: "Articles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Autors",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Autors_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Autors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

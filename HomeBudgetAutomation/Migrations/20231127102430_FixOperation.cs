using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeBudgetAutomation.Migrations
{
    public partial class FixOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_operations_articles",
                table: "operations");

            migrationBuilder.AlterColumn<decimal>(
                name: "debit",
                table: "operations",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "credit",
                table: "operations",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_date",
                table: "operations",
                type: "timestamp(3) without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp(3) without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "article_id",
                table: "operations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_operations_articles",
                table: "operations",
                column: "article_id",
                principalTable: "articles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_operations_articles",
                table: "operations");

            migrationBuilder.AlterColumn<decimal>(
                name: "debit",
                table: "operations",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "credit",
                table: "operations",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_date",
                table: "operations",
                type: "timestamp(3) without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(3) without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "article_id",
                table: "operations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_operations_articles",
                table: "operations",
                column: "article_id",
                principalTable: "articles",
                principalColumn: "id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mission_08Team3_10.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Home" },
                    { 2, "School" },
                    { 3, "Work" },
                    { 4, "Church" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "CategoryId", "Completed", "DueDate", "Quadrant", "TaskName" },
                values: new object[,]
                {
                    { 6, null, false, null, 4, "Browse Social Media" },
                    { 1, 1, false, new DateOnly(2026, 3, 1), 1, "Pay Bills" },
                    { 2, 2, false, new DateOnly(2026, 3, 5), 1, "Study for Exam" },
                    { 3, 1, false, null, 2, "Exercise" },
                    { 4, 4, false, null, 2, "Read Scriptures" },
                    { 5, 3, false, new DateOnly(2026, 3, 1), 3, "Answer Emails" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);
        }
    }
}

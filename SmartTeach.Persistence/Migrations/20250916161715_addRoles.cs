using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTeach.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
            { "8D04DCE2-969A-435D-BBA4-DF3F325983DC", Guid.NewGuid().ToString(), "Admin", "ADMIN" },
            { "B6A5DCE2-1234-435D-BBA4-DF3F325983DC", Guid.NewGuid().ToString(), "Teacher", "TEACHER" },
            { "C7B6DCE2-5678-435D-BBA4-DF3F325983DC", Guid.NewGuid().ToString(), "Student", "STUDENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Name IN ('Admin', 'Teacher', 'Student')");
        }
    }
    }

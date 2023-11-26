using Microsoft.EntityFrameworkCore.Migrations;
using System.Globalization;

#nullable disable

namespace HomeBudgetAutomation.Migrations
{
    public partial class CreateTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(string.Format(
                CultureInfo.InvariantCulture,
                "{1}{0}SqlScripts{0}{2}",
                Path.DirectorySeparatorChar,
                AppContext.BaseDirectory,
                "create_new_operation_trigger.sql")));

            migrationBuilder.Sql(File.ReadAllText(string.Format(
                CultureInfo.InvariantCulture,
                "{1}{0}SqlScripts{0}{2}",
                Path.DirectorySeparatorChar,
                AppContext.BaseDirectory,
                "create_old_operation_trigger.sql")));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(string.Format(
                CultureInfo.InvariantCulture,
                "{1}{0}SqlScripts{0}{2}",
                Path.DirectorySeparatorChar,
                AppContext.BaseDirectory,
                "drop_new_operation_trigger.sql")));

            migrationBuilder.Sql(File.ReadAllText(string.Format(
                CultureInfo.InvariantCulture,
                "{1}{0}SqlScripts{0}{2}",
                Path.DirectorySeparatorChar,
                AppContext.BaseDirectory,
                "drop_old_operation_trigger.sql")));
        }
    }
}

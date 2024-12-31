using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiProject.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDb : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[,]
            {
                { "1", "Admin", "ADMIN", "f6a69e1b-f746-4e6f-b11f-32fe09e1d8f9" },
                { "2", "User", "USER", "b98c850f-d8a0-41ed-bc88-bf2b62db9a52" }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "1");

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "2");
    }
}

}

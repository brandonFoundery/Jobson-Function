using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobson.Migrations
{
    public partial class Add_UpworkProfileKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpworkProfileKey",
                table: "UpworkProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpworkProfileKey",
                table: "UpworkProfiles");
        }
    }
}

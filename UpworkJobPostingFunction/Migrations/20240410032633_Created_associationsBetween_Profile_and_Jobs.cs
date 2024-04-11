using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobson.Migrations
{
    public partial class Created_associationsBetween_Profile_and_Jobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrelloBoardId",
                table: "UpworkProfiles",
                type: "nvarchar(450)",  // Change from nvarchar(max) to nvarchar(450) or another suitable length
                maxLength: 50,         // Optional, adds a constraint to the database to ensure data integrity
                nullable: true);


            migrationBuilder.AddColumn<string>(
                name: "UpworkJobCategory",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpworkJobDescription",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpworkJobId",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpworkJobTitle",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpworkJobUrl",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpworkProfileId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrelloBoard",
                columns: table => new
                {
                    BoardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrelloBoard", x => x.BoardId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpworkProfiles_TrelloBoardId",
                table: "UpworkProfiles",
                column: "TrelloBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UpworkProfileId",
                table: "Jobs",
                column: "UpworkProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_UpworkProfiles_UpworkProfileId",
                table: "Jobs",
                column: "UpworkProfileId",
                principalTable: "UpworkProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Changed from Cascade to NoAction

            migrationBuilder.AddForeignKey(
                name: "FK_UpworkProfiles_TrelloBoard_TrelloBoardId",
                table: "UpworkProfiles",
                column: "TrelloBoardId",
                principalTable: "TrelloBoard",
                principalColumn: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_UpworkProfiles_UpworkProfileId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_UpworkProfiles_TrelloBoard_TrelloBoardId",
                table: "UpworkProfiles");

            migrationBuilder.DropTable(
                name: "TrelloBoard");

            migrationBuilder.DropIndex(
                name: "IX_UpworkProfiles_TrelloBoardId",
                table: "UpworkProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_UpworkProfileId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TrelloBoardId",
                table: "UpworkProfiles");

            migrationBuilder.DropColumn(
                name: "UpworkJobCategory",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UpworkJobDescription",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UpworkJobId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UpworkJobTitle",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UpworkJobUrl",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UpworkProfileId",
                table: "Jobs");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobson.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LLMModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlmProviderType = table.Column<int>(type: "int", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LLMModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LLMPrompts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LLMPromptType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LLMProviderId = table.Column<int>(type: "int", nullable: false),
                    PromptText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LLMPrompts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LLMPrompts_LLMModels_LLMProviderId",
                        column: x => x.LLMProviderId,
                        principalTable: "LLMModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Guid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileTypeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PubDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BatchRunTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrelloCardId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrelloCardUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrelloListId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrelloListName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrelloBoardId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrelloBoardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrelloCardShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Jobs_ProfileTypes_ProfileTypeId",
                        column: x => x.ProfileTypeId,
                        principalTable: "ProfileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpworkProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileTypeId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpworkProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpworkProfiles_ProfileTypes_ProfileTypeId",
                        column: x => x.ProfileTypeId,
                        principalTable: "ProfileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpworkRssFeedUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpworkRssFeedUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpworkRssFeedUrls_ProfileTypes_ProfileTypeId",
                        column: x => x.ProfileTypeId,
                        principalTable: "ProfileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.InsertData(
                table: "LLMModels",
                columns: new[] { "Name", "LlmProviderType", "ApiKey" },
                values: new object[] { "OpenAI Model", 0, "your_openai_api_key_here" });

            migrationBuilder.InsertData(
                table: "LLMModels",
                columns: new[] { "Name", "LlmProviderType", "ApiKey" },
                values: new object[] { "Anthropic Model", 1, "your_anthropic_api_key_here" });

            migrationBuilder.InsertData(
                table: "LLMModels",
                columns: new[] { "Name", "LlmProviderType", "ApiKey" },
                values: new object[] { "Minstril Model", 2, "your_minstril_api_key_here" });

            migrationBuilder.InsertData(
                table: "LLMPrompts",
                columns: new[] { "LLMPromptType", "LLMProviderId", "PromptText" },
                values: new object[] { "JobFilter", 1, "Your prompt text for JobFilter with OpenAI" });

            migrationBuilder.InsertData(
                table: "ProfileTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 0, "Development" });

            migrationBuilder.InsertData(
                table: "ProfileTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Marketing" });

            migrationBuilder.InsertData(
                table: "UpworkRssFeedUrls",
                columns: new[] { "Id", "Name", "ProfileTypeId", "Url" },
                values: new object[] { 1, "Expert Asp.Net Jobs", 0, "https://www.upwork.com/ab/feed/jobs/rss?amount=1000-4999&category2_uid=531770282580668418&contractor_tier=3&paging=0-10&q=asp.net&sort=recency&t=1&api_params=1&securityToken=a833c2c7918b7c7527f8e8deb3174ff06d3b39c6ac7621b2fd95dae3cf913e71113798900e6bfb6cd1798607d8a5c339f6bb29f2b424395cb19dbefbf4fd0041&userUid=1750194899056226304&orgUid=1750194899056226305" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ProfileTypeId",
                table: "Jobs",
                column: "ProfileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LLMPrompts_LLMProviderId",
                table: "LLMPrompts",
                column: "LLMProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_UpworkProfiles_ProfileTypeId",
                table: "UpworkProfiles",
                column: "ProfileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UpworkRssFeedUrls_ProfileTypeId",
                table: "UpworkRssFeedUrls",
                column: "ProfileTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "LLMPrompts");

            migrationBuilder.DropTable(
                name: "UpworkProfiles");

            migrationBuilder.DropTable(
                name: "UpworkRssFeedUrls");

            migrationBuilder.DropTable(
                name: "LLMModels");

            migrationBuilder.DropTable(
                name: "ProfileTypes");
        }
    }
}

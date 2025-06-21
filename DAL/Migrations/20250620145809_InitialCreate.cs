using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemAccounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountRole = table.Column<int>(type: "int", nullable: false),
                    AccountPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemAccounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticles",
                columns: table => new
                {
                    NewsArticleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NewsTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Headline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewsContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsStatus = table.Column<bool>(type: "bit", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticles", x => x.NewsArticleId);
                    table.ForeignKey(
                        name: "FK_NewsArticles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsArticles_SystemAccounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "SystemAccounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_NewsArticles_SystemAccounts_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "SystemAccounts",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "NewsTags",
                columns: table => new
                {
                    NewsArticleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTags", x => new { x.NewsArticleId, x.TagId });
                    table.ForeignKey(
                        name: "FK_NewsTags_NewsArticles_NewsArticleId",
                        column: x => x.NewsArticleId,
                        principalTable: "NewsArticles",
                        principalColumn: "NewsArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName", "IsActive", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "News related to political affairs and government updates", "Politics", true, null },
                    { 2, "Latest updates from the sports industry", "Sports", true, null },
                    { 3, "News on technological advancements and innovations", "Technology", true, null },
                    { 4, "Covers movies, music, and celebrity news", "Entertainment", true, null }
                });

            migrationBuilder.InsertData(
                table: "SystemAccounts",
                columns: new[] { "AccountId", "AccountEmail", "AccountName", "AccountPassword", "AccountRole" },
                values: new object[,]
                {
                    { -3, "staff@gmail.com", "Staff nek", "123", 1 },
                    { -2, "lecturer@gmail.com", "Lecture nek", "123", 2 },
                    { -1, "admin@gmail.com", "Admin nek", "123", 3 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "Note", "TagName" },
                values: new object[,]
                {
                    { 1, "Important and urgent news updates.", "Breaking News" },
                    { 2, "Covers political topics and government affairs.", "Politics" },
                    { 3, "News about sports, teams, and events.", "Sports" },
                    { 4, "Covers tech innovations, AI, and software updates.", "Technology" },
                    { 5, "Covers movies, music, and celebrity news.", "Entertainment" },
                    { 6, "Covers stock markets, economy, and business news.", "Finance" },
                    { 7, "Medical news, fitness, and health tips.", "Health" },
                    { 8, "International news and global events.", "World" },
                    { 9, "Scientific discoveries and space exploration.", "Science" },
                    { 10, "Latest trends in artificial intelligence and ML.", "AI & Machine Learning" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName", "IsActive", "ParentCategoryId" },
                values: new object[,]
                {
                    { 5, "News related to elections worldwide", "Elections", true, 1 },
                    { 6, "Latest football news and match updates", "Football", true, 2 },
                    { 7, "Latest updates on artificial intelligence and machine learning", "AI & Machine Learning", true, 3 },
                    { 8, "News on Hollywood movies and celebrities", "Hollywood", true, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_CategoryId",
                table: "NewsArticles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_CreatedById",
                table: "NewsArticles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_UpdatedById",
                table: "NewsArticles",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTags_TagId",
                table: "NewsTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsTags");

            migrationBuilder.DropTable(
                name: "NewsArticles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "SystemAccounts");
        }
    }
}

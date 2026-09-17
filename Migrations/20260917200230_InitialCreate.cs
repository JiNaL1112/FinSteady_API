using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinSteady_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__19093A0B7045B95C", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Saving_Rule",
                columns: table => new
                {
                    SavingRuleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rulename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Saving_R__4050F25DE7C1E185", x => x.SavingRuleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ProfileImage = table.Column<string>(type: "text", nullable: true),
                    KYCDoc = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()"),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CC4CA1835B49", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    TotalBalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SavingBalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AvailableBalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Card__55FECDAE1FCE8F4F", x => x.CardId);
                    table.ForeignKey(
                        name: "FK__Card__UserId__52593CB8",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Saving_Goal",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    TargetAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TargetDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    SavingRuleId = table.Column<int>(type: "integer", nullable: true),
                    FundBalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Saving_G__8A4FFFD11414036C", x => x.GoalId);
                    table.ForeignKey(
                        name: "FK__Saving_Go__Categ__656C112C",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK__Saving_Go__Savin__66603565",
                        column: x => x.SavingRuleId,
                        principalTable: "Saving_Rule",
                        principalColumn: "SavingRuleId");
                    table.ForeignKey(
                        name: "FK__Saving_Go__UserI__6477ECF3",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FromUserId = table.Column<int>(type: "integer", nullable: true),
                    ToUserId = table.Column<int>(type: "integer", nullable: true),
                    TransactionType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__55433A6BD830D540", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK__Transacti__FromU__571DF1D5",
                        column: x => x.FromUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Transacti__ToUse__5812160E",
                        column: x => x.ToUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_UserId",
                table: "Card",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "idx_Saving_Goal_TargetDate",
                table: "Saving_Goal",
                column: "TargetDate");

            migrationBuilder.CreateIndex(
                name: "IX_Saving_Goal_CategoryId",
                table: "Saving_Goal",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Saving_Goal_SavingRuleId",
                table: "Saving_Goal",
                column: "SavingRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Saving_Goal_UserId",
                table: "Saving_Goal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "idx_Transaction_Date",
                table: "Transaction",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FromUserId",
                table: "Transaction",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ToUserId",
                table: "Transaction",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "idx_User_Email",
                table: "User",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "UQ__User__A9D105347001A560",
                table: "User",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Saving_Goal");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Saving_Rule");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MP.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "access");

            migrationBuilder.CreateTable(
                name: "UserStatusReferences",
                schema: "access",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatusReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "access",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserStatusReferences_Status",
                        column: x => x.Status,
                        principalSchema: "access",
                        principalTable: "UserStatusReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "UserStatusReferences",
                column: "Id",
                value: 0);

            migrationBuilder.InsertData(
                schema: "access",
                table: "UserStatusReferences",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                schema: "access",
                table: "UserStatusReferences",
                column: "Id",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "access",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Status",
                schema: "access",
                table: "Users",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "access");

            migrationBuilder.DropTable(
                name: "UserStatusReferences",
                schema: "access");
        }
    }
}

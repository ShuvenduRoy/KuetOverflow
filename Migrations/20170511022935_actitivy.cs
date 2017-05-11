using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KuetOverflow.Migrations
{
    public partial class actitivy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityID",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivityID",
                table: "Answer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_ActivityID",
                table: "Question",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_ActivityID",
                table: "Answer",
                column: "ActivityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Activity_ActivityID",
                table: "Answer",
                column: "ActivityID",
                principalTable: "Activity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Activity_ActivityID",
                table: "Question",
                column: "ActivityID",
                principalTable: "Activity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Activity_ActivityID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Activity_ActivityID",
                table: "Question");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Question_ActivityID",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Answer_ActivityID",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "ActivityID",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "ActivityID",
                table: "Answer");
        }
    }
}

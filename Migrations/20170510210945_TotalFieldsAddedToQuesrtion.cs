using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KuetOverflow.Migrations
{
    public partial class TotalFieldsAddedToQuesrtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "TotalAnswers",
                table: "Question",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStars",
                table: "Question",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalVote",
                table: "Question",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Question",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAnswers",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "TotalStars",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "TotalVote",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Question");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Notifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

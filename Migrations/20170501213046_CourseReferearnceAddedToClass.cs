using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KuetOverflow.Migrations
{
    public partial class CourseReferearnceAddedToClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Lectures",
                table: "Lectures");

            migrationBuilder.RenameTable(
                name: "Lectures",
                newName: "Lecture");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Lecture",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecture",
                table: "Lecture",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecture",
                table: "Lecture");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Lecture");

            migrationBuilder.RenameTable(
                name: "Lecture",
                newName: "Lectures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lectures",
                table: "Lectures",
                column: "ID");
        }
    }
}

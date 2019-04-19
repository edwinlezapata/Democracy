using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Democracy.Web.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Candidates_CandidateId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_CandidateId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDay",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Candidates",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BirthDay",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CandidateId",
                table: "Candidates",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Candidates_CandidateId",
                table: "Candidates",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

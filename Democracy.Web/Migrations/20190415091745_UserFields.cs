using Microsoft.EntityFrameworkCore.Migrations;

namespace Democracy.Web.Migrations
{
    public partial class UserFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votings_Votings_VotingId",
                table: "Votings");

            migrationBuilder.DropIndex(
                name: "IX_Votings_VotingId",
                table: "Votings");

            migrationBuilder.DropColumn(
                name: "VotingId",
                table: "Votings");

            migrationBuilder.AddColumn<string>(
                name: "BirthDay",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "VotingId",
                table: "Votings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votings_VotingId",
                table: "Votings",
                column: "VotingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votings_Votings_VotingId",
                table: "Votings",
                column: "VotingId",
                principalTable: "Votings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

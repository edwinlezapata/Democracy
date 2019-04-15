using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Democracy.Web.Migrations
{
    public partial class Voting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Votings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VotingEventId = table.Column<int>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Vote = table.Column<int>(nullable: false),
                    VotingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votings_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votings_VotingEvents_VotingEventId",
                        column: x => x.VotingEventId,
                        principalTable: "VotingEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votings_Votings_VotingId",
                        column: x => x.VotingId,
                        principalTable: "Votings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votings_CandidateId",
                table: "Votings",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votings_UserId",
                table: "Votings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Votings_VotingEventId",
                table: "Votings",
                column: "VotingEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Votings_VotingId",
                table: "Votings",
                column: "VotingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votings");
        }
    }
}

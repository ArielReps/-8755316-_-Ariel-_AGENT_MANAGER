using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMissionsPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Missions",
                table: "Missions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Missions",
                table: "Missions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_AgentId",
                table: "Missions",
                column: "AgentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Missions",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_AgentId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Missions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Missions",
                table: "Missions",
                columns: new[] { "AgentId", "TargetId" });
        }
    }
}

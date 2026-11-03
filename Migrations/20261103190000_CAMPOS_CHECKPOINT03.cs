using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class CAMPOS_CHECKPOINT03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Importancia",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Importancia",
                table: "Tarefas");
        }
    }
}

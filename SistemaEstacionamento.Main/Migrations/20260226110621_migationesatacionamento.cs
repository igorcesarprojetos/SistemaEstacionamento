using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEstacionamento.Main.Migrations
{
    /// <inheritdoc />
    public partial class migationesatacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecoHora",
                table: "Estacionamento",
                newName: "PrecoHoraAdicional");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecoHoraAdicional",
                table: "Estacionamento",
                newName: "PrecoHora");
        }
    }
}

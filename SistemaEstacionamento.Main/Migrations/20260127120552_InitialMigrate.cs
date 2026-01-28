using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEstacionamento.Main.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estacionamento",
                columns: table => new
                {
                    Id_Estacionamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoHora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeHoras = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlacaVeiculo = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    ModeloVeiculo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pago = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacionamento", x => x.Id_Estacionamento);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estacionamento");
        }
    }
}

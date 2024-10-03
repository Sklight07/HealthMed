using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationHeathMedInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HM_MEDICO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR(11)", nullable: false),
                    NumeroCrm = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    UfCrm = table.Column<string>(type: "VARCHAR(2)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Permissao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HM_MEDICO", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HM_MEDICO");
        }
    }
}

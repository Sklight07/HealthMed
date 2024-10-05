using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationHeathMedHorariosDisp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HM_HORARIOS_DISPONIVEIS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(type: "INT", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HorarioInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HorarioFim = table.Column<TimeSpan>(type: "time", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HM_HORARIOS_DISPONIVEIS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HM_HORARIOS_DISPONIVEIS_HM_MEDICO_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "HM_MEDICO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_Medico_Dia_Horario",
                table: "HM_HORARIOS_DISPONIVEIS",
                columns: new[] { "MedicoId", "DiaSemana", "HorarioInicio" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HM_HORARIOS_DISPONIVEIS");
        }
    }
}

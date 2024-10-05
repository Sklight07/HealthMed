using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationHeathMedAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HM_AGENDAMENTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(type: "INT", nullable: false),
                    PacienteId = table.Column<int>(type: "INT", nullable: false),
                    HorarioId = table.Column<int>(type: "INT", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HM_AGENDAMENTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HM_AGENDAMENTO_HM_HORARIOS_DISPONIVEIS_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "HM_HORARIOS_DISPONIVEIS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HM_AGENDAMENTO_HM_MEDICO_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "HM_MEDICO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HM_AGENDAMENTO_HM_PACIENTE_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "HM_PACIENTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_HorarioId",
                table: "HM_AGENDAMENTO",
                column: "HorarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HM_AGENDAMENTO_MedicoId",
                table: "HM_AGENDAMENTO",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_HM_AGENDAMENTO_PacienteId",
                table: "HM_AGENDAMENTO",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HM_AGENDAMENTO");
        }
    }
}

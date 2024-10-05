using HealthMed.Application.Requests.Medico;
using HealthMed.Application.Responses.Comum;
using HealthMed.Application.Responses.Medico;
using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Interfaces
{
    public interface IMedicoUseCase
    {
        List<MedicoModel> ListarTodosMedicosComHorariosDisponiveis();
        List<HorariosCadastradosResponse> ListarTodosHorariosPorMedico(int idMedico);
        CadastroResponse CadastroMedico(MedicoCadastroRequest medicoCadastroRequest);
        CadastroResponse CadastrarHorarios(CadastroHorarioMedicoRequest cadastroHorarioMedicoRequest);
        MedicoModel ObterPorEmailSenha(string email, string senha);
        CadastroResponse ExcluirHorariosPorDiaDaSemana(DateTime data, int idMedico);
        List<HorariosCadastradosResponse> ListarHorariosCadastradosPorDia(DateTime data, int idMedico);
        List<HorariosCadastradosResponse> ListarHorariosDisponiveis(int idMedico, DateTime data);
    }
}

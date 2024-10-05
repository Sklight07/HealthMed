using HealthMed.Application.Requests.Paciente;
using HealthMed.Application.Responses.Comum;
using HealthMed.Application.Responses.Paciente;
using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Interfaces
{
    public interface IPacienteUseCase
    {
        List<PacienteModel> ObterTodos();
        CadastroResponse CadastroPaciente(PacienteCadastroRequest pacienteCadastroRequest);
        PacienteModel ObterPorEmailSenha(string email, string senha);
        CadastroResponse AgendarConsulta(AgendarConsultaRequest agendarConsultaRequest);
        List<AgendamentosPacienteResponse> ListarAgendamentosPaciente(int idPaciente);
        CadastroResponse ExcluirAgendamentoConsulta(int idConsulta);
    }
}

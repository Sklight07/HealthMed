using HealthMed.Application.Responses.Comum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Responses.Paciente
{
    public class AgendamentosPacienteResponse
    {
        public int IdConsulta { get; set; }
        public DateTime DataConsulta { get; set; }
        public string NomeMedico { get; set; }
        public string CrmMedico { get; set; }
        public string UFCrm { get; set; }
        public CadastroResponse? CadastroResponse { get; set; }
    }
}

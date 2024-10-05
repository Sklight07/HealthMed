using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Entities
{
    public class AgendamentoModel : ComumModel
    {
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public int HorarioId { get; set; }
        public DateTime DataConsulta { get; set; }

        public PacienteModel Paciente { get; set; }
        public MedicoModel Medico { get; set; }
        public HorariosDisponiveisMedicoModel HorarioDisponivel { get; set; }
    }
}

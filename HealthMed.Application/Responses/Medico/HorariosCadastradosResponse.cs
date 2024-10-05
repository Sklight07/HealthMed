using HealthMed.Application.Responses.Comum;
using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Responses.Medico
{
    public class HorariosCadastradosResponse
    {
        public int MedicoId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFim { get; set; }
        public bool Disponivel { get; set; }
        public DateTime Data { get; set; }
        public string DiaSemanaConvertido { get; set; }

        public CadastroResponse? CadastroResponse { get; set; }
    }
}

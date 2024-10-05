using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Entities
{
    public class HorariosDisponiveisMedicoModel : ComumModel
    {
        public int MedicoId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFim { get; set; }
        public bool Disponivel { get; set; }
        public DateTime Data { get; set; }
        public MedicoModel Medico { get; set; }
    }
}

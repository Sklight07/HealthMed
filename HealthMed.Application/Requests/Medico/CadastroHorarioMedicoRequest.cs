using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Requests.Medico
{
    public class CadastroHorarioMedicoRequest
    {
        [Required]
        public int MedicoId { get; set; }
        [Required]
        public DayOfWeek DiaSemana { get; set; }
        [Required]
        public TimeSpan HorarioInicio { get; set; }
        [Required]
        public TimeSpan HorarioFim { get; set; }
        [Required]
        public DateTime Data { get; set; }
    }
}

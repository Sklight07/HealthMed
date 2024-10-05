using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Requests.Paciente
{
    public class AgendarConsultaRequest
    {
        [Required]
        public int PacienteId { get; set; }
        [Required]
        public int MedicoId { get; set; }
        [Required]
        public int HorarioId { get; set; }
        public DateTime DataConsulta { get; set; } //pode remover esse depois 
    }
}

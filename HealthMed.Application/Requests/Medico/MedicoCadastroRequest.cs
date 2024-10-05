using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Requests.Medico
{
    public class MedicoCadastroRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string NumeroCrm { get; set; }
        [Required]
        public string UfCrm { get; set; }
    }
}

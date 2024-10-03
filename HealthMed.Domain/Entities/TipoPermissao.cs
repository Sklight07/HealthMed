using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Entities
{
    public enum TipoPermissao
    {
        Medico = 1,
        Paciente = 2
    }

    public static class Permissao
    {
        public const string Medico = "Medico";
        public const string Paciente = "Paciente";
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Entities
{
    public class MedicoModel : UsuarioComumModel
    {
        public string NumeroCrm { get; set; }
        public string UfCrm { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<HorariosDisponiveisMedicoModel> HorariosDisponiveis { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<AgendamentoModel> Consultas { get; set; }
    }
}

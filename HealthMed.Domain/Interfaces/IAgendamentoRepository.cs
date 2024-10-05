using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Interfaces
{
    public interface IAgendamentoRepository : IComumRepository<AgendamentoModel>
    {
        List<AgendamentoModel> ObterPorIdPaciente(int idPaciente);
    }
}

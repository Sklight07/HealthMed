using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Interfaces
{
    public interface IHorariosDisponiveisMedicoRepository : IComumRepository<HorariosDisponiveisMedicoModel>
    {
        bool VerificaExisteHorariosMedicoPorDataEHora(int medicoID, DayOfWeek diaSemana, TimeSpan dataInicio, DateTime data);
        IEnumerable<HorariosDisponiveisMedicoModel> ObterPorDiaDaSemanaEMedico(DateTime data, int idMedico);
        IEnumerable<HorariosDisponiveisMedicoModel> ObterPorMedico(int idMedico);
        HorariosDisponiveisMedicoModel RetornaHorarioDisponivelPorId(int horarioId);
    }
}

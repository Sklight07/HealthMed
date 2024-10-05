using HealthMed.Data.Data;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Data.Repository
{
    public class HorariosDisponiveisMedicoRepository : ComumRepository<HorariosDisponiveisMedicoModel>, IHorariosDisponiveisMedicoRepository
    {
        protected ApplicationDbContext _dbContext;

        public HorariosDisponiveisMedicoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<HorariosDisponiveisMedicoModel> ObterPorDiaDaSemanaEMedico(DateTime data, int idMedico)
        {
            try
            {
                return _dbContext.HorariosDisponiveisMedico.Where(h => h.MedicoId == idMedico
                                    && h.Data == data.Date).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<HorariosDisponiveisMedicoModel> ObterPorMedico(int idMedico)
        {
            try
            {
                return _dbContext.HorariosDisponiveisMedico.Where(h => h.MedicoId == idMedico).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool VerificaExisteHorariosMedicoPorDataEHora(int medicoID, DayOfWeek diaSemana, TimeSpan dataInicio, DateTime data)
        {
            try
            {
                return _dbContext.HorariosDisponiveisMedico
                                .Any(h => h.MedicoId == medicoID
                                    && h.DiaSemana == diaSemana
                                    && h.HorarioInicio == dataInicio
                                    && h.Data == data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HorariosDisponiveisMedicoModel RetornaHorarioDisponivelPorId(int horarioId)
        {
            try
            {
                return _dbContext.HorariosDisponiveisMedico
                                .Where(w => w.Id == horarioId && w.Disponivel == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

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
    public class AgendamentoRepository : ComumRepository<AgendamentoModel>, IAgendamentoRepository
    {
        protected ApplicationDbContext _dbContext;

        public AgendamentoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AgendamentoModel> ObterPorIdPaciente(int idPaciente)
        {
            try
            {
                return _dbContext.Agendamento.Include(m => m.Medico).Where(w => w.PacienteId == idPaciente).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

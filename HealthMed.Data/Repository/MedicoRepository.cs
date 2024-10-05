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
    public class MedicoRepository : ComumRepository<MedicoModel>, IMedicoRepository
    {
        protected ApplicationDbContext _dbContext;

        public MedicoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MedicoModel> ListarTodosMedicosComHorariosDisponiveis()
        {
            try
            {
                return _dbContext.Medico
                    .Include(m => m.HorariosDisponiveis)
                    .AsNoTracking()// Inclui os horários associados
                    .Where(m => m.HorariosDisponiveis.Any(h => h.Disponivel)) // Filtra médicos com ao menos um horário disponível
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicoModel ObterEmailSenha(string email, string senha)
        {
            try
            {
                return _dbContext.Medico.Where(w => w.Email == email && w.Senha == senha).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using HealthMed.Data.Data;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Data.Repository
{
    public class PacienteRepository : ComumRepository<PacienteModel>, IPacienteRepository
    {
        protected ApplicationDbContext _dbContext;

        public PacienteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public PacienteModel ObterEmailSenha(string email, string senha)
        {
            try
            {
                return _dbContext.Paciente.Where(w => w.Email == email && w.Senha == senha).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

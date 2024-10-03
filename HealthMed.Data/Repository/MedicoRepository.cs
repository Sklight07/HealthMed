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

        public MedicoModel ObterEmailSenha(string email, string senha)
        {
            try
            {
                return _dbContext.Medico.Where(w => w.Email == email && w.Senha == senha).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

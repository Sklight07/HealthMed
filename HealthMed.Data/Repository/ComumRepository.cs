using HealthMed.Data.Data;
using HealthMed.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Domain.Entities;

namespace HealthMed.Data.Repository
{
    public class ComumRepository<T> : IComumRepository<T> where T : ComumModel
    {
        protected ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public ComumRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _dbSet.Update(entidade);
            _dbContext.SaveChanges();
        }

        public int Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
            _dbContext.SaveChanges();
            return entidade.Id;
        }

        public void Excluir(int id)
        {
            _dbSet.Remove(ObterPorId(id));
            _dbContext.SaveChanges();
        }

        public T ObterPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public IList<T> ObterTodos()
        {
            return _dbSet.ToList();
        }
    }
}

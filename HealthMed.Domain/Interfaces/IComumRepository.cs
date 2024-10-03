using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Interfaces
{
    public interface IComumRepository<T> where T : ComumModel
    {
        IList<T> ObterTodos();
        T ObterPorId(int id);
        void Cadastrar(T comum);
        void Alterar(T comum);
        void Excluir(int id);
    }
}

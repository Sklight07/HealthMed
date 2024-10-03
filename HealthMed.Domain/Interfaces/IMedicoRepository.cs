using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Interfaces
{
    public interface IMedicoRepository : IComumRepository<MedicoModel>
    {
        MedicoModel ObterEmailSenha(string email, string senha);
    }
}

using HealthMed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Interfaces
{
    public interface IPacienteUseCase
    {
        List<PacienteModel> ObterTodos();
        PacienteModel ObterPorEmailSenha(string email, string senha);
    }
}

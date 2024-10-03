using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Services.Medico
{
    public class MedicoUseCase : IMedicoUseCase
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoUseCase(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public MedicoModel ObterPorEmailSenha(string email, string senha)
        {
            var retorno = _medicoRepository.ObterEmailSenha(email, senha);
            return retorno;
        }

        public List<MedicoModel> ObterTodos()
        {
            var medicos = _medicoRepository.ObterTodos();
            return medicos.ToList();
        }
    }
}

using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;

namespace HealthMed.Application.Services.Paciente
{
    public class PacienteUseCase : IPacienteUseCase
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteUseCase(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public PacienteModel ObterPorEmailSenha(string email, string senha)
        {
            var retorno = _pacienteRepository.ObterEmailSenha(email, senha);
            return retorno;
        }

        public List<PacienteModel> ObterTodos()
        {
            var pacientes = _pacienteRepository.ObterTodos();
            return pacientes.ToList();
        }
    }
}

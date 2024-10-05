using HealthMed.Application.Interfaces;
using HealthMed.Application.Requests.Paciente;
using HealthMed.Application.Responses.Comum;
using HealthMed.Application.Responses.Paciente;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Util;
using Mapster;
using MimeKit;
using MailKit.Net.Smtp;

namespace HealthMed.Application.Services.Paciente
{
    public class PacienteUseCase : IPacienteUseCase
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IHorariosDisponiveisMedicoRepository _horariosDisponiveisMedicoRepository;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMedicoRepository _medicoRepository;

        public PacienteUseCase(IPacienteRepository pacienteRepository, IHorariosDisponiveisMedicoRepository horariosDisponiveisMedicoRepository,
            IAgendamentoRepository agendamentoRepository, IMedicoRepository medicoRepository)
        {
            _pacienteRepository = pacienteRepository;
            _horariosDisponiveisMedicoRepository = horariosDisponiveisMedicoRepository;
            _agendamentoRepository = agendamentoRepository;
            _medicoRepository = medicoRepository;
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

        public CadastroResponse CadastroPaciente(PacienteCadastroRequest pacienteCadastroRequest)
        {
            try
            {
                var paciente = new PacienteModel();
                paciente = pacienteCadastroRequest.Adapt<PacienteModel>();
                paciente.Permissao = TipoPermissao.Paciente;
                _pacienteRepository.Cadastrar(paciente);
                return new CadastroResponse() { Id = paciente.Id, mensagem = paciente.Id != 0 ? "Cadastrado com sucesso!" : "Erro ao se cadastrar" };

            }
            catch (Exception ex) 
            {
                return new CadastroResponse() {mensagem = $"Erro ao se cadastrar: {ex.Message}" };
            }
        }

        public CadastroResponse AgendarConsulta(AgendarConsultaRequest agendarConsultaRequest)
        {
            try
            {
                var horarioDisponivel = _horariosDisponiveisMedicoRepository.RetornaHorarioDisponivelPorId(agendarConsultaRequest.HorarioId);

                if (horarioDisponivel == null)
                    return new CadastroResponse() { mensagem = "Erro: o horario já nã oestá mais disponivel" };

                horarioDisponivel.Disponivel = false;

                var consulta = new AgendamentoModel
                {
                    PacienteId = agendarConsultaRequest.PacienteId,
                    MedicoId = horarioDisponivel.MedicoId,
                    HorarioId = horarioDisponivel.Id,
                    DataConsulta = agendarConsultaRequest.DataConsulta
                };

                var gravacao = _agendamentoRepository.Cadastrar(consulta);

                if (gravacao == 0)
                    return new CadastroResponse() { mensagem = "Erro: houve um problema durante a gravação do agendamento" };

                _horariosDisponiveisMedicoRepository.Alterar(horarioDisponivel);

                var medico = _medicoRepository.ObterPorId(agendarConsultaRequest.MedicoId);
                var paciente = _pacienteRepository.ObterPorId(agendarConsultaRequest.PacienteId);
                //aqui email
                if (medico != null && paciente != null)
                    Enviaremail(medico.Email, medico.Nome, agendarConsultaRequest.DataConsulta, paciente.Nome);

                return new CadastroResponse() { Id = gravacao, mensagem = "Agendamento realizado com sucesso" };
            }
            catch (Exception ex)
            {
                return new CadastroResponse() { mensagem = $"Erro: houve um problema durante a gravação do agendamento: {ex.Message}" };
            }
            

        }

        private void Enviaremail(string destinatario, string nomeMedico, DateTime dataConsulta, string nomePaciente)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("HealthMed", "alex.agumon@gmail.com"));
            email.To.Add(new MailboxAddress(nomeMedico, destinatario));

            email.Subject = "Health&Med - Nova consulta agendada";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<b>Olá, Dr. {nomeMedico}!</b>  <b>Você tem uma nova consulta marcada! Paciente: {nomePaciente}.</b> <b>Data e horário: {dataConsulta.Date} às {dataConsulta.Hour}:{dataConsulta.Minute}.</b>"

            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("alex.agumon@gmail.com", "jvpz cjua hizp obpk");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        public List<AgendamentosPacienteResponse> ListarAgendamentosPaciente(int idPaciente)
        {
            try
            {
                var agendamentos = _agendamentoRepository.ObterPorIdPaciente(idPaciente);

                if(agendamentos.NullOrEmpty())
                    return new List<AgendamentosPacienteResponse>() { new AgendamentosPacienteResponse() { CadastroResponse = new CadastroResponse() { mensagem = $"Erro: Não existem horarios agendados para o paciente" } } };

                List<AgendamentosPacienteResponse> retorno = new List<AgendamentosPacienteResponse>();

                foreach (var agendamento in agendamentos)
                {
                    AgendamentosPacienteResponse reg = new AgendamentosPacienteResponse();
                    reg.IdConsulta = agendamento.Id;
                    reg.NomeMedico = agendamento.Medico.Nome;
                    reg.DataConsulta = agendamento.DataConsulta;
                    reg.CrmMedico = agendamento.Medico.NumeroCrm;
                    reg.UFCrm = agendamento.Medico.UfCrm;

                    retorno.Add(reg);
                }

                return retorno;

            }
            catch (Exception ex)
            {
                return new List<AgendamentosPacienteResponse>() { new AgendamentosPacienteResponse() { CadastroResponse = new CadastroResponse() { mensagem = $"Erro: {ex.Message}" } } };
            }
        }

        public CadastroResponse ExcluirAgendamentoConsulta(int idConsulta)
        {
            try
            {
                var agendamento = _agendamentoRepository.ObterPorId(idConsulta);
                var horarioDisponivel = _horariosDisponiveisMedicoRepository.ObterPorId(agendamento.HorarioId);

                if (agendamento == null)
                    return new CadastroResponse() { mensagem = "Erro: agendamento não existente" };

                _agendamentoRepository.Excluir(agendamento.Id);
                
                if(horarioDisponivel != null)
                {
                    horarioDisponivel.Disponivel = true;
                    _horariosDisponiveisMedicoRepository.Alterar(horarioDisponivel);
                }

                return new CadastroResponse() { mensagem = "Agendamento excluido com sucesso" };
            }
            catch (Exception ex)
            {
                return new CadastroResponse() { mensagem = $"Erro: houve um erro ao excluir: {ex.Message}" };
            }
        }
    }
}

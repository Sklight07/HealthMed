using HealthMed.Application.Interfaces;
using HealthMed.Application.Requests.Medico;
using HealthMed.Application.Responses.Comum;
using HealthMed.Application.Responses.Medico;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Util;
using Mapster;

namespace HealthMed.Application.Services.Medico
{
    public class MedicoUseCase : IMedicoUseCase
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IHorariosDisponiveisMedicoRepository _horariosDisponiveisMedicoRepository;

        public MedicoUseCase(IMedicoRepository medicoRepository, IHorariosDisponiveisMedicoRepository horariosDisponiveisMedicoRepository)
        {
            _medicoRepository = medicoRepository;
            _horariosDisponiveisMedicoRepository = horariosDisponiveisMedicoRepository;
        }

        public MedicoModel ObterPorEmailSenha(string email, string senha)
        {
            var retorno = _medicoRepository.ObterEmailSenha(email, senha);
            return retorno;
        }

        public List<MedicoModel> ListarTodosMedicosComHorariosDisponiveis()
        {
            var medicos = _medicoRepository.ListarTodosMedicosComHorariosDisponiveis();
            return medicos.ToList();
        }

        public CadastroResponse CadastroMedico(MedicoCadastroRequest medicoCadastroRequest)
        {
            try
            {
                var medico = new MedicoModel();
                medico = medicoCadastroRequest.Adapt<MedicoModel>();
                medico.Permissao = TipoPermissao.Medico;
                _medicoRepository.Cadastrar(medico);
                return new CadastroResponse() { Id = medico.Id, mensagem = medico.Id != 0 ? "Cadastrado com sucesso!" : "Erro ao se cadastrar" };
            }
            catch (Exception ex)
            {
                return new CadastroResponse() { mensagem = $"Erro ao se cadastrar: {ex.Message}" };
            }
        }

        public CadastroResponse ExcluirHorariosPorDiaDaSemana(DateTime data, int idMedico)
        {
            try
            {
                var horariosMedicoPorDia = _horariosDisponiveisMedicoRepository.ObterPorDiaDaSemanaEMedico(data.Date, idMedico);

                if (horariosMedicoPorDia == null)
                    return new CadastroResponse() { mensagem = "Erro: horarios não encontrados para o dia escolhido" };

                if(!horariosMedicoPorDia.Where(w => w.Disponivel == false).ToList().NullOrEmpty())
                    return new CadastroResponse() { mensagem = "Erro: Não é possível excluir os horarios pois ja existem pacientes marcados" };

                foreach (var horario in horariosMedicoPorDia)
                {
                    _horariosDisponiveisMedicoRepository.Excluir(horario.Id);
                }

                return new CadastroResponse() { mensagem = "Horarios excluídos com sucesso" };
            }
            catch (Exception ex)
            {
                return new CadastroResponse() { mensagem = $"Erro: houve um erro ao excluir: {ex.Message}" };
            }

        }

        public CadastroResponse CadastrarHorarios(CadastroHorarioMedicoRequest cadastroHorarioMedicoRequest)
        {
            try
            {
                var medico = _medicoRepository.ObterPorId(cadastroHorarioMedicoRequest.MedicoId);

                if (medico == null)
                    return new CadastroResponse() { mensagem = "Médico não encontrado" };

                var horarioAtual = cadastroHorarioMedicoRequest.HorarioInicio;
                while (horarioAtual < cadastroHorarioMedicoRequest.HorarioFim)
                {
                    var proximoHorario = horarioAtual.Add(TimeSpan.FromMinutes(20));

                    // Verifica se o slot já existe para evitar duplicidade
                    var horarioExistente = _horariosDisponiveisMedicoRepository
                        .VerificaExisteHorariosMedicoPorDataEHora(cadastroHorarioMedicoRequest.MedicoId, cadastroHorarioMedicoRequest.DiaSemana, 
                        horarioAtual, cadastroHorarioMedicoRequest.Data.Date);

                    if (!horarioExistente)
                    {
                        var novoHorario = new HorariosDisponiveisMedicoModel
                        {
                            MedicoId = cadastroHorarioMedicoRequest.MedicoId,
                            DiaSemana = cadastroHorarioMedicoRequest.DiaSemana,
                            HorarioInicio = horarioAtual,
                            HorarioFim = proximoHorario,
                            Data = cadastroHorarioMedicoRequest.Data,
                            Disponivel = true
                        };

                        _horariosDisponiveisMedicoRepository.Cadastrar(novoHorario);
                    }

                    // Avança para o próximo intervalo de 20 minutos
                    horarioAtual = proximoHorario;
                }

                return new CadastroResponse() { mensagem = "Registros cadastrados com sucesso" };
            }
            catch (Exception ex)
            {
                return new CadastroResponse() { mensagem = $"Erro ao cadastrar: {ex.Message}" };
            }


        }

        public List<HorariosCadastradosResponse> ListarHorariosCadastradosPorDia(DateTime data, int idMedico)
        {
            try
            {
                var retorno = new List<HorariosCadastradosResponse>();
                var horariosMedicoPorDia = _horariosDisponiveisMedicoRepository.ObterPorDiaDaSemanaEMedico(data.Date, idMedico);

                if (horariosMedicoPorDia.NullOrEmpty())
                    return new List<HorariosCadastradosResponse>() { new HorariosCadastradosResponse() { CadastroResponse = new CadastroResponse() { mensagem = "Erro: Não existem horarios para esse medico" } } };

                foreach (var horario in horariosMedicoPorDia)
                {
                    var registro = horario.Adapt<HorariosCadastradosResponse>();
                    registro.DiaSemanaConvertido = ConverterDiaParaPortugues(registro.DiaSemana);
                    retorno.Add(registro);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                return new List<HorariosCadastradosResponse>() { new HorariosCadastradosResponse() { CadastroResponse = new CadastroResponse() { mensagem = $"Erro: {ex.Message}" } } };
            }
        }

        public static string ConverterDiaParaPortugues(DayOfWeek dayOfWeek)
        {
            var diasSemanaPortugues = new Dictionary<DayOfWeek, string>
        {
            { DayOfWeek.Sunday, "Domingo" },
            { DayOfWeek.Monday, "Segunda-feira" },
            { DayOfWeek.Tuesday, "Terça-feira" },
            { DayOfWeek.Wednesday, "Quarta-feira" },
            { DayOfWeek.Thursday, "Quinta-feira" },
            { DayOfWeek.Friday, "Sexta-feira" },
            { DayOfWeek.Saturday, "Sábado" }
        };

            return diasSemanaPortugues[dayOfWeek];
        }

        public List<HorariosCadastradosResponse> ListarTodosHorariosPorMedico(int idMedico)
        {
            try
            {
                var retorno = new List<HorariosCadastradosResponse>();
                var horariosMedicoPorDia = _horariosDisponiveisMedicoRepository.ObterPorMedico(idMedico);

                if (horariosMedicoPorDia == null)
                    return null;

                foreach (var horario in horariosMedicoPorDia)
                {
                    var registro = horario.Adapt<HorariosCadastradosResponse>();
                    registro.DiaSemanaConvertido = ConverterDiaParaPortugues(registro.DiaSemana);
                    retorno.Add(registro);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HorariosCadastradosResponse> ListarHorariosDisponiveis(int idMedico, DateTime data)
        {
            try
            {
                var horarios = _horariosDisponiveisMedicoRepository.ObterPorMedico(idMedico);

                if (horarios == null)
                    return new List<HorariosCadastradosResponse>() { new HorariosCadastradosResponse() { CadastroResponse = new CadastroResponse() { mensagem = "Não existem horarios para o médico" } } };

                var horariosDiaDisponiveis = horarios.Where(w => w.Data == data.Date && w.Disponivel == true).ToList();
                if (horariosDiaDisponiveis.NullOrEmpty())//botar pela data depois quando tiver
                    return new List<HorariosCadastradosResponse>() { new HorariosCadastradosResponse() { CadastroResponse = new CadastroResponse() { mensagem = "Não existem mais horarios disponiveis para o médico no dia" } } };

                var retorno = new List<HorariosCadastradosResponse>();

                foreach (var horario in horariosDiaDisponiveis)
                {
                    var registro = horario.Adapt<HorariosCadastradosResponse>();
                    registro.DiaSemanaConvertido = ConverterDiaParaPortugues(registro.DiaSemana);
                    retorno.Add(registro);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                return new List<HorariosCadastradosResponse>() { new HorariosCadastradosResponse() { CadastroResponse = new CadastroResponse() { mensagem = $"Erro: {ex.Message}" } } };
            }
        }
    }
}

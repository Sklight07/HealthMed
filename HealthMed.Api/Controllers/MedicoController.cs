using HealthMed.Application.Interfaces;
using HealthMed.Application.Requests.Medico;
using HealthMed.Application.Services.Paciente;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoUseCase _medicoUseCase;
        private readonly ILogger<MedicoController> _logger;

        public MedicoController(ILogger<MedicoController> logger, IMedicoUseCase medicoUseCase)
        {
            _logger = logger;
            _medicoUseCase = medicoUseCase;
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Paciente}")]
        [HttpGet]
        [Route(nameof(ListarTodosMedicosComHorariosDisponiveis))]
        public ActionResult ListarTodosMedicosComHorariosDisponiveis()
        {
            try
            {
                return Ok(_medicoUseCase.ListarTodosMedicosComHorariosDisponiveis());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a busca");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Paciente}")]
        [HttpGet]
        [Route(nameof(ListarTodosHorariosPorMedico))]
        public ActionResult ListarTodosHorariosPorMedico(int idMedico)
        {
            try
            {
                return Ok(_medicoUseCase.ListarTodosHorariosPorMedico(idMedico));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a busca");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Medico}")]
        [HttpPost]
        [Route(nameof(CadastrarHorarios))]
        public ActionResult CadastrarHorarios([FromBody] CadastroHorarioMedicoRequest cadastroHorarioMedicoRequest)
        {
            try
            {
                if (cadastroHorarioMedicoRequest.HorarioFim <= cadastroHorarioMedicoRequest.HorarioInicio)
                    return BadRequest("Horário de fim deve ser maior que o de início.");

                var retorno = _medicoUseCase.CadastrarHorarios(cadastroHorarioMedicoRequest);
                if(retorno.Id == 0)
                    return BadRequest($"Houve um erro durante o cadastro: {retorno.mensagem}");
                else
                    return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante o cadastro");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Medico}")]
        [HttpDelete]
        [Route(nameof(ExcluirHorariosMedicoPorDia))]
        public ActionResult ExcluirHorariosMedicoPorDia(DateTime data, int idMedico)
        {
            try
            {
                var retorno = _medicoUseCase.ExcluirHorariosPorDiaDaSemana(data, idMedico);
                if (retorno.mensagem.Contains("Erro"))
                    return BadRequest(retorno.mensagem);
                else
                    return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante o cadastro");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Medico}")]
        [HttpGet]
        [Route(nameof(ListarHorariosCadastradosPorDia))]
        public ActionResult ListarHorariosCadastradosPorDia(DateTime data, int idMedico)
        {
            try
            {
                var retorno = _medicoUseCase.ListarHorariosCadastradosPorDia(data.Date, idMedico);

                if (retorno.FirstOrDefault().CadastroResponse != null && retorno.FirstOrDefault().CadastroResponse.mensagem.Contains("Erro"))
                    return BadRequest(retorno.FirstOrDefault().CadastroResponse.mensagem);
                else
                    return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest($"Houve um erro durante a busca: {ex.Message} ");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Medico}, {Permissao.Paciente}")]
        [HttpGet]
        [Route(nameof(ListarHorariosDisponiveisMedico))]
        public ActionResult ListarHorariosDisponiveisMedico(int idMedico, DateTime data)
        {
            try
            {
                var retorno = _medicoUseCase.ListarHorariosDisponiveis(idMedico, data);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest($"Houve um erro durante a busca: {ex.Message} ");
            }
        }
    }
}

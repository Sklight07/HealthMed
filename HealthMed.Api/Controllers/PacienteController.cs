using HealthMed.Application.Interfaces;
using HealthMed.Application.Requests.Medico;
using HealthMed.Application.Requests.Paciente;
using HealthMed.Application.Services.Medico;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthMed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteUseCase _pacienteUseCase;
        private readonly ILogger<PacienteController> _logger;

        public PacienteController(ILogger<PacienteController> logger, IPacienteUseCase pacienteUseCase)
        {
            _logger = logger;
            _pacienteUseCase = pacienteUseCase;
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Medico}")]
        [HttpGet]
        [Route(nameof(ListarTodosPacientes))]
        public ActionResult ListarTodosPacientes()
        {
            try
            {
                return Ok(_pacienteUseCase.ObterTodos());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a busca");
            }
        }

        [Authorize(Roles = $"{Permissao.Paciente}")]
        [HttpPost]
        [Route(nameof(AgendarConsulta))]
        public ActionResult AgendarConsulta([FromBody] AgendarConsultaRequest agendarConsultaRequest)
        {
            try
            {
                var retorno = _pacienteUseCase.AgendarConsulta(agendarConsultaRequest);
                if (retorno.Id == 0)
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
        [Authorize(Roles = $"{Permissao.Paciente}")]
        [HttpGet]
        [Route(nameof(ListarAgendamentosPaciente))]
        public ActionResult ListarAgendamentosPaciente(int idPaciente)
        {
            try
            {
                var retorno = _pacienteUseCase.ListarAgendamentosPaciente(idPaciente);

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
        [Authorize(Roles = $"{Permissao.Paciente}")]
        [HttpDelete]
        [Route(nameof(ExcluirAgendamentoConsulta))]
        public ActionResult ExcluirAgendamentoConsulta(int idConsulta)
        {
            try
            {
                var retorno = _pacienteUseCase.ExcluirAgendamentoConsulta(idConsulta);
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
    }
}

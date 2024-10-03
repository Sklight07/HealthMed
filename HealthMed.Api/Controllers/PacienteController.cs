using HealthMed.Application.Interfaces;
using HealthMed.Application.Services.Medico;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}

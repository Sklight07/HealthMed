using HealthMed.Application.Interfaces;
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
        [Route(nameof(ListarTodosMedicos))]
        public ActionResult ListarTodosMedicos()
        {
            try
            {
                return Ok(_medicoUseCase.ObterTodos());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a busca");
            }
        }
    }
}

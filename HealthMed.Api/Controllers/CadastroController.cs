using HealthMed.Api.Services;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Requests.Login;
using HealthMed.Application.Requests.Medico;
using HealthMed.Application.Requests.Paciente;
using HealthMed.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        private readonly IMedicoUseCase _medicoUseCase;
        private readonly IPacienteUseCase _pacienteUseCase;

        public CadastroController(IMedicoUseCase medicoUseCase, IPacienteUseCase pacienteUseCase)
        {
            _medicoUseCase = medicoUseCase;
            _pacienteUseCase = pacienteUseCase;
        }

        [AllowAnonymous]
        [HttpPost("Medico")]
        public IActionResult CadastroMedico([FromBody] MedicoCadastroRequest medicoCadastroRequest)
        {
            try
            {
                var retorno = _medicoUseCase.CadastroMedico(medicoCadastroRequest);
                return Ok(retorno);
            }
            catch (Exception ex )
            {
                return BadRequest("Houve um erro durante o cadastro: " + ex.Message );
            }
        }

        [AllowAnonymous]
        [HttpPost("Paciente")]
        public IActionResult CadastroPaciente([FromBody] PacienteCadastroRequest pacienteCadastroRequest)
        {
            try
            {
                var retorno =  _pacienteUseCase.CadastroPaciente(pacienteCadastroRequest);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest("Houve um erro durante a compra da ação" + ex.Message);
            }
        }
    }
}

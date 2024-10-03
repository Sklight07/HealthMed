using HealthMed.Api.Services;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Requests.Login;
using HealthMed.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMedicoUseCase _medicoUseCase;
        private readonly IPacienteUseCase _pacienteUseCase;
        private readonly ITokenService _tokenService;

        public LoginController(IMedicoUseCase medicoUseCase, IPacienteUseCase pacienteUseCase, ITokenService tokenService)
        {
            _medicoUseCase = medicoUseCase;
            _pacienteUseCase = pacienteUseCase;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var userMedico = _medicoUseCase.ObterPorEmailSenha(request.Email, request.Senha);
            UsuarioComumModel usuario = new UsuarioComumModel();
            if(userMedico != null)
                usuario = userMedico.Adapt<UsuarioComumModel>();
            else
            {
                var userPaciente = _pacienteUseCase.ObterPorEmailSenha(request.Email, request.Senha);
                if(userPaciente != null)
                    usuario = userPaciente.Adapt<UsuarioComumModel>();
                else
                    return Unauthorized(new { mensagem = "Usuário ou senha não encontrados" });
            }

            var token = _tokenService.GenerateToken(usuario);

            return Ok(new { token });
        }
    }
}

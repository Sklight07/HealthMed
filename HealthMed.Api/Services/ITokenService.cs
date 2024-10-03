using HealthMed.Domain.Entities;

namespace HealthMed.Api.Services
{
    public interface ITokenService
    {
        string GenerateToken(UsuarioComumModel usuario);
    }
}

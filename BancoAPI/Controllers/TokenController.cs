using Domain;
using Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using RepositoryEntity.Context;
using Services;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        TokenServices _services;
        UsuarioService _usuarioService;
        BancoContext _contexto;

        public TokenController(IConfiguration configuration,
                               BancoContext contexto)
        {
            _services = new TokenServices(configuration);
            _contexto = contexto;
            _usuarioService = new UsuarioService(configuration, _contexto);
        }

        [HttpPost("GerarToken")]
        public ActionResult<string> GerarToken([FromBody] LoginResource login)
        {
            try
            {
                var user = _usuarioService.Login(login);

                if (user is not null)
                {
                    string token = _services.GenerateJwtToken(login.Username);
                    return Ok(new { token });
                }

                return BadRequest("Token Inválido!");
            }
            catch (Exception)
            {
                return BadRequest("Login inválido!");
            }
        }
    }
}
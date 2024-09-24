using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        LoginServices _services;

        public LoginController(IConfiguration configuration)
        {
            _services = new LoginServices(configuration);
        }

        [HttpPost("GerarToken")]
        public ActionResult<string> GerarToken([FromBody] Login login)
        {
            if (login.UserName == "teste" && login.Password == "123456")
            {
                string token = _services.GenerateJwtToken(login.UserName);
                return Ok(new { token });
            }
            return BadRequest("Token Inválido!");
        }
    }
}
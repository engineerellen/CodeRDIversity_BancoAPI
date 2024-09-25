using Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using RepositoryEntity.Context;
using Services;


namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        UsuarioService _service;
        IConfiguration _configuration;
        BancoContext _bancoContext;

        public UsuariosController(IConfiguration configuration, BancoContext bancoContext)
        {  
            _configuration = configuration;
            _bancoContext = bancoContext;
            _service = new UsuarioService(_configuration, _bancoContext);
        }

        [HttpPost("NovoUsuario")]
        public ActionResult NovoUsuario([FromBody] RegistroResource novoUsuario)
        {
            try
            {
              var user =   _service.Registro(novoUsuario);

                if (user is not null)
                    return Created();

                return BadRequest("Não foi possível inserir o usuario!");
            }
            catch 
            {
                return BadRequest("Não foi possível inserir o usuario!");
            }
        }


        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginResource login)
        {
            try
            {
              var user =  _service.Login(login);

                if (user is not null)
                    return Ok($"{user.username},{user.email}");
                return BadRequest("login inválido!");

            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar o login: " + ex.Message);
            }
        }
    }
}
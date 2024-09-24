using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryEntity;
using RepositoryEntity.Context;
using Services;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaPessoaFisicaController : ControllerBase
    {
        BancoContext _contexto;
        IConfiguration _configuration;
        ContaPessoaFisicaService _service;

        public ContaPessoaFisicaController(IConfiguration configuration
                                           , BancoContext contexto)
        {
            _configuration = configuration;
            _contexto = contexto;
            _service = new ContaPessoaFisicaService(_contexto, _configuration);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ContaPessoaFisicaDomain>> Get()
        {
            try
            {
                return _service.GetAllContasPessoasFisica() ?? new List<ContaPessoaFisicaDomain>();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("VerificarExtrato")]
        public ActionResult<List<Historico>>? VerificarExtrato(int idConta, int mesReferencia)
        {
            try
            {
                return _service.VerificarExtrato(idConta, mesReferencia) ?? new List<Historico>();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContaPessoaFisicaDomain contaPessoaFisica)
        {
            try
            {
                _service.CadastrarContaPesoaFisica(contaPessoaFisica);
                return Ok("Conta cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Sacar")]
        public IActionResult Sacar(decimal valorSaque, [FromBody] ContaPessoaFisicaDomain contaPessoaFisica)
        {
            try
            {
                contaPessoaFisica.Sacar(valorSaque);
                _service.AtualizarConta(contaPessoaFisica);
                _service.CadastrarHistorico(contaPessoaFisica, ETipoTransacao.Saque);

                return Ok("Saque feito com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Depositar")]
        public IActionResult Depositar(decimal valorDeposito, [FromBody] ContaPessoaFisicaDomain contaPessoaFisica)
        {
            try
            {
                contaPessoaFisica.Depositar(valorDeposito);
                _service.AtualizarConta(contaPessoaFisica);
                _service.CadastrarHistorico(contaPessoaFisica, ETipoTransacao.Deposito);

                return Ok("Depósito feito com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idContaPF}")]
        public IActionResult Put(int idConta, int idContaPF, string nomeConta)
        {
            try
            {
                ContaPessoaFisicaDomain contaPF = new() { IDConta = idConta, ID_ContaPF = idContaPF };
                contaPF.SetarNome(nomeConta);

                _service.AtualizarConta(contaPF);

                return Ok("Nome Setado com sucesso!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idContaPF}")]
        public IActionResult InativarConta(int idConta, int idContaPF)
        {
            try
            {
                ContaPessoaFisicaDomain contaPF = new() { IDConta = idConta, ID_ContaPF = idContaPF };

                _service.InativarConta(contaPF);
                return Ok("Conta encerrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
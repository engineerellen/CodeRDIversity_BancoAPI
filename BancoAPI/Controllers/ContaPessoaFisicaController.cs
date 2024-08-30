using BancoAPI.DTO;
using Domain;
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
        public ActionResult<List<Domain.ContaPessoaFisica>> Get()
        {
            try
            {
                return _service.GetAllContasPessoasFisica()??new List<Domain.ContaPessoaFisica>();
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
                return _service.VerificarExtrato(idConta, mesReferencia)?? new List<Historico>();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Domain.ContaPessoaFisica contaPessoaFisica)
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

        [HttpPost("Transferir")]
        public IActionResult Transferir([FromBody] ContaPara contaPara)
        {
            try
            {
                if (contaPara.TipoContaPFPJ != "PF")
                    return BadRequest("Conta precisa ser pessoa física!");

                var ctaPF = contaPara.TransferirConta(contaPara);
                _service.AtualizarConta(ctaPF);
                _service.CadastrarHistorico(ctaPF, ETipoTransacao.Transferencia);

                return Ok("Transferido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Sacar")]
        public IActionResult Sacar(decimal valorSaque, [FromBody] Domain.ContaPessoaFisica contaPessoaFisica)
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
        public IActionResult Depositar(decimal valorDeposito, [FromBody] Domain.ContaPessoaFisica contaPessoaFisica)
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
                Domain.ContaPessoaFisica contaPF = new() { IDConta = idConta, ID_ContaPF = idContaPF };
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
                Domain.ContaPessoaFisica contaPF = new() { IDConta = idConta, ID_ContaPF = idContaPF };

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
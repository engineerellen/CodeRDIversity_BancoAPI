using BancoAPI.DTO;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaPessoaFisicaController : ControllerBase
    {
        Conta conta;

        IConfiguration _configuration;
        ContaPessoaFiscaRepositoryADO _repositoryADO;

        public ContaPessoaFisicaController(IConfiguration configuration)
        {
            _configuration = configuration;
            _repositoryADO = new ContaPessoaFiscaRepositoryADO(_configuration);

            conta = new ContaPessoaFisica("Aposentadoria", ETipoConta.Investimento);
            conta.Agencia = "0001";
            conta.ID = 1;
            conta.Digito = "1";
            conta.NumeroConta = " 2345678";
            conta.Pix = "(11)99999-9999";
            conta.ValorConta = 1000000;
        }

        [HttpGet]
        public ActionResult<List<ContaPessoaFisica>> Get()
        {
            try
            {
                return _repositoryADO.RetornarContasPF(ETipoConta.Corrente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{data}")]
        public ActionResult<IEnumerable<Conta>> Get(DateTime data)
        {
            try
            {
                return conta.VerExtrato(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContaPessoaFisica contaPessoaFisica)
        {
            try
            {
                _repositoryADO.CadastrarContasPF(contaPessoaFisica);
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

                Conta ctaPara = new ContaPessoaFisica();
                ctaPara.Pix = contaPara.Pix;
                ctaPara.ValorConta = conta.ValorConta;

                conta.Transferir(ctaPara);
                return Ok("Transferido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string value)
        {
            try
            {
                conta.SetarNome(value);
                return Ok("Nome Setado com sucesso!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{codigoConta}")]
        public IActionResult Delete([FromBody] ContaPessoaFisica contaPessoaFisica)
        {
            try
            {
                _repositoryADO.InativarContasPF(contaPessoaFisica);
                return Ok("Conta encerrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
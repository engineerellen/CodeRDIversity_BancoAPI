using BancoTeste.Mocker;
using Domain;
using Microsoft.Extensions.Configuration;
using Moq;
using RepositoryEntity;
using RepositoryEntity.Context;
using RepositoryEntity.Models;
using Services;


namespace BancoTeste
{
    public class TesteContaPessoaFisicaService
    {
        private readonly Mock<BancoContext> _mockContext;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ContaPessoaFisicaService _service;


        public TesteContaPessoaFisicaService()
        {
            _mockContext = new Mock<BancoContext>();
            _configurationMock = new Mock<IConfiguration>();
            _service = new ContaPessoaFisicaService(_mockContext.Object, _configurationMock.Object);
        }

        [Fact]
        public void CadastrarContaPessoaFisica_ContaExistente_ReturnsContaExistenteMessage()
        {
            // Arrange
            var contaExistente = new Contum
            {
                IdConta = 1,
                Agencia = "1234",
                NumeroConta = "56789",
                Digito = "0",
                IdTipoConta = (int)ETipoConta.Corrente
            };

            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 1,
                ID_ContaPF = 1,
                Agencia = "1234",
                NumeroConta = "56789",
                Digito = "0",
                TipoConta = ETipoConta.Corrente
            };

            // Mock GetContaById 
            _mockContext.Setup(c => c.Conta)
                .Returns(DbSetMocker.GetQueryableMockDbSet(new List<Contum> { contaExistente }));

            // Act
            var result = _service.CadastrarContaPesoaFisica(contaPFDomain);

            // Assert
            Assert.Equal("Conta existente no sistema!", result);
        }

        [Fact]
        public void CadastrarContaPessoaFisica_NovaConta_ReturnsSuccessMessage()
        {
            // Arrange
            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 0,
                Agencia = "1234",
                NumeroConta = "56789",
                Digito = "0",
                TipoConta = ETipoConta.Corrente,
                CPF = "12345678900",
                NomeCliente = "Cliente Teste",
                ValorConta = 1000m
            };

            // Mock GetContaByID
            _mockContext.Setup(c => c.Conta).Returns(DbSetMocker.GetQueryableMockDbSet(new List<Contum>()));
            _mockContext.Setup(c => c.ContaPessoaFisicas).Returns(DbSetMocker.GetQueryableMockDbSet(new List<ContaPessoaFisica>()));

            // Act
            var result = _service.CadastrarContaPesoaFisica(contaPFDomain);

            // Assert
            Assert.Equal("Conta Pessoa Física cadastrada com sucesso!", result);
        }

        [Fact]
        public void CadastrarContaPessoaFisica_NovaConta_ReturnsContaInvalidaMessage()
        {
            // Arrange
            ContaPessoaFisicaDomain contaPFDomain = null;

            // Act
            var result = _service.CadastrarContaPesoaFisica(contaPFDomain);

            // Assert
            Assert.Equal("Conta inválida!", result);
        }

        [Fact]
        public void AtualizarConta_ContaExistente_ReturnsSuccessMessage()
        {
            // Arrange
            var contaExistente = new Contum
            {
                IdConta = 1,
                Agencia = "1234",
                NumeroConta = "56789",
                Digito = "0",
                IdTipoConta = (int)ETipoConta.Corrente,
                Ativo = true
            };

            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 1,
                Agencia = "1234",
                NumeroConta = "56789",
                Digito = "0",
                TipoConta = ETipoConta.Corrente,
                CPF = "12345678900",
                NomeCliente = "Cliente Atualizado",
                ValorConta = 2000m
            };

            // Mock GetContaByID
            _mockContext.Setup(c => c.Conta)
                .Returns(DbSetMocker.GetQueryableMockDbSet(new List<Contum> { contaExistente }));

            // Act
            var result = _service.AtualizarConta(contaPFDomain);

            // Assert
            Assert.Equal("Conta Pessoa Física alterada com sucesso!", result);
        }

        [Fact]
        public void AtualizarConta_ContaExistente_ReturnsContaInvalidaMessage()
        {
            // Arrange
            ContaPessoaFisicaDomain contaPFDomain = null;

            // Act
            var result = _service.AtualizarConta(contaPFDomain);

            // Assert
            Assert.Equal("Conta inválida!", result);
        }

        [Fact]
        public void GetContaPessoaFisicaByID_Success()
        {
            //Arrange
            var contaPF = new ContaPessoaFisica
            {
                IdContaPf = 1,
                NomeCliente = "Alexa",
                Cpf = "99999999999"
            };

            // Mock GetContaByID
            _mockContext.Setup(c => c.ContaPessoaFisicas)
                .Returns(DbSetMocker.GetQueryableMockDbSet(new List<ContaPessoaFisica> { contaPF }));

            //Act
            var result = _service.GetContaPessoaFisicaById(contaPF.IdContaPf);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetContaPessoaFisicaByID_ReturnNull_IDContaZero()
        {
            //Act
            var result = _service.GetContaPessoaFisicaById(0);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetContaPessoaFisicaByID_ReturnNull_ResultadoNaoEncontrado()
        {
            //Act
            var result = _service.GetContaPessoaFisicaById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetContaByID_Success()
        {
            //Arrange
            var conta = new Contum
            {
                IdConta = 1,
                Agencia = "0001"
               ,
                NomeConta = "Conta Corrente1"
               ,
                NumeroConta = "367895"
               ,
                Digito = "0"
               ,
                Pix = "(11)99999-9999"
            };

            // Mock GetContaByID
            _mockContext.Setup(c => c.Conta)
                .Returns(DbSetMocker.GetQueryableMockDbSet(new List<Contum> { conta }));

            //Act
            var result = _service.GetContaById(conta.IdConta);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetContaByID_ReturnNull_IDContaZero()
        {
            //Act
            var result = _service.GetContaById(0);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetContaByID_ReturnNull_ResultadoNaoEncontrado()
        {
            //Act
            var result = _service.GetContaById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllContasPessoasFisica_Success()
        {
            //Arrange
            var contaPF = new ContaPessoaFisica
            {
                IdContaPf = 1,
                NomeCliente = "Alexa",
                Cpf = "99999999999"
            };

            // Mock GetContaByID
            _mockContext.Setup(c => c.ContaPessoaFisicas)
                .Returns(DbSetMocker.GetQueryableMockDbSet(new List<ContaPessoaFisica> { contaPF }));

            //Act
            var result = _service.GetAllContasPessoasFisica();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public void GetAllContasPessoasFisica_ReturnNull()
        {
            //Act
            var result = _service.GetAllContasPessoasFisica();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void InativarConta_ContaExistente_ReturnsSuccessMessage()
        {
            // Arrange
            var contaExistente = new Contum
            {
                IdConta = 1,
                NomeConta = "Conta Teste",
                Ativo = false
            };

            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 1,
                EstaAtiva = false
            };

            //Mock GetContaByID
            _mockContext.Setup(c => c.Conta)
                .Returns(DbSetMocker.GetQueryableMockDbSet(new List<Contum> { contaExistente }));

            // Act
            var result = _service.InativarConta(contaPFDomain);

            // Assert
            Assert.Equal($"Conta {contaExistente.NomeConta} inativada com sucesso!", result);
        }

        [Fact]
        public void InativarConta_ContaExistente_ReturnsContaInvalidaMessage()
        {
            // Arrange
            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 0,
                EstaAtiva = false
            };

            // Act
            var result = _service.InativarConta(contaPFDomain);

            // Assert
            Assert.Equal("Conta inválida! Por favor tente novamente.", result);
        }

        [Fact]
        public void InativarConta_ContaExistente_ReturnsContaNaoCadastradaMessage()
        {
            // Arrange
            var contaExistente = new Contum
            {
                IdConta = 1
            };

            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 1,
                EstaAtiva = false
            };

            // Act
            var result = _service.InativarConta(contaPFDomain);

            // Assert
            Assert.Equal("Conta não cadastrada!", result);
        }

        [Fact]
        public void CadastrarHistorico_ReturnsSuccessMessage()
        {
            // Arrange
            var conta = new Contum
            {
                IdConta = 1,
                NomeConta = "Conta Teste",
                Agencia = "0001",
                NumeroConta = "1362531",
                Digito = "9",
                Pix = "(11)99999-9999"
            };

            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 1,
                Agencia = "1234",
                NumeroConta = "56789",
                Digito = "0",
                TipoConta = ETipoConta.Corrente,
                CPF = "12345678900",
                NomeCliente = "Cliente Teste",
                ValorConta = 1000m
            };

            ETipoTransacao tipoTransacaoEnum = ETipoTransacao.Saque;
            TipoTransacao tipoTransacaoMock = new() { IdTipoTransacao = 1, Codigo = (int)tipoTransacaoEnum };

            // Mock GetContaByID
            _mockContext.Setup(c => c.Conta).Returns(DbSetMocker.GetQueryableMockDbSet(new List<Contum>() { conta }));

            //mock TipoTransacao
            _mockContext.Setup(c => c.TipoTransacaos).Returns(DbSetMocker.GetQueryableMockDbSet(new List<TipoTransacao>() { tipoTransacaoMock }));

            // Act
            var result = _service.CadastrarHistorico(contaPFDomain, tipoTransacaoEnum);

            // Assert
            Assert.Equal("Transação cadastrada com sucesso!", result);
        }

        [Fact]
        public void CadastrarHistorico_ReturnsContaInexistenteMessage()
        {
            // Arrange
            var conta = new Contum
            {
                IdConta = 1,
                NomeConta = "Conta Teste",
                Agencia = "0001",
                NumeroConta = "1362531",
                Digito = "9",
                Pix = "(11)99999-9999"
            };

            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 1,
                Agencia = "1234",
                NumeroConta = "56789",
                Digito = "0",
                TipoConta = ETipoConta.Corrente,
                CPF = "12345678900",
                NomeCliente = "Cliente Teste",
                ValorConta = 1000m
            };

            ETipoTransacao tipoTransacaoEnum = ETipoTransacao.Saque;
            TipoTransacao tipoTransacaoMock = new() { IdTipoTransacao = 1, Codigo = (int)tipoTransacaoEnum };

            //mock TipoTransacao
            _mockContext.Setup(c => c.TipoTransacaos).Returns(DbSetMocker.GetQueryableMockDbSet(new List<TipoTransacao>() { tipoTransacaoMock }));

            // Act
            var result = _service.CadastrarHistorico(contaPFDomain, tipoTransacaoEnum);

            // Assert
            Assert.Equal("Conta inexistente!", result);
        }

        [Fact]
        public void CadastrarHistorico_ReturnsContaInvalidaMessage()
        {
            // Arrange

            ContaPessoaFisicaDomain contaPFDomain = null;
            ETipoTransacao tipoTransacaoEnum = ETipoTransacao.Saque;

            // Act
            var result = _service.CadastrarHistorico(contaPFDomain, tipoTransacaoEnum);

            // Assert
            Assert.Equal("Conta inválida!", result);
        }

        [Fact]
        public void VerificarExtrato_Success()
        {
            //Arrange
            var historico = new Historico
            {
                DtTransacao = DateTime.Now,
                IdConta = 1,
                IdHistorico = 1,
                IdTipoTransacao = 1,
                Valor = 200
            };

            // Mock GetContaByID
            _mockContext.Setup(c => c.Historicos)
                .Returns(DbSetMocker.GetQueryableMockDbSet(new List<Historico> { historico }));

            //Act
            var result = _service.VerificarExtrato(historico.IdConta, historico.DtTransacao.Month);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public void VerificarExtrato_Null()
        {
            //Act
            var result = _service.VerificarExtrato(1, 8);

            //Assert
            Assert.Null(result);
        }
    }
}
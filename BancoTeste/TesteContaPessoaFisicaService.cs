using Moq;
using RepositoryEntity.Context;
using Services;
using Microsoft.Extensions.Configuration;
using RepositoryEntity.Models;
using BancoTeste.Mocker;
using Domain;


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

            // Mock DbSets
            _mockContext.Setup(c => c.Conta).Returns(DbSetMocker.GetQueryableMockDbSet(new List<Contum>()));
            _mockContext.Setup(c => c.ContaPessoaFisicas).Returns(DbSetMocker.GetQueryableMockDbSet(new List<ContaPessoaFisica>()));

            // Act
            var result = _service.CadastrarContaPesoaFisica(contaPFDomain);

            // Assert
            Assert.Equal("Conta Pessoa Física cadastrada com sucesso!", result);

            // Verifica se as entidades foram adicionadas ao contexto
            _mockContext.Verify(c => c.Add(It.IsAny<Contum>()), Times.Once);
            _mockContext.Verify(c => c.Add(It.IsAny<ContaPessoaFisica>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Exactly(2)); // Salva tanto para Conta quanto para ContaPessoaFisica
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
        public void InativarConta_ContaExistente_ReturnsSuccessMessage()
        {
            // Arrange
            var contaExistente = new Contum
            {
                IdConta = 1,
                NomeConta = "Conta Teste",
                Ativo = true
            };

            var contaPFDomain = new ContaPessoaFisicaDomain
            {
                IDConta = 1,
                EstaAtiva = false
            };

            //Mock Conta
            _mockContext.Setup(c => c.Conta)
                .Returns(DbSetMocker.GetQueryableMockDbSet(new List<Contum> { contaExistente }));

            // Act
            var result = _service.InativarConta(contaPFDomain);

            // Assert
            Assert.Equal("Conta Conta Teste inativada com sucesso!", result);

            // Verifica se a entidade foi atualizada no contexto
            _mockContext.Verify(c => c.Update(It.IsAny<Contum>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}

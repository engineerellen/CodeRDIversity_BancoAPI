using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryEntity;
using RepositoryEntity.Context;
using RepositoryEntity.Models;

namespace Services
{
    public class ContaPessoaFisicaService
    {
        private BancoContext _contexto;
        IConfiguration _configuration;

        public ContaPessoaFisicaService(BancoContext contexto, IConfiguration configuration)
        {
            _contexto = contexto;
            _configuration = configuration;
        }

        public string CadastrarContaPesoaFisica(ContaPessoaFisicaDomain contaPFDomain)
        {
            try
            {
                _contexto.Database?.BeginTransaction();

                if (contaPFDomain != null)
                {
                    var contaExistente = GetContaById(contaPFDomain.IDConta);

                    if (contaExistente != null)
                    {
                        if (contaExistente.Agencia == contaPFDomain.Agencia
                            && contaExistente.NumeroConta == contaPFDomain.NumeroConta
                            && contaExistente.Digito == contaPFDomain.Digito
                            && contaExistente.IdTipoConta == (int)contaPFDomain.TipoConta)
                             return "Conta existente no sistema!";
                    }

                    if (contaExistente == null)
                    {
                        var conta_entity = new Contum()
                        {
                            Agencia = contaPFDomain.Agencia
                        ,
                            Ativo = contaPFDomain.EstaAtiva
                        ,
                            Digito = contaPFDomain.Digito
                        ,
                            IdTipoConta = (int)contaPFDomain.TipoConta
                        ,
                            NomeConta = contaPFDomain.NomeConta
                        ,
                            NumeroConta = contaPFDomain.NumeroConta
                        ,
                            Pix = contaPFDomain.Pix
                        ,
                            ValorConta = contaPFDomain.ValorConta
                        };

                        _contexto.Add(conta_entity);
                        _contexto.SaveChanges();

                        var contaPFEntity = new ContaPessoaFisica();

                        var contaEntity = _contexto.Conta.Where(c => c.Agencia == contaPFDomain.Agencia &&
                                                          c.NumeroConta == contaPFDomain.NumeroConta &&
                                                          c.Digito == contaPFDomain.Digito
                                                          && c.IdTipoConta == (int)contaPFDomain.TipoConta).LastOrDefault();

                        contaPFEntity.IdConta = contaEntity is not null ? contaEntity.IdConta : 0;
                        contaPFEntity.Cpf = contaPFDomain.CPF;
                        contaPFEntity.RendaFamiliar = contaPFDomain.RendaFamiliar;
                        contaPFEntity.Profissao = contaPFDomain.Profissao;
                        contaPFEntity.Endereco = contaPFDomain.Endereco;
                        contaPFEntity.Genero = contaPFDomain?.Genero;
                        contaPFEntity.NomeCliente = contaPFDomain?.NomeCliente ?? string.Empty;

                        _contexto.Add(contaPFEntity);
                        _contexto.SaveChanges();

                        _contexto.Database?.CommitTransaction();

                        return "Conta Pessoa Física cadastrada com sucesso!";
                    }
                    else
                        return "Conta já existente!";
                }
                else
                    return "Conta inválida!";
            }
            catch (SqlException)
            {
                _contexto.Database?.RollbackTransaction();

                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                _contexto.Database?.RollbackTransaction();

                return ex.Message;
            }
        }

        public string AtualizarConta(ContaPessoaFisicaDomain contaPFDomain)
        {
            try
            {
                if (contaPFDomain != null)
                {
                    var contaDbo = GetContaById(contaPFDomain.IDConta);

                    if (contaDbo is not null)
                    {
                        contaDbo.Agencia = contaPFDomain.Agencia == "string" ? contaDbo.Agencia : contaPFDomain.Agencia;
                        contaDbo.ValorConta = contaPFDomain.ValorConta;
                        contaDbo.NomeConta = contaPFDomain.NomeConta == "string" ? contaDbo.NomeConta : contaPFDomain.NomeConta;
                        contaDbo.NumeroConta = contaPFDomain.NumeroConta == "string" ? contaDbo.NumeroConta : contaPFDomain.NumeroConta;
                        contaDbo.Ativo = contaPFDomain.EstaAtiva;
                        contaDbo.Digito = contaPFDomain.Digito == "string" ? contaDbo.Digito : contaPFDomain.Digito;
                        contaDbo.IdTipoConta = (int)contaPFDomain.TipoConta;
                        contaDbo.Pix = contaPFDomain.Pix == "string" ? contaDbo.Pix : contaPFDomain.Pix;

                        _contexto.Update(contaDbo);
                        _contexto.SaveChanges();
                    }

                    if (contaPFDomain.CPF != "string")
                    {
                        var contaPFEntity = new ContaPessoaFisica();
                        contaPFEntity.Cpf = contaPFDomain.CPF;
                        contaPFEntity.RendaFamiliar = contaPFDomain.RendaFamiliar;
                        contaPFEntity.Profissao = contaPFDomain.Profissao;
                        contaPFEntity.Endereco = contaPFDomain.Endereco;
                        contaPFEntity.Genero = contaPFDomain?.Genero;
                        contaPFEntity.NomeCliente = contaPFDomain?.NomeCliente ?? string.Empty;

                        _contexto.Update(contaPFEntity);
                        _contexto.SaveChanges();
                    }
                    return "Conta Pessoa Física alterada com sucesso!";
                }
                else
                    return "Conta inválida!";
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ContaPessoaFisica? GetContaPessoaFisicaById(int idContaPF)
        {
            var pessoaFisica = new ContaPessoaFisica();

            try
            {
                if (idContaPF == 0)
                    return null;

                var pf = _contexto.ContaPessoaFisicas.Where(x => x.IdContaPf == idContaPF).ToList();
                pessoaFisica = pf?.SingleOrDefault();

                if (pessoaFisica != null)
                    return pessoaFisica;

                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Contum? GetContaById(int idConta)
        {
            var conta = new Contum();

            try
            {
                if (idConta == 0)
                    return null;

                var cta = _contexto.Conta.Where(x => x.IdConta == idConta).ToList();
                conta = cta?.SingleOrDefault();

                if (conta != null)
                    return conta;

                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ContaPessoaFisicaDomain>? GetAllContasPessoasFisica()
        {
            var lstContasPF_Entity = new List<ContaPessoaFisica>();
            try
            {
                lstContasPF_Entity = _contexto.ContaPessoaFisicas.ToList();

                if (lstContasPF_Entity != null)
                {
                    var listaContaPF_Domain = new List<ContaPessoaFisicaDomain>();

                    foreach (var contaPF in lstContasPF_Entity)
                    {
                        listaContaPF_Domain.Add(
                            new ContaPessoaFisicaDomain()
                            {
                                CPF = contaPF?.Cpf ?? string.Empty
                               ,
                                NomeCliente = contaPF?.NomeCliente ?? string.Empty
                               ,
                                Genero = contaPF?.Genero ?? string.Empty
                               ,
                                Endereco = contaPF?.Endereco ?? string.Empty
                               ,
                                Profissao = contaPF?.Profissao ?? string.Empty
                               ,
                                RendaFamiliar = contaPF?.RendaFamiliar ?? decimal.MinValue
                            });
                    }
                    return listaContaPF_Domain;
                }

                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string InativarConta(ContaPessoaFisicaDomain contaPF)
        {
            try
            {
                if (contaPF.IDConta == 0)
                    return "Conta inválida! Por favor tente novamente.";

                else
                {
                    var contaDbo = GetContaById(contaPF.IDConta);

                    if (contaDbo != null)
                    {
                        contaPF.EncerrarConta();

                        contaDbo.Ativo = contaPF.EstaAtiva;

                        _contexto.Update(contaDbo);
                        _contexto.SaveChanges();

                        return $"Conta {contaDbo.NomeConta} inativada com sucesso!";
                    }
                    else
                        return "Conta não cadastrada!";
                }
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string CadastrarHistorico(ContaPessoaFisicaDomain contaPFDomain, ETipoTransacao tipoTransacao)
        {
            try
            {
                if (contaPFDomain != null)
                {
                    var contaExistente = GetContaById(contaPFDomain.IDConta);

                    var objTpTransacao = _contexto.TipoTransacaos.Where(t => t.Codigo == (int)tipoTransacao).FirstOrDefault();

                    if (contaExistente != null)
                    {
                        var historico = new Historico()
                        {
                            DtTransacao = DateTime.Now
                            ,
                            IdConta = contaPFDomain.IDConta
                            ,
                            IdTipoTransacao = objTpTransacao != null ? objTpTransacao.IdTipoTransacao : (int)tipoTransacao
                            ,
                            Valor = contaPFDomain.ValorConta
                        };

                        _contexto.Add(historico);
                        _contexto.SaveChanges();

                        return "Transação cadastrada com sucesso!";
                    }
                    else
                    {
                        return "Conta inexistente!";
                    }
                }
                else
                    return "Conta inválida!";
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Historico>? VerificarExtrato(int idConta, int mesReferencia)
        {
            try
            {
                if (idConta > 0)
                {
                    var contaExistente = GetContaById(idConta);

                    return _contexto.Historicos.Where(h => h.IdConta == idConta
                                                   && h.DtTransacao.Month == mesReferencia
                                                   && h.DtTransacao.Year == DateTime.Now.Year).ToList();
                }

                else
                    return null;
            }
            catch (SqlException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
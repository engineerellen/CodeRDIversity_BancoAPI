using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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

        public string CadastrarContaPesoaFisica(Domain.ContaPessoaFisica contaPFDomain)
        {
            try
            {
                _contexto.Database.BeginTransaction();

                if (contaPFDomain != null)
                {
                    var contaExistente = GetContaById(contaPFDomain.IDConta);

                    if (contaExistente != null)
                    {
                        if (contaExistente.Agencia == contaPFDomain.Agencia
                            && contaExistente.NumeroConta == contaPFDomain.NumeroConta
                            && contaExistente.Digito == contaPFDomain.Digito
                            && contaExistente.IdTipoConta == (int)contaPFDomain.TipoConta)
                        {
                            return "Conta existente no sistema!";
                        }
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
                            ValorConta = (decimal)contaPFDomain.ValorConta
                        };

                        _contexto.Add(conta_entity);
                        _contexto.SaveChanges();

                        var contaPFEntity = new RepositoryEntity.Models.ContaPessoaFisica();

                        var contaEntity = _contexto.Conta.ToList().Where(c => c.Agencia == contaPFDomain.Agencia &&
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

                        _contexto.Database.CommitTransaction();

                        return "Conta Pessoa Física cadastrada com sucesso!";
                    }
                    else
                    {
                        return "Conta já existente!";
                    }
                }
                else
                    return "Conta inválida!";
            }
            catch (SqlException)
            {
                _contexto.Database.RollbackTransaction();

                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                _contexto.Database.RollbackTransaction();

                return ex.Message;
            }
        }

        public string AtualizarConta(Domain.ContaPessoaFisica contaPFDomain)
        {
            try
            {
                if (contaPFDomain != null)
                {
                    var contaDbo = GetContaById(contaPFDomain.IDConta);

                    if (contaDbo is not null)
                    {
                        contaDbo.Agencia = contaPFDomain.Agencia;
                        contaDbo.ValorConta = contaPFDomain.ValorConta;
                        contaDbo.NomeConta = contaPFDomain.NomeConta;
                        contaDbo.NumeroConta = contaPFDomain.NumeroConta;
                        contaDbo.Ativo = contaPFDomain.EstaAtiva;
                        contaDbo.Digito = contaPFDomain.Digito;
                        contaDbo.IdTipoConta = (int)contaPFDomain.TipoConta;
                        contaDbo.Pix = contaPFDomain.Pix;

                        _contexto.Update(contaDbo);
                        _contexto.SaveChanges();
                    }

                    var contaPFEntity = new RepositoryEntity.Models.ContaPessoaFisica();
                    contaPFEntity.Cpf = contaPFDomain.CPF;
                    contaPFEntity.RendaFamiliar = contaPFDomain.RendaFamiliar;
                    contaPFEntity.Profissao = contaPFDomain.Profissao;
                    contaPFEntity.Endereco = contaPFDomain.Endereco;
                    contaPFEntity.Genero = contaPFDomain?.Genero;
                    contaPFEntity.NomeCliente = contaPFDomain?.NomeCliente ?? string.Empty;

                    _contexto.Update(contaPFEntity);
                    _contexto.SaveChanges();

                    return "Conta Pessoa Física alterada com sucesso!";
                }
                else
                    return "Conta inválido!";
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

        public RepositoryEntity.Models.ContaPessoaFisica? GetContaPessoaFisicaById(int idContaPF)
        {
            var pessoaFisica = new RepositoryEntity.Models.ContaPessoaFisica();

            try
            {
                if (idContaPF == 0)
                {
                    return null;
                }

                var pf = _contexto.ContaPessoaFisicas.Where(x => x.IdContaPf == idContaPF).ToList();
                pessoaFisica = pf.FirstOrDefault();

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
                conta = cta?.FirstOrDefault();

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

        public List<Domain.ContaPessoaFisica>? GetAllContasPessoasFisica()
        {
            var lstContasPF_Entity = new List<RepositoryEntity.Models.ContaPessoaFisica>();
            try
            {
                lstContasPF_Entity = _contexto.ContaPessoaFisicas.ToList();

                if (lstContasPF_Entity != null)
                {
                    var listaContaPF_Domain = new List<Domain.ContaPessoaFisica>();

                    foreach (var contaPF in lstContasPF_Entity)
                    {
                        listaContaPF_Domain.Add(
                            new Domain.ContaPessoaFisica()
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

        public string InativarConta(Domain.ContaPessoaFisica contaPF)
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
    }
}
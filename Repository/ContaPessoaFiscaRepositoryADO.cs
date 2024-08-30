using Domain;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Repository
{
    public class ContaPessoaFiscaRepositoryADO
    {
        IConfiguration _configuration;
        string connectionString;


        public ContaPessoaFiscaRepositoryADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
        public List<ContaPessoaFisica> RetornarContasPF()
        {
            var listaContas = new List<ContaPessoaFisica>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    //query de consulta
                    var query = new StringBuilder();
                    query.AppendLine("SELECT CC.ID_CONTA");
                    query.AppendLine("	  ,CC.Agencia");
                    query.AppendLine("	  ,CC.NumeroConta");
                    query.AppendLine("	  ,CC.Digito");
                    query.AppendLine("	  ,CC.PIX");
                    query.AppendLine("	  ,CC.Ativo");
                    query.AppendLine("	  ,CC.ValorConta");
                    query.AppendLine("	  ,CC.NomeConta");
                    query.AppendLine("	  ,TP.DESCRICAO_TIPO");
                    query.AppendLine("	  ,TP.CODIGO");
                    query.AppendLine("    ,PF.CPF");
                    query.AppendLine("    ,PF.NomeCliente");
                    query.AppendLine("    ,PF.Genero");
                    query.AppendLine("    ,PF.Endereco");
                    query.AppendLine("    ,PF.Profissao");
                    query.AppendLine("    ,PF.RendaFamiliar");
                    query.AppendLine("FROM CONTA_PESSOA_FISICA PF");
                    query.AppendLine("INNER JOIN CONTA CC ON(PF.ID_CONTA = CC.ID_CONTA)");
                    query.AppendLine("INNER JOIN TIPO_CONTA TP ON(TP.ID_TIPO_CONTA = CC.ID_TIPO_CONTA)");


                    //command
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    cmd.CommandType = CommandType.Text;

                    //abre a conexao
                    connection.Open();

                    //executa a consulta e adiciona o resultado em um stream
                    SqlDataReader contaPFDataReader = cmd.ExecuteReader();

                    //percorre o stream e adiciona os valores em objetos
                    while (contaPFDataReader.Read())
                    {
                        ContaPessoaFisica conta = new ContaPessoaFisica();
                        conta.IDConta = Convert.ToInt32(contaPFDataReader["ID_CONTA"]);
                        conta.Agencia = Convert.ToString(contaPFDataReader["Agencia"]) ?? string.Empty;
                        conta.NumeroConta = Convert.ToString(contaPFDataReader["NumeroConta"]) ?? string.Empty;
                        conta.Digito = Convert.ToString(contaPFDataReader["Digito"]) ?? string.Empty;
                        conta.Pix = Convert.ToString(contaPFDataReader["PIX"]) ?? string.Empty;
                        conta.EstaAtiva = Convert.ToBoolean(contaPFDataReader["Ativo"]);
                        conta.ValorConta = Convert.ToDecimal(contaPFDataReader["ValorConta"]);
                        conta.NomeConta = Convert.ToString(contaPFDataReader["NomeConta"]) ?? string.Empty;
                        conta.CPF = Convert.ToString(contaPFDataReader["CPF"]) ?? string.Empty;
                        conta.NomeCliente = Convert.ToString(contaPFDataReader["NomeCliente"]) ?? string.Empty;
                        conta.Genero = Convert.ToString(contaPFDataReader["Genero"]) ?? string.Empty;
                        conta.Endereco = Convert.ToString(contaPFDataReader["Endereco"]) ?? string.Empty;
                        conta.Profissao = Convert.ToString(contaPFDataReader["Profissao"]) ?? string.Empty;
                        conta.RendaFamiliar = Convert.ToDecimal(contaPFDataReader["RendaFamiliar"]);
                        conta.TipoConta = (ETipoConta)Convert.ToInt32(contaPFDataReader["CODIGO"]);
                        conta.DescricaoTipoConta = Convert.ToString(contaPFDataReader["DESCRICAO_TIPO"]) ?? string.Empty;

                        listaContas.Add(conta);
                    }

                    contaPFDataReader.Close();
                    connection.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return listaContas;
        }

        public int CadastrarContasPF(ContaPessoaFisica conta)
        {
            int retorno = 0;
            SqlTransaction? transacao = null;

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                //insert da tabela conta pessoa fisica
                var comandoSQLContaPF = new StringBuilder();
                comandoSQLContaPF.AppendLine("INSERT INTO CONTA_PESSOA_FISICA(CPF,NomeCliente,Genero,Endereco,Profissao,RendaFamiliar,ID_CONTA)");
                comandoSQLContaPF.AppendLine("VALUES(@CPF, @NomeCliente, @Genero, @Endereco, @Profissao, @RendaFamiliar, @ID_CONTA);");

                //comando da conta pessoa fisica
                SqlCommand cmdContaPF = new SqlCommand(comandoSQLContaPF.ToString(), conexao);
                cmdContaPF.CommandType = CommandType.Text;

                //parametro da conta pessoa fisica
                cmdContaPF.Parameters.AddWithValue("@CPF", conta.CPF);
                cmdContaPF.Parameters.AddWithValue("@NomeCliente", conta.NomeCliente);
                cmdContaPF.Parameters.AddWithValue("@Genero", conta.Genero);
                cmdContaPF.Parameters.AddWithValue("@Endereco", conta.Endereco);
                cmdContaPF.Parameters.AddWithValue("@Profissao", conta.Profissao);
                cmdContaPF.Parameters.AddWithValue("@RendaFamiliar", conta.RendaFamiliar);

                try
                {
                    //abre a conexao
                    conexao.Open();

                    //inicia uma transação
                    transacao = conexao.BeginTransaction();

                    //insere a conta na tabela conta primeiro
                    InserirConta(conta, transacao, conexao);

                    //obtem o id da conta cadastrado na inserção acima
                    conta.IDConta = ConsultarConta(conta, transacao, conexao);

                    //adiciona o id da conta como parametro
                    cmdContaPF.Parameters.AddWithValue("@ID_CONTA", conta.IDConta);

                    cmdContaPF.Transaction = transacao;
                    retorno = cmdContaPF.ExecuteNonQuery();


                    //commita a transacao
                    transacao.Commit();
                }
                catch (Exception)
                {
                    //se der excecao, da rollback na transacao, caso nao for nula
                    transacao?.Rollback();
                    throw;
                }
                finally
                {
                    conexao.Close();
                }

                return retorno;
            }
        }

        private int ConsultarConta(ContaPessoaFisica conta, SqlTransaction? transacao, SqlConnection conexao)
        {
            //consulta o id cadastrado
            SqlCommand cmdconsulta = new SqlCommand("SELECT MAX(ID_CONTA) ID_CONTA FROM CONTA WHERE Agencia = @Agencia AND NumeroConta = @NumeroConta AND Digito = @Digito AND ID_TIPO_CONTA = @ID_TIPO_CONTA", conexao);
            cmdconsulta.CommandType = CommandType.Text;

            cmdconsulta.Parameters.AddWithValue("@Agencia", conta.Agencia);
            cmdconsulta.Parameters.AddWithValue("@NumeroConta", conta.NumeroConta);
            cmdconsulta.Parameters.AddWithValue("@Digito", conta.Digito);
            cmdconsulta.Parameters.AddWithValue("@ID_TIPO_CONTA", (int)conta.TipoConta);

            cmdconsulta.Transaction = transacao;
            SqlDataReader contaPFDataReader = cmdconsulta.ExecuteReader();

            while (contaPFDataReader.Read())
                conta.IDConta = Convert.ToInt32(contaPFDataReader["ID_CONTA"]);

            contaPFDataReader.Close();

            return conta.IDConta;
        }

        private void InserirConta(ContaPessoaFisica conta, SqlTransaction? transacao, SqlConnection conexao)
        {
            //insert da tabela conta
            var comandoSQLConta = new StringBuilder();
            comandoSQLConta.AppendLine("INSERT INTO CONTA (Agencia, NumeroConta, Digito, PIX, Ativo, ValorConta, NomeConta, ID_TIPO_CONTA)");
            comandoSQLConta.AppendLine("VALUES(@Agencia, @NumeroConta, @Digito, @PIX, @Ativo, @ValorConta, @NomeConta, @ID_TIPO_CONTA);");

            //comando da conta
            SqlCommand cmdConta = new SqlCommand(comandoSQLConta.ToString(), conexao);
            cmdConta.CommandType = CommandType.Text;

            //parametro da conta
            cmdConta.Parameters.AddWithValue("@Agencia", conta.Agencia);
            cmdConta.Parameters.AddWithValue("@NumeroConta", conta.NumeroConta);
            cmdConta.Parameters.AddWithValue("@Digito", conta.Digito);
            cmdConta.Parameters.AddWithValue("@PIX", conta.Pix);
            cmdConta.Parameters.AddWithValue("@Ativo", conta.EstaAtiva);
            cmdConta.Parameters.AddWithValue("@ValorConta", conta.ValorConta);
            cmdConta.Parameters.AddWithValue("@NomeConta", conta.NomeConta);
            cmdConta.Parameters.AddWithValue("@ID_TIPO_CONTA", (int)conta.TipoConta);

            //executa o insert da conta primeiro para pegar o id e inserir na conta pessoa fisica
            cmdConta.Transaction = transacao;
            cmdConta.ExecuteNonQuery();
        }

        public int AlterarContasPF(ContaPessoaFisica conta)
        {
            int retorno = 0;

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                //comando de update
                var comandoSQL = new StringBuilder();
                comandoSQL.AppendLine("UPDATE CONTA ");
                comandoSQL.AppendLine(" SET Agencia = @Agencia");
                comandoSQL.AppendLine(", NumeroConta = @NumeroConta");
                comandoSQL.AppendLine(", Digito = @Digito");
                comandoSQL.AppendLine(", PIX = @PIX");
                comandoSQL.AppendLine(", Ativo = @Ativo");
                comandoSQL.AppendLine(", ValorConta = @ValorConta");
                comandoSQL.AppendLine(", NomeConta = @NomeConta");
                comandoSQL.AppendLine(" WHERE ID_CONTA = @ID_CONTA");


                comandoSQL.AppendLine("UPDATE CONTA_PESSOA_FISICA ");
                comandoSQL.AppendLine(" SET CPF = @CPF");
                comandoSQL.AppendLine(", NomeCliente = @NomeCliente");
                comandoSQL.AppendLine(", Genero = @Genero");
                comandoSQL.AppendLine(", Endereco = @Endereco");
                comandoSQL.AppendLine(", Profissao = @Profissao");
                comandoSQL.AppendLine(", RendaFamiliar = @RendaFamiliar");
                comandoSQL.AppendLine(" WHERE ID_CONTA_PF = @ID_CONTA_PF");

                //associa o comando de update a conexao
                SqlCommand cmd = new SqlCommand(comandoSQL.ToString(), conexao);
                cmd.CommandType = CommandType.Text;

                //parametros setados
                cmd.Parameters.AddWithValue("@Agencia", conta.Agencia);
                cmd.Parameters.AddWithValue("@NumeroConta", conta.NumeroConta);
                cmd.Parameters.AddWithValue("@Digito", conta.Digito);
                cmd.Parameters.AddWithValue("@PIX", conta.Pix);
                cmd.Parameters.AddWithValue("@Ativo", conta.EstaAtiva);
                cmd.Parameters.AddWithValue("@ValorConta", conta.ValorConta);
                cmd.Parameters.AddWithValue("@NomeConta", conta.NomeConta);
                cmd.Parameters.AddWithValue("@ID_CONTA", conta.IDConta);

                cmd.Parameters.AddWithValue("@CPF", conta.CPF);
                cmd.Parameters.AddWithValue("@NomeCliente", conta.NomeCliente);
                cmd.Parameters.AddWithValue("@Genero", conta.Genero);
                cmd.Parameters.AddWithValue("@Endereco", conta.Endereco);
                cmd.Parameters.AddWithValue("@Profissao", conta.Profissao);
                cmd.Parameters.AddWithValue("@RendaFamiliar", conta.RendaFamiliar);
                cmd.Parameters.AddWithValue("@ID_CONTA_PF", conta.ID_ContaPF);

                try
                {
                    conexao.Open();
                    retorno = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conexao.Close();
                }
            }
            return retorno;
        }

        public int InativarContasPF(ContaPessoaFisica conta)
        {
            int retorno = 0;

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {

                var comandoSQL = new StringBuilder();
                comandoSQL.AppendLine("UPDATE CONTA");
                comandoSQL.AppendLine("SET ATIVO = @ATIVO");
                comandoSQL.AppendLine("WHERE ID_CONTA = @ID_CONTA");

                SqlCommand cmd = new SqlCommand(comandoSQL.ToString(), conexao);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID_CONTA", conta.IDConta);
                cmd.Parameters.AddWithValue("@ATIVO", conta.EstaAtiva);

                try
                {
                    conexao.Open();
                    retorno = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conexao.Close();
                }
            }

            return retorno;
        }
    }
}
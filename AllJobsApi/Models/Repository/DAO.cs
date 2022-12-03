using AllJobsApi.Models.Interface;
using AllJobsApi.Models.Model;
using System.Data;
using System.Data.OleDb;
using System.Drawing;

namespace AllJobsApi.Models.Repository
{
    public class DAO : IDAO
    {
        static string basePath = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        static string stringConn = "";
        static OleDbConnection Con;
        
        public DAO(string caminho)
        {
            stringConn = basePath + caminho + ";Persist Security Info=True;Jet OLEDB:Database Password=";
          
        }

        public List<Produto> BuscaProdutos()
        {
            try
            {
                var retorno = new List<Produto>();
                var query = "SELECT * FROM tbl_Produtos";
                OleDbCommand command = new OleDbCommand(query, Con);
                Con.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        retorno.Add(new Produto()
                        {
                            Id = Convert.ToInt32(reader["CODPRO"].ToString()),
                            NomeProduto = reader["ccNomPro"].ToString(),
                            Valor = Convert.ToDouble(reader["ccPreçoVenda"])
                        });
                    }
                }
                Con.Close();
                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar os produtos => {e}");
            }

        }
        public List<Produto> BuscaProdutosPeloPedido(int pedido)
        {
            try
            {
                var retorno = new List<Produto>();
                var query = $"SELECT * FROM tbl_Produtos prod " +
                            $"inner join TBBARES_PRODUTOS tbprod " +
                            $"ON prod.CODPRO = tbprod.CODPRO WHERE tbprod.Reg ={pedido}";
                OleDbCommand command = new OleDbCommand(query, Con);
                Con.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var observacao = reader["Observacao"].ToString();
                        retorno.Add(new Produto()
                        {
                            Id = Convert.ToInt32(reader["prod.CODPRO"].ToString()),
                            NomeProduto = reader["ccNomPro"].ToString(),
                            Valor = Convert.ToDouble(reader["Valor"].ToString()),
                            Quantidade = Convert.ToInt32(reader["Qte"].ToString()),
                            Observacao = string.IsNullOrEmpty(observacao) 
                                        && string.IsNullOrWhiteSpace(observacao) ?
                                        "Não Informado" : observacao,
                        });
                    }
                }
                Con.Close();
                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar os produtos => {e}");
            }

        }
        public List<Atendente> BuscaAtendente()
        {
            try
            {
                var retorno = new List<Atendente>();
                var query = "SELECT * FROM tbl_Vendedores WHERE ccAtivo = true";
                OleDbCommand command = new OleDbCommand(query, Con);
                Con.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        retorno.Add(new Atendente()
                        {
                            Id = Convert.ToInt32(reader["CODVEN"].ToString()),
                            Nome = reader["ccNomVen"].ToString(),

                        });
                    }
                }
                Con.Close();
                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar os atendentes => {e}");
            }
        }
        public List<Mesa> BuscaMesasOcupadas()
        {
            try
            {
                var retorno = new List<Mesa>();
                var query = "SELECT * FROM TBBARES_VENDAS WHERE Status = 'Aberto'";
                OleDbCommand command = new OleDbCommand(query, Con);
                Con.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        retorno.Add(new Mesa()
                        {
                            Atendente = int.Parse(reader["CodVen"].ToString()),
                            Nome = reader["Cliente"].ToString(),
                            Numero = int.Parse(reader["Mesa"].ToString()),
                            Pedido = int.Parse(reader["Reg"].ToString()),

                        });
                    }
                }
                Con.Close();
                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar os produtos => {e}");
            }
        }
        public int AbrirComanda(Mesa m)
        {
            try
            {
                Con.Open();
                var query = "SELECT MAX(Reg) FROM TBBARES_VENDAS";
                OleDbCommand command = new OleDbCommand(query, Con);
                var indice = (Int32)command.ExecuteScalar() + 1;
                query = "Insert Into TBBARES_VENDAS(Reg,CodVen,Mesa,Data,Cliente,Status) " +
                $"VALUES('{indice}','{m.Atendente}','{m.Numero}','{DateTime.Now.Date}','{m.Nome}','Aberto')";
                command = new OleDbCommand(query, Con);
                command.ExecuteNonQuery();
                Con.Close();

                return indice;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AdicionarPedidoMesa(Mesa m)
        {
            try
            {
                Con.Open();
                var query = $"SELECT Reg FROM TBBARES_VENDAS WHERE Mesa = {m.Numero} AND Status = 'Aberto'";
                OleDbCommand command = new OleDbCommand(query, Con);
                m.Pedido = (Int32)command.ExecuteScalar();
                foreach (var item in m.Produtos)
                {
                    query = "Insert Into TBBARES_PRODUTOS(Reg,Mesa,CODPRO,Qte,Valor, Observacao) " +
                    $"VALUES('{m.Pedido}','{m.Numero}','{item.Id}','{item.Quantidade}','{item.Valor}','{item.Observacao}')";

                    command = new OleDbCommand(query, Con);
                    command.ExecuteNonQuery();

                }
                Con.Close();

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool VerificaSenhaBanco(string senha)
        {
            try
            { 
                Con = new OleDbConnection(stringConn + senha);
                Con.Open();
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                    return true;
                }
                else
                    return false;

            }
            catch (OleDbException ex)
            {
                return false;
            }
        }

    }
}

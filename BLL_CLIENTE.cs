
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENTE
{
	class BLL_CLIENTE
	{
		SqlCommand cmd;



		//string ABC = COXE.bancodedados;

		//SqlConnection conexaoSQL = new SqlConnection(ConfigurationManager.ConnectionStrings["conexaosql"].ConnectionString);
		SqlConnection conexaoSQL;///= new SqlConnection(COXE.bancodedados());

		public void sqlc(DTO_CLIENTE DTO)
		{
			conexaoSQL = new SqlConnection(DTO.bancodedados);
		}
		


		#region GRIDS

		public DataTable VSGridTK(DTO_CLIENTE DTO)
		{
			try
			{
				cmd = new SqlCommand("SELECT A.TICKET 'A.TICKET',B.DATA_VENDA 'B.DATA_VENDA',C.DESC_PRODUTO 'B.PRODUTO',B.VALOR_TOTAL 'B.VALOR_TOTAL' ,B.PRODUTO'CODC' " +
					"FROM LOJA_VENDA A " +
					"INNER JOIN LOJA_VENDA_PRODUTO B ON A.TICKET = B.TICKET " +
					"INNER JOIN PRODUTOS C ON B.PRODUTO = C.PRODUTO " +
					"WHERE A.CODIGO_CLIENTE ='" + DTO.BuscaTicket + "'"+
	 "   ORDER BY B.DATA_VENDA asc", conexaoSQL);
				SqlDataAdapter DA = new SqlDataAdapter();
				DA.SelectCommand = cmd;
				DataTable DT = new DataTable();

				DA.Fill(DT);


				return DT;



			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				conexaoSQL.Close();
			}
		}

		public DataTable VSGridProdutos(DTO_CLIENTE DTO)
		{
			try
			{
				cmd = new SqlCommand("SELECT  A.TICKET 'TICKET',A.DATA_VENDA'DATA', A.CODIGO_CLIENTE,D.CLIENTE_VAREJO 'CLIENTE', B.NOME_VENDEDOR'VENDEDOR',E.DESC_PRODUTO'PRODUTO' " +
					"FROM LOJA_VENDA A " +
					  "INNER JOIN LOJA_VENDEDORES B ON B.VENDEDOR = A.VENDEDOR " +
					  "INNER JOIN LOJA_VENDA_PRODUTO C ON C.TICKET = A.TICKET  " +
					  "INNER JOIN CLIENTES_VAREJO D ON A.CODIGO_CLIENTE = D.CODIGO_CLIENTE " +
					  "INNER JOIN PRODUTOS E ON C.PRODUTO = E.PRODUTO " +
					  "WHERE  A.CODIGO_CLIENTE <> '' " +
					  "AND A.CODIGO_CLIENTE <> '0' " +   DTO.SQLDATA + DTO.SQLGRUPO +
					   DTO.SQLPROD  +
					  "ORDER BY A.DATA_VENDA asc" +
					"", conexaoSQL);
				SqlDataAdapter DA = new SqlDataAdapter();
				DA.SelectCommand = cmd;

				DataTable X = new DataTable();

				DA.Fill(X);

				DataTable DT = new DataTable();
				DT = DtProdutos();
				DT.Clear();

				string TICKET, DATA, CODIGO_CLIENTE, CLIENTE, VENDEDOR, PRODUTO;

				

				for (int i = 0; i < X.Rows.Count; i++)
				{

					TICKET = null; DATA = null; CODIGO_CLIENTE = null; CLIENTE = null; VENDEDOR = null; PRODUTO = null;

					TICKET = Convert.ToString(X.Rows[i]["TICKET"]);
					DATA = Convert.ToString(X.Rows[i]["DATA"]);
					CODIGO_CLIENTE = Convert.ToString(X.Rows[i]["CODIGO_CLIENTE"]);
					CLIENTE = Convert.ToString(X.Rows[i]["CLIENTE"]);
					VENDEDOR = Convert.ToString(X.Rows[i]["VENDEDOR"]);
					PRODUTO = Convert.ToString(X.Rows[i]["PRODUTO"]);


					DT.Rows.Add(TICKET.Replace(" ", ""), DATA.Replace("00:00:00", ""), CODIGO_CLIENTE.Replace(" ", ""), CLIENTE.Replace("  ", "").ToUpper(), VENDEDOR.Replace("  ", ""), PRODUTO.Replace("  ", ""));


				}
				return DT;



			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
				throw ex;
			}
			finally
			{
				conexaoSQL.Close();
			}
		}

		public List<string> GRUPO()
		{
			List<string> LS = new List<string>();
			try
			{
				cmd = new SqlCommand("SELECT distinct[GRUPO_PRODUTO] FROM [PRODUTOS] ORDER BY [GRUPO_PRODUTO] asc", conexaoSQL);
				SqlDataAdapter DA = new SqlDataAdapter();
				DA.SelectCommand = cmd;
				DataTable DT = new DataTable();

				DA.Fill(DT);


				for (int i = 0; i < DT.Rows.Count; i++)
				{
					LS.Add(DT.Rows[i]["GRUPO_PRODUTO"].ToString());

				}


				return LS;



			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				conexaoSQL.Close();
			}


			//return X;
		}

		private DataTable DtProdutos()
		{
			DataTable X = new DataTable();

			X.Columns.Add("TICKET", typeof(string));
			X.Columns.Add("DATA", typeof(string));
			X.Columns.Add("CODIGO_CLIENTE", typeof(string));
			X.Columns.Add("CLIENTE", typeof(string));
			X.Columns.Add("VENDEDOR", typeof(string));
			X.Columns.Add("PRODUTO", typeof(string));








			return X;
		}

		public DataTable BuscaProdutoLIKE(string COD)
		{
			cmd = new SqlCommand("SELECT PRODUTO , DESC_PRODUTO FROM [PRODUTOS] WHERE DESC_PRODUTO LIKE '%"+COD+"%' ORDER BY [GRUPO_PRODUTO] asc", conexaoSQL);
			try
			{				
				SqlDataAdapter DA = new SqlDataAdapter();
				DA.SelectCommand = cmd;
				DataTable DT = new DataTable();

				DA.Fill(DT);


				return DT;



			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				conexaoSQL.Close();
			}

		}

		public DataTable BuscaAnirver(int mes,string ano)
		{
			cmd = new SqlCommand("SELECT CODIGO_CLIENTE,[CLIENTE_VAREJO],[ANIVERSARIO]  " +
				"from[CLIENTES_VAREJO] where month([ANIVERSARIO]) = " + mes + " " + ano + "   ORDER BY ANIVERSARIO asc ", conexaoSQL);
			try
			{
				SqlDataAdapter DA = new SqlDataAdapter();
				DA.SelectCommand = cmd;
				DataTable DT = new DataTable();

				DA.Fill(DT);


				return DT;



			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				conexaoSQL.Close();
			}

		}

		#endregion

		public void DadosCliente(DTO_CLIENTE DTO)
		{
			cmd = new SqlCommand("SELECT [CODIGO_CLIENTE],[CLIENTE_VAREJO],[DDD],[TELEFONE],[ANIVERSARIO],[CADASTRAMENTO],[EMAIL],[ULTIMA_COMPRA],[DDD_CELULAR],[CELULAR]" +
				"FROM [CLIENTES_VAREJO]" +
	"			WHERE [CODIGO_CLIENTE] = '" +DTO.BuscaTicket + "'", conexaoSQL);

			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd.ExecuteReader();
				if (RDR.Read())
				{
					DTO.NOME = RDR["CLIENTE_VAREJO"].ToString();
					DTO.TEL = RDR["DDD"].ToString() + " - " + RDR["TELEFONE"].ToString();
					DTO.CEL = RDR["DDD_CELULAR"].ToString() + " - " + RDR["CELULAR"].ToString();
					DTO.EMAIL = RDR["EMAIL"].ToString();
					DTO.DATANASC = RDR["ANIVERSARIO"].ToString();
					DTO.DATACADASTR = RDR["CADASTRAMENTO"].ToString();
					DTO.DATAUTCP = RDR["ULTIMA_COMPRA"].ToString();
				}

				//bIdCargo.Text = rdd2["id"].ToString();
			}
			catch (Exception ex)
			{

				MessageBox.Show("DadosCliente > " + ex);
			}
			finally
			{
				conexaoSQL.Close();
			}
		}

		public string BuscaProdutoDesc(string COD)
		{
			cmd = new SqlCommand("SELECT TOP 1 DESC_PRODUTO   FROM PRODUTOS  WHERE PRODUTO = '" + COD + "'", conexaoSQL);
			string X = null;
			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd.ExecuteReader();
				if (RDR.Read())
				{
					X = RDR["DESC_PRODUTO"].ToString();
				}
				return X;
			}
			catch (Exception x)
			{

				throw;
			}
			finally
			{
				conexaoSQL.Close();
			}

		}





		//














	}
}


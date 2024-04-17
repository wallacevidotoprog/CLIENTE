using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INIFile;

namespace CLIENTE
{
	public partial class Form_PRIMAL : Form
	{
		public Form_PRIMAL()
		{
			InitializeComponent();
		}

		DTO_CLIENTE DTO = new DTO_CLIENTE();BLL_CLIENTE BLL = new BLL_CLIENTE();
		INIFile.INI ini;
		ToolTip A = new ToolTip(); ToolTip B = new ToolTip(); ToolTip C = new ToolTip();


		private void Form_PRIMAL_Load(object sender, EventArgs e)
		{
			Checked();
			comboBox_ANO.Text = "TODOS";comboBox_MES.Text = "JANEIRO";
			A.SetToolTip(pictureBox_cliente, "BUSCA DE CLIENTE DETALHADA");
			A.SetToolTip(pictureBox_produtos, "BUSCA DE PRODUTOS E FINS");
			A.SetToolTip(pictureBox_aniver, "BUSCA ANIVERSARIANTES");


			ini = new INIFile.INI(Path.Combine(System.Windows.Forms.Application.StartupPath, @"sql.ini"));
			DTO.bancodedados = ini.ReadValue("conexao", "stringConexao"); BLL.sqlc(DTO);

			comboBox_grupo.DataSource = BLL.GRUPO();
		}

		private void button_CLIENTE_Click(object sender, EventArgs e)
		{
			tabControl_APP.SelectTab(tabPage_CLIENTE);
		}

		private void button_PRODUTO_Click(object sender, EventArgs e)
		{
			tabControl_APP.SelectTab(tabPage_PRODUTO);
		}

		private void button_ANIVER_Click(object sender, EventArgs e)
		{
			tabControl_APP.SelectTab(tabPage_ANIVER);
		}

		private void button_BUSCAR_Click(object sender, EventArgs e)
		{
			DTO.BuscaTicket = null;
			DTO.BuscaTicket = textBox_CPF_CNPJ.Text;

			textBox_NOME.Clear(); DTO.NOME = null;
			textBox_TEL.Clear(); DTO.TEL = null;
			textBox_CEL.Clear(); DTO.CEL = null;
			textBox_EMAIL.Clear(); DTO.EMAIL = null;
			textBox_NASCIMENTO.Clear(); DTO.DATANASC = null;
			textBox_CADAST.Clear(); DTO.DATACADASTR = null;
			textBox_ULTIMACOMPRA.Clear(); DTO.DATAUTCP = null;

			if (textBox_CPF_CNPJ.Text != "")
			{
				DataTable DT = new DataTable();

				DT = BLL.VSGridTK(DTO); BLL.DadosCliente(DTO);
				dataGridView_CLIENTE.DataSource = DT;


				try
				{
					textBox_NOME.Text = DTO.NOME.Replace("  ", "");
					textBox_TEL.Text = DTO.TEL.Replace("  ", "");
					textBox_CEL.Text = DTO.CEL.Replace("  ", "");
					textBox_EMAIL.Text = (DTO.EMAIL).Replace(" ", "");
					textBox_NASCIMENTO.Text = Convert.ToString(DTO.DATANASC).Replace("00:00:00", "");
					textBox_CADAST.Text = Convert.ToString(DTO.DATACADASTR).Replace("00:00:00", "");
					textBox_ULTIMACOMPRA.Text = Convert.ToString(DTO.DATAUTCP).Replace("00:00:00", "");
				}
				catch (Exception)
				{

					textBox_NOME.Clear(); DTO.NOME = null;
					textBox_TEL.Clear(); DTO.TEL = null;
					textBox_CEL.Clear(); DTO.CEL = null;
					textBox_EMAIL.Clear(); DTO.EMAIL = null;
					textBox_NASCIMENTO.Clear(); DTO.DATANASC = null;
					textBox_CADAST.Clear(); DTO.DATACADASTR = null;
					textBox_ULTIMACOMPRA.Clear(); DTO.DATAUTCP = null;
				}

				TotalGrid();

			}
			else
			{
				MessageBox.Show("Digite um CPF ou CNPJ valido.","ERRO",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}


		void Checked()
		{
			if (checkBox_dataAtiva.Checked == true)
			{
				label11.Enabled = true;
				label12.Enabled = true;
				dateTimePicker1.Enabled = true;
				dateTimePicker2.Enabled = true;

			}
			else
			{
				label11.Enabled = false;
				label12.Enabled = false;
				dateTimePicker1.Enabled = false;
				dateTimePicker2.Enabled = false;
			}
			if (checkBox_grupo.Checked == true)
			{
				comboBox_grupo.Enabled = true;
				label10.Enabled = true;
			}
			else
			{
				comboBox_grupo.Enabled = false;
				label10.Enabled = false;
			}

			if (checkBox_Produtos.Checked == true)
			{
				label13.Enabled = true;
				textBox_referencia.Enabled = true;
				button_addKey.Enabled = true;
				label9.Enabled = true;
				textBox_addProd.Enabled = true;
				button_add.Enabled = true;
				//button_buscaproduto.Enabled = true;

			}
			else
			{
				label13.Enabled = false;
				textBox_referencia.Enabled = false;
				button_addKey.Enabled = false;
				label9.Enabled = false;
				textBox_addProd.Enabled = false;
				button_add.Enabled = false;
				//button_buscaproduto.Enabled = false;
			}
		}

		private void checkBox_dataAtiva_CheckedChanged(object sender, EventArgs e)
		{
			Checked();
		}

		private void checkBox_grupo_CheckedChanged(object sender, EventArgs e)
		{
			checkBox_Produtos.Checked = false;
			Checked();

		}

		private void button_add_Click(object sender, EventArgs e)
		{
			if (BLL.BuscaProdutoDesc(textBox_addProd.Text) != null)
			{
				dataGridView_buscaP.Rows.Add(Convert.ToString(textBox_addProd.Text).ToUpper(), BLL.BuscaProdutoDesc(textBox_addProd.Text), "DELL");
				textBox_addProd.Clear();
			}
		}

		private void button_buscaproduto_Click(object sender, EventArgs e)
		{
			buscaProdutos();

		}

		private void dataGridView_buscaP_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (dataGridView_buscaP.Columns[e.ColumnIndex].Name == "REMOO")
				{
					MessageBoxButtons BT = MessageBoxButtons.YesNo;

					if (MessageBox.Show(this, "Deseja excluir esse dado ?", "AVISO", BT, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						try
						{
							dataGridView_buscaP.Rows.RemoveAt(dataGridView_buscaP.CurrentRow.Index);

							dataGridView_buscaP.ClearSelection();
						}
						catch (Exception ex)
						{

							MessageBox.Show("" + ex);
						}
					}

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex);
			}
		}


		void buscaProdutos()
		{
			string produtos = null;
			int Y = 0;
			int X = dataGridView_buscaP.Rows.Count;
			DataTable DT = new DataTable();
			bool Chec = false;

			


			if (produtos == null /*&& Chec == false *//*dataGridView_buscaP.Rows.Count != 0*/)
			{

				if (checkBox_Produtos.Checked == true)
				{

					if (produtos == null && dataGridView_buscaP.Rows.Count != 0)
					{
						foreach (DataGridViewRow item in dataGridView_buscaP.Rows)
						{
							produtos += " ' " + item.Cells[0].Value + " ' ";

							Y++;

							if (Y != X)
							{
								produtos += ",";
							}

						}
						DTO.SQLPROD = " AND C.PRODUTO IN (" + produtos.Replace(" ", "") + ") ";
					}

					


				}
				else
				{
					DTO.SQLPROD = " ";
				}

				if (checkBox_dataAtiva.Checked == true)
				{
					DTO.SQLDATA = " AND A.DATA_VENDA BETWEEN '" + Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyyMMdd")+"' AND '"+ Convert.ToDateTime(dateTimePicker2.Text).ToString("yyyyMMdd") + "' ";
				}
				else
				{
					DTO.SQLDATA = "";
				}

				if (checkBox_grupo.Checked == true)
				{
					DTO.SQLGRUPO=(" AND E.GRUPO_PRODUTO = '"+comboBox_grupo.Text+"'  ");
				}
				else
				{
					DTO.SQLGRUPO = (" ");
				}



				DTO.PRODUTOS = null;
				DTO.PRODUTOS = produtos;
				DT = BLL.VSGridProdutos(DTO);
				dataGridView_produtos.DataSource = DT;
			}
			else
			{
				MessageBox.Show("Sem produto para pesquisar");
			}

		}

		private void dataGridView_produtos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			textBox_CPF_CNPJ.Clear();
			try
			{
				textBox_CPF_CNPJ.Text = dataGridView_produtos.Rows[e.RowIndex].Cells[2].Value.ToString();
			}
			catch (Exception)
			{

				
			}
			
			button_BUSCAR_Click( sender,  e);
			tabControl_APP.SelectTab(tabPage_CLIENTE);
		}

		private void dataGridView_CLIENTE_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (dataGridView_CLIENTE.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
				{
					Form_foto form = new Form_foto();
					form.FOTO = dataGridView_CLIENTE.Rows[e.RowIndex].Cells[4].Value.ToString();
					form.ShowDialog();
				}
			}
			catch (Exception)
			{

				
			}
			
			
		}

		private void textBox_CPF_CNPJ_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
			{
				button_BUSCAR_Click( sender,  e);
			}
		}



		void TotalGrid()
		{


			decimal X = 0;

			foreach (DataGridViewRow i in dataGridView_CLIENTE.Rows)
			{
				X += Convert.ToDecimal(i.Cells[3].Value.ToString());
			}

			textBox_Somas.Text = String.Format("total gasto   R${0:N}", X);

		}

		private void checkBox_Produtos_CheckedChanged(object sender, EventArgs e)
		{
			checkBox_grupo.Checked = false;

			Checked();
		}

		private void button_addKey_Click(object sender, EventArgs e)
		{
			if (BLL.BuscaProdutoLIKE(textBox_referencia.Text) != null || textBox_referencia.Text != "")
			{
				DataTable DT = new DataTable();
				DT = BLL.BuscaProdutoLIKE(textBox_referencia.Text);


				for (int i = 0; i < DT.Rows.Count; i++)
				{
					dataGridView_buscaP.Rows.Add(Convert.ToString(DT.Rows[i]["PRODUTO"].ToString()), Convert.ToString(DT.Rows[i]["DESC_PRODUTO"].ToString()), "DELL");
				}

				DT.Clear(); BLL.BuscaProdutoLIKE(textBox_referencia.Text).Clear();
 
				 textBox_referencia.Clear();
			}
		}

		private void button_limpar_Click(object sender, EventArgs e)
		{
			DTO.PRODUTOS = ""; DTO.SQLDATA = "";DTO.SQLGRUPO = ""; DTO.SQLPROD = " AND C.PRODUTO IN ('0') ";
			BLL.BuscaProdutoLIKE("0").Clear(); BLL.VSGridProdutos(DTO).Clear() ;
			dataGridView_buscaP.Rows.Clear(); dataGridView_produtos.DataSource = BLL.VSGridProdutos(DTO);
		}

		private void button_BUSCANIRVER_Click(object sender, EventArgs e)
		{
			if (dataGridView_anirver.Rows.Count == 0)
			{

			}
			else
			{
				dataGridView_anirver.Rows.Clear();
			}
			string SQL;

			if (comboBox_ANO.Text != "TODOS")
			{
				SQL = ("AND year([CADASTRAMENTO])= " + comboBox_ANO.Text + "");
			}
			else
			{
				SQL = (" ");
			}

			DataTable dataTable = new DataTable();
			dataTable=BLL.BuscaAnirver(MeuMes(comboBox_MES.Text), SQL);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{

				dataGridView_anirver.Rows.Add(dataTable.Rows[i]["CODIGO_CLIENTE"].ToString().Replace(" ", ""), dataTable.Rows[i]["CLIENTE_VAREJO"].ToString().Replace("  ", ""), dataTable.Rows[i]["ANIVERSARIO"].ToString().Replace("00:00:00", ""));

		}


	}



		int MeuMes(string dd)
		{
			int X = 0;

			switch (dd)
			{
				case "JANEIRO":
					X = 1;
					break;
				case "FEVEREIRO":
					X = 2;
					break;
				case "MARÇO":
					X = 3;
					break;
				case "ABRIL":
					X = 4;
					break;
				case "MAIO":
					X = 5;
					break;
				case "JUNHO":
					X = 6;
					break;
				case "JULHO":
					X = 7;
					break;
				case "AGOSTO":
					X = 8;
					break;
				case "SETEMBRO":
					X = 9;
					break;
				case "OUTUBRO":
					X = 10;
					break;
				case "NOVEMBRO":
					X = 11;
					break;
				case "DEZEMBRO":
					X = 12;
					break;

			}


			return X;
		}

		private void dataGridView_anirver_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			textBox_CPF_CNPJ.Clear();
			try
			{
				textBox_CPF_CNPJ.Text = dataGridView_anirver.Rows[e.RowIndex].Cells[0].Value.ToString();
			}
			catch (Exception)
			{


			}

			button_BUSCAR_Click(sender, e);
			tabControl_APP.SelectTab(tabPage_CLIENTE);
		}
	}
}

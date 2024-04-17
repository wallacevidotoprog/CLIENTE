using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENTE
{
	public partial class Form_foto : Form
	{
		public Form_foto()
		{
			InitializeComponent();
		}
		public string FOTO { get; set; }
		private void Form_foto_Load(object sender, EventArgs e)
		{
			pictureBox1.ImageLocation = "http://imagens.imgnet.com.br/f/IMAGINARIUM/"+FOTO;

		}
	}
}

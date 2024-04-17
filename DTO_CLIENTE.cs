
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIENTE
{
	class DTO_CLIENTE
	{

		public string bancodedados { get; set; }




		public string BuscaTicket { get; set; }

		public string NOME { get; set; }
		public string TEL { get; set; }
		public string CEL { get; set; }
		public string EMAIL { get; set; }
		public string DATANASC { get; set; }
		public string DATACADASTR { get; set; }
		public string DATAUTCP { get; set; }














		public DateTime DataDe { get; set; }
		public DateTime DataAte { get; set; }
		//public string BuscaTicket { get; set; }
		public string BuscaCFE { get; set; }
		public DateTime DataTicket { get; set; }
		public string PDV_Loja { get; set; }


		public string TicketUsado { get; set; }
		public DateTime DataTK { get; set; }
		public DateTime DataDigitacao { get; set; }
		public int FeitoPor { get; set; }
		public string CpfCliente { get; set; }


			
		//public string NOME { get; set; }
		public string CEP { get; set; }
		public string LOGA { get; set; }
		public string NUM { get; set; }
		public string COMPLE { get; set; }
		public string BAIRRO { get; set; }
		public string MUNI { get; set; }
		public string UF { get; set; }
		public Int32 GIA { get; set; }
		public Int32 IBGE { get; set; }






		//produtos

		public string PRODUTOS { get; set; }
		public string SQLDATA { get; set; }
		public string SQLPROD { get; set; }
		public string SQLGRUPO { get; set; }
		public string SQLANO { get; set; }







	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
	public class DtoCuponMuestra
	{
		public int Id_Cupon { get; set; }
		
		public string Nombre { get; set; }
		
		public string Descripcion { get; set; }
		
		public decimal? PorcentajeDto { get; set; }
		
		public decimal? ImportePromo { get; set; }
		
		public DateTime FechaInicio { get; set; }
		
		public DateTime FechaFinal { get; set; }
		
		public string NombreTipoCupon { get; set; }
		public string Estado { get; set; }
	}
}

using System;
using System.Collections.Generic;

namespace ProyectoASPNETGRUPOC.Model.DTO
{
	public class DtoCuponesMuestraConArticulos
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
		public DtoArticulo Articulos { get; set; } 
	}
}

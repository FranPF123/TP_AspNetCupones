namespace ProyectoASPNETGRUPOC.Model.DTO
{
	public class DtoCuponConArticulos
	{
		public int Id_Cupon {  get; set; }

		public string nombreCupon {  set; get; }
		
		public List<DtoArticuloMuestra>? ArticulosAsociados {  get; set; }

	}
}

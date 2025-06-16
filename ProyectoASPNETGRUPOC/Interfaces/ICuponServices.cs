using ProyectoASPNETGRUPOC.Model.DTO;

namespace ProyectoASPNETGRUPOC.Interfaces
{
	public interface ICuponesServices
	{
		Task CrearCupones(DtoCupones dtoCupones);

		Task<DtoCuponesMuestra> GetCuponPorId(int idCupones);


		Task<List<DtoCuponesMuestra>> ListaDeCuponesActivos();









		//Validaciones
		bool existeCuponId(int idCupon);

		bool existeNombreCupon(string Nombre);

		DateTime CalcularFechaFinPorDias(DtoCupones dtoCupon);

	}
}

using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;

namespace ProyectoASPNETGRUPOC.Interfaces
{
	public interface ICuponesServices
	{
		Task CrearCupones(DtoCupones dtoCupones);
		Task<CuponesDetalle> AsignarCuponAArticulo(int idCupon, DtoCuponDetalle dtoCuponDetalle);

		Task<DtoCuponConArticulos> ObtenerCuponConArticulos(int idCupon);
		Task<DtoCuponesMuestra> GetCuponPorId(int idCupones);
		Task<List<DtoCuponesMuestra>> ListaDeCuponesActivos();
        Task<List<DtoCuponesMuestra>> ObtenerTodosLosCupones();

        Task<bool> ModificarCupon(int id, DtoCupones dtoCupones);
        Task<bool> EliminarCupon(int id);
        //Validaciones
        Task<bool> existeCuponId(int idCupon);

		Task<bool> existeNombreCupon(string Nombre);

		DateTime CalcularFechaFinPorDias(DtoCupones dtoCupon);

		Task<DtoCuponesMuestra> ReclamarCupon(int idCupon, int idUsuario);
		Task<List<DtoCuponesClientes>> ObtenerCuponesDelCliente(int idUsuario);
		Task<bool> UsarCuponReclamado(int idUsuario, string nroCupon);
		Task<List<DtoHistorialCupon>> ObtenerHistorialDeCupones(int idUsuario);


    }
}

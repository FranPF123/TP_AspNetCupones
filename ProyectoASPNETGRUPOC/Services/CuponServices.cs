using Microsoft.EntityFrameworkCore;
using ProyectoASPNETGRUPOC.Data;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;

namespace ProyectoASPNETGRUPOC.Services
{
	public class CuponServices : ICuponesServices
	{
		public readonly ApplicationDbContext _context;

		public CuponServices(ApplicationDbContext context)
		{
			_context = context;
		}
		//Crear Cupones
		public async Task CrearCupones(DtoCupones dtoCupones)
		{
			if (existeNombreCupon(dtoCupones.Nombre) || dtoCupones.DiasDuracionDeCupon <= 0)
			{
				throw new KeyNotFoundException("Ya existe un cupon con este Nombre.");
			}
			DateTime FechaFin = CalcularFechaFinPorDias(dtoCupones);



			Cupones cupon = new Cupones()
			{

				Nombre = dtoCupones.Nombre,
				Descripcion = dtoCupones.Descripcion,
				PorcentajeDto = dtoCupones.PorcentajeDto,
				ImportePromo = dtoCupones.ImportePromo,
				FechaInicio = dtoCupones.FechaInicio,
				FechaFin = FechaFin,
				Id_Tipo_Cupon = dtoCupones.Id_Tipo_Cupon,

			};


			_context.Cupones.Add(cupon);

			await _context.SaveChangesAsync();
		}

		//5.Debe haber una lista de cupones activos y que se encuentren en el rango de fechas de uso
		public async Task<List<DtoCuponesMuestra>> ListaDeCuponesActivos()
		{
			List<DtoCuponesMuestra> ListaDeCuponesActivos = await _context.Cupones.Include(c => c.TipoCupon).Where(c => c.FechaInicio < c.FechaFin && c.Activo == true).Select(c => new DtoCuponesMuestra
			{
                Id_Cupon = c.Id_Cupon,
                Nombre = c.Nombre,
				Descripcion = c.Descripcion,
				PorcentajeDto = c.PorcentajeDto,
				ImportePromo = c.ImportePromo,
				FechaInicio = c.FechaInicio,
				FechaFinal = c.FechaFin,
				NombreTipoCupon = c.TipoCupon.Nombre,
                Estado = c.Activo == true ? "Activo" : "Inactivo"
            }).ToListAsync();


			if (!ListaDeCuponesActivos.Any()) throw new KeyNotFoundException("No se encontro ningun tipo de Cupon activo.");

			return ListaDeCuponesActivos;
		}
		//Obtener Cupon por Id ACTIVO Y INACTIVO
		public async Task<DtoCuponesMuestra> GetCuponPorId(int idCupones)
		{
			DtoCuponesMuestra? Cupones = await _context.Cupones.Include(c => c.TipoCupon).Where(c => c.Id_Cupon == idCupones).Select(c => new DtoCuponesMuestra
			{
				Nombre = c.Nombre,
				Descripcion = c.Descripcion,
				PorcentajeDto = c.PorcentajeDto,
				ImportePromo = c.ImportePromo,
				FechaInicio = c.FechaInicio,
				FechaFinal = c.FechaFin,
				NombreTipoCupon = c.TipoCupon.Nombre
            }).FirstOrDefaultAsync();

			if (Cupones == null) throw new KeyNotFoundException("No existe un Cupon con este Id.");

			return Cupones;
		}

		// Obtener todos los cupones tanto Activos como Inactivos
        public async Task<List<DtoCuponesMuestra>> ObtenerTodosLosCupones()
        {
            var cupones = await _context.Cupones
                .Include(c => c.TipoCupon)
                .Select(c => new DtoCuponesMuestra
                {
                    Id_Cupon = c.Id_Cupon,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    PorcentajeDto = c.PorcentajeDto,
                    ImportePromo = c.ImportePromo,
                    FechaInicio = c.FechaInicio,
                    FechaFinal = c.FechaFin,
                    NombreTipoCupon = c.TipoCupon.Nombre,
                    Estado = c.Activo == true ? "Activo" : "Inactivo"
                }).ToListAsync();

            if (!cupones.Any())
                throw new KeyNotFoundException("No se encontraron cupones registrados.");

            return cupones;
        }


        // Modificar cupon existente
        public async Task<bool> ModificarCupon(int id, DtoCupones dtoCupones)
        {
            var cupon = await _context.Cupones.FindAsync(id);

            if (cupon == null || cupon.Activo == false)
                throw new KeyNotFoundException($"No se encontró un cupón activo con ID {id}");

            if (existeNombreCupon(dtoCupones.Nombre) && dtoCupones.Nombre != cupon.Nombre)
                throw new Exception("Ya existe otro cupón con ese nombre.");

            cupon.Nombre = dtoCupones.Nombre;
            cupon.Descripcion = dtoCupones.Descripcion;
            cupon.PorcentajeDto = dtoCupones.PorcentajeDto;
            cupon.ImportePromo = dtoCupones.ImportePromo;
            cupon.FechaInicio = dtoCupones.FechaInicio;
            cupon.FechaFin = CalcularFechaFinPorDias(dtoCupones);
            cupon.Id_Tipo_Cupon = dtoCupones.Id_Tipo_Cupon;

            _context.Cupones.Update(cupon);
            await _context.SaveChangesAsync();

            return true;
        }

        // Eliminar cupon (dar de baja)
        public async Task<bool> EliminarCupon(int id)
        {
            var cupon = await _context.Cupones.FindAsync(id);
            if (cupon == null || cupon.Activo == false)
                throw new KeyNotFoundException("No se encontró el cupón o ya está dado de baja.");

            cupon.Activo = false;
            _context.Cupones.Update(cupon);
            await _context.SaveChangesAsync();

            return true;
        }



        public bool existeCuponId(int idCupon)
		{
			var CuponPorId = _context.Cupones.Where(c => c.Id_Cupon == idCupon && c.Activo == true).FirstOrDefault();

			if (CuponPorId == null) return false;

			return true;
		}

		public bool existeNombreCupon(string Nombre)
		{
			var Cupon = _context.Cupones.Where(c => c.Nombre == Nombre && c.Activo == true).FirstOrDefault();

			if (Cupon == null) return false;

			return true;
		}

		public DateTime CalcularFechaFinPorDias(DtoCupones dtoCupon)
		{
			DateTime fechaFin = dtoCupon.FechaInicio.AddDays(dtoCupon.DiasDuracionDeCupon);


			if (dtoCupon.FechaInicio >= fechaFin)
			{
				throw new KeyNotFoundException("La fecha de inicio no puede ser mayor a la fecha de finalizacion");
			}

			return fechaFin;
		}



	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoASPNETGRUPOC.Data;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;

namespace ProyectoASPNETGRUPOC.Services
{
	public class CuponServices : ICuponesServices
	{
		public readonly ApplicationDbContext _context;
		public readonly IArticuloService ArtService;

		public CuponServices(ApplicationDbContext context, IArticuloService artService)
		{
			_context = context;
			ArtService = artService;
		}
		//Crear Cupones
		public async Task CrearCupones(DtoCupones dtoCupones)
		{
			if (await existeNombreCupon(dtoCupones.Nombre) || dtoCupones.DiasDuracionDeCupon <= 0)
			{
				throw new KeyNotFoundException("Ya existe un cupon con este Nombre.");
			}
			DateTime FechaFin = CalcularFechaFinPorDias(dtoCupones);
			
			
			await ValidacionPorRoles(dtoCupones);


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

            if (await existeNombreCupon(dtoCupones.Nombre) && dtoCupones.Nombre != cupon.Nombre)
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

		public async Task ValidacionPorRoles(DtoCupones dtoCupon)
		{
			TipoCupon tipo = await _context.Tipo_Cupon.FindAsync(dtoCupon.Id_Tipo_Cupon);
			if (tipo == null) throw new Exception("Tipo de cupon no valido");


			// Validaciones según el tipo
			if (tipo.Nombre == "Porcentaje")
			{
				if (dtoCupon.PorcentajeDto == null || dtoCupon.PorcentajeDto <= 0)
					throw new Exception("Debe ingresar un porcentaje mayor a cero.");
				//if (dtoCupon.ImportePromo != null) throw new Exception("No debe ingresar un monto si el tipo es porcentaje.");
			}
			else if (tipo.Nombre == "Monto")
			{
				if (dtoCupon.ImportePromo == null || dtoCupon.ImportePromo <= 0)
					throw new Exception("Debe ingresar un monto mayor a cero.");
				// if (dtoCupon.PorcentajeDto != null) throw new Exception("No debe ingresar un porcentaje si el tipo es monto.");
			}
		}


        public async Task<bool> existeCuponId(int idCupon)
		{
			var CuponPorId = await _context.Cupones.Where(c => c.Id_Cupon == idCupon && c.Activo == true).FirstOrDefaultAsync();

			if (CuponPorId == null) return false;

			return true;
		}

		public async Task<bool> existeNombreCupon(string Nombre)
		{
			var Cupon = await _context.Cupones.Where(c => c.Nombre == Nombre && c.Activo == true).FirstOrDefaultAsync();

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

		public async Task<CuponesDetalle> AsignarCuponAArticulo(int idCupon, DtoCuponDetalle dtoCuponDetalle)
		{
			if (! await existeCuponId(idCupon))
			{
				throw new KeyNotFoundException("NO existe un cupon con este ID.");
			}
			//await _context.Cupones_Detalle.Where(cd => cd.Id_Cupon == idCupon && cd.Id_Articulo == dtoCuponDetalle.Id_Articulo).FirstOrDefaultAsync();

			if (!await ArtService.existeArticuloId(dtoCuponDetalle.Id_Articulo))
			{
				throw new KeyNotFoundException("NO existe un Articulo con este ID.");
			}

			if (await ArtService.precioDeArticulo(dtoCuponDetalle.Id_Articulo))
			{
				throw new KeyNotFoundException("No se puede asignar un articulo con precio igual a cero a un cupon.");
			}

			CuponesDetalle CuponDetalle = new CuponesDetalle()
			{
				Id_Cupon = idCupon,
				Id_Articulo = dtoCuponDetalle.Id_Articulo,
				cantidad = dtoCuponDetalle.cantidad
			};

			_context.Cupones_Detalle.Add(CuponDetalle);

			await _context.SaveChangesAsync();
			CuponesDetalle cuponDetalle = await _context.Cupones_Detalle.Where(cd => cd.Id_Cupon == idCupon && cd.Id_Articulo == dtoCuponDetalle.Id_Articulo).FirstOrDefaultAsync();
			return cuponDetalle;
		}

		public async Task<DtoCuponConArticulos> ObtenerCuponConArticulos(int idCupon)
		{
			if (!await existeCuponId(idCupon))
			{
				throw new Exception("No existe un cupon con este id.");
			}

			DtoCuponConArticulos? CuponConArticulos = await _context.Cupones.Where(c => c.Id_Cupon == idCupon).Select(c => new DtoCuponConArticulos
			{
				Id_Cupon = c.Id_Cupon,
				nombreCupon = c.Nombre,
				ArticulosAsociados = c.CuponesDetalles.Select(cd =>	 new DtoArticuloMuestra
				{
					Nombre_articulo = cd.Articulos.Nombre_articulo,
					Descripcion_articulo = cd.Articulos.Descripcion_articulo,
					Precio = cd.Articulos.Precio,
					cantidad = cd.cantidad
				}).ToList()
			}).FirstOrDefaultAsync();

			if (!CuponConArticulos.ArticulosAsociados.Any())
			{
				throw new Exception("Este cupon no tiene ningun Articulo Asociado");
			}
			return CuponConArticulos;
		}



        public async Task<DtoCuponesMuestra> ReclamarCupon(int idCupon, int idUsuario)
        {
            try
            {
                var cupon = await _context.Cupones
			    .Include(c => c.TipoCupon)
				.FirstOrDefaultAsync(c => c.Id_Cupon == idCupon
					&& c.Activo == true
					&& c.FechaInicio <= DateTime.Now
					&& c.FechaFin >= DateTime.Now);

                if (cupon == null)
                    throw new Exception("El cupón no existe o está inactivo.");

                //para verificar si el cupon esta recalmado
                var yaReclamado = await _context.Cupones_Clientes
                    .AnyAsync(cc => cc.Id_Cupon == idCupon && cc.Id_Usuario == idUsuario);

                if (yaReclamado)
                    throw new Exception("Este cupón ya fue reclamado por el usuario.");


                var nuevoRegistro = new CuponesClientes
                {
                    Id_Cupon = idCupon,
                    Id_Usuario = idUsuario,
                    FechaAsignado = DateTime.Now
                };

                _context.Cupones_Clientes.Add(nuevoRegistro);
                await _context.SaveChangesAsync();

                return new DtoCuponesMuestra
                {
                    Id_Cupon = cupon.Id_Cupon,
                    Nombre = cupon.Nombre,
                    Descripcion = cupon.Descripcion,
                    PorcentajeDto = cupon.PorcentajeDto,
                    ImportePromo = cupon.ImportePromo,
                    FechaInicio = cupon.FechaInicio,
                    FechaFinal = cupon.FechaFin,
                    NombreTipoCupon = cupon.TipoCupon.Nombre,
                    Estado = "Reclamado"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al reclamar cupón: {ex.Message}");
            }
        }


    }


}

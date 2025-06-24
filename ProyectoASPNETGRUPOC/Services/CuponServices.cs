using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoASPNETGRUPOC.Data;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		public async Task<List<DtoCuponesMuestraConArticulos>> ListaDeCuponesActivos()
		{
            DateTime FechaActual = DateTime.Now;
            List<DtoCuponesMuestraConArticulos> ListaDeCuponesActivos = await _context.Cupones_Detalle.Where(cd => cd.Cupon.Activo == true && FechaActual < cd.Cupon.FechaFin)
                .Select(cd => new DtoCuponesMuestraConArticulos
                {
                    Id_Cupon = cd.Cupon.Id_Cupon,
                    Nombre = cd.Cupon.Nombre,
                    Descripcion = cd.Cupon.Descripcion,
                    PorcentajeDto = cd.Cupon.PorcentajeDto,
                    ImportePromo = cd.Cupon.ImportePromo,
                    FechaInicio = cd.Cupon.FechaInicio,
                    FechaFinal = cd.Cupon.FechaFin,
                    NombreTipoCupon = cd.Cupon.TipoCupon.Nombre,
                    Estado = cd.Cupon.Activo == true ? "Activo" : "Inactivo",
                    Articulo = new DTOArticulosMuestraFront
                    {
                        Nombre_articulo = cd.Articulos.Nombre_articulo,
                        Precio = cd.Articulos.Precio
                    }
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


        //Reclamar Cupon y generar nroCupon
        public async Task<DtoCuponesMuestra> ReclamarCupon(int idCupon, int idUsuario)
        {
            try
            {
                var cupon = await _context.Cupones
                    .Include(c => c.TipoCupon)
                    .FirstOrDefaultAsync(c =>
                        c.Id_Cupon == idCupon &&
                        c.Activo == true &&
                        c.FechaInicio <= DateTime.Now &&
                        c.FechaFin >= DateTime.Now);
                var Cliente = await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.id == idUsuario && u.Estado == true);

                if (Cliente == null) throw new Exception("El usuario no existe");
                if (cupon == null)
                    throw new Exception("El cupon no existe o está inactivo o fuera de fecha");

                var yaReclamado = await _context.Cupones_Clientes
                    .AnyAsync(cc => cc.Id_Cupon == idCupon && cc.Id_Usuario == idUsuario);

                // verificar si ya fue reclamado por el cliente
                if (yaReclamado)
                    throw new Exception("Este cupón ya fue reclamado por el usuario");

                // Generar nroCupon 123-456-789
                string nuevoNroCupon;
                do
                {
                    nuevoNroCupon = GenerarNroCuponAleatorio();
                }
                while (await _context.Cupones_Clientes.AnyAsync(cc => cc.NroCupon == nuevoNroCupon));

                var nuevoRegistro = new CuponesClientes
				{
                    Id_Cupon = idCupon,
                    Id_Usuario = idUsuario,
                    FechaAsignado = DateTime.Now,
                    NroCupon = nuevoNroCupon
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
                throw new Exception($"Error al reclamar cupon: {ex.Message}");
            }
        }

        //Generar numeros randoms del 100 al 999
        private string GenerarNroCuponAleatorio()
        {
            var random = new Random();
            int parte1 = random.Next(100, 999);
            int parte2 = random.Next(100, 999);
            int parte3 = random.Next(100, 999);
            return $"{parte1}-{parte2}-{parte3}";
        }

        // ver lista de cupones reclamados
        public async Task<List<DtoCuponesClientes>> ObtenerCuponesDelCliente(int idUsuario)
        {
            var cupones = await _context.Cupones_Clientes
                .Include(cc => cc.Cupon)
                .ThenInclude(c => c.TipoCupon)
                .Where(cc => cc.Id_Usuario == idUsuario)
                .Select(cc => new DtoCuponesClientes
                {
                    NroCupon = cc.NroCupon,
                    NombreCupon = cc.Cupon.Nombre,
                    Descripcion = cc.Cupon.Descripcion,
                    FechaAsignado = cc.FechaAsignado,
                    FechaInicio = cc.Cupon.FechaInicio,
                    FechaFin = cc.Cupon.FechaFin,
                    PorcentajeDto = cc.Cupon.PorcentajeDto,
                    ImportePromo = cc.Cupon.ImportePromo,
                    TipoCupon = cc.Cupon.TipoCupon.Nombre,
                    Estado = cc.Cupon.Activo == true ? "Activo" : "Inactivo"
                })
                .ToListAsync();

            if (!cupones.Any())
                throw new Exception("El usuario no tiene cupones reclamados");

            return cupones;
        }

        //Quemar el Cupon
        public async Task<bool> UsarCuponReclamado(int idUsuario, string nroCupon)
        {
            var cuponReclamado = await _context.Cupones_Clientes
                .Include(cc => cc.Cupon)
                .FirstOrDefaultAsync(cc => cc.NroCupon == nroCupon && cc.Id_Usuario == idUsuario);

            if (cuponReclamado == null)
                throw new Exception("El cupón no está asignado al usuario o ya fue utilizado");

            var cupon = cuponReclamado.Cupon;

            // Validaciones
            if (!cupon.Activo ?? false)
                throw new Exception("El cupón no está activo");

            if (cupon.FechaInicio > DateTime.Now || cupon.FechaFin < DateTime.Now)
                throw new Exception("El cupón no está en un rango de fechas válido");

            // Crear historial
            var historial = new CuponesHistorial
            {
                Id_Cupon = cuponReclamado.Id_Cupon,
                NroCupon = cuponReclamado.NroCupon,
                Id_Usuario = idUsuario,
                FechaUso = DateTime.Now
            };

            // Eliminar de Cupones_Clientes
            _context.Cupones_Clientes.Remove(cuponReclamado);

            // Agregar al historial
            _context.Cupones_Historial.Add(historial);

            await _context.SaveChangesAsync();

            return true;
        }

        //ver Historial de Cupones
        public async Task<List<DtoHistorialCupon>> ObtenerHistorialDeCupones()
        {
            var historial = await _context.Cupones_Historial
                .Include(ch => ch.Usuario)
                .Include(ch => ch.Cupon)
                .OrderByDescending(ch => ch.FechaUso)
                .Select(ch => new DtoHistorialCupon
                {
                    NroCupon = ch.NroCupon,
                    Nombre = ch.Cupon.Nombre,
                    FechaUso = ch.FechaUso,
                    NombreUsuario = ch.Usuario.User_Name
                })
                .ToListAsync();

            if (!historial.Any())
                throw new Exception("El usuario no tiene cupones utilizados");

            return historial;
        }

        //REPORTES 
        public async Task<List<DtoReporteCuponesUsados>> ReporteCuponesMasUsados()
        {
            var reporte = await _context.Cupones_Historial
                .GroupBy(h => new { h.Id_Cupon, h.Cupon.Nombre })
                .Select(g => new DtoReporteCuponesUsados
                {
                    IdCupon = g.Key.Id_Cupon,
                    Nombre = g.Key.Nombre,
                    CantidadUsos = g.Count()
                })
                .OrderByDescending(r => r.CantidadUsos)
                .ToListAsync();

            return reporte;
        }


        public async Task<List<DtoReporteCuponesUsados>> ReporteCuponesUsadosPorFechas(DateTime desde, DateTime hasta)
        {
            var reporte = await _context.Cupones_Historial
                .Where(h => h.FechaUso >= desde && h.FechaUso <= hasta)
                .GroupBy(h => h.Id_Cupon)
                .Select(g => new DtoReporteCuponesUsados
                {
                    IdCupon = g.Key,
                    Nombre = _context.Cupones
                        .Where(c => c.Id_Cupon == g.Key)
                        .Select(c => c.Nombre)
                        .FirstOrDefault(),
                    CantidadUsos = g.Count()
                })
                .OrderByDescending(r => r.CantidadUsos)
                .ToListAsync();

            return reporte;
        }


        public async Task<List<DtoCuponesReclamados>> ObtenerCuponesMasReclamados()
        {
            var resultado = await _context.Cupones_Clientes.GroupBy(cc => cc.Id_Cupon).Select(group => new DtoCuponesReclamados
        {
            Id_Cupon = group.Key,
            NombreDelCupon = group.First().Cupon.Nombre,
            CantidadReclamaciones = group.Count()
        })
        .OrderByDescending(c => c.CantidadReclamaciones)
        .ToListAsync();

            return resultado;
        }
    }


}

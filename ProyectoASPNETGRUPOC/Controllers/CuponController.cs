using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model.DTO;
using ProyectoASPNETGRUPOC.Services;

namespace ProyectoASPNETGRUPOC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CuponController : ControllerBase
	{
		private readonly ICuponesServices CServices;
        private readonly IUsuarioServices UsuarioServices;
        private readonly IEmailService _emailService;

        public CuponController(ICuponesServices cServices, IUsuarioServices usuarioServices, IEmailService emailService)
        {
			CServices = cServices;
            UsuarioServices = usuarioServices;
            _emailService = emailService;
        }

        [HttpPost("/Cupones/{idCupon}/AsociarArticulo")]

        public async Task<IActionResult> AsignarCuponAArticulo(int idCupon, DtoCuponDetalle cuponDetalle)
        {
            try
            {
                if (cuponDetalle == null) return BadRequest("Debes asignar el id del articulo y la cantidad");



                var cupDetalles = await CServices.AsignarCuponAArticulo(idCupon, cuponDetalle);

                return Ok("Cupon: " + cupDetalles.Cupon.Nombre + " Asignado a articulo:" + cupDetalles.Articulos.Nombre_articulo + " ");
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Cupones/{idCupon}/Articulos")]
        public async Task<IActionResult> ObtenerArticulonesDeCupon(int idCupon)
        {
            try
            {
                DtoCuponConArticulos cuponConArticulos = await CServices.ObtenerCuponConArticulos(idCupon);

                return Ok(cuponConArticulos);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


		[HttpPost("/CrearCupon")]
		[Authorize(Policy = "ControlUsuarios")]
		public async Task<IActionResult> CrearCupon(DtoCupones dtoCupones)
		{
			try
			{
				if (dtoCupones == null) return BadRequest("NO es posible ingresar un Cupon vacio.");


				await CServices.CrearCupones(dtoCupones);



				return Ok(new
				{
					Mensaje = "El Cupon " + dtoCupones.Nombre + " fue creado correctamente"
				});
			}
			catch (KeyNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("/ObtenerCuponPorId")]
		[Authorize(Policy = "ControlUsuarios")]
		public async Task<IActionResult> ObtenerCuponPorId(int IdCupon)
		{
			try
			{
				DtoCuponesMuestra CuponObtenidoPorId = await CServices.GetCuponPorId(IdCupon);

				return Ok(new
				{
					Mensaje = "Cupon con Id" + IdCupon,
					CuponObtenidoPorID = CuponObtenidoPorId
				});

			}
			catch (KeyNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("/ObtenerCuponesDisponibles")]
		[Authorize]
		public async Task<IActionResult> ObtenerCuponesDisponibles()
		{
			try
			{
				List<DtoCuponesMuestraConArticulos> CuponesDisponibles = await CServices.ListaDeCuponesActivos();


                return Ok(CuponesDisponibles);
			}
			catch (KeyNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}


		}

        [HttpGet("/ObtenerCupones")]
        [Authorize(Policy = "ControlUsuarios")]
        public async Task<IActionResult> ObtenerCupones()
        {
            try
            {
                List<DtoCuponesMuestra> todosLosCupones = await CServices.ObtenerTodosLosCupones();

                return Ok(new
                {
                    Mensaje = "Listado completo de cupones",
                    Cupones = todosLosCupones
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ControlUsuarios")]
        public async Task<IActionResult> ModificarCupon(int id, [FromBody] DtoCupones dto)
        {
            try
            {
                bool resultado = await CServices.ModificarCupon(id, dto);
                if (!resultado) return NotFound("No se pudo modificar el cupón.");

                return Ok(new
                {
                    Mensaje = $"Cupón con ID {id} modificado correctamente"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/EliminarCupon/{id}")]
        [Authorize(Policy = "ControlUsuarios")]
        public async Task<IActionResult> EliminarCupon(int id)
        {
            try
            {
                bool resultado = await CServices.EliminarCupon(id);
                if (!resultado) return NotFound("No se pudo eliminar el cupón.");

                return Ok(new
                {
                    Mensaje = $"Cupón con ID {id} fue dado de baja correctamente"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Reclamar el cupon
        [HttpPost("/ReclamarCupon/{idCupon}")]
        [Authorize(Policy = "Clientes")]
        public async Task<IActionResult> ReclamarCupon(int idCupon)
        {
            try
            {
                var idClaim = User.FindFirst("Id")?.Value;
                if (string.IsNullOrEmpty(idClaim)) throw new Exception("Token Incorrecto");
                int idUsuario = int.Parse(idClaim);
                var reclamo = await CServices
                    .ReclamarCupon(idCupon, idUsuario);

                if (reclamo == null) return NotFound("Error al reclamar el cupon");

                var usuario = await UsuarioServices.obtenerDatosDeUsuarioLogeado(idUsuario);
                var cupon = await CServices.GetCuponPorId(idCupon);

                var body = $@"
                <h2>¡Cupón reclamado exitosamente!</h2>
                <p>Hola {usuario.Nombre} {usuario.Apellido},</p>
                <p>Has reclamado el siguiente cupón:</p>
                <ul>
                <li><strong>Nombre:</strong> {cupon.Nombre}</li>
                <li><strong>Descripción:</strong> {cupon.Descripcion}</li>
                <li><strong>Válido desde:</strong> {cupon.FechaInicio.ToShortDateString()}</li>
                <li><strong>Válido hasta:</strong> {cupon.FechaFinal.ToShortDateString()}</li>
                <li><strong>Tipo:</strong> {cupon.NombreTipoCupon}</li>
                </ul>
                ";
                await _emailService.SendEmail(usuario.Email, "Cupón reclamado con éxito", body);

                return Ok("El cupon fue reclamado con exito");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //VerCupones del cliente
        [HttpGet("cupones-del-cliente/")]
        [Authorize]
        public async Task<IActionResult> GetCuponesDelCliente()
        {
            try
            {
				var idClaim = User.FindFirst("Id")?.Value;
				if (string.IsNullOrEmpty(idClaim)) throw new Exception("Token Incorrecto");
                int idUsuario = int.Parse(idClaim);
				var lista = await CServices.ObtenerCuponesDelCliente(idUsuario);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Quemar el Cupon
        [HttpPost("usar-cupon/{nroCupon}")]
        [Authorize]
        public async Task<IActionResult> UsarCupon(string nroCupon)
        {
            try
            {
				var idClaim = User.FindFirst("Id")?.Value;
				if (string.IsNullOrEmpty(idClaim)) throw new Exception("Token Incorrecto");
				int idUsuario = int.Parse(idClaim);
				var resultado = await CServices.UsarCuponReclamado(idUsuario, nroCupon);

                return Ok("El cupon fue usado con exito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






		//Authorize roles y admin: 
		[HttpGet("historial-cupones-usados")]
		[Authorize(Policy = "Auditor")]
		public async Task<IActionResult> HistorialCupones()
        {
            try
            {
                var historial = await CServices.ObtenerHistorialDeCupones();

                return Ok(historial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("reporte/mas-usados")]
        [Authorize(Policy = "Auditor")]
        public async Task<IActionResult> ReporteCuponesMasUsados()
        {
            var resultado = await CServices.ReporteCuponesMasUsados();
            return Ok(resultado);
        }



        [HttpGet("reporte/usados-por-fechas")]
        [Authorize(Policy = "Auditor")]
        public async Task<IActionResult> ReporteCuponesPorFechas(DateTime desde, DateTime hasta)
        {
            var resultado = await CServices.ReporteCuponesUsadosPorFechas(desde, hasta);
            return Ok(resultado);
        }




        [HttpGet("reporte/mas-reclamados")]
        [Authorize(Policy = "Auditor")]
        public async Task<IActionResult> ReporteCuponesMasReclamados()
        {
            try
            {
                var resultado = await CServices.ObtenerCuponesMasReclamados();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}

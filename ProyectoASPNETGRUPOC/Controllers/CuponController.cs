using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model.DTO;

namespace ProyectoASPNETGRUPOC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CuponController : ControllerBase
	{
		public readonly ICuponesServices CServices;

		public CuponController(ICuponesServices cServices)
		{
			CServices = cServices;
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
				List<DtoCuponesMuestra> CuponesDisponibles = await CServices.ListaDeCuponesActivos();


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

        //ver historial de cupones usados
        [HttpGet("historial-cupones-usados/{idUsuario}")]
        [Authorize]
        public async Task<IActionResult> HistorialCupones(int idUsuario)
        {
            try
            {
                var historial = await CServices.ObtenerHistorialDeCupones(idUsuario);

                return Ok(new
                {
                    Mensaje = $"Historial de cupones utilizados por el usuario {idUsuario}",
                    Historial = historial
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

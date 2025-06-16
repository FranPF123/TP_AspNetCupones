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

		[HttpPost]
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
		[Authorize(Policy = "ControlUsuarios")]
		public async Task<IActionResult> ObtenerCuponesDisponibles()
		{
			try
			{
				List<DtoCuponesMuestra> CuponesDisponibles = await CServices.ListaDeCuponesActivos();


				return Ok(new
				{
					Mensaje = "Cupones disponibles:",
					CuponesDisponibleS = CuponesDisponibles
				});
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
    }
}

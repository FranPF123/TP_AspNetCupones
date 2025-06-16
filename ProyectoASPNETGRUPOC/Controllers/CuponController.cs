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
	}
}

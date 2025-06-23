using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model.DTO;
using ProyectoASPNETGRUPOC.Model;
using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices AServices;
        public AuthController(IAuthServices _AServices)
        {
            AServices = _AServices;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Complete los campos correspondiente.");
                }
                string tokenString = await AServices.LoginUsuario(model);
                //if (usuarioEntity == null) return BadRequest("Usuario o contraseña incorrectos.");

                return Ok(new
                {
                    Mensaje = "Login Correcto",
                    Token = tokenString,
                    Vencimiento = DateTime.Now.AddHours(1),
                });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception exp)
            {
                return BadRequest(new
                {
                    Mensaje = "Error al loguarse",
                    Error = exp.Message
                });
            }
        }

        [HttpPost("/Registrar")]
        public async Task<IActionResult> RegistrarUsuario(DtoUsuario dtoUsuario)
        {
            try
            {

                if (dtoUsuario == null) return BadRequest("Datos mal formados.");

                await AServices.Registrar(dtoUsuario);

                return Ok(new
                {
                    Mensaje = "el Usuario con el Email: " + dtoUsuario.Email + " fue creado correctamente."
                });


            }
            catch (KeyNotFoundException exK)
            {
                return Conflict(exK.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

	}
}

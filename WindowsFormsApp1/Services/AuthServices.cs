using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Services
{
	public class AuthServices
	{
		private readonly HttpClient ClientHttp;

		public AuthServices()
		{
			this.ClientHttp = new HttpClient();
		}
		public async Task<string> Login(string username, string password)
		{
			try
			{
				string url = "https://localhost:7199/api/Auth";
				LoginModel usuario = new LoginModel
				{
					UserName = username,
					Password = password
				};

				string json = JsonConvert.SerializeObject(usuario);

				HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await ClientHttp.PostAsync(url, content);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{

					ResponseLogin respuestaDelLogin = JsonConvert.DeserializeObject<ResponseLogin>(jsonResponse);

					return respuestaDelLogin.Token;
				}
				else
				{
					throw new KeyNotFoundException(jsonResponse);
				}

		
			}
			catch (KeyNotFoundException ex)
			{
				throw new KeyNotFoundException(ex.Message);
			}
		}


		public async Task<string> Registro(string nombre, string apellido, string userName, string password, string email, string dni)
		{
			try
			{
				string url = "https://localhost:7199/Registrar";
				DtoUsuario UsuarioARegistrar = new DtoUsuario
				{
					UserName = userName,
					Password = password,
					Nombre = nombre,
					Apellido = apellido,
					Dni = dni,
					Email = email
				};
				
				string json = JsonConvert.SerializeObject(UsuarioARegistrar);
				HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await ClientHttp.PostAsync(url, content);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{

					ResponseLogin respuestaDelLogin = JsonConvert.DeserializeObject<ResponseLogin>(jsonResponse);

					return respuestaDelLogin.Mensaje;
				}
				else
				{
					throw new KeyNotFoundException(jsonResponse);
				}
			}
			catch (KeyNotFoundException ex)
			{

				throw new KeyNotFoundException(ex.Message);
			}

		}

	}
}

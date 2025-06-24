using Newtonsoft.Json;
using ProyectoASPNETGRUPOC.Model.DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Services
{
	public class CuponServices
	{
		public readonly HttpClient httpClient;

		public CuponServices()
		{
			this.httpClient = new HttpClient();
		}

		public async Task<List<DtoCuponesMuestraConArticulos>> obtenerCuponesActivos()
		{
			try
			{
				string url = "https://localhost:7199/ObtenerCuponesDisponibles";

				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);


				HttpResponseMessage response = await httpClient.GetAsync(url);

				string jsonResponse = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					List<DtoCuponesMuestraConArticulos> lista = JsonConvert.DeserializeObject<List<DtoCuponesMuestraConArticulos>>(jsonResponse);
					return lista;
				}

				Permisos(response);

				throw new KeyNotFoundException(jsonResponse);
			}
			catch (KeyNotFoundException ex)
			{

				throw new KeyNotFoundException(ex.Message);

			}
		}
		public async Task<List<DtoCuponesClientes>> ObtenerMisCupones()
		{

			try
			{
				string url = "https://localhost:7199/api/Cupon/cupones-del-cliente";
				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);

				HttpResponseMessage response = await httpClient.GetAsync(url);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{
					List<DtoCuponesClientes> ListaCuponesDelCliente = JsonConvert.DeserializeObject<List<DtoCuponesClientes>>(jsonResponse);
					return ListaCuponesDelCliente;
				}
				Permisos(response);

				throw new Exception(jsonResponse);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}


		public async Task<List<DtoHistorialCupon>> HistorialCupones()
		{
			try
			{
				string url = $"https://localhost:7199/api/Cupon/historial-cupones-usados/";
				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);


				HttpResponseMessage response = await httpClient.GetAsync(url);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{
					List<DtoHistorialCupon> listaHistorial = JsonConvert.DeserializeObject<List<DtoHistorialCupon>>(jsonResponse);
					return listaHistorial;
				}
				Permisos(response);
				throw new Exception(jsonResponse);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<List<DtoCuponesReclamados>> CuponesMasReclamados()
		{
			try
			{
				string url = $"https://localhost:7199/api/Cupon/reporte/mas-reclamados";
				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);


				HttpResponseMessage response = await httpClient.GetAsync(url);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{
					List<DtoCuponesReclamados> ListaCuponesMasReclamados = JsonConvert.DeserializeObject<List<DtoCuponesReclamados>>(jsonResponse);
					return ListaCuponesMasReclamados;
				}
				Permisos(response);
				throw new Exception(jsonResponse);
			}
			catch( Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<List<DtoReporteCuponesUsados>> ReporteCuponesUsadosPorFechas(DateTime Desde, DateTime Hasta)
		{
			try
			{
				string url = $"https://localhost:7199/api/Cupon/reporte/usados-por-fechas?desde={Desde:yyyy-MM-dd}&hasta={Hasta:yyyy-MM-dd}";

				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);

				HttpResponseMessage response = await httpClient.GetAsync(url);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{
					List<DtoReporteCuponesUsados> listaCuponesMasUsados = JsonConvert.DeserializeObject<List<DtoReporteCuponesUsados>>(jsonResponse);
					return listaCuponesMasUsados;
				}
				Permisos(response);
				throw new Exception(jsonResponse);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<List<DtoReporteCuponesUsados>> ReporteCuponesMasUsados()
		{
			try
			{
				string url = $"https://localhost:7199/api/Cupon/reporte/mas-usados";
				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);

				HttpResponseMessage response = await httpClient.GetAsync(url);
				string jsonResponse = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					List<DtoReporteCuponesUsados> ListaCuponesMasUsados = JsonConvert.DeserializeObject<List<DtoReporteCuponesUsados>>(jsonResponse);
					return ListaCuponesMasUsados;
				}

				Permisos(response);
				throw new Exception(jsonResponse);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<string> usarCupon(string nroCupon)
		{
			try
			{
				string url = $"https://localhost:7199/api/Cupon/usar-cupon/{nroCupon}";

				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);
				var datos = new
				{
					NroCupon = nroCupon
				};
				string json = JsonConvert.SerializeObject(datos);
				HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, content);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{
					return jsonResponse;
				}
				Permisos(response);
				throw new Exception(jsonResponse);
			}catch(Exception ex)
			{
				return ex.Message;
			}
		}

		public async Task<string> ReclamarCupon(int idCupon)
		{
			try
			{
				string url = $"https://localhost:7199/ReclamarCupon/{idCupon}";
				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);
				var datos = new
				{
					IdCupon = idCupon,
				};

				string json = JsonConvert.SerializeObject(datos);
				HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, content);

				string jsonResponse = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					
					return jsonResponse;
				}
				Permisos(response);
				throw new Exception(jsonResponse);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public void Permisos(HttpResponseMessage response)
		{
			if ((int)response.StatusCode == 401)
			{
				throw new Exception("No se encuentra autenticado");
			}
			else if ((int)response.StatusCode == 402)
			{
				throw new Exception("No tiene permisos para este recurso");
			}
		}
	}
}

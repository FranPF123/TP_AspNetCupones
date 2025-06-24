using Newtonsoft.Json;
using ProyectoASPNETGRUPOC.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Services
{
	public class ArticuloServices
	{
		private readonly HttpClient httpClient;

		public ArticuloServices()
		{
			this.httpClient = new HttpClient();
		}

		public async Task<List<DtoReporteArticulos>> ArticulosMasUsados()
		{
			try
			{
				string url = "https://localhost:7199/api/Articulo/Reporte/ArticulosMasUsados";
				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.Token);

				HttpResponseMessage response = await httpClient.GetAsync(url);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{
					List<DtoReporteArticulos> ListaArticulosMas = JsonConvert.DeserializeObject<List<DtoReporteArticulos>>(jsonResponse);
					return ListaArticulosMas;
				}
				Permisos(response);

				throw new Exception(jsonResponse);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void Permisos(HttpResponseMessage response)
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

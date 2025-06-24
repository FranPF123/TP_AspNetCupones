using ProyectoASPNETGRUPOC.Model.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Forms
{
	public partial class ArtMasUsados : Form
	{
		private readonly ArticuloServices AServices;
		private List<DtoReporteArticulos> lista;
		public ArtMasUsados(ArticuloServices aServices)
		{
			lista = new List<DtoReporteArticulos>();
			AServices = aServices;
			InitializeComponent();
		}

		private void ArtMasUsados_Load(object sender, EventArgs e)
		{
			cargarGrilla();
		}

		private async void cargarGrilla()
		{

			try
			{

				lista.Clear();
				lista = await AServices.ArticulosMasUsados();


				//Es necesario hacerlo así para que recargue la grilla
				if (lista != null)
				{
					dgvArtMasUsados.DataSource = null;
					dgvArtMasUsados.AutoGenerateColumns = true;
					dgvArtMasUsados.DataSource = lista;
				}
				else
				{
					MessageBox.Show("Lista Vacia");
				}

			}
			catch (KeyNotFoundException ex)
			{
				MessageBox.Show(ex.Message);
			}
			catch (Exception exx)
			{
				MessageBox.Show(exx.Message);
			}

		}
	}
}

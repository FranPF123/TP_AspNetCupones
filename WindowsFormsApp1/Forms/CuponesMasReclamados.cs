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
	public partial class CuponesMasReclamados : Form
	{
		private readonly CuponServices Cservices;
		private List<DtoCuponesReclamados> listaReclamados;
		public CuponesMasReclamados(CuponServices cservices)
		{
			listaReclamados = new List<DtoCuponesReclamados>();
			this.Cservices = cservices;
			InitializeComponent();
		}
		private async void cargarGrilla()
		{

			try
			{

				listaReclamados.Clear();
				listaReclamados = await Cservices.CuponesMasReclamados();


				//Es necesario hacerlo así para que recargue la grilla
				if (listaReclamados != null)
				{
					dgvMasReclamados.DataSource = null;
					dgvMasReclamados.AutoGenerateColumns = true;
					dgvMasReclamados.DataSource = listaReclamados;
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

		private void CuponesMasReclamados_Load(object sender, EventArgs e)
		{
			cargarGrilla();
		}
	}
}

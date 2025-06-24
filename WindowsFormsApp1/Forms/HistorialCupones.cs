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
	public partial class HistorialCupones : Form
	{
		private readonly CuponServices Cservices;
		private List<DtoHistorialCupon> listaHistorial;
		public HistorialCupones(CuponServices Cservices)
		{
			this.Cservices = Cservices;
			this.listaHistorial = new List<DtoHistorialCupon>();
			InitializeComponent();

		}

		private async void cargarGrilla()
		{

			try
			{

				listaHistorial.Clear();
				listaHistorial = await Cservices.HistorialCupones();


				//Es necesario hacerlo así para que recargue la grilla
				if (listaHistorial != null)
				{
					dgbHistorialCupones.DataSource = null;
					dgbHistorialCupones.AutoGenerateColumns = true;
					dgbHistorialCupones.DataSource = listaHistorial;
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

		private void dgbHistorialCupones_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void HistorialCupones_Load(object sender, EventArgs e)
		{
			cargarGrilla();
		}
	}
}

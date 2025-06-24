using ProyectoASPNETGRUPOC.Model.DTO;
using System;
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
	public partial class CuponesMasUsados : Form
	{

		private List<DtoReporteCuponesUsados> lista;
		private readonly CuponServices Cservices;
		public CuponesMasUsados(CuponServices cServices)
		{
			this.Cservices = cServices;
			lista = new List<DtoReporteCuponesUsados>();
			InitializeComponent();
		}

		private async void cargarGrilla()
		{

			try
			{

				lista.Clear();
				lista = await Cservices.ReporteCuponesMasUsados();


				//Es necesario hacerlo así para que recargue la grilla
				if (lista != null)
				{
					dgvCuponesMasUsados.DataSource = null;
					dgvCuponesMasUsados.AutoGenerateColumns = true;
					dgvCuponesMasUsados.DataSource = lista;
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

		private void CuponesMasUsados_Load(object sender, EventArgs e)
		{
			cargarGrilla();
		}

		private async void btnPorFecha_Click(object sender, EventArgs e)
		{
			try
			{
				if (DateTime.TryParse(txtDesde.Text, out DateTime Desde) && DateTime.TryParse(txtHasta.Text, out DateTime Hasta))
				{
					


					if (Desde > Hasta)
					{
						MessageBox.Show("La fecha 'Desde' no puede ser mayor a la fecha 'Hasta'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					lista.Clear();
					lista = await Cservices.ReporteCuponesUsadosPorFechas(Desde, Hasta);
					dgvCuponesMasUsados.DataSource = null;
					dgvCuponesMasUsados.AutoGenerateColumns = true;
					dgvCuponesMasUsados.DataSource = lista;
				}
				else
				{
					MessageBox.Show("Error en las fechas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnMasUsados_Click(object sender, EventArgs e)
		{
			cargarGrilla();
		}
	}
}

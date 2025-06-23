using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Forms;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
	public partial class ReclamarCupon : Form
	{
		public CuponServices CServices;

		public List<DtoCuponMuestra> lista;
		public ReclamarCupon(CuponServices cuponServices)
		{
			this.CServices = cuponServices;
			lista = new List<DtoCuponMuestra>();
			InitializeComponent();
		}

		private async void ReclamarCupon_Load(object sender, EventArgs e)
		{


			if (CServices != null)
			{
				this.cargarGrilla();
			}


		}


		public async void cargarGrilla()
		{

			try
			{

				lista.Clear();
				lista = await CServices.obtenerCuponesActivos();


				//Es necesario hacerlo así para que recargue la grilla
				if (lista != null)
				{
					dgvCupones.DataSource = null;
					dgvCupones.AutoGenerateColumns = true;
					dgvCupones.DataSource = lista;
				}
				else
				{
					MessageBox.Show("Lista Vacia");
				}

			}
			catch (KeyNotFoundException ex)
			{
				MessageBox.Show(ex.Message);
			}catch(Exception exx)
			{
				MessageBox.Show(exx.Message);
			}

		}

		private async void btnActualizar_Click(object sender, EventArgs e)
		{
			this.cargarGrilla();
		}

		private async void btnReclamar_Click(object sender, EventArgs e)
		{
			try {
				if (string.IsNullOrEmpty(txtIdCupon.Text))
				{
					MessageBox.Show("Completar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				int idCupon = int.Parse(txtIdCupon.Text);
				
				string mensaje = await CServices.ReclamarCupon(idCupon);

				MessageBox.Show(mensaje, "Cupon reclamado", MessageBoxButtons.OK);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			

		}
	
	}
}


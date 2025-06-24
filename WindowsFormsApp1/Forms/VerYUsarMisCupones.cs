using ProyectoASPNETGRUPOC.Model.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Forms
{
	public partial class VerYUsarMisCupones : Form
	{
		private List<DtoCuponesClientes> ListaCupones;
		private readonly CuponServices CServices;
		public VerYUsarMisCupones(CuponServices cServices)
		{
			ListaCupones = new List<DtoCuponesClientes>();
			this.CServices = cServices;
			InitializeComponent();

		}



		public async void cargarGrilla()
		{

			try
			{

				ListaCupones.Clear();
				ListaCupones = await CServices.ObtenerMisCupones();


				//Es necesario hacerlo así para que recargue la grilla
				if (ListaCupones != null)
				{
					dgwMisCupones.DataSource = null;
					dgwMisCupones.AutoGenerateColumns = true;
					dgwMisCupones.DataSource = ListaCupones;
				}
				else
				{
					MessageBox.Show("Lista Vacia");
					return;
				}

			}
			catch (KeyNotFoundException ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
			catch (Exception exx)
			{
				MessageBox.Show(exx.Message);
				return;
			}

		}

		private void VerYUsarMisCupones_Load(object sender, EventArgs e)
		{
			


			if (CServices != null)
			{
				this.cargarGrilla();
			}
		}

		public string TransformarNroCupones(string nroCuponeSinGuion)
		{
			if (nroCuponeSinGuion.Length != 9)
			{
				throw new Exception("El numero de cupon debe tener 9 caracteres");
			}
			string nroCupones = "";

			for (int i = 0; i <= nroCuponeSinGuion.Length - 1; i++)
			{

				if (i == 3 || i == 6)
				{
				
					nroCupones += "-";

				}
				nroCupones += nroCuponeSinGuion[i];

			}
			return nroCupones;
		}

		private async void btnUsarCupon_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(txtNroCupon.Text))
				{
					MessageBox.Show("Debe completar el numero de cupon - Sin Guion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string  nroCupon = TransformarNroCupones(txtNroCupon.Text);
				
				
				string mensaje = await CServices.usarCupon(nroCupon);
				label1.Text = nroCupon;
				MessageBox.Show(mensaje, "Cupon usado con exito", MessageBoxButtons.OK);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}

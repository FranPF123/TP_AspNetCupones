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
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Forms.Auth
{
	public partial class Registro : Form
	{
		public readonly HttpClient httpClient;
		public AuthServices AServices;
		private Login FormLogin;
		public Registro(HttpClient _httpClient, AuthServices aServices, Login formLogin)
		{
			httpClient = _httpClient;
			AServices = aServices;
			InitializeComponent();
			FormLogin = formLogin;
		}

		private void Registro_Load(object sender, EventArgs e)
		{

		}

		private void txtLogin_Click(object sender, EventArgs e)
		{
			this.Hide();
			FormLogin.Show();
		}

		private async void txtRegistro_Click(object sender, EventArgs e)
		{
			try
			{
				if(string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtDni.Text))
				{
					MessageBox.Show("Completar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				var Mensaje = await AServices.Registro(txtNombre.Text, txtApellido.Text, txtUserName.Text, txtPassword.Text, txtEmail.Text, txtDni.Text);

				MessageBox.Show(Mensaje.ToString(), "Login Correcto", MessageBoxButtons.OK);
			}
			catch (KeyNotFoundException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}

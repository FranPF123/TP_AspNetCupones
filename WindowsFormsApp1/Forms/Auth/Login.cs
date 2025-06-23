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
using WindowsFormsApp1.Jwt;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Forms.Auth
{
	public partial class Login : Form
	{
		public readonly HttpClient httpClient = new HttpClient();
		public AuthServices AServices;
		public Login()
		{
			
			InitializeComponent();
			httpClient.BaseAddress = new Uri("https://localhost:7199/api/");
		}

		private void Login_Load(object sender, EventArgs e)
		{
			 AServices = new AuthServices();
		}

		private async void btnIngresar_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPassword.Text))
				{
					MessageBox.Show("Completar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}


				var token = await  AServices.Login(txtUser.Text, txtPassword.Text);
				Session.Token = token;
				Session.User_Name = txtUser.Text;
				Session.idUsuario = HerramientaJwt.obtenerClaim(token, "Id");//Guarda el id como string, cuando lo usemos hay que parsearlo
				Session.Rol = HerramientaJwt.obtenerClaim(token, "role");
				MessageBox.Show("Login Correcto", "Login Correcto", MessageBoxButtons.OK);
				txtPassword.Text = "";
				txtUser.Text = "";
				this.Hide();
				FormInicio FormInicio = new FormInicio(this);
				FormInicio.Show();
				//ReclamarCupon VistaReclamarCupon = new ReclamarCupon(httpClient);
				//VistaReclamarCupon.Show();


			}
			catch(KeyNotFoundException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnRegistrar_Click(object sender, EventArgs e)
		{
			this.Hide();
			Registro registro = new Registro(httpClient, AServices, this);
			registro.Show();
		}
	}
}

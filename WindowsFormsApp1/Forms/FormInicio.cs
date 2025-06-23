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
using WindowsFormsApp1.Forms.Auth;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Forms
{
	public partial class FormInicio : Form
	{
		public readonly CuponServices cuponServices;
		public readonly Form Login;

		public FormInicio(Form login)
		{
			this.cuponServices = new CuponServices();
			InitializeComponent();
			this.Login = login;
		}
		private void abrirFormEnPanel(ToolStripMenuItem OpcionMenu, Form Formulario)
		{
			try
			{
				Panel.Controls.Clear();

				Formulario.TopLevel = false;
				Formulario.FormBorderStyle = FormBorderStyle.None;
				Formulario.Dock = DockStyle.Fill;

				Panel.Controls.Add(Formulario);
				Formulario.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al abrir la ventana.");
			}
		}
		private void FormInicio_Load(object sender, EventArgs e)
		{
			if(Session.Rol == "Cliente")
			{
				Reportes.Visible = false;
			}
		}

		private void PanelPrincipal_Paint(object sender, PaintEventArgs e)
		{

		}

		private void altaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			abrirFormEnPanel(ReclamarVerCupones, new ReclamarCupon(cuponServices));
		}

		private void UsarVerMisCupones_Click(object sender, EventArgs e)
		{
			abrirFormEnPanel(UsarVerMisCupones, new VerYUsarMisCupones(cuponServices));
		}

		private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Session.Token = null;
			Session.idUsuario = "0";
			Session.User_Name = null;
			this.Close();
			Login.Show();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}

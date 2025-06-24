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
		private readonly CuponServices cuponServices;
		private readonly Form Login;
		private readonly ArticuloServices artServices;
		public FormInicio(Form login)
		{
			this.cuponServices = new CuponServices();
			this.artServices = new ArticuloServices();
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
			else
			{
				UsarVerMisCupones.Visible = false;
				ReclamarVerCupones.Visible = false;
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

		private void historialCuponesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			abrirFormEnPanel(historialCuponesToolStripMenuItem, new HistorialCupones(cuponServices));
		}

		private void cuponesMasUsadosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			abrirFormEnPanel(cuponesMasUsadosToolStripMenuItem, new CuponesMasUsados(cuponServices));
		}

		private void articulosMasUsadosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			abrirFormEnPanel(articulosMasUsadosToolStripMenuItem, new ArtMasUsados(artServices));
		}

		private void cuponesMasReclamadosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			abrirFormEnPanel(cuponesMasReclamadosToolStripMenuItem, new CuponesMasReclamados(cuponServices));
		}
	}
}

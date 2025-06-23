namespace WindowsFormsApp1.Forms
{
	partial class FormInicio
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Panel = new System.Windows.Forms.Panel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.InfoVeterinaria = new System.Windows.Forms.ToolStripMenuItem();
			this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ReclamarVerCupones = new System.Windows.Forms.ToolStripMenuItem();
			this.UsarVerMisCupones = new System.Windows.Forms.ToolStripMenuItem();
			this.Reportes = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Panel
			// 
			this.Panel.Location = new System.Drawing.Point(0, 27);
			this.Panel.Name = "Panel";
			this.Panel.Size = new System.Drawing.Size(819, 423);
			this.Panel.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.Snow;
			this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InfoVeterinaria,
            this.ReclamarVerCupones,
            this.UsarVerMisCupones,
            this.Reportes});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(819, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// InfoVeterinaria
			// 
			this.InfoVeterinaria.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.InfoVeterinaria.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesionToolStripMenuItem});
			this.InfoVeterinaria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.InfoVeterinaria.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.InfoVeterinaria.Name = "InfoVeterinaria";
			this.InfoVeterinaria.Size = new System.Drawing.Size(88, 20);
			this.InfoVeterinaria.Text = "Cerrar Sesion";
			// 
			// cerrarSesionToolStripMenuItem
			// 
			this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
			this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
			this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
			// 
			// ReclamarVerCupones
			// 
			this.ReclamarVerCupones.Name = "ReclamarVerCupones";
			this.ReclamarVerCupones.Size = new System.Drawing.Size(134, 20);
			this.ReclamarVerCupones.Text = "Reclamar/Ver Cupones";
			this.ReclamarVerCupones.Click += new System.EventHandler(this.altaToolStripMenuItem_Click);
			// 
			// UsarVerMisCupones
			// 
			this.UsarVerMisCupones.Name = "UsarVerMisCupones";
			this.UsarVerMisCupones.Size = new System.Drawing.Size(129, 20);
			this.UsarVerMisCupones.Text = "Usar/Ver mis cupones";
			this.UsarVerMisCupones.Click += new System.EventHandler(this.UsarVerMisCupones_Click);
			// 
			// Reportes
			// 
			this.Reportes.Name = "Reportes";
			this.Reportes.Size = new System.Drawing.Size(65, 20);
			this.Reportes.Text = "Reportes";
			// 
			// FormInicio
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(819, 450);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.Panel);
			this.Name = "FormInicio";
			this.Text = "FormInicio";
			this.Load += new System.EventHandler(this.FormInicio_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel Panel;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem InfoVeterinaria;
		private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ReclamarVerCupones;
		private System.Windows.Forms.ToolStripMenuItem UsarVerMisCupones;
		private System.Windows.Forms.ToolStripMenuItem Reportes;
	}
}
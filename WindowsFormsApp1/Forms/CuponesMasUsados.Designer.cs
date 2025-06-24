namespace WindowsFormsApp1.Forms
{
	partial class CuponesMasUsados
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
			this.dgvCuponesMasUsados = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.btnPorFecha = new System.Windows.Forms.Button();
			this.txtDesde = new System.Windows.Forms.TextBox();
			this.txtHasta = new System.Windows.Forms.TextBox();
			this.btnMasUsados = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvCuponesMasUsados)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvCuponesMasUsados
			// 
			this.dgvCuponesMasUsados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvCuponesMasUsados.Location = new System.Drawing.Point(106, 71);
			this.dgvCuponesMasUsados.Name = "dgvCuponesMasUsados";
			this.dgvCuponesMasUsados.Size = new System.Drawing.Size(609, 320);
			this.dgvCuponesMasUsados.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(114, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Cupones mas usados";
			// 
			// btnPorFecha
			// 
			this.btnPorFecha.Location = new System.Drawing.Point(546, 46);
			this.btnPorFecha.Name = "btnPorFecha";
			this.btnPorFecha.Size = new System.Drawing.Size(105, 23);
			this.btnPorFecha.TabIndex = 3;
			this.btnPorFecha.Text = "Buscar por fecha";
			this.btnPorFecha.UseVisualStyleBackColor = true;
			this.btnPorFecha.Click += new System.EventHandler(this.btnPorFecha_Click);
			// 
			// txtDesde
			// 
			this.txtDesde.Location = new System.Drawing.Point(341, 48);
			this.txtDesde.Name = "txtDesde";
			this.txtDesde.Size = new System.Drawing.Size(84, 20);
			this.txtDesde.TabIndex = 5;
			// 
			// txtHasta
			// 
			this.txtHasta.Location = new System.Drawing.Point(444, 48);
			this.txtHasta.Name = "txtHasta";
			this.txtHasta.Size = new System.Drawing.Size(84, 20);
			this.txtHasta.TabIndex = 6;
			// 
			// btnMasUsados
			// 
			this.btnMasUsados.Location = new System.Drawing.Point(3, 159);
			this.btnMasUsados.Name = "btnMasUsados";
			this.btnMasUsados.Size = new System.Drawing.Size(97, 37);
			this.btnMasUsados.TabIndex = 7;
			this.btnMasUsados.Text = "Cupones mas usados";
			this.btnMasUsados.UseVisualStyleBackColor = true;
			this.btnMasUsados.Click += new System.EventHandler(this.btnMasUsados_Click);
			// 
			// CuponesMasUsados
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btnMasUsados);
			this.Controls.Add(this.txtHasta);
			this.Controls.Add(this.txtDesde);
			this.Controls.Add(this.btnPorFecha);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgvCuponesMasUsados);
			this.Name = "CuponesMasUsados";
			this.Text = "CuponesMasUsados";
			this.Load += new System.EventHandler(this.CuponesMasUsados_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvCuponesMasUsados)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvCuponesMasUsados;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnPorFecha;
		private System.Windows.Forms.TextBox txtDesde;
		private System.Windows.Forms.TextBox txtHasta;
		private System.Windows.Forms.Button btnMasUsados;
	}
}
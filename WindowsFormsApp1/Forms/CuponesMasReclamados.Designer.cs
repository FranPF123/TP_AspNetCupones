namespace WindowsFormsApp1.Forms
{
	partial class CuponesMasReclamados
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
			this.dgvMasReclamados = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dgvMasReclamados)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvMasReclamados
			// 
			this.dgvMasReclamados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMasReclamados.Location = new System.Drawing.Point(198, 79);
			this.dgvMasReclamados.Name = "dgvMasReclamados";
			this.dgvMasReclamados.Size = new System.Drawing.Size(493, 270);
			this.dgvMasReclamados.TabIndex = 0;
			// 
			// CuponesMasReclamados
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.dgvMasReclamados);
			this.Name = "CuponesMasReclamados";
			this.Text = "CuponesMasReclamados";
			this.Load += new System.EventHandler(this.CuponesMasReclamados_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvMasReclamados)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvMasReclamados;
	}
}
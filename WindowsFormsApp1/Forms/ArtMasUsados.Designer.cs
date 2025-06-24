namespace WindowsFormsApp1.Forms
{
	partial class ArtMasUsados
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
			this.dgvArtMasUsados = new System.Windows.Forms.DataGridView();
			this.btnActualizar = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvArtMasUsados)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvArtMasUsados
			// 
			this.dgvArtMasUsados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvArtMasUsados.Location = new System.Drawing.Point(199, 65);
			this.dgvArtMasUsados.Name = "dgvArtMasUsados";
			this.dgvArtMasUsados.Size = new System.Drawing.Size(497, 299);
			this.dgvArtMasUsados.TabIndex = 0;
			// 
			// btnActualizar
			// 
			this.btnActualizar.Location = new System.Drawing.Point(32, 135);
			this.btnActualizar.Name = "btnActualizar";
			this.btnActualizar.Size = new System.Drawing.Size(75, 23);
			this.btnActualizar.TabIndex = 1;
			this.btnActualizar.Text = "Actualizar";
			this.btnActualizar.UseVisualStyleBackColor = true;
			// 
			// ArtMasUsados
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btnActualizar);
			this.Controls.Add(this.dgvArtMasUsados);
			this.Name = "ArtMasUsados";
			this.Text = "ArtMasUsados";
			this.Load += new System.EventHandler(this.ArtMasUsados_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvArtMasUsados)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvArtMasUsados;
		private System.Windows.Forms.Button btnActualizar;
	}
}
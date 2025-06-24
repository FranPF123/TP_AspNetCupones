namespace WindowsFormsApp1.Forms
{
	partial class HistorialCupones
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
			this.dgbHistorialCupones = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgbHistorialCupones)).BeginInit();
			this.SuspendLayout();
			// 
			// dgbHistorialCupones
			// 
			this.dgbHistorialCupones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgbHistorialCupones.Location = new System.Drawing.Point(153, 55);
			this.dgbHistorialCupones.Name = "dgbHistorialCupones";
			this.dgbHistorialCupones.Size = new System.Drawing.Size(589, 351);
			this.dgbHistorialCupones.TabIndex = 0;
			this.dgbHistorialCupones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbHistorialCupones_CellContentClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(159, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Historial Cupones";
			// 
			// HistorialCupones
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgbHistorialCupones);
			this.Name = "HistorialCupones";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.HistorialCupones_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgbHistorialCupones)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgbHistorialCupones;
		private System.Windows.Forms.Label label1;
	}
}
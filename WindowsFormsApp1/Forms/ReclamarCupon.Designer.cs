namespace WindowsFormsApp1
{
	partial class ReclamarCupon
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
			this.dgvCupones = new System.Windows.Forms.DataGridView();
			this.btnReclamar = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtIdCupon = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnActualizar = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvCupones)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvCupones
			// 
			this.dgvCupones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvCupones.Location = new System.Drawing.Point(218, 88);
			this.dgvCupones.Name = "dgvCupones";
			this.dgvCupones.Size = new System.Drawing.Size(544, 322);
			this.dgvCupones.TabIndex = 0;
			// 
			// btnReclamar
			// 
			this.btnReclamar.Location = new System.Drawing.Point(35, 175);
			this.btnReclamar.Name = "btnReclamar";
			this.btnReclamar.Size = new System.Drawing.Size(100, 33);
			this.btnReclamar.TabIndex = 1;
			this.btnReclamar.Text = "Reclamar Cupon";
			this.btnReclamar.UseVisualStyleBackColor = true;
			this.btnReclamar.Click += new System.EventHandler(this.btnReclamar_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(215, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Cupones";
			// 
			// txtIdCupon
			// 
			this.txtIdCupon.Location = new System.Drawing.Point(35, 135);
			this.txtIdCupon.Name = "txtIdCupon";
			this.txtIdCupon.Size = new System.Drawing.Size(100, 20);
			this.txtIdCupon.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(43, 119);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Id del cupon";
			// 
			// btnActualizar
			// 
			this.btnActualizar.Location = new System.Drawing.Point(652, 56);
			this.btnActualizar.Name = "btnActualizar";
			this.btnActualizar.Size = new System.Drawing.Size(75, 23);
			this.btnActualizar.TabIndex = 5;
			this.btnActualizar.Text = "Actualizar";
			this.btnActualizar.UseVisualStyleBackColor = true;
			this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
			// 
			// ReclamarCupon
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btnActualizar);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtIdCupon);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnReclamar);
			this.Controls.Add(this.dgvCupones);
			this.Name = "ReclamarCupon";
			this.Text = "ReclamarCupon";
			this.Load += new System.EventHandler(this.ReclamarCupon_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvCupones)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvCupones;
		private System.Windows.Forms.Button btnReclamar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtIdCupon;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnActualizar;
	}
}
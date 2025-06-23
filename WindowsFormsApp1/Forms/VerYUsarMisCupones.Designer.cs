namespace WindowsFormsApp1.Forms
{
	partial class VerYUsarMisCupones
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
			this.btnUsarCupon = new System.Windows.Forms.Button();
			this.txtNroCupon = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dgwMisCupones = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dgwMisCupones)).BeginInit();
			this.SuspendLayout();
			// 
			// btnUsarCupon
			// 
			this.btnUsarCupon.Location = new System.Drawing.Point(40, 151);
			this.btnUsarCupon.Name = "btnUsarCupon";
			this.btnUsarCupon.Size = new System.Drawing.Size(75, 23);
			this.btnUsarCupon.TabIndex = 0;
			this.btnUsarCupon.Text = "Usar Cupon";
			this.btnUsarCupon.UseVisualStyleBackColor = true;
			this.btnUsarCupon.Click += new System.EventHandler(this.btnUsarCupon_Click);
			// 
			// txtNroCupon
			// 
			this.txtNroCupon.Location = new System.Drawing.Point(40, 125);
			this.txtNroCupon.MaxLength = 9;
			this.txtNroCupon.Name = "txtNroCupon";
			this.txtNroCupon.Size = new System.Drawing.Size(100, 20);
			this.txtNroCupon.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(46, 109);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "label1";
			// 
			// dgwMisCupones
			// 
			this.dgwMisCupones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwMisCupones.Location = new System.Drawing.Point(200, 63);
			this.dgwMisCupones.Name = "dgwMisCupones";
			this.dgwMisCupones.Size = new System.Drawing.Size(550, 261);
			this.dgwMisCupones.TabIndex = 3;
			// 
			// VerYUsarMisCupones
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.dgwMisCupones);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtNroCupon);
			this.Controls.Add(this.btnUsarCupon);
			this.Name = "VerYUsarMisCupones";
			this.Text = "VerYUsarMisCupones";
			this.Load += new System.EventHandler(this.VerYUsarMisCupones_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgwMisCupones)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnUsarCupon;
		private System.Windows.Forms.TextBox txtNroCupon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dgwMisCupones;
	}
}
namespace ControlDeStock.Forms
{
    partial class FrmAlertaStock
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvStockBajo = new System.Windows.Forms.DataGridView();
            this.btnCerrrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockBajo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(167, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estos componentes tienen bajo stock";
            // 
            // dgvStockBajo
            // 
            this.dgvStockBajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockBajo.Location = new System.Drawing.Point(48, 84);
            this.dgvStockBajo.Name = "dgvStockBajo";
            this.dgvStockBajo.Size = new System.Drawing.Size(473, 220);
            this.dgvStockBajo.TabIndex = 1;
            // 
            // btnCerrrar
            // 
            this.btnCerrrar.Location = new System.Drawing.Point(446, 353);
            this.btnCerrrar.Name = "btnCerrrar";
            this.btnCerrrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrrar.TabIndex = 2;
            this.btnCerrrar.Text = "Cerrar";
            this.btnCerrrar.UseVisualStyleBackColor = true;
            this.btnCerrrar.Click += new System.EventHandler(this.btnCerrrar_Click);
            // 
            // FrmAlertaStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 388);
            this.Controls.Add(this.btnCerrrar);
            this.Controls.Add(this.dgvStockBajo);
            this.Controls.Add(this.label1);
            this.Name = "FrmAlertaStock";
            this.Text = "FrmAlertaStock";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockBajo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvStockBajo;
        private System.Windows.Forms.Button btnCerrrar;
    }
}
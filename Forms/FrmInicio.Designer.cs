namespace ControlDeStock.Forms
{
    partial class FrmInicio
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnGestionEquipos = new System.Windows.Forms.Button();
            this.btnGestionComponentes = new System.Windows.Forms.Button();
            this.btn_TerminarTrabajo = new System.Windows.Forms.Button();
            this.lbl_Gracias = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Algerian", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(230, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bienvenidos a Gestión Dermotec";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bernard MT Condensed", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(324, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "¿Qué vas a hacer hoy?";
            // 
            // btnGestionEquipos
            // 
            this.btnGestionEquipos.Location = new System.Drawing.Point(164, 216);
            this.btnGestionEquipos.Name = "btnGestionEquipos";
            this.btnGestionEquipos.Size = new System.Drawing.Size(151, 82);
            this.btnGestionEquipos.TabIndex = 2;
            this.btnGestionEquipos.Text = "Gestionar Equipos";
            this.btnGestionEquipos.UseVisualStyleBackColor = true;
            this.btnGestionEquipos.Click += new System.EventHandler(this.btnGestionEquipos_Click);
            // 
            // btnGestionComponentes
            // 
            this.btnGestionComponentes.Location = new System.Drawing.Point(505, 216);
            this.btnGestionComponentes.Name = "btnGestionComponentes";
            this.btnGestionComponentes.Size = new System.Drawing.Size(146, 82);
            this.btnGestionComponentes.TabIndex = 3;
            this.btnGestionComponentes.Text = "Gestionar Componentes";
            this.btnGestionComponentes.UseVisualStyleBackColor = true;
            this.btnGestionComponentes.Click += new System.EventHandler(this.btnGestionComponentes_Click);
            // 
            // btn_TerminarTrabajo
            // 
            this.btn_TerminarTrabajo.Location = new System.Drawing.Point(647, 415);
            this.btn_TerminarTrabajo.Name = "btn_TerminarTrabajo";
            this.btn_TerminarTrabajo.Size = new System.Drawing.Size(141, 23);
            this.btn_TerminarTrabajo.TabIndex = 4;
            this.btn_TerminarTrabajo.Text = "Terminar trabajo";
            this.btn_TerminarTrabajo.UseVisualStyleBackColor = true;
            this.btn_TerminarTrabajo.Click += new System.EventHandler(this.btn_TerminarTrabajo_Click);
            // 
            // lbl_Gracias
            // 
            this.lbl_Gracias.AutoSize = true;
            this.lbl_Gracias.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Gracias.Location = new System.Drawing.Point(230, 373);
            this.lbl_Gracias.Name = "lbl_Gracias";
            this.lbl_Gracias.Size = new System.Drawing.Size(365, 24);
            this.lbl_Gracias.TabIndex = 5;
            this.lbl_Gracias.Text = "Gracias por trabajar con nosotros hoy!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 437);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(556, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Proyecto personal. Creado por Racciatti Gabriel, Técnico Programador de Sistemas." +
    " Todos los derechos reservados.";
            // 
            // FrmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_Gracias);
            this.Controls.Add(this.btn_TerminarTrabajo);
            this.Controls.Add(this.btnGestionComponentes);
            this.Controls.Add(this.btnGestionEquipos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmInicio";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.FrmInicio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGestionEquipos;
        private System.Windows.Forms.Button btnGestionComponentes;
        private System.Windows.Forms.Button btn_TerminarTrabajo;
        private System.Windows.Forms.Label lbl_Gracias;
        private System.Windows.Forms.Label label3;
    }
}
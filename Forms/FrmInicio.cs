using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDeStock.Forms
{
    public partial class FrmInicio : Form
    {
        Timer escribirTimer;
        Timer mostrarTimer;   
        string mensaje = "Proyecto personal. Creado por Racciatti Gabriel, Técnico Programador de Sistemas. Todos los derechos reservados ©."; 
        int indice = 0; 

        public FrmInicio()
        {
            InitializeComponent();
            lbl_Gracias.Visible = false;
            label3.Visible = false;  
            InicializarTimers(); 
        }

        private void InicializarTimers()
        {
            escribirTimer = new Timer();
            escribirTimer.Interval = 80; 
            escribirTimer.Tick += EscribirTimer_Tick;

       
            mostrarTimer = new Timer();
            mostrarTimer.Interval = 5000; 
            mostrarTimer.Tick += MostrarTimer_Tick;
        }

        private void EscribirTimer_Tick(object sender, EventArgs e)
        {
            if (indice < mensaje.Length)
            {
                label3.Text += mensaje[indice]; 
                indice++;  
            }
            else
            {
                escribirTimer.Stop();
                mostrarTimer.Start();
            }
        }

        private void MostrarTimer_Tick(object sender, EventArgs e)
        {
            mostrarTimer.Stop();
            label3.Text = "";  
            indice = 0;  
            label3.Visible = true; 
            escribirTimer.Start();  
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            label3.Visible = true; 
            label3.Text = ""; 
            indice = 0;
            escribirTimer.Start(); 
        }

        private void btnGestionEquipos_Click(object sender, EventArgs e)
        {
            var formEquipos = new FrmGestionEquipos();
            formEquipos.Show();
            lbl_Gracias.Visible = true;
        }

        private void btnGestionComponentes_Click(object sender, EventArgs e)
        {
            var formComponentes = new FrmGestionComponentes();
            formComponentes.Show();
            lbl_Gracias.Visible = true;
        }

        private void btn_TerminarTrabajo_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

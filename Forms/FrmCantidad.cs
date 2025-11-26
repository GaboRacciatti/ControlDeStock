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
    public partial class FrmCantidad : Form
    {
        public int Cantidad { get; private set; } // Propiedad para devolver la cantidad

        public FrmCantidad(string nombreComponente)
        {
            InitializeComponent();
            this.Text = "Ingresar Cantidad"; // Título del formulario
            lblComponente.Text = $"Componente: {nombreComponente}"; // Mostrar el nombre del componente
        } 


        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            // Validar que se haya ingresado un número válido
            if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                Cantidad = cantidad; // Almacenar la cantidad ingresada
                this.DialogResult = DialogResult.OK; // Cerrar el formulario con éxito
                this.Close();
            }
            else
            {
                MessageBox.Show("Ingrese una cantidad válida (mayor a 0).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }



}


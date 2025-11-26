using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlDeStock.DataBase;


namespace ControlDeStock.Forms
{
    public partial class FrmIngresarComponente : Form
    {
        public FrmIngresarComponente()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "INSERT INTO componentes (codigo, nombre, descripcion, cantidadDisponible) " +
                                "VALUES (@codigo, @nombre, @descripcion, @cantidadDisponible)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim());
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                        command.Parameters.AddWithValue("@cantidadDisponible", int.Parse(txtCantidad.Text.Trim()));

                        command.ExecuteNonQuery();
                        MessageBox.Show("Componente ingresado correctamente.");
                        this.DialogResult = DialogResult.OK; // Cierra el formulario indicando éxito
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ingresar el componente: {ex.Message}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmIngresarComponente_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "INSERT INTO componentes (codigo, nombre, descripcion, cantidadDisponible) " +
                                "VALUES (@codigo, @nombre, @descripcion, @cantidadDisponible)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim());
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                        command.Parameters.AddWithValue("@cantidadDisponible", int.Parse(txtCantidad.Text.Trim()));

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Componente ingresado correctamente.");
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Error: No se afectó ninguna fila en la base de datos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ingresar el componente: {ex.Message}");
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

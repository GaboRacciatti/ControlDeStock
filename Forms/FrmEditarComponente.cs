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
    public partial class FrmEditarComponente : Form
    {
        private string codigoOriginal;

        public FrmEditarComponente(string codigo)
        {
            InitializeComponent();
            codigoOriginal = codigo;
            CargarDatosComponente();
        }

        private void CargarDatosComponente()
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "SELECT codigo, nombre, descripcion, cantidadDisponible FROM componentes WHERE codigo = @codigo";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codigo", codigoOriginal);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtCodigo.Text = reader["codigo"].ToString();
                                txtNombre.Text = reader["nombre"].ToString();
                                txtDescripcion.Text = reader["descripcion"].ToString();
                                txtCantidad.Text = reader["cantidadDisponible"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del componente: {ex.Message}");
            }
        }

        private void FrmEditarComponente_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidarCampos();
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "UPDATE componentes SET codigo = @codigo, nombre = @nombre, descripcion = @descripcion, " +
                                "cantidadDisponible = @cantidadDisponible WHERE codigo = @codigoOriginal";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim());
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                        command.Parameters.AddWithValue("@cantidadDisponible", int.Parse(txtCantidad.Text.Trim()));
                        command.Parameters.AddWithValue("@codigoOriginal", codigoOriginal);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Componente actualizado correctamente.");
                        this.DialogResult = DialogResult.OK; // Cierra el formulario indicando éxito
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el componente: {ex.Message}");
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return false;
            }

            if (!int.TryParse(txtCantidad.Text.Trim(), out _))
            {
                MessageBox.Show("La cantidad disponible debe ser un número entero.");
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

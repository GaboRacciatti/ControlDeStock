using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlDeStock.DataBase;
using ControlDeStock.Forms;

namespace ControlDeStock.Forms
{
    public partial class FrmIngresarEquipo : Form
    {
        public FrmIngresarEquipo()
        {
            InitializeComponent();
            dgvComponentesDisponibles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvComponentesAsociados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CargarComponentes();
            ConfigurarDgvComponentesAsociados();
        }

        private void ConfigurarDgvComponentesAsociados()
        {
            // Configurar columnas
            dgvComponentesAsociados.Columns.Clear(); // Limpia cualquier configuración previa
            dgvComponentesAsociados.Columns.Add("Codigo", "Código");
            dgvComponentesAsociados.Columns.Add("Nombre", "Nombre");
            dgvComponentesAsociados.Columns.Add("Cantidad", "Cantidad");

            // Opcional: Configurar la propiedad para que las filas sean editables
            dgvComponentesAsociados.AllowUserToAddRows = false; // Evitar que el usuario agregue filas directamente
            dgvComponentesAsociados.ReadOnly = false; // Permitir edición si es necesario
        }


        private void CargarComponentes()
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "SELECT Codigo, Nombre, Descripcion, CantidadDisponible FROM Componentes";
                    var adapter = new MySqlDataAdapter(query, connection);
                    var table = new DataTable();
                    adapter.Fill(table);

                    // Limpiar y recargar el DataGridView
                    dgvComponentesDisponibles.DataSource = null;
                    dgvComponentesDisponibles.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar componentes: {ex.Message}");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvComponentesDisponibles.SelectedRows.Count > 0)
            {
                string codigo = dgvComponentesDisponibles.SelectedRows[0].Cells["Codigo"].Value.ToString();
                string nombre = dgvComponentesDisponibles.SelectedRows[0].Cells["Nombre"].Value.ToString();

                // Abre un formulario para ingresar la cantidad
                FrmCantidad formCantidad = new FrmCantidad(nombre);
                if (formCantidad.ShowDialog() == DialogResult.OK)
                {
                    int cantidad = formCantidad.Cantidad;

                    // Agrega al DataGridView de la derecha
                    dgvComponentesAsociados.Rows.Add(codigo, nombre, cantidad);
                }
            }
        }


        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            string codigoEquipo = txtCodigo.Text;
            string nombreEquipo = txtNombre.Text;
            DateTime fechaCreacion = DateTime.Now;

            using (var connection = DbConnection.GetConnection())
            {
                connection.Open();

                // Insertar el equipo
                var query = "INSERT INTO Equipos (codigo, nombre, ult_fecha_creacion) VALUES (@Codigo, @Nombre, @Fecha)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Codigo", codigoEquipo);
                command.Parameters.AddWithValue("@Nombre", nombreEquipo);
                command.Parameters.AddWithValue("@Fecha", fechaCreacion);
                command.ExecuteNonQuery();

                // Insertar componentes seleccionados
                foreach (DataGridViewRow row in dgvComponentesAsociados.Rows)
                {
                    if (row.IsNewRow) continue; // Saltar la última fila vacía del DataGridView

                    string codigoComponente = row.Cells["Codigo"].Value.ToString();
                    int cantidad = int.Parse(row.Cells["Cantidad"].Value.ToString());

                    // Insertar en Equipos_Componentes
                    var queryRelacion = "INSERT INTO Equipos_Componentes (codigo_equipo, codigo_componente, cantidad) VALUES (@CodigoEquipo, @CodigoComponente, @Cantidad)";
                    var commandRelacion = new MySqlCommand(queryRelacion, connection);
                    commandRelacion.Parameters.AddWithValue("@CodigoEquipo", codigoEquipo);
                    commandRelacion.Parameters.AddWithValue("@CodigoComponente", codigoComponente);
                    commandRelacion.Parameters.AddWithValue("@Cantidad", cantidad);
                    commandRelacion.ExecuteNonQuery();

                    // Actualizar stock en Componentes
                    var queryStock = "UPDATE Componentes SET CantidadDisponible = CantidadDisponible - @Cantidad WHERE Codigo = @Codigo";
                    var commandStock = new MySqlCommand(queryStock, connection);
                    commandStock.Parameters.AddWithValue("@Cantidad", cantidad);
                    commandStock.Parameters.AddWithValue("@Codigo", codigoComponente);
                    commandStock.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Equipo registrado exitosamente.");
         }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            dgvComponentesAsociados.Rows.Clear();
            Close();
        }
    }
}

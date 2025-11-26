using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using ControlDeStock.DataBase;

namespace ControlDeStock.Forms
{
    public partial class FrmEditarEquipo : Form
    {
        private readonly string codigoEquipoOriginal;

        public FrmEditarEquipo(string codigoEquipo)
        {
            InitializeComponent();
            dgvComponentesDisponibles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvComponentesSeleccionados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtCodigo.Text = codigoEquipo;
            codigoEquipoOriginal = codigoEquipo;

            CargarNombreEquipo(codigoEquipo);

            ConfigurarDgv(dgvComponentesDisponibles);
            ConfigurarDgv(dgvComponentesSeleccionados);

            CargarComponentesDisponibles();
            CargarComponentesDelEquipo(codigoEquipo);
        }

        private void ConfigurarDgv(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AutoGenerateColumns = true;
            dgv.Refresh();
        }

        private void CargarNombreEquipo(string codigoEquipo)
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "SELECT nombre FROM Equipos WHERE codigo = @CodigoEquipo";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CodigoEquipo", codigoEquipo);
                        var nombre = command.ExecuteScalar()?.ToString();
                        if (!string.IsNullOrEmpty(nombre))
                        {
                            txtNombre.Text = nombre;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el nombre del equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComponentesDisponibles()
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "SELECT Codigo, Nombre, CantidadDisponible FROM Componentes WHERE CantidadDisponible > 0";
                    using (var adapter = new MySqlDataAdapter(query, connection))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        dgvComponentesDisponibles.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar componentes disponibles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComponentesDelEquipo(string codigoEquipo)
        {

            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = @"SELECT c.Codigo, c.Nombre, ec.Cantidad 
                          FROM Equipos_Componentes ec
                          JOIN Componentes c ON ec.codigo_componente = c.Codigo
                          WHERE ec.codigo_equipo = @CodigoEquipo";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CodigoEquipo", codigoEquipo);
                    var adapter = new MySqlDataAdapter(command);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvComponentesSeleccionados.DataSource = null;
                    dgvComponentesSeleccionados.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar componentes del equipo: {ex.Message}");
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El nombre y el código del equipo son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nuevoCodigo = txtCodigo.Text;
            string nuevoNombre = txtNombre.Text;

            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var queryEquipo = "UPDATE Equipos SET codigo = @NuevoCodigo, nombre = @NuevoNombre WHERE codigo = @CodigoOriginal";
                            using (var commandEquipo = new MySqlCommand(queryEquipo, connection, transaction))
                            {
                                commandEquipo.Parameters.AddWithValue("@NuevoCodigo", nuevoCodigo);
                                commandEquipo.Parameters.AddWithValue("@NuevoNombre", nuevoNombre);
                                commandEquipo.Parameters.AddWithValue("@CodigoOriginal", codigoEquipoOriginal);
                                commandEquipo.ExecuteNonQuery();
                            }

                            var queryEliminar = "DELETE FROM Equipos_Componentes WHERE codigo_equipo = @CodigoEquipo";
                            using (var commandEliminar = new MySqlCommand(queryEliminar, connection, transaction))
                            {
                                commandEliminar.Parameters.AddWithValue("@CodigoEquipo", nuevoCodigo);
                                commandEliminar.ExecuteNonQuery();
                            }

                            foreach (DataGridViewRow row in dgvComponentesSeleccionados.Rows)
                            {
                                if (row.IsNewRow) continue;
                                string codigoComponente = row.Cells["Codigo"].Value.ToString();
                                int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);

                                var queryInsertar = "INSERT INTO Equipos_Componentes (codigo_equipo, codigo_componente, cantidad) VALUES (@CodigoEquipo, @CodigoComponente, @Cantidad)";
                                using (var commandInsertar = new MySqlCommand(queryInsertar, connection, transaction))
                                {
                                    commandInsertar.Parameters.AddWithValue("@CodigoEquipo", nuevoCodigo);
                                    commandInsertar.Parameters.AddWithValue("@CodigoComponente", codigoComponente);
                                    commandInsertar.Parameters.AddWithValue("@Cantidad", cantidad);
                                    commandInsertar.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Equipo actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvComponentesDisponibles.SelectedRows.Count > 0)
            {
                var selectedRow = dgvComponentesDisponibles.SelectedRows[0];
                string codigo = selectedRow.Cells["Codigo"].Value.ToString();
                string nombre = selectedRow.Cells["Nombre"].Value.ToString();

                using (var formCantidad = new FrmCantidad(nombre))
                {
                    if (formCantidad.ShowDialog() == DialogResult.OK)
                    {
                        int cantidad = formCantidad.Cantidad;
                        AgregarComponenteAlEquipo(codigo,nombre, cantidad);
                    }
                }
            }
        }

        private void btnQuitar_Click_1(object sender, EventArgs e)
        {
            if (dgvComponentesSeleccionados.SelectedRows.Count > 0)
            {
                dgvComponentesSeleccionados.Rows.Remove(dgvComponentesSeleccionados.SelectedRows[0]);
            }
        }

        private void AgregarComponenteAlEquipo(string codigo, string nombre, int cantidad)
        {
            if (dgvComponentesSeleccionados.DataSource is DataTable table)
            {
                // Agregar nueva fila al DataTable
                DataRow row = table.NewRow();
                row["Codigo"] = codigo;
                row["Nombre"] = nombre;
                row["Cantidad"] = cantidad;
                table.Rows.Add(row);

                // Refrescar DataGridView (aunque no es necesario en la mayoría de los casos)
                dgvComponentesSeleccionados.Refresh();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el componente porque la fuente de datos no es válida.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

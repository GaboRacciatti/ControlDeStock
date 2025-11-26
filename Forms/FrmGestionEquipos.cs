using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ControlDeStock.DataBase;

namespace ControlDeStock.Forms
{
    public partial class FrmGestionEquipos : Form
    {
        public FrmGestionEquipos()
        {
            InitializeComponent();
            dgvEquipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CargarEquipos();
        }

        private void CargarEquipos()
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "SELECT Codigo, Nombre FROM Equipos";
                    var adapter = new MySqlDataAdapter(query, connection);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvEquipos.DataSource = null;
                    dgvEquipos.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar equipos: {ex.Message}");
            }
        }



        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            var formIngresar = new FrmIngresarEquipo();
            if (formIngresar.ShowDialog() == DialogResult.OK)
            {
                CargarEquipos();
            }

            CargarEquipos();
            dgvEquipos.Refresh();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "SELECT Codigo, Nombre FROM Equipos WHERE Nombre LIKE @nombre OR Codigo = @codigo";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", $"%{txtBuscar.Text}%");
                    command.Parameters.AddWithValue("@codigo", txtBuscar.Text);

                    var adapter = new MySqlDataAdapter(command);
                    var table = new DataTable();
                    adapter.Fill(table);
                    dgvEquipos.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la búsqueda: {ex.Message}");
            }
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count > 0)
            {
                var codigo = dgvEquipos.SelectedRows[0].Cells["Codigo"].Value.ToString();
                var formEditar = new FrmEditarEquipo(codigo);
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    CargarEquipos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un equipo para editar.");
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count > 0)
            {
                var codigo = dgvEquipos.SelectedRows[0].Cells["Codigo"].Value.ToString();
                var confirm = MessageBox.Show("¿Está seguro de eliminar este equipo?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        using (var connection = DbConnection.GetConnection())
                        {
                            connection.Open();

                            // Eliminar primero los componentes vinculados
                            var deleteComponentsQuery = "DELETE FROM Equipos_Componentes WHERE codigo_equipo = @codigo";
                            var deleteComponentsCommand = new MySqlCommand(deleteComponentsQuery, connection);
                            deleteComponentsCommand.Parameters.AddWithValue("@codigo", codigo);
                            deleteComponentsCommand.ExecuteNonQuery();

                            // Luego eliminar el equipo
                            var deleteEquipoQuery = "DELETE FROM Equipos WHERE Codigo = @codigo";
                            var deleteEquipoCommand = new MySqlCommand(deleteEquipoQuery, connection);
                            deleteEquipoCommand.Parameters.AddWithValue("@codigo", codigo);
                            deleteEquipoCommand.ExecuteNonQuery();

                            MessageBox.Show("Equipo eliminado correctamente.");
                            CargarEquipos();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar equipo: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un equipo para eliminar.");
            }
        }

        private void btnConstruir_Click_1(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count > 0)
            {
                var codigoEquipo = dgvEquipos.SelectedRows[0].Cells["Codigo"].Value.ToString();
                try
                {
                    using (var connection = DbConnection.GetConnection())
                    {
                        connection.Open();
                        // Modificamos la query para usar la tabla correcta Equipos_Componentes
                        var query = @"
                    SELECT c.Codigo, c.Nombre, c.CantidadDisponible, ec.Cantidad
                    FROM Componentes c
                    INNER JOIN Equipos_Componentes ec ON c.Codigo = ec.codigo_componente
                    WHERE ec.codigo_equipo = @codigoEquipo";

                        var command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@codigoEquipo", codigoEquipo);

                        var adapter = new MySqlDataAdapter(command);
                        var table = new DataTable();
                        adapter.Fill(table);

                        // Validar si hay suficiente stock
                        foreach (DataRow row in table.Rows)
                        {
                            var cantidadDisponible = Convert.ToInt32(row["CantidadDisponible"]);
                            var cantidadRequerida = Convert.ToInt32(row["Cantidad"]); // Cambiado de CantidadRequerida a Cantidad
                            if (cantidadDisponible < cantidadRequerida)
                            {
                                MessageBox.Show($"No hay suficiente stock del componente {row["Nombre"]}. " +
                                    $"Disponible: {cantidadDisponible}, Requerido: {cantidadRequerida}");
                                return;
                            }
                        }

                        // Descontar los componentes
                        foreach (DataRow row in table.Rows)
                        {
                            var codigoComponente = row["Codigo"].ToString();
                            var cantidadRequerida = Convert.ToInt32(row["Cantidad"]); // Cambiado de CantidadRequerida a Cantidad
                            var descontarQuery = "UPDATE Componentes SET CantidadDisponible = CantidadDisponible - @cantidad WHERE Codigo = @codigo";
                            var descontarCommand = new MySqlCommand(descontarQuery, connection);
                            descontarCommand.Parameters.AddWithValue("@cantidad", cantidadRequerida);
                            descontarCommand.Parameters.AddWithValue("@codigo", codigoComponente);
                            descontarCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Equipo construido correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al construir equipo: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un equipo para construir.");
            }
        }

        private void FrmGestionEquipos_Load(object sender, EventArgs e)
        {

        }

        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

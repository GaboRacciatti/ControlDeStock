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
using ControlDeStock.Models;

namespace ControlDeStock.Forms
{
    public partial class FrmGestionComponentes : Form
    {
        public FrmGestionComponentes()
        {
            InitializeComponent();
            dgvComponentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CargarComponentes();
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

                    dgvComponentes.DataSource = null;
                    dgvComponentes.DataSource = table;

                    // Verificar si hay componentes con menos de 10 unidades
                    bool hayStockBajo = table.AsEnumerable().Any(row => Convert.ToInt32(row["CantidadDisponible"]) <= 10);

                    // Mostrar el icono si hay stock bajo
                    pbAlertaStock.Visible = hayStockBajo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar componentes: {ex.Message}");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "SELECT Codigo, Nombre, Descripcion, CantidadDisponible FROM Componentes WHERE Nombre LIKE @nombre OR Codigo = @codigo";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", $"%{txtBuscar.Text}%");
                    command.Parameters.AddWithValue("@codigo", txtBuscar.Text);

                    var adapter = new MySqlDataAdapter(command);
                    var table = new DataTable();
                    adapter.Fill(table);
                    dgvComponentes.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la búsqueda: {ex.Message}");
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            var formIngresar = new FrmIngresarComponente();
            if (formIngresar.ShowDialog() == DialogResult.OK)
            {
                CargarComponentes(); // Recargar la lista
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvComponentes.SelectedRows.Count > 0)
            {
                var codigo = Convert.ToInt32(dgvComponentes.SelectedRows[0].Cells["Codigo"].Value);
                var formEditar = new FrmEditarComponente(codigo.ToString());
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    CargarComponentes();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un componente para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvComponentes.SelectedRows.Count > 0)
            {
                var codigo = Convert.ToInt32(dgvComponentes.SelectedRows[0].Cells["Codigo"].Value);
                var confirm = MessageBox.Show("¿Está seguro de eliminar este componente?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        using (var connection = DbConnection.GetConnection())
                        {
                            connection.Open();

                            // Eliminar primero las referencias en Equipos_Componentes
                            var deleteComponentsQuery = "DELETE FROM Equipos_Componentes WHERE codigo_componente = @codigo";
                            var deleteComponentsCommand = new MySqlCommand(deleteComponentsQuery, connection);
                            deleteComponentsCommand.Parameters.AddWithValue("@codigo", codigo);
                            deleteComponentsCommand.ExecuteNonQuery();

                            // Ahora eliminar el componente de la tabla Componentes
                            var deleteQuery = "DELETE FROM Componentes WHERE Codigo = @codigo";
                            var deleteCommand = new MySqlCommand(deleteQuery, connection);
                            deleteCommand.Parameters.AddWithValue("@codigo", codigo);
                            deleteCommand.ExecuteNonQuery();

                            MessageBox.Show("Componente eliminado correctamente.");
                            CargarComponentes(); // Recargar la lista de componentes
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar componente: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un componente para eliminar.");
            }
        }

        private void FrmGestionComponentes_Load(object sender, EventArgs e)
        {

        }

        private void dgvComponentes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbAlertaStock_Click(object sender, EventArgs e)
        {
            FrmAlertaStock formAlerta = new FrmAlertaStock();
            formAlerta.ShowDialog();
        }
    }
}

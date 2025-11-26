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
    public partial class FrmAlertaStock : Form
    {
        public FrmAlertaStock()
        {
            InitializeComponent();
            CargarComponentesConStockBajo();
            dgvStockBajo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }

        private void CargarComponentesConStockBajo()
        {
            try
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    var query = "SELECT Codigo, Nombre, CantidadDisponible FROM Componentes WHERE CantidadDisponible <= 10";
                    var adapter = new MySqlDataAdapter(query, connection);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvStockBajo.DataSource = null;
                    dgvStockBajo.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alertas de stock: {ex.Message}");
            }
        }

        private void btnCerrrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    
}

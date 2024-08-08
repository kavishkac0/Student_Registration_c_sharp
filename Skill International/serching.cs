using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Skill_International
{
    public partial class serching : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=db_skill_international;Uid=root;Pwd=;");
        MySqlCommand cmd;
        DataTable dt = new DataTable(); // DataTable for holding search results

        public serching()
        {
            InitializeComponent();
        }

        private void serching_Load(object sender, EventArgs e)
        {
            // Initialize DataGridView properties
            dataGridView1.AutoGenerateColumns = true; // Enable auto column generation
        }

        public void PerformSearch(string searchQuery)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM tb_student_details WHERE first_name LIKE @Search OR last_name LIKE @Search";
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dt.Clear(); // Clear existing data before filling with new results
                da.Fill(dt);
                con.Close();

                // Bind DataTable to DataGridView
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No students found with the given name.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Home().Show();
            this.Hide();
        }
    }
}
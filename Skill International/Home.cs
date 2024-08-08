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
using MySql.Data.MySqlClient;

namespace Skill_International
{
    public partial class Home : Form
    {

        MySqlConnection con = new MySqlConnection("Server=localhost;Database=db_skill_international;Uid=root;Pwd=;");
        MySqlCommand cmd;
        public Home()
        {
            InitializeComponent();
        }

        private void btn_reg_Click(object sender, EventArgs e)
        {
            new Student_Registration().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new about().Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            {
                string searchQuery = txt_searching.Text.Trim();
                if (string.IsNullOrEmpty(searchQuery))
                {
                    MessageBox.Show("Please enter a name to search.", "Search Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                serching searchForm = new serching();
                searchForm.PerformSearch(searchQuery);
                this.Hide();
               searchForm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Edit_Registration().Show(); 
            this.Hide();    
        }
    }
}

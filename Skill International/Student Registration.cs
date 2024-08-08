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
using MySql.Data.MySqlClient;

namespace Skill_International
{
    public partial class Student_Registration : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=db_skill_international;Uid=root;Pwd=;");
        MySqlCommand cmd;


        public Student_Registration()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Home().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_first_name.Text) || string.IsNullOrWhiteSpace(txt_last_name.Text) ||
               string.IsNullOrWhiteSpace(dateTimePicker1.Text) || string.IsNullOrWhiteSpace(txt_nic.Text) ||
              (!radioMale.Checked && !radioFemale.Checked) || string.IsNullOrWhiteSpace(txt_address.Text) ||
               string.IsNullOrWhiteSpace(txt_email.Text) || string.IsNullOrWhiteSpace(txt_contact.Text) ||
               string.IsNullOrWhiteSpace(txt_parent_name.Text) || string.IsNullOrWhiteSpace(txt_parent_contact.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string gender = radioMale.Checked ? "Male" : "Female";

            try
            {
                con.Open();
                string query = "INSERT INTO tb_student_details (first_name, last_name, birthday, nic, gender, address, email, student_contact_no, parent_name, parent_contact_no) " +
                               "VALUES (@FirstName, @LastName, @Birthday, @NIC, @Gender, @Address, @Email, @StudentContact, @ParentName, @ParentContact)";
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", txt_first_name.Text);
                cmd.Parameters.AddWithValue("@LastName", txt_last_name.Text);
                cmd.Parameters.AddWithValue("@Birthday", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@NIC", txt_nic.Text);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@Email", txt_email.Text);
                cmd.Parameters.AddWithValue("@StudentContact", txt_contact.Text);
                cmd.Parameters.AddWithValue("@ParentName", txt_parent_name.Text);
                cmd.Parameters.AddWithValue("@ParentContact", txt_parent_contact.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student registered successfully.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txt_first_name.Text = "";
            txt_last_name.Text = "";
            dateTimePicker1.Text = "";
            txt_nic.Text = "";
            radioMale.Checked = false;
            radioFemale.Checked = false;
            txt_address.Text = "";
            txt_email.Text = "";
            txt_contact.Text = "";
            txt_parent_name.Text = "";
            txt_parent_contact.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}

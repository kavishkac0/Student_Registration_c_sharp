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
    public partial class Edit_Registration : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=db_skill_international;Uid=root;Pwd=;");
        MySqlCommand cmd;

        public Edit_Registration()
        {
            InitializeComponent();
        }

        private void Edit_Registration_Load(object sender, EventArgs e) { }

        private void btn_search_Click_Click(object sender, EventArgs e) 
        {

            string regNo = textBox1.Text;
            string firstName = txt_first_name.Text;
            string lastName = txt_last_name.Text;
            DateTime birthday = dateTimePicker1.Value;
            string nic = txt_nic.Text;
            string gender = radioMale.Checked ? "Male" : "Female";
            string address = txt_address.Text;
            string email = txt_email.Text;
            string contactNo = txt_contact.Text;
            string parentName = txt_parent_name.Text;
            string parentContact = txt_parent_contact.Text;

            UpdateStudentDetails(regNo, firstName, lastName, birthday, nic, gender, address, email, contactNo, parentName, parentContact);
        

    }


        private void btn_search_edit_Click(object sender, EventArgs e)
        {
            string regNo = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(regNo))
            {
                FetchStudentDetails(regNo);
            }
            else
            {
                MessageBox.Show("Please enter a registration number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void FetchStudentDetails(string regNo)
        {
            string query = "SELECT * FROM tb_student_details WHERE reg_no = @reg_no";

            try
            {
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@reg_no", regNo);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_first_name.Text = reader["first_name"].ToString();
                    txt_last_name.Text = reader["last_name"].ToString();
                    dateTimePicker1.Value = DateTime.Parse(reader["birthday"].ToString());
                    txt_nic.Text = reader["nic"].ToString();
                    if (reader["gender"].ToString() == "Male")
                    {
                        radioMale.Checked = true;
                    }
                    else
                    {
                        radioFemale.Checked = true;
                    }
                    txt_address.Text = reader["address"].ToString();
                    txt_email.Text = reader["email"].ToString();
                    txt_contact.Text = reader["student_contact_no"].ToString();
                    txt_parent_name.Text = reader["parent_name"].ToString();
                    txt_parent_contact.Text = reader["parent_contact_no"].ToString();
                }
                else
                {
                    MessageBox.Show("Student not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string regNo = textBox1.Text;
            string firstName = txt_first_name.Text;
            string lastName = txt_last_name.Text;
            DateTime birthday = dateTimePicker1.Value;
            string nic = txt_nic.Text;
            string gender = radioMale.Checked ? "Male" : "Female";
            string address = txt_address.Text;
            string email = txt_email.Text;
            string contactNo = txt_contact.Text;
            string parentName = txt_parent_name.Text;
            string parentContact = txt_parent_contact.Text;

            UpdateStudentDetails(regNo, firstName, lastName, birthday, nic, gender, address, email, contactNo, parentName, parentContact);
        }

        private void UpdateStudentDetails(string regNo, string firstName, string lastName, DateTime birthday, string nic, string gender, string address, string email, string contactNo, string parentName, string parentContact)
        {
            string query = @"
                UPDATE tb_student_details
                SET first_name = @FirstName,
                    last_name = @LastName,
                    birthday = @Birthday,
                    nic = @NIC,
                    gender = @Gender,
                    address = @Address,
                    email = @Email,
                    student_contact_no = @ContactNo,
                    parent_name = @ParentName,
                    parent_contact_no = @ParentContact
                WHERE reg_no = @RegNo";

            try
            {
                con.Open();
                cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@RegNo", regNo);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Birthday", birthday);
                cmd.Parameters.AddWithValue("@NIC", nic);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@ContactNo", contactNo);
                cmd.Parameters.AddWithValue("@ParentName", parentName);
                cmd.Parameters.AddWithValue("@ParentContact", parentContact);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record updated successfully.");
                }
                else
                {
                    MessageBox.Show("Record not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Home().Show();
            this.Hide();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string regNo = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(regNo))
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteStudentDetails(regNo);
                }
            }
            else
            {
                MessageBox.Show("Please enter a registration number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteStudentDetails(string regNo)
        {
            string query = "DELETE FROM tb_student_details WHERE reg_no = @RegNo";

            try
            {
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@RegNo", regNo);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record deleted successfully.");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Record not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void ClearFields()
        {
            textBox1.Text = string.Empty;
            txt_first_name.Text = string.Empty;
            txt_last_name.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Today;
            txt_nic.Text = string.Empty;
            radioMale.Checked = false;
            radioFemale.Checked = false;
            txt_address.Text = string.Empty;
            txt_email.Text = string.Empty;
            txt_contact.Text = string.Empty;
            txt_parent_name.Text = string.Empty;
            txt_parent_contact.Text = string.Empty;
        }
    
    }

}

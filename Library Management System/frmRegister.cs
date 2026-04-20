using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            // add roles to dropdown
            cboRole.Items.Clear();
            cboRole.Items.Add("Librarian");
            cboRole.Items.Add("Borrower");
            cboRole.SelectedIndex = 1; // default to Borrower
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullname = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // check empty fields
            if (fullname == "" || username == "" || password == "")
            {
                MessageBox.Show("Please fill in all fields.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // check if role is selected
            if (cboRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a role.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = cboRole.SelectedItem.ToString();

            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                // check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM users WHERE username=@username";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@username", username);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Username already exists. Choose another.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Clear();
                    txtUsername.Focus();
                    conn.Close();
                    return;
                }

                // insert new user
                string insertQuery = "INSERT INTO users (fullname, username, password, role) VALUES (@fullname, @username, @password, @role)";
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@fullname", fullname);
                insertCmd.Parameters.AddWithValue("@username", username);
                insertCmd.Parameters.AddWithValue("@password", password);
                insertCmd.Parameters.AddWithValue("@role", role);
                insertCmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Registration successful! You can now log in.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // go back to login
                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Close();
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

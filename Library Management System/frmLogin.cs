using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // check if fields are empty
            if (username == "" || password == "")
            {
                MessageBox.Show("Please fill in all fields.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                string query = "SELECT * FROM users WHERE username=@username AND password=@password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["role"].ToString();
                    int userId = Convert.ToInt32(reader["id"]);
                    string fullname = reader["fullname"].ToString();

                    reader.Close();
                    conn.Close();

                    // route to correct dashboard based on role
                    if (role == "Admin")
                    {
                        frmAdminDashboard admin = new frmAdminDashboard();
                        admin.Show();
                    }
                    else if (role == "Librarian")
                    {
                        frmLibrariansDashboard librarian = new frmLibrariansDashboard();
                        librarian.Show();
                    }
                    else if (role == "Borrower")
                    {
                        frmBorrowersDashboard borrower = new frmBorrowersDashboard(userId, fullname);
                        borrower.Show();
                    }

                    this.Hide();
                }
                else
                {
                    reader.Close();
                    conn.Close();
                    MessageBox.Show("Invalid username or password.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegister register = new frmRegister();
            register.Show();
            this.Hide();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}

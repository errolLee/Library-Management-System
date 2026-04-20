using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class frmUsersMenu : Form
    {
        public frmUsersMenu()
        {
            InitializeComponent();

            // wire events manually since designer didn't wire them
            btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            btnSearch.TextChanged += new System.EventHandler(this.btnSearch_TextChanged);
            cbFilterRole.SelectedIndexChanged += new System.EventHandler(this.cbFilterRole_SelectedIndexChanged);
        }

        private void frmUsersMenu_Load(object sender, EventArgs e)
        {
            // populate role filter dropdown
            cbFilterRole.Items.Clear();
            cbFilterRole.Items.Add("All");
            //cbFilterRole.Items.Add("Admin");
            cbFilterRole.Items.Add("Librarian");
            cbFilterRole.Items.Add("Borrower");
            cbFilterRole.SelectedIndex = 0; // default to All

            LoadUsers("", "All");
        }

        private void LoadUsers(string keyword, string role)
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                string query = @"SELECT id, fullname AS FullName, username AS Username, role AS Role 
                                 FROM users 
                                 WHERE (fullname LIKE @keyword OR username LIKE @keyword)";

                // filter by role if not All
                if (role != "All")
                    query += " AND role = @role";

                query += " ORDER BY role, fullname";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                if (role != "All")
                    cmd.Parameters.AddWithValue("@role", role);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                // hide the id column
                dataGridView1.Columns["id"].Visible = false;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // search as user types
        private void btnSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = btnSearch.Text.Trim();
            string role = cbFilterRole.SelectedItem != null ? cbFilterRole.SelectedItem.ToString() : "All";
            LoadUsers(keyword, role);
        }

        // filter by role when dropdown changes
        private void cbFilterRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keyword = btnSearch.Text.Trim();
            string role = cbFilterRole.SelectedItem.ToString();
            LoadUsers(keyword, role);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a user to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
            string currentFullname = dataGridView1.CurrentRow.Cells["FullName"].Value.ToString();
            string currentUsername = dataGridView1.CurrentRow.Cells["Username"].Value.ToString();
            string currentRole = dataGridView1.CurrentRow.Cells["Role"].Value.ToString();

            // ask for new values
            string newFullname = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit Full Name:", "Edit User", currentFullname);
            if (newFullname.Trim() == "") return;

            string newUsername = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit Username:", "Edit User", currentUsername);
            if (newUsername.Trim() == "") return;

            string newPassword = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter new password (leave blank to keep current):", "Edit User", "");

            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                // check if new username already exists (excluding current user)
                string checkQuery = "SELECT COUNT(*) FROM users WHERE username=@username AND id != @id";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@username", newUsername);
                checkCmd.Parameters.AddWithValue("@id", userId);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Username already exists. Choose another.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    conn.Close();
                    return;
                }

                string updateQuery;

                // if password was entered, update it too
                if (newPassword.Trim() != "")
                {
                    updateQuery = "UPDATE users SET fullname=@fullname, username=@username, password=@password WHERE id=@id";
                }
                else
                {
                    updateQuery = "UPDATE users SET fullname=@fullname, username=@username WHERE id=@id";
                }

                MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@fullname", newFullname);
                updateCmd.Parameters.AddWithValue("@username", newUsername);
                updateCmd.Parameters.AddWithValue("@id", userId);

                if (newPassword.Trim() != "")
                    updateCmd.Parameters.AddWithValue("@password", newPassword);

                updateCmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("User updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // reload with current filter
                string keyword = btnSearch.Text.Trim();
                string role = cbFilterRole.SelectedItem.ToString();
                LoadUsers(keyword, role);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a user to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
            string fullname = dataGridView1.CurrentRow.Cells["FullName"].Value.ToString();
            string role = dataGridView1.CurrentRow.Cells["Role"].Value.ToString();

            // don't allow deleting admin accounts
            if (role == "Admin")
            {
                MessageBox.Show("Admin accounts cannot be deleted.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete user '" + fullname + "'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    MySqlConnection conn = DBConnection.GetConnection();
                    conn.Open();

                    string deleteQuery = "DELETE FROM users WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("User deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string keyword = btnSearch.Text.Trim();
                    string filterRole = cbFilterRole.SelectedItem.ToString();
                    LoadUsers(keyword, filterRole);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting user: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

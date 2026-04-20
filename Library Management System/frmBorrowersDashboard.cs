using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class frmBorrowersDashboard : Form
    {
        private int userId;
        private string fullname;

        public frmBorrowersDashboard(int userId, string fullname)
        {
            InitializeComponent();
            this.userId = userId;
            this.fullname = fullname;
        }

        private void frmBorrowersDashboard_Load(object sender, EventArgs e)
        {
            this.Text = "Borrower's Dashboard - " + fullname;
            LoadMyBooks();
            LoadStats();
        }

        private void LoadMyBooks()
        {
            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT b.title, b.author, t.borrow_date, t.due_date,
                             CASE 
                                WHEN t.return_date IS NULL THEN 'Not Returned' 
                                ELSE 'Returned' 
                             END AS status
                             FROM transactions t
                             JOIN books b ON t.book_id = b.book_id
                             WHERE t.user_id = @userId
                             ORDER BY t.borrow_date DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgBooks.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message);
            }
        }

        private void LoadStats()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                // total borrowed (not yet returned)
                MySqlCommand cmd1 = new MySqlCommand(
                    "SELECT COUNT(*) FROM transactions WHERE user_id=@uid AND return_date IS NULL", conn);
                cmd1.Parameters.AddWithValue("@uid", userId);
                labelBorrowed.Text = cmd1.ExecuteScalar().ToString();

                // overdue books
                MySqlCommand cmd2 = new MySqlCommand(
                    "SELECT COUNT(*) FROM transactions WHERE user_id=@uid AND due_date < CURDATE() AND return_date IS NULL", conn);
                cmd2.Parameters.AddWithValue("@uid", userId);
                lblOverdue.Text = cmd2.ExecuteScalar().ToString();

                // total fine (10 pesos per day overdue)
                MySqlCommand cmd3 = new MySqlCommand(
                    @"SELECT IFNULL(SUM(DATEDIFF(CURDATE(), due_date) * 10), 0) 
                      FROM transactions 
                      WHERE user_id=@uid AND due_date < CURDATE() AND return_date IS NULL", conn);
                cmd3.Parameters.AddWithValue("@uid", userId);
                lblFine.Text = "₱" + cmd3.ExecuteScalar().ToString();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stats: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //string keyword = txtSearch.Text.Trim();

            //try
            //{
            //    MySqlConnection conn = DBConnection.GetConnection();
            //    conn.Open();

            //    string query = @"SELECT b.title, b.author, t.borrow_date, t.due_date,
            //                     CASE WHEN t.return_date IS NULL THEN 'Not Returned' ELSE 'Returned' END AS status
            //                     FROM transactions t
            //                     JOIN books b ON t.book_id = b.book_id
            //                     WHERE t.user_id = @userId
            //                     AND (b.title LIKE @keyword OR b.author LIKE @keyword)
            //                     ORDER BY t.borrow_date DESC";

            //    MySqlCommand cmd = new MySqlCommand(query, conn);
            //    cmd.Parameters.AddWithValue("@userId", userId);
            //    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

            //    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    adapter.Fill(dt);

            //    dgBooks.DataSource = dt;

            //    conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error searching: " + ex.Message, "Error",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadSearchResults();
        }

        private void LoadSearchResults()
        {
            string keyword = txtSearch.Text.Trim();

            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                string query = @"SELECT b.title, b.author, t.borrow_date, t.due_date,
                                 CASE WHEN t.return_date IS NULL THEN 'Not Returned' ELSE 'Returned' END AS status
                                 FROM transactions t
                                 JOIN books b ON t.book_id = b.book_id
                                 WHERE t.user_id = @userId
                                 AND (b.title LIKE @keyword OR b.author LIKE @keyword)
                                 ORDER BY t.borrow_date DESC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgBooks.DataSource = dt;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

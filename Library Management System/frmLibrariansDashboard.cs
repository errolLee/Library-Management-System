using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class frmLibrariansDashboard : Form
    {
        public frmLibrariansDashboard()
        {
            InitializeComponent();
        }

        private void frmLibrariansDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardStats();
        }

        private void LoadDashboardStats()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM books", conn);
                lblTotalBooks.Text = cmd1.ExecuteScalar().ToString();

                MySqlCommand cmd2 = new MySqlCommand("SELECT COUNT(*) FROM users WHERE role='Borrower'", conn);
                lblTotalBorrowers.Text = cmd2.ExecuteScalar().ToString();

                MySqlCommand cmd3 = new MySqlCommand("SELECT COUNT(*) FROM users WHERE role='Librarian'", conn);
                lblTotalLibrarians.Text = cmd3.ExecuteScalar().ToString();

                MySqlCommand cmd4 = new MySqlCommand("SELECT COUNT(*) FROM transactions WHERE DATE(borrow_date)=CURDATE()", conn);
                lblBorrowedToday.Text = cmd4.ExecuteScalar().ToString();

                MySqlCommand cmd5 = new MySqlCommand("SELECT COUNT(*) FROM transactions WHERE return_date IS NULL", conn);
                lblPendingReturns.Text = cmd5.ExecuteScalar().ToString();

                MySqlCommand cmd6 = new MySqlCommand("SELECT COUNT(*) FROM transactions WHERE due_date < CURDATE() AND return_date IS NULL", conn);
                lblOverdue.Text = cmd6.ExecuteScalar().ToString();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stats: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // menu - Books
        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBooks books = new frmBooks();
            books.Show();
        }

        // menu - Make Transactions (Issue/Return Book)
        private void issueBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTransaction transaction = new frmTransaction();
            transaction.Show();
        }

        // menu - Fines
        private void finesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFines fines = new frmFines();
            fines.Show();
        }

        // menu - Logout
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}

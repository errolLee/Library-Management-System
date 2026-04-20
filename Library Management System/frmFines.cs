using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Library_Management_System
{
    // This form is for Librarians to view fines
    // Design: create a new Windows Form named frmFines
    // Add these controls:
    //   - DataGridView: dgFines
    //   - Button: btnRefresh
    //   - Label: lblTotalFines

    public partial class frmFines : Form
    {
        public frmFines()
        {
            InitializeComponent();
        }

        private void frmFines_Load(object sender, EventArgs e)
        {
            LoadFines();
        }

        private void LoadFines()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                // get all overdue transactions and calculate fine (10 pesos per day)
                string query = @"SELECT u.fullname AS Borrower, b.title AS Book,
                                 t.borrow_date AS BorrowDate,
                                 t.due_date AS DueDate,
                                 DATEDIFF(CURDATE(), t.due_date) AS DaysOverdue,
                                 DATEDIFF(CURDATE(), t.due_date) * 10 AS Fine
                                 FROM transactions t
                                 JOIN users u ON t.user_id = u.id
                                 JOIN books b ON t.book_id = b.book_id
                                 WHERE t.due_date < CURDATE() AND t.return_date IS NULL
                                 ORDER BY t.due_date ASC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgFines.DataSource = dt;

                // calculate total fines
                MySqlCommand totalCmd = new MySqlCommand(
                    @"SELECT IFNULL(SUM(DATEDIFF(CURDATE(), due_date) * 10), 0)
                      FROM transactions 
                      WHERE due_date < CURDATE() AND return_date IS NULL", conn);

                decimal totalFines = Convert.ToDecimal(totalCmd.ExecuteScalar());
                lblTotalFines.Text = "Total Fines: ₱" + totalFines.ToString("N2");

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading fines: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFines();
        }
    }
}

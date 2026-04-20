using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Library_Management_System
{
  

    public partial class frmTransaction : Form
    {
        public frmTransaction()
        {
            InitializeComponent();
        }

        private void frmTransaction_Load(object sender, EventArgs e)
        {
            LoadActiveTransactions();
            LoadAvailableBooks();
            SetupBorrowerAutocomplete();
        }

        private void LoadAvailableBooks()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                string query = "SELECT book_id, title, author, quantity FROM books WHERE quantity > 0";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgBooks.DataSource = dt;
                dgBooks.Columns["book_id"].Visible = false;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadActiveTransactions()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                string query = @"SELECT t.transaction_id, u.fullname, b.title, 
                                 t.borrow_date, t.due_date
                                 FROM transactions t
                                 JOIN users u ON t.user_id = u.id
                                 JOIN books b ON t.book_id = b.book_id
                                 WHERE t.return_date IS NULL
                                 ORDER BY t.borrow_date DESC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgTransactions.DataSource = dt;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            // check if a book is selected
            if (dgBooks.CurrentRow == null)
            {
                MessageBox.Show("Please select a book to issue.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtBorrower.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the borrower's username.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(dgBooks.CurrentRow.Cells["book_id"].Value);
            string bookTitle = dgBooks.CurrentRow.Cells["title"].Value.ToString();
            string borrowerUsername = txtBorrower.Text.Trim();

            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                // get borrower's user id
                MySqlCommand findUser = new MySqlCommand(
                        "SELECT id FROM users WHERE (username=@input OR fullname=@input) AND role='Borrower'", conn);

                findUser.Parameters.AddWithValue("@input", borrowerUsername);
                object result = findUser.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Borrower username not found.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    conn.Close();
                    return;
                }

                int borrowerId = Convert.ToInt32(result);
                DateTime borrowDate = DateTime.Today;
                DateTime dueDate = DateTime.Today.AddDays(7); // 7 days to return

                // insert transaction
                string insertQuery = @"INSERT INTO transactions (user_id, book_id, borrow_date, due_date) 
                                       VALUES (@uid, @bid, @borrow, @due)";
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@uid", borrowerId);
                insertCmd.Parameters.AddWithValue("@bid", bookId);
                insertCmd.Parameters.AddWithValue("@borrow", borrowDate);
                insertCmd.Parameters.AddWithValue("@due", dueDate);
                insertCmd.ExecuteNonQuery();

                // decrease book quantity
                MySqlCommand updateBook = new MySqlCommand(
                    "UPDATE books SET quantity = quantity - 1 WHERE book_id=@id", conn);
                updateBook.Parameters.AddWithValue("@id", bookId);
                updateBook.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Book '" + bookTitle + "' issued to " + borrowerUsername + "!\nDue date: " + dueDate.ToShortDateString(),
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtBorrower.Clear();
                LoadAvailableBooks();
                LoadActiveTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error issuing book: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            // select from the active transactions grid
            if (dgTransactions.CurrentRow == null)
            {
                MessageBox.Show("Please select a transaction to return.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int transactionId = Convert.ToInt32(dgTransactions.CurrentRow.Cells["transaction_id"].Value);
            string borrowerName = dgTransactions.CurrentRow.Cells["fullname"].Value.ToString();
            string bookTitle = dgTransactions.CurrentRow.Cells["title"].Value.ToString();

            DialogResult confirm = MessageBox.Show(
                "Return '" + bookTitle + "' from " + borrowerName + "?",
                "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    MySqlConnection conn = DBConnection.GetConnection();
                    conn.Open();

                    // get book_id from transaction
                    MySqlCommand getBook = new MySqlCommand(
                        "SELECT book_id FROM transactions WHERE transaction_id=@id", conn);
                    getBook.Parameters.AddWithValue("@id", transactionId);
                    int bookId = Convert.ToInt32(getBook.ExecuteScalar());

                    // set return date
                    MySqlCommand returnCmd = new MySqlCommand(
                        "UPDATE transactions SET return_date=CURDATE() WHERE transaction_id=@id", conn);
                    returnCmd.Parameters.AddWithValue("@id", transactionId);
                    returnCmd.ExecuteNonQuery();

                    // increase book quantity back
                    MySqlCommand updateBook = new MySqlCommand(
                        "UPDATE books SET quantity = quantity + 1 WHERE book_id=@id", conn);
                    updateBook.Parameters.AddWithValue("@id", bookId);
                    updateBook.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Book returned successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAvailableBooks();
                    LoadActiveTransactions();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error returning book: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBorrower_TextChanged(object sender, EventArgs e)
        {

        }
        private void SetupBorrowerAutocomplete()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                // Get names of all users who are 'Borrower'
                string query = "SELECT fullname FROM users WHERE role='Borrower'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                AutoCompleteStringCollection source = new AutoCompleteStringCollection();

                while (reader.Read())
                {
                    source.Add(reader["fullname"].ToString());
                }

                // Apply to your TextBox
                txtBorrower.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtBorrower.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtBorrower.AutoCompleteCustomSource = source;

                conn.Close();
            }
            catch (Exception ex)
            {
                // Don't crash the form if autocomplete fails, just log it
                Console.WriteLine("Autocomplete Error: " + ex.Message);
            }
        }
    }
}

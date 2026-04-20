using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class frmBooks : Form
    {
        public frmBooks()
        {
            InitializeComponent();
        }

        private void frmBooks_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                string query = "SELECT book_id, title, author, genre, quantity FROM books";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgBooks.DataSource = dt;

                // rename column headers
                dgBooks.Columns["book_id"].HeaderText = "ID";
                dgBooks.Columns["title"].HeaderText = "Title";
                dgBooks.Columns["author"].HeaderText = "Author";
                dgBooks.Columns["genre"].HeaderText = "Genre";
                dgBooks.Columns["quantity"].HeaderText = "Quantity";

                dgBooks.Columns["book_id"].Visible = false;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // ask for book details using InputBox
            string title = Microsoft.VisualBasic.Interaction.InputBox("Enter book title:", "Add Book", "");
            if (title.Trim() == "") return;

            string author = Microsoft.VisualBasic.Interaction.InputBox("Enter author:", "Add Book", "");
            if (author.Trim() == "") return;

            string genre = Microsoft.VisualBasic.Interaction.InputBox("Enter genre:", "Add Book", "");
            if (genre.Trim() == "") return;

            string qtyStr = Microsoft.VisualBasic.Interaction.InputBox("Enter quantity:", "Add Book", "1");
            int quantity;
            if (!int.TryParse(qtyStr, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                string insertQuery = "INSERT INTO books (title, author, genre, quantity) VALUES (@title, @author, @genre, @qty)";
                MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@genre", genre);
                cmd.Parameters.AddWithValue("@qty", quantity);
                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Book added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding book: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgBooks.CurrentRow == null)
            {
                MessageBox.Show("Please select a book to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(dgBooks.CurrentRow.Cells["book_id"].Value);
            string currentTitle = dgBooks.CurrentRow.Cells["title"].Value.ToString();
            string currentAuthor = dgBooks.CurrentRow.Cells["author"].Value.ToString();
            string currentGenre = dgBooks.CurrentRow.Cells["genre"].Value.ToString();
            string currentQty = dgBooks.CurrentRow.Cells["quantity"].Value.ToString();

            string title = Microsoft.VisualBasic.Interaction.InputBox("Edit title:", "Edit Book", currentTitle);
            if (title.Trim() == "") return;

            string author = Microsoft.VisualBasic.Interaction.InputBox("Edit author:", "Edit Book", currentAuthor);
            if (author.Trim() == "") return;

            string genre = Microsoft.VisualBasic.Interaction.InputBox("Edit genre:", "Edit Book", currentGenre);
            if (genre.Trim() == "") return;

            string qtyStr = Microsoft.VisualBasic.Interaction.InputBox("Edit quantity:", "Edit Book", currentQty);
            int quantity;
            if (!int.TryParse(qtyStr, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MySqlConnection conn = DBConnection.GetConnection();
                conn.Open();

                string updateQuery = "UPDATE books SET title=@title, author=@author, genre=@genre, quantity=@qty WHERE book_id=@id";
                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@genre", genre);
                cmd.Parameters.AddWithValue("@qty", quantity);
                cmd.Parameters.AddWithValue("@id", bookId);
                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Book updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating book: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgBooks.CurrentRow == null)
            {
                MessageBox.Show("Please select a book to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(dgBooks.CurrentRow.Cells["book_id"].Value);
            string title = dgBooks.CurrentRow.Cells["title"].Value.ToString();

            DialogResult result = MessageBox.Show("Are you sure you want to delete '" + title + "'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    MySqlConnection conn = DBConnection.GetConnection();
                    conn.Open();

                    string deleteQuery = "DELETE FROM books WHERE book_id=@id";
                    MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@id", bookId);
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Book deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting book: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT book_id, title, author, quantity
                             FROM books
                             WHERE title LIKE @keyword 
                                OR author LIKE @keyword
                             ORDER BY title ASC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@keyword", keyword + "%");
                    // starts with (N, nole, etc.)

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgBooks.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

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

        // LOAD ALL BOOKS ====
        private void LoadBooks()
        {
            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT book_id, title, author, genre, quantity FROM books";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgBooks.DataSource = dt;

                    FormatGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message);
            }
        }

        // FORMAT GRID ====
        private void FormatGrid()
        {
            if (dgBooks.Columns.Contains("book_id"))
            {
                dgBooks.Columns["book_id"].Visible = false;
            }

            if (dgBooks.Columns.Contains("book_id"))
                dgBooks.Columns["book_id"].HeaderText = "ID";

            if (dgBooks.Columns.Contains("title"))
                dgBooks.Columns["title"].HeaderText = "Title";

            if (dgBooks.Columns.Contains("author"))
                dgBooks.Columns["author"].HeaderText = "Author";

            if (dgBooks.Columns.Contains("genre"))
                dgBooks.Columns["genre"].HeaderText = "Genre";

            if (dgBooks.Columns.Contains("quantity"))
                dgBooks.Columns["quantity"].HeaderText = "Quantity";
        }

        // ADD BOOK====
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = Microsoft.VisualBasic.Interaction.InputBox("Enter book title:", "Add Book", "");
            if (title.Trim() == "") return;

            string author = Microsoft.VisualBasic.Interaction.InputBox("Enter author:", "Add Book", "");
            if (author.Trim() == "") return;

            string genre = Microsoft.VisualBasic.Interaction.InputBox("Enter genre:", "Add Book", "");
            if (genre.Trim() == "") return;

            string qtyStr = Microsoft.VisualBasic.Interaction.InputBox("Enter quantity:", "Add Book", "1");

            if (!int.TryParse(qtyStr, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO books (title, author, genre, quantity)
                                     VALUES (@title, @author, @genre, @qty)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@author", author);
                    cmd.Parameters.AddWithValue("@genre", genre);
                    cmd.Parameters.AddWithValue("@qty", quantity);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Book added successfully!");
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding book: " + ex.Message);
            }
        }

        //  EDIT BOOK===
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgBooks.CurrentRow == null) return;

            int id = Convert.ToInt32(dgBooks.CurrentRow.Cells["book_id"].Value);

            string title = dgBooks.CurrentRow.Cells["title"].Value.ToString();
            string author = dgBooks.CurrentRow.Cells["author"].Value.ToString();
            string genre = dgBooks.CurrentRow.Cells["genre"].Value.ToString();
            string qty = dgBooks.CurrentRow.Cells["quantity"].Value.ToString();

            string newTitle = Microsoft.VisualBasic.Interaction.InputBox("Edit title:", "Edit Book", title);
            if (newTitle.Trim() == "") return;

            string newAuthor = Microsoft.VisualBasic.Interaction.InputBox("Edit author:", "Edit Book", author);
            if (newAuthor.Trim() == "") return;

            string newGenre = Microsoft.VisualBasic.Interaction.InputBox("Edit genre:", "Edit Book", genre);
            if (newGenre.Trim() == "") return;

            string qtyStr = Microsoft.VisualBasic.Interaction.InputBox("Edit quantity:", "Edit Book", qty);

            if (!int.TryParse(qtyStr, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"UPDATE books 
                                     SET title=@title, author=@author, genre=@genre, quantity=@qty 
                                     WHERE book_id=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@title", newTitle);
                    cmd.Parameters.AddWithValue("@author", newAuthor);
                    cmd.Parameters.AddWithValue("@genre", newGenre);
                    cmd.Parameters.AddWithValue("@qty", quantity);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Book updated successfully!");
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating book: " + ex.Message);
            }
        }

        // DELETE BOOK ===
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgBooks.CurrentRow == null) return;

            int id = Convert.ToInt32(dgBooks.CurrentRow.Cells["book_id"].Value);
            string title = dgBooks.CurrentRow.Cells["title"].Value.ToString();

            if (MessageBox.Show("Delete " + title + "?", "Confirm",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                using (MySqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string query = "DELETE FROM books WHERE book_id=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Book deleted successfully!");
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting book: " + ex.Message);
            }
        }

        // SEARCH ==
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

                    string query = @"SELECT book_id, title, author, genre, quantity
                                     FROM books
                                     WHERE title LIKE @keyword 
                                        OR author LIKE @keyword
                                        OR genre LIKE @keyword
                                     ORDER BY title ASC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgBooks.DataSource = dt;

                    FormatGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }
    }
}
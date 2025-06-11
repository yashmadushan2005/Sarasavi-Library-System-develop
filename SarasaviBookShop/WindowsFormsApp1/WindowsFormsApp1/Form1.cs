using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SarasaviLibrarySystem
{
    public partial class FormInquiry : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True;";

        public FormInquiry()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a book title or author name.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        B.Title,
                        B.Author,
                        C.CopyID,
                        CASE WHEN C.IsReference = 1 THEN 'Yes' ELSE 'No' END AS ReferenceCopy,
                        CASE WHEN C.IsBorrowed = 1 THEN 'Yes' ELSE 'No' END AS Borrowed,
                        CASE WHEN C.IsReserved = 1 THEN 'Yes' ELSE 'No' END AS Reserved
                    FROM Books B
                    INNER JOIN Copies C ON B.BookID = C.BookID
                    WHERE B.Title LIKE @search OR B.Author LIKE @search";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + keyword + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvResults.DataSource = dt;
                }
            }
        }
    }
}

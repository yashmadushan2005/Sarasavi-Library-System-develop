using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SarasaviLibrarySystem
{
    public partial class FormInquiry : Form
    {
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

            try
            {
                string connectionString = @"Data Source = (localdb)\ProjectModels;Initial Catalog = Sarasavi; Integrated Security = True;";
                string qry = "select * from Books where Title = @keyword";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmnd = new SqlCommand(qry, conn);
                    cmnd.Parameters.AddWithValue("@keyword", keyword);

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmnd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvResults.DataSource = dt;
                    conn.Close();

                    //SqlDataReader rdr = cmnd.ExecuteReader();
                    //while (rdr.Read())
                    //{
                    //    title = rdr[1].ToString();
                    //    author = rdr[2].ToString();
                    //    publisher = rdr[3].ToString();
                    //    isbn = rdr[4].ToString();

                    //    MessageBox.Show("Title : " +title+ ", Author" + author + ", Publisher" + publisher + ", Isbn" + isbn);

                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}

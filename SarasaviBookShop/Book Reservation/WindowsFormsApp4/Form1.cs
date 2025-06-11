using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnReservationSearch_Click(object sender, EventArgs e)
        {
            string accessionNo = txtAccessionNo.Text.Trim();
            if (string.IsNullOrEmpty(accessionNo))
            {
                lblReservationStatus.Text = "Please enter Accession No.";
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Accession No");
            dt.Columns.Add("Title");
            dt.Columns.Add("Status");
            dt.Rows.Add(accessionNo, "Sample Book Title", "Available");

            dgvReservationResults.DataSource = dt;
            lblReservationStatus.Text = "Book availability shown below.";
        }

        private void btnConfirmReservation_Click(object sender, EventArgs e)
        {
            string borrowerId = txtBorrowerId.Text.Trim();
            if (string.IsNullOrEmpty(borrowerId))
            {
                lblReservationStatus.Text = "Please enter Borrower ID.";
                return;
            }

            if (dgvReservationResults.Rows.Count == 0)
            {
                lblReservationStatus.Text = "No book selected for reservation.";
                return;
            }

            lblReservationStatus.ForeColor = System.Drawing.Color.Green;
            lblReservationStatus.Text = $"Book reserved successfully for Borrower ID: {borrowerId}";
        }
    }
}

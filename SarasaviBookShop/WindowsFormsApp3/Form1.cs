using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {

        private ClassificationTracker tracker = new ClassificationTracker();
        public Form1()
        {
            InitializeComponent();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string classification = txtClassification.Text.Trim().ToUpper();
            if (classification.Length != 1)
            {
                MessageBox.Show("Classification must be a single character.");
                return;
            }

            int number = tracker.GetNextNumber(classification);
            Book newBook = new Book()
            {
                Classification = classification,
                Number = number,
                Title = txtTitle.Text.Trim(),
                Publisher = txtPublisher.Text.Trim(),
                IsReference = chkReference.Checked,
                Copies = (int)numCopies.Value
            };

            List<string> bookNumbers = newBook.GenerateBookNumbers();
            lstBookNumbers.Items.Clear();
            foreach (var num in bookNumbers)
            {
                lstBookNumbers.Items.Add(num);
            }
        }
    }
}


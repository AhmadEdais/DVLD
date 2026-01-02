using ConsoleApp1;
using DVLD_Project.Test_Appointments;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.License
{
    public partial class frmListDetainedLicenses : Form
    {
        DataTable _dtDetainedLicenses;
        DataView _dvDetainedLicenses;
        int _selectedRowIndex = -1;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        private void _HideFilterControls()
        {
            txtFilterByValue.Visible = false;
            cbIsReleased.Visible = false;
        }
        private void _StartUpSettings()
        {
            cbFilterBy.SelectedItem = "None";


            _HideFilterControls();
        }
        private void _RefreshDetainedLicensesList()
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            _dvDetainedLicenses = _dtDetainedLicenses.DefaultView;
            dgvAllDetainedicenses.DataSource = _dvDetainedLicenses;
            lblRecordsNumber.Text = dgvAllDetainedicenses.Rows.Count.ToString();    
        }
        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshDetainedLicensesList();
            _StartUpSettings();
            if(dgvAllDetainedicenses.Rows.Count > 0)
            {
                dgvAllDetainedicenses.Columns["D.ID"].Width = 50;
                dgvAllDetainedicenses.Columns["L.ID"].Width = 50;
                dgvAllDetainedicenses.Columns["D.Date"].Width = 150;
                dgvAllDetainedicenses.Columns["Is Released"].Width = 120;
                dgvAllDetainedicenses.Columns["Fine Fees"].Width = 120;
                dgvAllDetainedicenses.Columns["Release Date"].Width = 150;
                dgvAllDetainedicenses.Columns["N.No."].Width = 80;
                dgvAllDetainedicenses.Columns["Full Name"].Width = 230;
                dgvAllDetainedicenses.Columns["Release App.ID"].Width = 120;


            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Handle "Is Released" Selection
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterByValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0; // Default to "All"
            }
            else
            {
                // 2. Handle Standard Filters
                cbIsReleased.Visible = false;
                txtFilterByValue.Visible = (cbFilterBy.Text != "None");

                if (cbFilterBy.Text == "None")
                {
                    
                    _dvDetainedLicenses.RowFilter = "";
                    lblRecordsNumber.Text = dgvAllDetainedicenses.Rows.Count.ToString();
                }
                else
                {
                    txtFilterByValue.Enabled = true;
                    txtFilterByValue.Text = "";
                    txtFilterByValue.Focus();
                }
            }
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Is Released";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    _dvDetainedLicenses.RowFilter = "";
                    break;
                case "Yes":
                    // "1" means True in SQL/DataView
                    _dvDetainedLicenses.RowFilter = string.Format("[{0}] = 1", FilterColumn);
                    break;
                case "No":
                    // "0" means False in SQL/DataView
                    _dvDetainedLicenses.RowFilter = string.Format("[{0}] = 0", FilterColumn);
                    break;
            }
            lblRecordsNumber.Text = dgvAllDetainedicenses.Rows.Count.ToString();
        }

        private void txtFilterByValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent typing letters if a Numeric Column is selected
            if (cbFilterBy.Text == "D.ID" || cbFilterBy.Text == "L.ID" || cbFilterBy.Text == "Release App.ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            // Map Selection to the exact Column Alias in your Query
            switch (cbFilterBy.Text)
            {
                case "D.ID":
                    FilterColumn = "D.ID";
                    break;

                case "L.ID":
                    FilterColumn = "L.ID";
                    break;

                case "N.No.":
                    FilterColumn = "N.No.";
                    break;

                case "Full Name":
                    FilterColumn = "Full Name";
                    break;

                case "Release App.ID":
                    FilterColumn = "Release App.ID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            // Reset if empty
            if (txtFilterByValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dvDetainedLicenses.RowFilter = "";
                lblRecordsNumber.Text = dgvAllDetainedicenses.Rows.Count.ToString();
                return;
            }

            // Apply Filter
            if (FilterColumn == "D.ID" || FilterColumn == "L.ID" || FilterColumn == "Release App.ID")
            {
                // Numeric Filter (Exact Match)
                if (int.TryParse(txtFilterByValue.Text.Trim(), out int id))
                {
                    _dvDetainedLicenses.RowFilter = string.Format("[{0}] = {1}", FilterColumn, id);
                }
                else
                {
                    _dvDetainedLicenses.RowFilter = ""; // Clear if invalid number input
                }
            }
            else
            {
                // String Filter (LIKE) - For Name and National No.
                _dvDetainedLicenses.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterByValue.Text.Trim());
            }

            lblRecordsNumber.Text = dgvAllDetainedicenses.Rows.Count.ToString();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicenses();
            frm.ShowDialog();
            _RefreshDetainedLicensesList();

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicense();
            frm.ShowDialog();
            _RefreshDetainedLicensesList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAllDetainedicenses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvAllDetainedicenses.ClearSelection();
                dgvAllDetainedicenses.Rows[e.RowIndex].Selected = true;
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                string No = (string)dgvAllDetainedicenses.Rows[_selectedRowIndex].Cells["N.No."].Value;
                int PersonID = clsPerson.GetPersonIDByNationalNo(No);
                // 3. Send just the ID to your form
                Form frm = new frmPersonDetails(PersonID);
                frm.ShowDialog();
                _RefreshDetainedLicensesList();
            }
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LicenseID = (int)dgvAllDetainedicenses.Rows[_selectedRowIndex].Cells["L.ID"].Value;
                // 3. Send just the ID to your form
                Form frm = new frmShowLicense(LicenseID);
                frm.ShowDialog();
                _RefreshDetainedLicensesList();
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LicenseID = (int)dgvAllDetainedicenses.Rows[_selectedRowIndex].Cells["L.ID"].Value;
                int DriverID = clsLicense.Find(LicenseID).DriverID;
                // 3. Send just the ID to your form
                Form frm = new frmLicenseHistory(DriverID);
                frm.ShowDialog();
                _RefreshDetainedLicensesList();
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LicenseID = (int)dgvAllDetainedicenses.Rows[_selectedRowIndex].Cells["L.ID"].Value;
                // 3. Send just the ID to your form
                Form frm = new frmReleaseDetainedLicenses(LicenseID);
                frm.ShowDialog();
                _RefreshDetainedLicensesList();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                bool IsReleased = (bool)dgvAllDetainedicenses.Rows[_selectedRowIndex].Cells["Is Released"].Value;
                releaseDetainedLicenseToolStripMenuItem.Enabled = !IsReleased;
            }
        }
    }
}

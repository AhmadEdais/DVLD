using ConsoleApp1;
using DVLD_Project.Local_Driving_Licenses;
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
    public partial class frmListInternationalLicenses : Form
    {
        private int _selectedRowIndex = -1;
        DataTable _dtAllIntApplications;
        DataView _dvIntApplications;
        public frmListInternationalLicenses()
        {
            InitializeComponent();
        }
        private void _HideFilterControls()
        {
            txtFilterByValue.Visible = false;
            cbStatus.Visible = false;
        }
        private void _StartUpSettings()
        {
            cbFilterBy.SelectedItem = "None";


            _HideFilterControls();
        }
        private void _RefreshIntApplications()
        {
            _dtAllIntApplications = clsInternationalLicense.GetAllInternationalLicenses();

            // Create a DataView from the DataTable
            _dvIntApplications = _dtAllIntApplications.DefaultView;

            // Bind the DataView to the grid (not the DataTable directly)
            dgvAllIntApplications.DataSource = _dvIntApplications;

            lblRecordsNumber.Text = dgvAllIntApplications.Rows.Count.ToString();

        }
        private void _AddEditIntApplication()
        {

            Form frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            _RefreshIntApplications();
        }
        private void _SetWidth()
        {
            if (dgvAllIntApplications.Columns.Count > 0)
            {

                // ID Column: Increased slightly to ensure the header "L.D.LAppID" fits comfortably
                dgvAllIntApplications.Columns["Int.License ID"].Width = 150;

                // Driving Class: Increased to 280 to fit long names like "Class 7 - Truck and heavy vehicle"
                dgvAllIntApplications.Columns["Application ID"].Width = 150;

                // National No: 90 is standard for this short text
                dgvAllIntApplications.Columns["Driver ID"].Width = 120;

                // Full Name: Increased to 350 because names in your region are often 4 parts long
                dgvAllIntApplications.Columns["L.License ID"].Width = 150;

                // Application Date: 170 fits the "dd-MMM-yy hh:mm tt" format perfectly
                dgvAllIntApplications.Columns["Issue Date"].Width = 170;

                // Passed Tests: Increased to 100 so the header "Passed Tests" doesn't wrap or get cut off
                dgvAllIntApplications.Columns["Expiration Date"].Width = 170;

                // Status: 100 is plenty for "New", "Cancelled", or "Completed"
                dgvAllIntApplications.Columns["Is Active"].Width = 130;

            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListInternationalLicenses_Load(object sender, EventArgs e)
        {
            _StartUpSettings();
            _RefreshIntApplications();
            _SetWidth();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RefreshIntApplications();
            txtFilterByValue.Text = "";
            if (cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                txtFilterByValue.Visible = false;
                cbStatus.Visible = true;
                cbStatus.SelectedItem = "Active";
            }
            else if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                _HideFilterControls();
            }

            else
            {
                txtFilterByValue.Visible = true;
                cbStatus.Visible = false;

            }

        }

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = cbFilterBy.Text;
            string FilterValue = txtFilterByValue.Text.Trim();

            // 1. Reset Logic: If filter is empty or "None" is selected
            if (FilterValue == "" || FilterColumn == "None")
            {
                _dvIntApplications.RowFilter = "";
                lblRecordsNumber.Text = dgvAllIntApplications.Rows.Count.ToString();
                return;
            }

            // 2. Determine if the selected column is a Number (ID) or Text
            // Based on your specific query headers:
            // [Int.License ID], [Application ID], [Driver ID], [L.License ID] are all Numbers.

            if (FilterColumn == "Int.License ID" || FilterColumn == "Application ID" ||
                FilterColumn == "Driver ID" || FilterColumn == "L.License ID")
            {
                // --- Numeric Filter Logic ---
                if (int.TryParse(FilterValue, out int id))
                {
                    // Exact match for numbers: [ColumnName] = 123
                    _dvIntApplications.RowFilter = string.Format("[{0}] = {1}", FilterColumn, id);
                }
                else
                {
                    // If user types text into a number field, clear filter (or show nothing)
                    _dvIntApplications.RowFilter = "";
                }
            }
            else
            {
                // --- String Filter Logic (e.g. for "Is Active" or other text columns) ---
                // Uses LIKE operator: [ColumnName] LIKE 'Value%'
                _dvIntApplications.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, FilterValue);
            }

            // 3. Update Record Count Label
            lblRecordsNumber.Text = dgvAllIntApplications.Rows.Count.ToString();
        }

        private void txtFilterByValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string FilterColumn = cbFilterBy.Text;

            if (FilterColumn == "Int.License ID" || FilterColumn == "Application ID" ||
                FilterColumn == "Driver ID" || FilterColumn == "L.License ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            }
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Is Active";
            string FilterValue = cbStatus.Text;

            switch (FilterValue)
            {
                case "All":
                    _dvIntApplications.RowFilter = "";
                    break;

                case "Active":
                    // Filter where Status column equals the word 'New'
                    _dvIntApplications.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, true);
                    break;

                case "InActive":
                    // Filter where Status column equals the word 'Cancelled'
                    _dvIntApplications.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, false);
                    break;
            }

            lblRecordsNumber.Text = dgvAllIntApplications.Rows.Count.ToString();
        }

        private void dgvAllIntApplications_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvAllIntApplications.ClearSelection();
                dgvAllIntApplications.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btnAddNewInternationalApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            _RefreshIntApplications();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int AppID = (int)dgvAllIntApplications.Rows[_selectedRowIndex].Cells["Application ID"].Value;
                clsApplication app = clsApplication.FindBaseApplication(AppID);
                Form frm = new frmPersonDetails(app.ApplicantPersonID);
                frm.ShowDialog();
                _RefreshIntApplications();
            }
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int IntLicenseID = (int)dgvAllIntApplications.Rows[_selectedRowIndex].Cells["Int.License ID"].Value;
                Form frm = new frmShowInternationalLicense(IntLicenseID);
                frm.ShowDialog();
                _RefreshIntApplications();
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int DriverID = (int)dgvAllIntApplications.Rows[_selectedRowIndex].Cells["Driver ID"].Value;
                Form frm = new frmLicenseHistory(DriverID);
                frm.ShowDialog();
                _RefreshIntApplications();
            }
        }
    }
}

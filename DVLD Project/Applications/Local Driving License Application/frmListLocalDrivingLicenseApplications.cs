using ConsoleApp1;
using DVLD_Buisness;
using DVLD_Project.License;
using DVLD_Project.Test_Appointments;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Local_Driving_Licenses
{

    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        private int _selectedRowIndex = -1;
        DataTable _dtAllLDLApplications;
        DataView _dvLDLApplications;
   
        public frmListLocalDrivingLicenseApplications()
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
        private void _RefreshLDLApplications()
        {
            _dtAllLDLApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            // Create a DataView from the DataTable
            _dvLDLApplications = _dtAllLDLApplications.DefaultView;

            // Bind the DataView to the grid (not the DataTable directly)
            dgvAllLDLApplications.DataSource = _dvLDLApplications;

            lblRecordsNumber.Text = dgvAllLDLApplications.Rows.Count.ToString();

        }
        private void _AddEditLDLApplication(int LDLApplicationID = -1)
        {

            Form frm = new frmAddLocalDrivingLicense(LDLApplicationID);
            frm.ShowDialog();
            _RefreshLDLApplications();
        }
        private void _DisableContextMenuItemsBasedOnStatus(int LDLAppID,int ClassID,string status)
        {
            // Example logic to disable/enable menu items based on status
            if (status == "Cancelled" || status == "Completed")
            {
                editApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                sechduleTestsToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                return;
            }

            // if has a License issued

            if (clsLicense.IsLicenseExistByLocalDrivingLicenseApplicationID(LDLAppID,ClassID))
            {

                sechduleTestsToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                return;
            }
            showLicenseToolStripMenuItem.Enabled = false;
            int TestPassed = clsLocalDrivingLicenseApplication.GetPassedTestCount(LDLAppID);
            //if passed street test
            if (TestPassed == 3)
            {
                sechduleTestsToolStripMenuItem.Enabled = false;
                return;
            }
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            //if passed written test
            if (TestPassed == 2)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                return;
            }
            //if passed vision test
            if(TestPassed == 1)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                return;
            }
            //if no tests passed

            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            scheduleStreetTestToolStripMenuItem.Enabled = false;
            return;
           

        }
        private void _EnableAllContextMenuItems()
        {
            editApplicationToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = true;
            sechduleTestsToolStripMenuItem.Enabled = true;
            scheduleVisionTestToolStripMenuItem.Enabled = true;
            scheduleWrittenTestToolStripMenuItem.Enabled = true;
            scheduleStreetTestToolStripMenuItem.Enabled = true;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = true;
        }
        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _SetWidth()
        {
            if(dgvAllLDLApplications.Columns.Count > 0)
            {
                
                // ID Column: Increased slightly to ensure the header "L.D.LAppID" fits comfortably
                dgvAllLDLApplications.Columns["L.D.LAppID"].Width = 120;

                // Driving Class: Increased to 280 to fit long names like "Class 7 - Truck and heavy vehicle"
                dgvAllLDLApplications.Columns["Driving Class"].Width = 280;

                // National No: 90 is standard for this short text
                dgvAllLDLApplications.Columns["National No"].Width = 120;

                // Full Name: Increased to 350 because names in your region are often 4 parts long
                dgvAllLDLApplications.Columns["Full Name"].Width = 300;

                // Application Date: 170 fits the "dd-MMM-yy hh:mm tt" format perfectly
                dgvAllLDLApplications.Columns["Application Date"].Width = 170;

                // Passed Tests: Increased to 100 so the header "Passed Tests" doesn't wrap or get cut off
                dgvAllLDLApplications.Columns["Passed Tests"].Width = 130;

                    // Status: 100 is plenty for "New", "Cancelled", or "Completed"
                    dgvAllLDLApplications.Columns["Status"].Width = 100;
              
            }
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _StartUpSettings();
            _RefreshLDLApplications();
            _SetWidth();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RefreshLDLApplications();
            txtFilterByValue.Text = "";
            if (cbFilterBy.SelectedItem.ToString() == "Status")
            {
                txtFilterByValue.Visible = false;
                cbStatus.Visible = true;
                cbStatus.SelectedItem = "New";
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

        private void dgvAllLDLApplications_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvAllLDLApplications.ClearSelection();
                dgvAllLDLApplications.Rows[e.RowIndex].Selected = true;
            }
        }

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "L.D.L AppID":  // Matches your screenshot (Space after L)
                    FilterColumn = "L.D.LAppID"; // Matches SQL Column Alias
                    break;

                case "NationalNO":   // Matches your screenshot (No space, Capital NO)
                    FilterColumn = "National No"; // Matches SQL Column Alias
                    break;

                case "FullName":     // Matches your screenshot (No space)
                    FilterColumn = "Full Name";   // Matches SQL Column Alias
                    break;

                case "Status":       // Matches your screenshot
                    FilterColumn = "Status";      // Matches SQL Column Alias
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            // Reset Logic
            if (txtFilterByValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dvLDLApplications.RowFilter = "";
                lblRecordsNumber.Text = dgvAllLDLApplications.Rows.Count.ToString();
                return;
            }

            // Apply Filter
            if (FilterColumn == "L.D.LAppID")
            {
                // Numeric Filter Safety Check
                if (int.TryParse(txtFilterByValue.Text.Trim(), out int id))
                {
                    _dvLDLApplications.RowFilter = string.Format("[{0}] = {1}", FilterColumn, id);
                }
                else
                {
                    _dvLDLApplications.RowFilter = ""; // Clear filter if invalid input
                }
            }
            else
            {
                // String Filter
                _dvLDLApplications.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterByValue.Text.Trim());
            }

            lblRecordsNumber.Text = dgvAllLDLApplications.Rows.Count.ToString();
        }

        private void txtFilterByValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // We only allow numbers if "User ID" or "Person ID" is selected
            if (cbFilterBy.Text == "L.D.LAppID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnAddNewLDLApplication_Click(object sender, EventArgs e)
        {
            _AddEditLDLApplication();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Status";
            string FilterValue = cbStatus.Text;

            switch (FilterValue)
            {
                case "All":
                    _dvLDLApplications.RowFilter = "";
                    break;

                case "New":
                    // Filter where Status column equals the word 'New'
                    _dvLDLApplications.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, FilterValue);
                    break;

                case "Cancelled":
                    // Filter where Status column equals the word 'Cancelled'
                    _dvLDLApplications.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, FilterValue);
                    break;

                case "Completed":
                    // Filter where Status column equals the word 'Completed'
                    _dvLDLApplications.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, FilterValue);
                    break;
            }

            lblRecordsNumber.Text = dgvAllLDLApplications.Rows.Count.ToString();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;

                // 3. Send just the ID to your form
                clsLocalDrivingLicenseApplication clsLocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                clsLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(LDLAppID);
                if(MessageBox.Show("Are you sure you want to cancel this application?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    clsLocalDrivingLicenseApplication.Cancel();
                _RefreshLDLApplications();
            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;

                // 3. Send just the ID to your form
                if (MessageBox.Show("Are you sure you want to Delete this application?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    clsLocalDrivingLicenseApplication.Delete(LDLAppID);
                _RefreshLDLApplications();
            }

        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;

                // 3. Send just the ID to your form
                _AddEditLDLApplication(LDLAppID);
                _RefreshLDLApplications();
            }
        }

        private void showApplicaionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;

                // 3. Send just the ID to your form
                Form frm = new frmShowLDLApplicationInfo(LDLAppID);
                frm.ShowDialog();
                _RefreshLDLApplications();
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;

                // 3. Send just the ID to your form
                Form frm = new frmTestAppointments(LDLAppID, clsTestTypes.enTestTypeID.Vision);
                frm.ShowDialog();
                _RefreshLDLApplications();
            }

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;

                // 3. Send just the ID to your form
                Form frm = new frmTestAppointments(LDLAppID, clsTestTypes.enTestTypeID.Written);
                frm.ShowDialog();
                _RefreshLDLApplications();
            }
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;
               
                // 3. Send just the ID to your form
                Form frm = new frmTestAppointments(LDLAppID, clsTestTypes.enTestTypeID.Road);
                frm.ShowDialog();
                _RefreshLDLApplications();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;
                int ClassID = clsLicenseClass.GetIDByClassName(dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["Driving Class"].Value.ToString());
                _DisableContextMenuItemsBasedOnStatus(LDLAppID, ClassID, dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["Status"].Value.ToString());
            }
        }

        private void contextMenuStrip1_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {

            _EnableAllContextMenuItems();

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;
                Form frm = new frmIssueLicense(LDLAppID);
                frm.ShowDialog();
                _RefreshLDLApplications();
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;

                Form frm = new frmShowLicense(clsLocalDrivingLicenseApplication.GetLicenseIDByLocalDrivingLicenseAppID(LDLAppID));
                frm.ShowDialog();
                _RefreshLDLApplications();
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int LDLAppID = (int)dgvAllLDLApplications.Rows[_selectedRowIndex].Cells["L.D.LAppID"].Value;

                Form frm = new frmLicenseHistory(clsDriver.GetDriverIDByLocalDrivingLicenseAppID(LDLAppID));
                frm.ShowDialog();
                _RefreshLDLApplications();
            }
        }
    }
}

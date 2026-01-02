using DVLD_Project.People;
using DVLDBusinessLayer;
using Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmManagePeople : Form
    {
        DataTable _dtPeople;
        DataView _dvPeople;
        private int _selectedRowIndex = -1;
        clsSettings.enPeopleFilterOptions _enFilterBy = clsSettings.enPeopleFilterOptions.None;
        public frmManagePeople()
        {
            InitializeComponent();
        }
        private void _RefreshPeopleList()
        {
            _dtPeople = clsPerson.GetAllPeopleInfoForManegePeople();
            _dvPeople = _dtPeople.DefaultView;
            dgvAllPeople.DataSource = _dvPeople;
            lblLiveNumberOfRecrords.Text = dgvAllPeople.Rows.Count.ToString();
        }
        private void _StartUpSettings()
        {
            cbFilterBy.SelectedItem ="None";
            mtxtBoxFilter.Visible = false;
            if(dgvAllPeople.Rows.Count > 0)
            {
                dgvAllPeople.Columns["Person ID"].Width = 80;
                dgvAllPeople.Columns["National NO."].Width = 100;
                dgvAllPeople.Columns["First Name"].Width = 100;
                dgvAllPeople.Columns["Second Name"].Width = 120;
                dgvAllPeople.Columns["Third Name"].Width = 100;
                dgvAllPeople.Columns["Last Name"].Width = 100;
                dgvAllPeople.Columns["Nationality"].Width = 100;
                dgvAllPeople.Columns["Phone"].Width = 100;
                dgvAllPeople.Columns["Email"].Width = 200;
                dgvAllPeople.Columns["Gendor"].Width = 70;
                dgvAllPeople.Columns["Date Of Birth"].Width = 200;
            }
        }
        private void _OpenAddNewEditPersonWindow(int ID = -1)
        
        {
            Form frm = new frmAddEditPersonInfo(ID);
            frm.ShowDialog();
            _RefreshPeopleList();
        }
        private void _DeletePerson(int ID)
        {
            if(MessageBox.Show("Are you sure you want to delete Person[" + ID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                clsPerson.DeletePerson(ID);
                _RefreshPeopleList();
                MessageBox.Show("Person[" + ID + "] Deleted Successfully.", "Person Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
            _StartUpSettings();
        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            _OpenAddNewEditPersonWindow();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Gendor")
            {
                mtxtBoxFilter.Visible = false;
                cbGender.Visible = true;
                cbGender.Focus();
                cbGender.SelectedIndex = 0;
            }
            else
            {
                mtxtBoxFilter.Visible = (cbFilterBy.Text != "None");
                cbGender.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    mtxtBoxFilter.Enabled = false;
                }
                else
                {
                    mtxtBoxFilter.Enabled = true;
                    mtxtBoxFilter.Text = "";
                    mtxtBoxFilter.Focus();
                }
            }


        }

        private void mtxtBoxFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            // Map Selection to SQL Column Alias
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "Person ID";
                    break;

                case "National NO":
                    FilterColumn = "National NO."; // Note the dot from your SQL
                    break;

                case "First Name":
                    FilterColumn = "First Name";
                    break;

                case "Second Name":
                    FilterColumn = "Second Name";
                    break;

                case "Third Name":
                    FilterColumn = "Third Name";
                    break;

                case "Last Name":
                    FilterColumn = "Last Name";
                    break;

                case "Nationality":
                    FilterColumn = "Nationality";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            // Reset if empty
            if (mtxtBoxFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dvPeople.RowFilter = "";
                lblLiveNumberOfRecrords.Text = dgvAllPeople.Rows.Count.ToString();
                return;
            }

            // Apply Filter
            if (FilterColumn == "Person ID")
            {
                // Numeric Filter (Exact Match)
                if (int.TryParse(mtxtBoxFilter.Text.Trim(), out int id))
                {
                    _dvPeople.RowFilter = string.Format("[{0}] = {1}", FilterColumn, id);
                }
                else
                {
                    _dvPeople.RowFilter = ""; // Clear if invalid number
                }
            }
            else
            {
                // String Filter (LIKE)
                // Works for Name, Phone, Email, National No.
                _dvPeople.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, mtxtBoxFilter.Text.Trim());
            }

            lblLiveNumberOfRecrords.Text = dgvAllPeople.Rows.Count.ToString();
        }

        private void cAddNewPerson_Click(object sender, EventArgs e)
        {
            _OpenAddNewEditPersonWindow();

        }

        private void cEdit_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int personID = (int)dgvAllPeople.Rows[_selectedRowIndex].Cells["Person ID"].Value;

                // 3. Send just the ID to your form

                _OpenAddNewEditPersonWindow(personID);

                // (Optional) Refresh your grid after the edit
                // RefreshPeopleData();

                // 4. Reset the index so it's not used by accident
                _selectedRowIndex = -1;
            }

        }

        private void dgvAllPeople_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvAllPeople.ClearSelection();
                dgvAllPeople.Rows[e.RowIndex].Selected = true;
            }
        }

        private void cDelete_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int personID = (int)dgvAllPeople.Rows[_selectedRowIndex].Cells["Person ID"].Value;

                // 3. Send just the ID to your form

               _DeletePerson(personID); 

                // (Optional) Refresh your grid after the edit
                // RefreshPeopleData();

                // 4. Reset the index so it's not used by accident
                _selectedRowIndex = -1;
            }
        }

        private void cShowDetails_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int personID = (int)dgvAllPeople.Rows[_selectedRowIndex].Cells["Person ID"].Value;

                // 3. Send just the ID to your form

                frmPersonDetails frm = new frmPersonDetails(personID);
                frm.ShowDialog();

                // (Optional) Refresh your grid after the edit
                // RefreshPeopleData();

                // 4. Reset the index so it's not used by accident
                _selectedRowIndex = -1;
            }
        }

        private void cPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet!", "Not Implemented!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet!", "Not Implemented!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mtxtBoxFilter_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Note: adjusting the filter to match how the data is stored in your grid.
            // If your Grid says "Male"/"Female", we compare strings. 
            // If your Grid says "0"/"1", we compare numbers. (Assuming Text here based on typical Views)

            string FilterColumn = "Gendor";
            string FilterValue = cbGender.Text;

            switch (FilterValue)
            {
                case "All":
                    _dvPeople.RowFilter = "";
                    break;
                case "Male":
                    // If your database returns 'Male', use: "Gendor = 'Male'"
                    // If your database returns 0, use: "Gendor = 0"
                    _dvPeople.RowFilter = string.Format("[{0}] = 'Male'", FilterColumn);
                    break;
                case "Female":
                    // If your database returns 'Female', use: "Gendor = 'Female'"
                    // If your database returns 1, use: "Gendor = 1"
                    _dvPeople.RowFilter = string.Format("[{0}] = 'Female'", FilterColumn);
                    break;
            }
            lblLiveNumberOfRecrords.Text = dgvAllPeople.Rows.Count.ToString();
        }

        private void mtxtBoxFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent typing letters if "Person ID" is selected
            if (cbFilterBy.Text == "Person ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}

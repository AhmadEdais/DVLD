using ConsoleApp1;
using DVLD_Project.People;
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

namespace DVLD_Project.Users
{
    public partial class frmManageUsers : Form
    {
        private int _selectedRowIndex = -1;
        DataTable _dtAllUsers;
        DataView _dvUsers;
        public frmManageUsers()
        {
            InitializeComponent();
        }
        private void _HideFilterControls()
        {
            txtFilterByValue.Visible = false;
            cbIsActive.Visible = false;
        }
        private void _StartUpSettings()
        {
            cbFilterBy.SelectedItem = "None";
            _HideFilterControls();
        }
        private void _RefreshUsers()
        {
            _dtAllUsers = clsUser.GetAllUsers();

            // Create a DataView from the DataTable
            _dvUsers = _dtAllUsers.DefaultView;

            // Bind the DataView to the grid (not the DataTable directly)
            dgvAllUsers.DataSource = _dvUsers;

            lblRecordsNumber.Text = dgvAllUsers.Rows.Count.ToString();

        }
        private void _AddEditUser(int UserID = -1)
        {

            Form frm = new frmAddUpdateUser(UserID);
            frm.ShowDialog();
            _RefreshUsers();
        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _StartUpSettings();
            _RefreshUsers();
            dgvAllUsers.Columns["User ID"].Width = 80;
            dgvAllUsers.Columns["Person ID"].Width = 80;
            dgvAllUsers.Columns["Full Name"].Width = 300;
            dgvAllUsers.Columns["UserName"].Width = 130;
            dgvAllUsers.Columns["Is Active"].Width = 120;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RefreshUsers();
            txtFilterByValue.Text = "";
            if (cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                txtFilterByValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedItem = "Yes";
            }
            else if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                _HideFilterControls();

            }
            else
            {
                txtFilterByValue.Visible = true;
                cbIsActive.Visible = false;
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            _AddEditUser();
        }

        private void dgvAllUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvAllUsers.ClearSelection();
                dgvAllUsers.Rows[e.RowIndex].Selected = true;
            }
        }

        private void cShowDetails_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int personID = (int)dgvAllUsers.Rows[_selectedRowIndex].Cells["Person ID"].Value;

                // 3. Send just the ID to your form

                frmPersonDetails frm = new frmPersonDetails(personID);
                frm.ShowDialog();

                // (Optional) Refresh your grid after the edit
                // RefreshPeopleData();

                // 4. Reset the index so it's not used by accident
                _selectedRowIndex = -1;
            }
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddEditUser();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int PersonID = (int)dgvAllUsers.Rows[_selectedRowIndex].Cells["Person ID"].Value;

                // 3. Send just the ID to your form

                _AddEditUser(PersonID);

                // (Optional) Refresh your grid after the edit
                // RefreshPeopleData();

                // 4. Reset the index so it's not used by accident
                _selectedRowIndex = -1;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int UserID = (int)dgvAllUsers.Rows[_selectedRowIndex].Cells["User ID"].Value;

                // 3. Send just the ID to your form


                if (MessageBox.Show("Are you sure you want to delete Person[" + UserID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (clsUser._DeleteUser(UserID))
                    {
                        _RefreshUsers();
                        MessageBox.Show("User[" + UserID + "] Deleted Successfully.", "User Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("This User Cannot be deleted ","User Deletion failed",MessageBoxButtons.OK,MessageBoxIcon.Error);

                    }




                    
                }

                // (Optional) Refresh your grid after the edit
                // RefreshPeopleData();

                // 4. Reset the index so it's not used by accident
                _selectedRowIndex = -1;
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int UserID = (int)dgvAllUsers.Rows[_selectedRowIndex].Cells["User ID"].Value;

                // 3. Send just the ID to your form
                Form frm = new frmChangePassword(UserID);
                frm.ShowDialog();   
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet!", "Not Implemented!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet!", "Not Implemented!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            // Map Selected Filter to real Column name
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "User ID";
                    break;

                case "Person ID":
                    FilterColumn = "Person ID";
                    break;

                case "Full Name":
                    FilterColumn = "Full Name";
                    break;

                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "Is Active":
                    FilterColumn = "Is Active";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            // Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterByValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dvUsers.RowFilter = "";
                lblRecordsNumber.Text = dgvAllUsers.Rows.Count.ToString();
                return;
            }

            // Apply Filter
            // Note: User ID and Person ID are numbers, others are strings
            if (FilterColumn == "User ID" || FilterColumn == "Person ID")
            {
                // For numbers (Equals)
                _dvUsers.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterByValue.Text.Trim());
            }
            else
            {
                // For strings (Like)
                _dvUsers.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterByValue.Text.Trim());
            }

            lblRecordsNumber.Text = dgvAllUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Is Active";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    _dvUsers.RowFilter = "";
                    break;
                case "Yes":
                    // Map "Yes" to 1 (True)
                    _dvUsers.RowFilter = string.Format("[{0}] = 1", FilterColumn);
                    break;
                case "No":
                    // Map "No" to 0 (False)
                    _dvUsers.RowFilter = string.Format("[{0}] = 0", FilterColumn);
                    break;
            }

            lblRecordsNumber.Text = dgvAllUsers.Rows.Count.ToString();
        }

        private void txtFilterByValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // We only allow numbers if "User ID" or "Person ID" is selected
            if (cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void lblRecordsNumber_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

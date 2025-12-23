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
        private int _selectedRowIndex = -1;
        clsSettings.enPeopleFilterOptions _enFilterBy = clsSettings.enPeopleFilterOptions.None;
        public frmManagePeople()
        {
            InitializeComponent();
        }
        private void _RefreshPeopleList(clsSettings.enPeopleFilterOptions enFilteredBy = clsSettings.enPeopleFilterOptions.PersonID, string Text = "")
        {
            dgvAllPeople.DataSource = clsPerson.GetFilterPeople(enFilteredBy,Text);
            lblLiveNumberOfRecrords.Text = clsPerson.GetPeopleCount(enFilteredBy, Text).ToString();
        }
        private void _StartUpSettings()
        {
            cbFilterBy.SelectedItem ="None";
            mtxtBoxFilter.Visible = false;
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
            mtxtBoxFilter.Clear();
            mtxtBoxFilter.Mask = "";
            _enFilterBy = clsSettings.enPeopleFilterOptions.PersonID;
            _RefreshPeopleList(_enFilterBy, mtxtBoxFilter.Text.ToString().Trim());

            switch (cbFilterBy.SelectedItem.ToString())
            {
                case ("None") :
                    mtxtBoxFilter.Visible = false;
                    break;
                case ("Person ID"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.PersonID;
                    mtxtBoxFilter.Mask = "999999";
                    break;
                case ("National NO"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.NationalNo;
                    mtxtBoxFilter.Mask = "aaaa";
                    break;
                case ("First Name"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.FirstName;
                    mtxtBoxFilter.Mask = "????????????????????";
                    break;
                case ("Second Name"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.SecondName;
                    mtxtBoxFilter.Mask = "????????????????????";
                    break;
                case ("Third Name"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.ThirdName;
                    mtxtBoxFilter.Mask = "????????????????????";
                    break;
                case ("Last Name"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.LastName;
                    mtxtBoxFilter.Mask = "CCCCCCCCCCCCCCCCCCCC";
                    break;
                case ("Nationality"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.Nationality;
                    mtxtBoxFilter.Mask = "?????????????????????????????????????????????????";
                    break;
                case ("Gendor"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.Gendor;
                    mtxtBoxFilter.Mask = "??????";
                    break;
                case ("Phone"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.Phone;
                    mtxtBoxFilter.Mask = "99999999999999";
                    break;
                case ("Email"):
                    mtxtBoxFilter.Visible = true;
                    _enFilterBy = clsSettings.enPeopleFilterOptions.Email;
                    mtxtBoxFilter.Mask = "CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC";
                    break;


            }


        }

        private void mtxtBoxFilter_TextChanged(object sender, EventArgs e)
        {
            if(mtxtBoxFilter.Text == "")
            {
                _RefreshPeopleList(clsSettings.enPeopleFilterOptions.PersonID, mtxtBoxFilter.Text.ToString().Trim());
                return;
            }
            _RefreshPeopleList(_enFilterBy, mtxtBoxFilter.Text.ToString().Trim());
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

        
    }
}

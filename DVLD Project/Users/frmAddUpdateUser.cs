using ConsoleApp1;
using DVLD.Classes;
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
    public partial class frmAddUpdateUser : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int UserID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        int _PersonID = -1,_UserID = -1;
        clsUser _CurrentUser = new clsUser();

        
        public frmAddUpdateUser(int PersonID = -1)
        {
            InitializeComponent();
            _PersonID = PersonID;
            if (PersonID == -1)
            { 
                btnSave.Enabled = false;
                btnNext.Enabled = false;
                _Mode = enMode.AddNew;
            }
            else
            {
                _CurrentUser = clsUser.FindByPersonID(_PersonID);
                _UserID = _CurrentUser.UserID;
                _Mode = enMode.Update;
                lblAddEditUser.Text = "Update User ID " + _UserID.ToString();
                lblUserID.Text = _UserID.ToString();
                txtUserName.Text = _CurrentUser.UserName;
                txtPassword.Text = _CurrentUser.Password;
                txtConfirmPassword.Text = _CurrentUser.Password;
                cbIsActive.Checked = _CurrentUser.IsActive;
            }
            ctrlFindPerson1.OnPersonSelected += DataBackEvent;
            ctrlFindPerson1.SelectMode(_PersonID);
        }
        private void DataBackEvent(int PersonID)
        {
            _PersonID = PersonID;

            // Optional: Enable the Next/Save button now that we have a person
            btnNext.Enabled = true;

            // Debugging check (optional)
            // MessageBox.Show("Person ID Received: " + PersonID);
        }

        private void ctrlFindPerson1_Load(object sender, EventArgs e)
        {
               
        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNew)
            {
                // 1. Validation: Prevent moving to the next tab if no person is selected
            if (_PersonID == -1)
            {
                MessageBox.Show("Please Select a Person first.", "Select Person",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Optional: specific focus method if you have one
                // ctrlFindPerson1.FilterFocus(); 
                return;
            }

                // 2. Validation: Check if a user already exists for this person
                // (We use the static method we created earlier in clsUser)
                if (clsUser.isUserExist(_PersonID))
                {
                    MessageBox.Show("Selected Person is already a user, choose another one.", "Select Person",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 3. Switch to the Login Info Tab
            // Tab indexes start at 0. So "Personal Info" is 0, "Login Info" is 1.
            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
        }
        private void _ValidateNotEmpty(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(currentTxtBox, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(currentTxtBox, null);
            }

        }
        //private void _ValidatePasswordMatch(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    TextBox currentTxtBox = (TextBox)sender;
        //    if (string.IsNullOrEmpty(currentTxtBox.Text.Trim()))
        //    {
        //        //e.Cancel = true;
        //        errorProvider1.SetError(currentTxtBox, "This field is required!");
        //    }
        //    else
        //    {
        //        //e.Cancel = false;
        //        errorProvider1.SetError(currentTxtBox, null);
        //    }

        //    if (txtPassword.Text != txtConfirmPassword.Text && (txtPassword.Text!=string.Empty || txtConfirmPassword.Text != string.Empty))
        //    {
        //        //e.Cancel = true;
        //        errorProvider1.SetError(txtConfirmPassword, "Passwords do not match!");
        //    }
        //    else
        //    {
        //        //e.Cancel = false;
        //        errorProvider1.SetError(txtConfirmPassword, null);
        //    }
        //}   

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                // 2. If validation fails, show a message and stop
                MessageBox.Show("Invalid Inputs, Please fix the fields marked with the ⓘ error icon.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 3. Stop the save method right here.
                return;
            }
            _CurrentUser.PersonID = _PersonID;
            _CurrentUser.UserName = txtUserName.Text.Trim();
            _CurrentUser.Password = txtPassword.Text;
            _CurrentUser.IsActive = cbIsActive.Checked;
            if (_CurrentUser.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblAddEditUser.Text = "Update User ID " + _CurrentUser.UserID.ToString();
                lblUserID.Text = _CurrentUser.UserID.ToString();
                if (_CurrentUser.UserID == Global.CurrentUser.UserID)
                {
                    clsUtil._SaveCredentialsToFile(_CurrentUser.UserName, _CurrentUser.Password);
                }
                DataBack?.Invoke(this, _CurrentUser.UserID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be blank");
                return;
            }

            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // Check if the user is trying to switch to the "Login Info" tab (Index 1)
            if (e.TabPageIndex == 1)
            {
               if(_Mode == enMode.AddNew) // Check if a person is selected
                {if (_PersonID == -1)
                {
                    // If no person selected, CANCEL the switch
                    e.Cancel = true;

                    // Show a friendly message
                    MessageBox.Show("Please Select a Person first.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Optional: Move focus back to the filter box
                    // ctrlFindPerson1.FilterFocus();
                }
                    // 2. Validation: Check if a user already exists for this person
                    // (We use the static method we created earlier in clsUser)
                    if (clsUser.isUserExist(_PersonID))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Selected Person is already a user, choose another one.", "Select Person",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {

        }
    }
}

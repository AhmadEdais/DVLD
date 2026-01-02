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
    public partial class frmChangePassword : Form
    {
        int _UserID = -1;
        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _User = clsUser.FindByUserID(_UserID);
            if(_User == null)
            {
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                ctrlUserInfo1.LoadInfo(_UserID);
                txtCurrentPassword.Focus();
            }

        }
        
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            txtCurrentPassword.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
            _User.Password = txtNewPassword.Text;
            if (_User.Save())
            {
                if (_User.UserID == Global.CurrentUser.UserID)
                { 
                    clsUtil._SaveCredentialsToFile(_User.UserName, _User.Password);
                }
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtCurrentPassword.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "This field cannot be Empty!");
            }
            if(txtCurrentPassword.Text != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password is incorrect!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "This field cannot be Empty!");
            }
            else if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Passwords do not match!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }
    }
}

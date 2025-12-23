using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Manage_Application_Type
{
    public partial class frmUpdateApplicationTypes : Form
    {
        clsApplicationTypes _applicationTypeToUpdate;
        int _applicationTypeID = -1;
        public frmUpdateApplicationTypes()
        {
            InitializeComponent();
        }
        public frmUpdateApplicationTypes(int ID )
        {
            InitializeComponent();
            _applicationTypeID = ID;

        }
        private void _LoadApplicationTypeData()
        {
            _applicationTypeToUpdate = clsApplicationTypes.FindByApplicationTypeID(_applicationTypeID);
            if(_applicationTypeToUpdate != null)
            {
                lblApplicationTypeID.Text = _applicationTypeID.ToString();
                txtApplicationTypeTitle.Text = _applicationTypeToUpdate.ApplicationTypeTitle;
                txtApplicationFees.Text = _applicationTypeToUpdate.ApplicationTypeFee.ToString();
            }
            else
            {
                MessageBox.Show("Application Type with ID " + _applicationTypeID.ToString() + " not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadApplicationTypeData();
        }
        private void txtNotEmpty_Validating(object sender, CancelEventArgs e)
        {
            TextBox currentBox = (TextBox)sender;

            if (string.IsNullOrEmpty(currentBox.Text.Trim()))
            {
                e.Cancel = true; // Prevent user from leaving this field
                errorProvider1.SetError(currentBox, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(currentBox, null);
            }
        }

        private void txtApplicationFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key is Control (like Backspace) or a Digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.')) // Allow Dot (remove this line if you want integers only)
            {
                e.Handled = true; // Stop the character
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true; // Stop the second dot
            }
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
            _applicationTypeToUpdate.ApplicationTypeTitle = txtApplicationTypeTitle.Text.Trim();
            _applicationTypeToUpdate.ApplicationTypeFee = decimal.Parse(txtApplicationFees.Text.Trim());
            if (_applicationTypeToUpdate.Save())
            {
                MessageBox.Show("Application Type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error updating Application Type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

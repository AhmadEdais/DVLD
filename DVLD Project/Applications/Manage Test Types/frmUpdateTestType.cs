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

namespace DVLD_Project.Applications.Manage_Test_Types
{
    public partial class frmUpdateTestType : Form
    {
        clsTestTypes _TestTypeToUpdate;
        int _TestTypeID = -1;
        public frmUpdateTestType()
        {
            InitializeComponent();
        }
        public frmUpdateTestType(int ID)
        {
            InitializeComponent();
            _TestTypeID = ID;
        }
        private void _LoadTestTypeData()
        {
            _TestTypeToUpdate = clsTestTypes.FindByTestTypeID(_TestTypeID);
            if (_TestTypeToUpdate != null)
            {
                lblApplicationTypeID.Text = _TestTypeID.ToString();
                txtApplicationTypeTitle.Text = _TestTypeToUpdate.TestTypesTitle;
                rtxtDescription.Text = _TestTypeToUpdate.TestTypesDescription;
                txtApplicationFees.Text = _TestTypeToUpdate.TestTypesFees.ToString();
            }
            else
            {
                MessageBox.Show("Application Type with ID " + _TestTypeID.ToString() + " not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _LoadTestTypeData();
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
        private void rtxtNotEmpty_Validating(object sender, CancelEventArgs e)
        {
            RichTextBox currentBox = (RichTextBox)sender;

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
            _TestTypeToUpdate.TestTypesTitle = txtApplicationTypeTitle.Text.Trim();
            _TestTypeToUpdate.TestTypesDescription = rtxtDescription.Text.Trim();
            _TestTypeToUpdate.TestTypesFees = decimal.Parse(txtApplicationFees.Text.Trim());
            if (_TestTypeToUpdate.Save())
            {
                MessageBox.Show("Test Type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update Test Type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

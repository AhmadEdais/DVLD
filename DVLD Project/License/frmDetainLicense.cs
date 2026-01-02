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

namespace DVLD_Project.License
{
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }
        private void _SetupSettings()
        {
            llblShowLicenseHistory.Enabled = false;
            llblShowLiceseInfo.Enabled = false;
            btnDetain.Enabled = false;
        }
        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _SetupSettings();
            ctrlFindLicense1.OnLicenseSelected += CtrlFindLicense1_OnLicenseSelected;

        }
        private void CtrlFindLicense1_OnLicenseSelected(int LicenseID)
        {
            // This code runs automatically when the user searches and finds a license

            _SetupSettings();
            if (ctrlFindLicense1.SelectedLicenseInfo.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("The selected local license is expired. Replacement is not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llblShowLicenseHistory.Enabled = true;
                return;
            }
            if (ctrlFindLicense1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("The selected local license is inactive. Replacememnt is not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llblShowLicenseHistory.Enabled = true;
                return;
            }
            if(clsDetainedLicense.IsLicenseDetained(ctrlFindLicense1.LicenseID))
            {
                MessageBox.Show("The selected local license is already detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llblShowLicenseHistory.Enabled = true;
                return;
            }
            lblLicenseID.Text = ctrlFindLicense1.LicenseID.ToString();
            lblDetainDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            btnDetain.Enabled = true;
            llblShowLicenseHistory.Enabled = true;

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (ctrlFindLicense1.SelectedLicenseInfo == null)
            {
                MessageBox.Show("Please select a valid local license first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!this.ValidateChildren())
            {
                // 2. If validation fails, show a message and stop
                MessageBox.Show("Invalid Inputs, Please fix the fields marked with the ⓘ error icon.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 3. Stop the save method right here.
                return;
            }
            int.Parse(txtFineFees.Text.Trim());
            int DetainedLicenseID =
                ctrlFindLicense1.SelectedLicenseInfo.Detain(int.Parse(txtFineFees.Text.Trim()),

                Global.CurrentUser.UserID);

            if (DetainedLicenseID == -1)
            {
                MessageBox.Show("Faild to Detain the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }
            lblDetainID.Text = DetainedLicenseID.ToString();
            MessageBox.Show("License Detained Successfully. Detain ID: " + DetainedLicenseID.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            llblShowLiceseInfo.Enabled = true;
            llblShowLicenseHistory.Enabled = true;
            btnDetain.Enabled = false;
            ctrlFindLicense1.FilterEnabled = false;
            txtFineFees.Enabled = false;
        }

        private void llblShowLiceseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicense(ctrlFindLicense1.LicenseID);
            frm.ShowDialog();
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseHistory(ctrlFindLicense1.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fine Fees is required.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");
            }
        }
    }
    
}

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
    public partial class frmReleaseDetainedLicenses : Form
    {
        int _ApplicationID = -1;    
        int _LicensesID = -1;
        public frmReleaseDetainedLicenses(int LicenseID = -1)
        {
            InitializeComponent();
            _LicensesID = LicenseID;
        }
        private void _SetupSettings()
        {
            llblShowLicenseHistory.Enabled = false;
            llblShowLiceseInfo.Enabled = false;
            btnRelease.Enabled = false;
        }

        private void frmReleaseDetainedLicenses_Load(object sender, EventArgs e)
        {
            _SetupSettings();
            ctrlFindLicense1.OnLicenseSelected += CtrlFindLicense1_OnLicenseSelected;
            if (_LicensesID != -1)
            {
                
                ctrlFindLicense1.SetID(_LicensesID);
                ctrlFindLicense1.FilterEnabled = false;
            }
            

        }
        private void CtrlFindLicense1_OnLicenseSelected(int LicenseID)
        {
            // This code runs automatically when the user searches and finds a license

            _SetupSettings();
            if (ctrlFindLicense1.SelectedLicenseInfo.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("The selected local license is expired. Release is not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llblShowLicenseHistory.Enabled = true;
                return;
            }
            if (ctrlFindLicense1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("The selected local license is inactive. Release is not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llblShowLicenseHistory.Enabled = true;
                return;
            }
            if (!clsDetainedLicense.IsLicenseDetained(ctrlFindLicense1.LicenseID))
            {
                MessageBox.Show("The selected local license is not detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llblShowLicenseHistory.Enabled = true;
                return;
            }
            lblDetainID.Text = ctrlFindLicense1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = ctrlFindLicense1.SelectedLicenseInfo.DetainedInfo.DetainDate.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = clsApplicationTypes.GetApplicationFees((int)clsApplicationTypes.enApplicationType.ReleaseDetained).ToString();
            lblLicenseID.Text = ctrlFindLicense1.LicenseID.ToString();
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblFineFees.Text = ctrlFindLicense1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (decimal.Parse(lblFineFees.Text.ToString().Trim()) + decimal.Parse(lblApplicationFees.Text.ToString().Trim())).ToString();
            btnRelease.Enabled = true;
            llblShowLicenseHistory.Enabled = true;

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (ctrlFindLicense1.SelectedLicenseInfo == null)
            {
                MessageBox.Show("Please select a valid local license first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ctrlFindLicense1.SelectedLicenseInfo.ReleaseDetainedLicense(Global.CurrentUser.UserID,ref _ApplicationID))
            {
                MessageBox.Show("An error occurred while releasing the detained license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("The detained license has been released successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblApplicationID.Text = _ApplicationID.ToString();
            btnRelease.Enabled = false;
            ctrlFindLicense1.FilterEnabled = false;
            llblShowLiceseInfo.Enabled = true;
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseHistory(ctrlFindLicense1.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();
        }

        private void llblShowLiceseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicense(ctrlFindLicense1.LicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

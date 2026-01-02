using ConsoleApp1;
using DVLD_Buisness;
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
    public partial class frmReplaceDrivingLicense : Form
    {
        int _NewLicenseID = -1;
        clsLicense.enIssueReason  _enReplaceType = clsLicense.enIssueReason.DamagedReplacement;
        public frmReplaceDrivingLicense()
        {
            InitializeComponent();
        }
        private void _SetupSettings()
        {
            rbDamagedLicense.Checked = true;
            this.Text = "Replace Damaged Driving License";
            llblShowLicenseHistory.Enabled = false;
            llblShowNewLiceseInfo.Enabled = false;
            btnReplace.Enabled = false;
        }
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _enReplaceType = rbDamagedLicense.Checked ? clsLicense.enIssueReason.DamagedReplacement : clsLicense.enIssueReason.LostReplacement;
            if(_enReplaceType == clsLicense.enIssueReason.DamagedReplacement)
            {
                this.Text = "Replace Damaged Driving License";
                lblTitle.Text = "Replace Damaged Driving License";
            }
            else
            {
                this.Text = "Replace Lost Driving License";
                lblTitle.Text = "Replace Lost Driving License";
            }
        }

        private void frmReplaceDrivingLicense_Load(object sender, EventArgs e)
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
            lblOldLicenseID.Text = ctrlFindLicense1.LicenseID.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = clsApplicationTypes.GetApplicationFees(_enReplaceType == clsLicense.enIssueReason.DamagedReplacement ? (int)clsApplicationTypes.enApplicationType.ReplacementForDamaged : (int)clsApplicationTypes.enApplicationType.ReplacmentForLost).ToString("F2");
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            btnReplace.Enabled = true;
            llblShowLicenseHistory.Enabled = true;

        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (ctrlFindLicense1.SelectedLicenseInfo == null)
            {
                MessageBox.Show("Please select a valid local license first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsLicense NewLicense =
                ctrlFindLicense1.SelectedLicenseInfo.ReplaceLicense(_enReplaceType,
                Global.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Replace the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            
            }
            _NewLicenseID = NewLicense.LicenseID;
            lblLRAppID.Text = NewLicense.ApplicationID.ToString();
            lblReplacedLicenseID.Text = NewLicense.LicenseID.ToString();
            llblShowNewLiceseInfo.Enabled = true;
            MessageBox.Show("Licensed Replaced Successfully with ID = " + NewLicense.LicenseID.ToString(), "License Replaced", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnReplace.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlFindLicense1.FilterEnabled = false;

        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseHistory(ctrlFindLicense1.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();
           

        }

        private void llblShowNewLiceseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicense(_NewLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

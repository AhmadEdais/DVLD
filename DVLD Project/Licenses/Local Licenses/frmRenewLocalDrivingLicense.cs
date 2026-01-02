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
    public partial class frmRenewLocalDrivingLicense : Form
    {
        int _NewLicenseID = -1;

        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _SetupSettings()
        {
            llblShowLicenseHistory.Enabled = false;
            llblShowLicenseInfo.Enabled = false;
            llblShowNewLiceseInfo.Enabled = false;
            btnRenew.Enabled = false;
            ctrlFindLicense1.txtLicenseFocus();
        }
        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _SetupSettings();
            ctrlFindLicense1.OnLicenseSelected += CtrlFindLicense1_OnLicenseSelected;
        }

        private void _ResetForm()
        {
            ctrlFindLicense1.ResetControl();
            lblOldLicenseID.Text = "-1";
            llblShowLicenseHistory.Enabled = false;
            llblShowLicenseInfo.Enabled = false;
            btnRenew.Enabled = false;
        }
        private void CtrlFindLicense1_OnLicenseSelected(int LicenseID)
        {
            // This code runs automatically when the user searches and finds a license

            _SetupSettings();
            if (ctrlFindLicense1.SelectedLicenseInfo.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("The selected local license is still valid untill " +
                ctrlFindLicense1.SelectedLicenseInfo.ExpirationDate.ToString() + " Renewal is not allowed at this time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llblShowLicenseHistory.Enabled = true;
                return;
            }
            if (ctrlFindLicense1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("The selected local license is inactive. Renewal is not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llblShowLicenseHistory.Enabled = true;
                return;
            }


            // 2. Check the history or enable the "Save" button
            lblOldLicenseID.Text = ctrlFindLicense1.LicenseID.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = clsApplicationTypes.GetApplicationFees((int)clsApplicationTypes.enApplicationType.RenewLicenseApplication).ToString("F2");
            lblLicenseFees.Text = clsLicenseClass.GetClassFees(ctrlFindLicense1.SelectedLicenseInfo.LicenseClass).ToString("F2");
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblTotalFees.Text = (decimal.Parse(lblApplicationFees.Text) + decimal.Parse(lblLicenseFees.Text)).ToString("F2");
            btnRenew.Enabled = true;
            llblShowLicenseHistory.Enabled = true;



        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (ctrlFindLicense1.SelectedLicenseInfo == null)
            {
                MessageBox.Show("Please select a valid local license first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsLicense NewLicense =
                ctrlFindLicense1.SelectedLicenseInfo.RenewLicense(rtxtNotes.Text.Trim(),
                Global.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            _NewLicenseID = NewLicense.LicenseID;
            lblRLAppID.Text = NewLicense.ApplicationID.ToString();
            lblRenewedLicense.Text = NewLicense.LicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID = " + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            llblShowNewLiceseInfo.Enabled = true;
            btnRenew.Enabled = false;
            ctrlFindLicense1.FilterEnabled = false;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicense(_NewLicenseID);
            frm.ShowDialog();
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseHistory(ctrlFindLicense1.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();
        }
    }
}

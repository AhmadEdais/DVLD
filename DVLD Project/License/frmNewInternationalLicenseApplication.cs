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
    public partial class frmNewInternationalLicenseApplication : Form
    {
        int _InternationalLicenseID = -1;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlFindLicense1_Load(object sender, EventArgs e)
        {
            ctrlFindLicense1.OnLicenseSelected += CtrlFindLicense1_OnLicenseSelected;
        }
        private void _SetupSettings()
        {
            llblShowLicenseHistory.Enabled = false;
            llblShowLicenseInfo.Enabled = false;
            btnIssue.Enabled = false;
            ctrlFindLicense1.txtLicenseFocus();
        }
        private void _ResetForm()
        {
            ctrlFindLicense1.ResetControl();
            lblLocalLicenseID.Text = "-1";
            llblShowLicenseHistory.Enabled = false;
            llblShowLicenseInfo.Enabled = false;
            btnIssue.Enabled = false;
        }
       
        private void CtrlFindLicense1_OnLicenseSelected(int LicenseID)
        {
            // This code runs automatically when the user searches and finds a license

            // 1. Store the ID
            clsLicense SelectedLicense = ctrlFindLicense1.SelectedLicenseInfo;
            if (SelectedLicense.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("The selected local license is expired. Cannot proceed with International License application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetForm();
                return;
            }
           
           
            // 2. Check the history or enable the "Save" button
            lblLocalLicenseID.Text = ctrlFindLicense1.LicenseID.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblFees.Text = clsApplicationTypes.GetApplicationFees((int)clsApplicationTypes.enApplicationType.NewInternationalLicense).ToString("F2"); 
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            btnIssue.Enabled = true;
            llblShowLicenseHistory.Enabled = true;

           
            
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            _SetupSettings();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(ctrlFindLicense1.SelectedLicenseInfo == null)
            {
                MessageBox.Show("Please select a valid local license first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (clsInternationalLicense.IsInternationalLicenseExistByLocalLicenseID(ctrlFindLicense1.LicenseID))
            {
                MessageBox.Show("An International License already exists for this driver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlFindLicense1.SelectedLicenseInfo.DriverID);
                llblShowLicenseInfo.Enabled = true;
                btnIssue.Enabled = false;
                return;
            }
            if(ctrlFindLicense1.SelectedLicenseInfo.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("The selected local license is expired. Cannot proceed with International License application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!(ctrlFindLicense1.SelectedLicenseInfo.LicenseClass == 3))
            {
                MessageBox.Show("The selected local license class is not eligible for International License application. Only Class 3 licenses are allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           

            clsInternationalLicense NewIntLicense = new clsInternationalLicense();
            NewIntLicense.ApplicantPersonID = ctrlFindLicense1.SelectedLicenseInfo.DriverInfo.PersonID;
            NewIntLicense.ApplicationDate = DateTime.Now;
            NewIntLicense.LastStatusDate = DateTime.Now;
            NewIntLicense.ApplicationTypeID = (int)clsApplicationTypes.enApplicationType.NewInternationalLicense;
            NewIntLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            NewIntLicense.PaidFees = clsApplicationTypes.GetApplicationFees((int)clsApplicationTypes.enApplicationType.NewInternationalLicense);
            NewIntLicense.CreatedByUserID = Global.CurrentUser.UserID;

            NewIntLicense.DriverID = ctrlFindLicense1.SelectedLicenseInfo.DriverID;
            NewIntLicense.IssuedUsingLocalLicenseID = ctrlFindLicense1.SelectedLicenseInfo.LicenseID;
            NewIntLicense.IssueDate = DateTime.Now;
            NewIntLicense.ExpirationDate = DateTime.Now.AddYears(1);
            NewIntLicense.IsActive = true;
            NewIntLicense.CreatedByUserID = Global.CurrentUser.UserID;
            if (NewIntLicense.Save())
            {
                MessageBox.Show("International License Application created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblIlAppID.Text = NewIntLicense.ApplicationID.ToString();
                lblInternationalLicenseID.Text = NewIntLicense.InternationalLicenseID.ToString();
                llblShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = NewIntLicense.InternationalLicenseID;
                ctrlFindLicense1.FilterEnabled = false;
                btnIssue.Enabled = false;

            }
            else
            {
                MessageBox.Show("Failed to create the International License Application. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseHistory(ctrlFindLicense1.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowInternationalLicense(_InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}

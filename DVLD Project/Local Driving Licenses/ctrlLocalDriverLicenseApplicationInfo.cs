using ConsoleApp1;
using DVLD_Buisness;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Local_Driving_Licenses
{
    public partial class ctrlLocalDriverLicenseApplicationInfo : UserControl
    {
        int _ApplicationID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        public ctrlLocalDriverLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        private void _SetControls()
        {
            llblShowLicenseInfo.Enabled = false;
        }
        private void _FillDetails()
        {

            // 1. Safety Check: If the object is null, STOP here.
            // This prevents the crash when the form is loading or in the designer.
            if (_LocalDrivingLicenseApplication == null)
            {
                return;
            }
            lblDLDAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = clsLocalDrivingLicenseApplication.GetPassedTestCount(_ApplicationID).ToString() + "/3";
            lblID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
            lblStatus.Text = _LocalDrivingLicenseApplication.ApplicationStatus.ToString();
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString("F2");
            lblType.Text = clsApplicationTypes.GetApplicationTypeTitle(_LocalDrivingLicenseApplication.ApplicationTypeID);
            lblApplicant.Text = clsPerson.GetFullName(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString("dd/MM/yyyy");
            lblStatusDate.Text = _LocalDrivingLicenseApplication.LastStatusDate.ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;
        }
        private void ctrlLocalDriverLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            
        }
        public void SetApplicationID(int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(_ApplicationID);
            _SetControls();
            _FillDetails();
        }

        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmPersonDetails(_LocalDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}

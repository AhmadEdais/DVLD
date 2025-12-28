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
    public partial class frmIssueLicense : Form
    {
        int _LocalDrivingLicenseApplicationID = -1,_ApplicationID = -1,_PersonID = -1,_DriverID = -1
            ,_LicenseClassID = -1;
        clsLicense _License;
        clsDriver _Driver;
        public frmIssueLicense(int LocalDrivingLicenseApplication = -1)
        {
            InitializeComponent();
            _License = new clsLicense();
           _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication;
           _ApplicationID = clsLocalDrivingLicenseApplication.GetApplicationIDByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
            _PersonID = clsLocalDrivingLicenseApplication.GetApplicantPersonIDByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
            _Driver = clsDriver.FindByPersonID(_PersonID);
            if (_Driver != null)
            {
                _DriverID = _Driver.DriverID;
            }
            _LicenseClassID = clsLocalDrivingLicenseApplication.GetLicenseClassIDByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);


        }

        private void ctrlLocalDriverLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {
            ctrlLocalDriverLicenseApplicationInfo1.SetApplicationID(_LocalDrivingLicenseApplicationID);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _License.ApplicationID = _ApplicationID;
            if (_DriverID == -1)
            {
                // Create a new Driver record if it doesn't exist
                _Driver = new clsDriver();
                _Driver.PersonID = _PersonID;
                _Driver.CreatedByUserID = Global.CurrentUser.UserID;
                _Driver.CreatedDate = DateTime.Now;
                _Driver.Save();
            }
            _License.DriverID = _Driver.DriverID;
            _License.LicenseClass = _LicenseClassID;
            _License.IssueDate = DateTime.Now;
            _License.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.GetDefaultValidityLength(_LicenseClassID));
            _License.Notes = rtxtNotes.Text.Trim();
            _License.PaidFees = clsLicenseClass.GetClassFees(_LicenseClassID);
            _License.IsActive = true;
            _License.IssueReason = 1; // New License
            _License.CreatedByUserID = Global.CurrentUser.UserID;
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm Issue", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_License.Save())
                {
                    MessageBox.Show("License issued successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clsApplication.UpdateStatus(_ApplicationID, 3);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to issue the license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

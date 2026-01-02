using DVLD_Project.Properties;
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
    public partial class ctrlShowInternationalLicenseInfo : UserControl
    {
        public ctrlShowInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        
        public void SetInternationalLicenseID(int InternationalLicenseID)
        {
            clsInternationalLicense intlLicense = clsInternationalLicense.Find(InternationalLicenseID);
            if (intlLicense != null)
            {
                clsLicense LocalLicense = clsLicense.Find(intlLicense.IssuedUsingLocalLicenseID);
                lblName.Text = LocalLicense.DriverInfo.PersonInfo.FullName;
                lblInternationalLicenseID.Text = intlLicense.InternationalLicenseID.ToString();
                lblLicenseID.Text = LocalLicense.LicenseID.ToString();
                lblNationalNO.Text = LocalLicense.DriverInfo.PersonInfo.NationalNO;
                lblGender.Text = LocalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
                pbGender.Image = LocalLicense.DriverInfo.PersonInfo.Gendor == 0 ? Resources.Man_32 : Resources.Woman_32;
                lblIssueDate.Text = intlLicense.IssueDate.ToString("dd/MM/yyyy");
                lblApplicationID.Text = intlLicense.ApplicationID.ToString();
                lblIsActive.Text = intlLicense.IsActive ? "Active" : "Inactive";
                lblDateOfBirth.Text = LocalLicense.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
                lblDriverID.Text = LocalLicense.DriverInfo.DriverID.ToString();
                lblExpirationDate.Text = intlLicense.ExpirationDate.ToString("dd/MM/yyyy");
                pbPersonImage.ImageLocation = LocalLicense.DriverInfo.PersonInfo.ImagePath;
            }
            else
            {
                MessageBox.Show("International License not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ctrlShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}

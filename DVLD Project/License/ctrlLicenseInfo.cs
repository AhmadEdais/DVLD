using DVLD_Buisness;
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
    public partial class ctrlLicenseInfo : UserControl
    {
        int _LicenseID = -1;
        clsDriver _Driver;
        clsLicense _License;
        public clsLicense SelectedLicenseInfo
        { get { return _License; } }
        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        private void ctrlLicenseInfo_Load(object sender, EventArgs e)
        {

        }
        private void _FillDetails()
        {
            lblClassName.Text  =clsLicenseClass.GetClassName(_License.LicenseClass);
            lblName.Text = _Driver.PersonInfo.FullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNO.Text = _Driver.PersonInfo.NationalNO;
            lblGender.Text = _Driver.PersonInfo.Gendor == 0 ? "Male" : "Female";
            pbGender.Image = _Driver.PersonInfo.Gendor == 0 ? Resources.Man_32 : Resources.Woman_32;
            lblIssueDate.Text = _License.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = _License.IssueReason switch
            {
                1 => "First Time",
                2 => "Renew",
                3 => "Replacement for Lost",
                4 => "Replacement for Damaged",
                5 => "Release Detained",
                6 => "New International License",
                _ => "Unknown"
            };
            lblNotes.Text = _License.Notes;
            lblIsActive.Text = _License.IsActive ? "Active" : "Inactive";
            lblDateOfBirth.Text = _Driver.PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriverID.Text = _Driver.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToString("dd/MM/yyyy");
            lblIsDetained.Text = clsDetainedLicense.IsLicenseDetained(_License.LicenseID) ? "Yes" : "No";
            // 1. Check if the ImagePath is valid (Not null and Not empty)
            if (_Driver.PersonInfo.ImagePath != null && _Driver.PersonInfo.ImagePath != "")
            {
                // Try to load the image from the path
                if (System.IO.File.Exists(_Driver.PersonInfo.ImagePath))
                {
                    pbPersonImage.ImageLocation = _Driver.PersonInfo.ImagePath;
                }
                else
                {
                    // File not found (deleted?), fallback to default
                    MessageBox.Show("Could not find this image: = " + _Driver.PersonInfo.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _LoadDefaultImage();
                }
            }
            else
            {
                // 2. If no path is stored, load the default image
                _LoadDefaultImage();
            }

            // ---------------------------------------------------------

            // Helper Method to pick the correct default image
            void _LoadDefaultImage()
            {
                // Assuming Gender: 0 = Male, 1 = Female
                if (_Driver.PersonInfo.Gendor == 0)
                    pbPersonImage.Image = Properties.Resources.Male_512;
                else
                    pbPersonImage.Image = Properties.Resources.Female_512;
            }
        }
        public void SetLicenseID(int LicenseID)
        {
            _LicenseID = LicenseID;
             _License = clsLicense.Find(_LicenseID);
            if(_License != null)
            _Driver = clsDriver.Find(_License.DriverID);
            

            _FillDetails();
        }
    }
}

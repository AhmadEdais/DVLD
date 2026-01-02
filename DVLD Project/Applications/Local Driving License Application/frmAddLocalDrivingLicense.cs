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

namespace DVLD_Project.Local_Driving_Licenses
{
    public partial class frmAddLocalDrivingLicense : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        int _LocalDrivingLicenseApplicationID = -1, _PersonID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        
        public frmAddLocalDrivingLicense(int LocalDrivingLicenseID = -1)
        {
            InitializeComponent();
            if (LocalDrivingLicenseID == -1)
            {
                btnSave.Enabled = false;
                btnNext.Enabled = false;
                _Mode = enMode.AddNew;
            }
            else
            {
                _LocalDrivingLicenseApplicationID = LocalDrivingLicenseID;
                
                _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
                _PersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                _Mode = enMode.Update;
            }
                _LoadInfo();
            ctrlFindPerson1.OnPersonSelected += DataBackEvent;
            ctrlFindPerson1.SelectMode(_PersonID);
        }
        private void DataBackEvent(int PersonID)
        {
            _PersonID = PersonID;

            // Optional: Enable the Next/Save button now that we have a person
            btnNext.Enabled = true;

            // Debugging check (optional)
            // MessageBox.Show("Person ID Received: " + PersonID);
        }
        private void _FillClassesComboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {

                cbAllLicenseClasses.Items.Add(row["ClassName"]);

            }
        }
        private void _LoadInfo()
        {
            _FillClassesComboBox();
            lblApplicationFees.Text = clsApplicationTypes.GetApplicationFees(1).ToString("F2");

            if (_LocalDrivingLicenseApplicationID == -1)
            {
                cbAllLicenseClasses.SelectedIndex = 2;
                lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblCreatedByUser.Text = Global.CurrentUser.UserName;

            }

            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                cbAllLicenseClasses.SelectedIndex = _LocalDrivingLicenseApplication.LicenseClassID - 1;
                lblDLApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString("dd/MM/yyyy");
                lblCreatedByUser.Text = _LocalDrivingLicenseApplication.CreatedByUserInfo.UserName;
            }

        }
        private void _SetInfo()
        {
            // --- Common Properties (Update & AddNew) ---
            _LocalDrivingLicenseApplication.LicenseClassID = cbAllLicenseClasses.SelectedIndex +1;
            _LocalDrivingLicenseApplication.ApplicantPersonID = _PersonID;

            // --- AddNew Specific Properties ---
            if (_Mode == enMode.AddNew)
            {
                _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
                _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
                _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
                _LocalDrivingLicenseApplication.PaidFees = decimal.Parse(lblApplicationFees.Text);
                _LocalDrivingLicenseApplication.CreatedByUserID = Global.CurrentUser.UserID;
                _LocalDrivingLicenseApplication.ApplicationTypeID = 1; // Local Driving License
            }
            // Note: In Update mode, we usually don't update Date, Creator, or Status here.
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if(clsLocalDrivingLicenseApplication.IsApplicationExist(_PersonID
                ,cbAllLicenseClasses.SelectedIndex+1
                ,(int)_LocalDrivingLicenseApplication.ApplicationStatus))
            {
                MessageBox.Show("An application for this person with the selected license class already exists.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(clsLicense.IsLicenseExistByPersonID(_PersonID, cbAllLicenseClasses.SelectedIndex + 1))
            {
                MessageBox.Show("This person already holds a license for the selected class.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _SetInfo();
            if (_LocalDrivingLicenseApplication.Save())
            {
                // Update the label and variables
                lblDLApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;

                // Change mode to Update so subsequent clicks behave correctly
                _Mode = enMode.Update;

                // Update Title Title
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Application Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // Check if the user is trying to switch to the "Login Info" tab (Index 1)
            if (e.TabPageIndex == 1)
            {
                if (_Mode == enMode.AddNew) // Check if a person is selected
                {
                    if (_PersonID == -1)
                    {
                        // If no person selected, CANCEL the switch
                        e.Cancel = true;

                        // Show a friendly message
                        MessageBox.Show("Please Select a Person first.", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Optional: Move focus back to the filter box
                        // ctrlFindPerson1.FilterFocus();
                    }
                }
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                // 1. Validation: Prevent moving to the next tab if no person is selected
                if (_PersonID == -1)
                {
                    MessageBox.Show("Please Select a Person first.", "Select Person",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Optional: specific focus method if you have one
                    // ctrlFindPerson1.FilterFocus(); 
                    return;
                }
            }
            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
        }
    }
}

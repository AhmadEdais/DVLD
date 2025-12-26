using ConsoleApp1;
using DVLD_Buisness;
using DVLDBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLD_Project.Test_Appointments
{
    public partial class frmAddEditTestAppointment : Form
    {
        private int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        bool _isLocked = false;
        private clsTestTypes.enTestTypeID _enTestTypeID = clsTestTypes.enTestTypeID.Vision;

        private clsTestAppointments _TestAppointment;

        public frmAddEditTestAppointment(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypeID enTestTypeID, int TestAppointmentID = -1,bool isLocked = false)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _enTestTypeID = enTestTypeID;
            _TestAppointmentID = TestAppointmentID;
            _isLocked = isLocked;
        }

        private void frmAddEditTestAppointment_Load(object sender, EventArgs e)
        {
            _SetSettings(_enTestTypeID);

            // 1. Initialize the Object FIRST (Crucial Step!)
            if (_TestAppointmentID == -1)
            {
                dtpDate.MinDate = DateTime.Now;
                // ============ NEW MODE ============
                _TestAppointment = new clsTestAppointments();
                _TestAppointment.RetakeTestApplicationID = -1; // Default for new appointments
            }
            else
            {

                // ============ UPDATE MODE ============
                if(_isLocked)
                {
                    lblLockedMessage.Text = "Person already sat for the test, appointment is locked";
                    dtpDate.Enabled = false;
                    btnSave.Enabled = false;
                }
                _TestAppointment = clsTestAppointments.Find(_TestAppointmentID);
                if (_TestAppointment == null)
                {
                    MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID);
                    this.Close();
                    return;
                }
                _TestAppointment.RetakeTestApplicationID = _TestAppointment.RetakeTestApplicationID; // Retake Info from DB

            }

            // 2. Now it is safe to fill the form because _TestAppointment exists
            _FillFormData();
        }

        private void _FillFormData()
        {// --- 1. Fixed Header Info ---
            lblDLDAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblClassName.Text = clsLocalDrivingLicenseApplication.GetClassNameByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            // Get Name
            lblName.Text = clsLocalDrivingLicenseApplication.GetApplicantFullNameByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            // Get Trials
            int trials = clsLocalDrivingLicenseApplication.GetTestTrials(_LocalDrivingLicenseApplicationID, _enTestTypeID);
            lblTrial.Text = trials.ToString();

            // Get Standard Test Fees (e.g., 10.00)
            lblFees.Text = clsTestTypes.GetTestTypeFees(_enTestTypeID).ToString();
            dtpDate.Value = DateTime.Now;

            // --- 2. Specific Logic for New vs Update ---
            if (_TestAppointmentID == -1)
            {
                // ================== NEW MODE ==================
                lblRTestAppID.Text = "N/A";

                // LOGIC: If Trials > 0, this is a Retake!
                if (trials > 0)
                {
                    lblRAppFees.Text = clsApplicationTypes.GetApplicationFees(7).ToString(); // Assuming ID 7 = Retake Test
                    gbRetakeTestInfo.Enabled = true;
                    lblTestAppointmentName.Text = "Schedule Retake Test";
                }
                else
                {
                    lblRAppFees.Text = "0";
                    gbRetakeTestInfo.Enabled = false;
                    lblTestAppointmentName.Text = "Schedule Test";
                }
            }
            else
            {
                // ================== UPDATE MODE ==================

                // Check if saved record has Retake info
                if (_TestAppointment.RetakeTestApplicationID == -1)
                {
                    lblRAppFees.Text = "0";
                    lblRTestAppID.Text = "N/A";
                    gbRetakeTestInfo.Enabled = false;
                }
                else
                {
                    lblRAppFees.Text = clsApplicationTypes.GetApplicationFees(7).ToString();
                    lblRTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                    gbRetakeTestInfo.Enabled = true;
                }
            }

            // --- 3. Calculate Total Fees (Standard + Retake) ---
            lblTotalFees.Text = (decimal.Parse(lblFees.Text) + decimal.Parse(lblRAppFees.Text)).ToString();
        
        }

        private void _SetSettings(clsTestTypes.enTestTypeID enTestTypeID)
        {
            switch (enTestTypeID)
            {
                case clsTestTypes.enTestTypeID.Vision:
                    pbAppointment.Image = DVLD_Project.Properties.Resources.Vision_512;
                    lblTestAppointmentName.Text = "Vision Test Appointment";
                    gbTestInfo.Text = "Vision Test Appointment"; 
                    this.Text = "Vision Test Appointment";
                    break;
                case clsTestTypes.enTestTypeID.Written:
                    pbAppointment.Image = DVLD_Project.Properties.Resources.Written_Test_512;
                    lblTestAppointmentName.Text = "Written Test Appointment";
                    gbTestInfo.Text = "Written Test Appointment";
                    this.Text = "Written Test Appointment";
                    break;
                case clsTestTypes.enTestTypeID.Road:
                    pbAppointment.Image = DVLD_Project.Properties.Resources.driving_test_512;
                    lblTestAppointmentName.Text = "Street Test Appointment";
                    gbTestInfo.Text = "Street Test Appointment";
                    this.Text = "Street Test Appointment";
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Update object values from UI before saving
            _TestAppointment.TestTypeID = (int)_enTestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpDate.Value;
            _TestAppointment.PaidFees = decimal.Parse(lblFees.Text);
            _TestAppointment.CreatedByUserID = Global.CurrentUser.UserID; // Make sure you have Global User
            _TestAppointment.IsLocked = false; // Default to false, can be changed later

            if (_TestAppointment.Save())
            {
                MessageBox.Show("Test Appointment Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
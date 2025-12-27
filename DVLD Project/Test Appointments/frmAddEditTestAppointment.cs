using ConsoleApp1;
using DVLD_Buisness;
using DVLD_Project.Properties;
using DVLDBusinessLayer;
using System;
using System.Windows.Forms;
using static ConsoleApp1.clsTestTypes;

namespace DVLD_Project.Test_Appointments
{
    public partial class frmAddEditTestAppointment : Form
    {
        private int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        bool _isLocked = false;
        private clsTestTypes.enTestTypeID _enTestTypeID = clsTestTypes.enTestTypeID.Vision;

        private clsTestAppointments _TestAppointment;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;


        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmAddEditTestAppointment(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypeID enTestTypeID, int TestAppointmentID = -1,bool isLocked = false)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _enTestTypeID = enTestTypeID;
            _TestAppointmentID = TestAppointmentID;
            _isLocked = isLocked;
        }
        //public clsTestTypes.enTestTypeID TestTypeID
        //{
        //    get
        //    {
        //        return _TestTypeID;
        //    }
        //    set
        //    {
        //        _TestTypeID = value;

        //        switch (_TestTypeID)
        //        {

        //            case clsTestTypes.enTestTypeID.Vision:
        //                {
        //                    gbTestInfo.Text = "Vision Test";
        //                    pbAppointment.Image = Resources.Vision_512;
        //                    break;
        //                }

        //            case clsTestTypes.enTestTypeID.Written:
        //                {
        //                    gbTestInfo.Text = "Written Test";
        //                    pbAppointment.Image = Resources.Written_Test_512;
        //                    break;
        //                }
        //            case clsTestTypes.enTestTypeID.Road:
        //                {
        //                    gbTestInfo.Text = "Street Test";
        //                    pbAppointment.Image = Resources.driving_test_512;
        //                    break;


        //                }
        //        }
        //    }
        //}
        private void _LoadInfo()
        {
            //if no appointment id this means AddNew mode otherwise it's update mode.
            if (_TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            //decide if the createion mode is retake test or not based if the person attended this test before
            int trails = clsLocalDrivingLicenseApplication.GetTestTrials(_LocalDrivingLicenseApplicationID, _enTestTypeID);
            if (trails > 0)

                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;


            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRAppFees.Text = clsApplicationTypes.GetApplicationFees(7).ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTestAppointmentName.Text = "Schedule Retake Test";
                lblRTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTestAppointmentName.Text = "Schedule Test";
                lblRAppFees.Text = "0";
                lblRTestAppID.Text = "N/A";
            }

            lblDLDAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblClassName.Text = clsLocalDrivingLicenseApplication.GetClassNameByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
            lblName.Text = clsLocalDrivingLicenseApplication.GetApplicantFullNameByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            //this will show the trials for this test before  
            lblTrial.Text = trails.ToString();


            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestTypes.GetTestTypeFees(_enTestTypeID).ToString();
                dtpDate.MinDate = DateTime.Now;
                lblDLDAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointments();
            }

            else
            {

                if (!_LoadTestAppointmentData())
                    return;
            }


            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRAppFees.Text)).ToString();


        }
      
        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointments.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            //we compare the current date with the appointment date to set the min date.
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _TestAppointment.AppointmentDate;

            dtpDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRAppFees.Text = "0";
                lblRTestAppID.Text = "N/A";
            }
            else
            {
                lblRAppFees.Text = clsApplicationTypes.GetApplicationFees(7).ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTestAppointmentName.Text = "Schedule Retake Test";
                lblRTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }
            return true;
        }
       
       
        private bool _HandleRetakeApplication()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                //incase the mode is add new and creation mode is retake test we should create a seperate application for it.
                //then we linke it with the appointment.

                //First Create Applicaiton 
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationTypes.GetApplicationFees((int)clsApplication.enApplicationType.RetakeTest);
                Application.CreatedByUserID = Global.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
        }
        private void frmAddEditTestAppointment_Load(object sender, EventArgs e)
        {
            //_SetSettings(_enTestTypeID);

            //// 1. Initialize the Object FIRST (Crucial Step!)
            //if (_TestAppointmentID == -1)
            //{
            //    dtpDate.MinDate = DateTime.Now;
            //    // ============ NEW MODE ============
            //    _TestAppointment = new clsTestAppointments();
            //    _TestAppointment.RetakeTestApplicationID = -1; // Default for new appointments
            //}
            //else
            //{

            //    // ============ UPDATE MODE ============
            //    if(_isLocked)
            //    {
            //        lblLockedMessage.Text = "Person already sat for the test, appointment is locked";
            //        dtpDate.Enabled = false;
            //        btnSave.Enabled = false;
            //    }
            //    _TestAppointment = clsTestAppointments.Find(_TestAppointmentID);
            //    if (_TestAppointment == null)
            //    {
            //        MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID);
            //        this.Close();
            //        return;
            //    }
            //    _TestAppointment.RetakeTestApplicationID = _TestAppointment.RetakeTestApplicationID; // Retake Info from DB

            //}

            //// 2. Now it is safe to fill the form because _TestAppointment exists
            //_FillFormData();
            _LoadInfo();
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
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = (int)_enTestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpDate.Value;
            _TestAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);
            _TestAppointment.CreatedByUserID = Global.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
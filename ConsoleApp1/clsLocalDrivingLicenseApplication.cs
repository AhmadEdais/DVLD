using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        // Optional: Add a property for License Class Name if you have a clsLicenseClass
        // public string LicenseClassName { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            this.Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            // 1. Initialize Base Class Properties
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = (enApplicationStatus)ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;

            // 2. Initialize Base Class Composition Objects
            this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

            // 3. Initialize Child Properties
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;

            this.Mode = enMode.Update;
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            // 1. Get the Local specific info (IDs)
            if (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(
                LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                // 2. Get the Base Application info using the ID we just found
                // We reuse the logic from clsApplication
                clsApplication BaseApp = clsApplication.FindBaseApplication(ApplicationID);

                if (BaseApp != null)
                {
                    // 3. Return the full object
                    return new clsLocalDrivingLicenseApplication(
                        LocalDrivingLicenseApplicationID, ApplicationID,
                        BaseApp.ApplicantPersonID, BaseApp.ApplicationDate, BaseApp.ApplicationTypeID,
                        (byte)BaseApp.ApplicationStatus, BaseApp.LastStatusDate,
                        BaseApp.PaidFees, BaseApp.CreatedByUserID, LicenseClassID);
                }
            }

            return null;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(
                this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(
                this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public bool Save()
        {
            // 1. Save the Base Application First
            // This will create the ApplicationID if it's new
            base.Mode = (clsApplication.enMode)this.Mode;
            if (!base.Save())
                return false;

            // 2. Save the Local Application
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }
        public static bool IsApplicationExist(int PersonID, int LicenseClassID, int ApplicationStatus)
        {
            // Note: I added LicenseClassID because usually you check if a person 
            // has an active application for a SPECIFIC class, not just "any" application.
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledApplication(PersonID, LicenseClassID, ApplicationStatus);
        }

        public static bool Delete(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        // Optional: Instance Method (if you already have the object loaded)
        public byte GetPassedTestCount()
        {
            return GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public static string GetClassNameByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetClassNameByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
        }

        public static decimal GetClassFeesByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetClassFeesByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
        }
        public static byte GetTestTrials(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypeID TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.GetTestTrials(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static string GetApplicantFullNameByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetApplicantFullNameByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
        }
        //// 1. Property to get the Class Name easily from an instance
        //public string LicenseClassName
        //{
        //    get
        //    {
        //        return clsLocalDrivingLicenseApplicationDataAccess.GetLicenseClassNameByID(this.LocalDrivingLicenseApplicationID);
        //    }
        //}

        // 2. Or, if you prefer a Static Method to call without loading the object
        public static string GetLicenseClassNameByID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetLicenseClassNameByID(LocalDrivingLicenseApplicationID);
        }
        // 1. Static Method (Call without loading the object)
        public static string GetApplicantFullNameByID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetApplicantFullNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
        }
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypeID TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static int GetApplicationIDByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetApplicationIDByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
        }
        public static int GetApplicantPersonIDByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetApplicantPersonIDByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
        }
        public static int GetLicenseClassIDByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetLicenseClassIDByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
        }
        public static int GetLicenseIDByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetLicenseIDByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
        }

        // Optional: A helper property to call directly on the object
        public int GetActiveLicenseID()
        {
            return clsLocalDrivingLicenseApplicationData.GetLicenseIDByLocalDrivingLicenseAppID(this.LocalDrivingLicenseApplicationID);
        }

    }

}

using DVLD_DataAccessLayer;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class clsApplication
    {
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enum enMode { AddNew = 0, Update = 1 };

            // Based on typical DVLD logic: 1=New, 2=Cancelled, 3=Completed
            public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

            public enMode Mode = enMode.AddNew;

            public int ApplicationID { get; set; }
            public int ApplicantPersonID { get; set; }
            public DateTime ApplicationDate { get; set; }
            public int ApplicationTypeID { get; set; }
            public enApplicationStatus ApplicationStatus { get; set; }
            public string StatusText
            {
                get
                {
                    switch (ApplicationStatus)
                    {
                        case enApplicationStatus.New: return "New";
                        case enApplicationStatus.Cancelled: return "Cancelled";
                        case enApplicationStatus.Completed: return "Completed";
                        default: return "Unknown";
                    }
                }
            }
            public DateTime LastStatusDate { get; set; }
            public decimal PaidFees { get; set; }
            public int CreatedByUserID { get; set; }

            // Composition: Access the Person Info easily
            public clsPerson ApplicantPersonInfo;

            // Composition: Access the User Info easily
            public clsUser CreatedByUserInfo;

            public clsApplication()
            {
                this.ApplicationID = -1;
                this.ApplicantPersonID = -1;
                this.ApplicationDate = DateTime.Now;
                this.ApplicationTypeID = -1;
                this.ApplicationStatus = enApplicationStatus.New;
                this.LastStatusDate = DateTime.Now;
                this.PaidFees = 0;
                this.CreatedByUserID = -1;
                this.Mode = enMode.AddNew;
            }

            private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
                int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate,
                decimal PaidFees, int CreatedByUserID)
            {
                this.ApplicationID = ApplicationID;
                this.ApplicantPersonID = ApplicantPersonID;
                this.ApplicationDate = ApplicationDate;
                this.ApplicationTypeID = ApplicationTypeID;
                this.ApplicationStatus = (enApplicationStatus)ApplicationStatus;
                this.LastStatusDate = LastStatusDate;
                this.PaidFees = PaidFees;
                this.CreatedByUserID = CreatedByUserID;

                // Load Composition Objects
                this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
                this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

                this.Mode = enMode.Update;
            }

            public static clsApplication FindBaseApplication(int ApplicationID)
            {
                int ApplicantPersonID = -1;
                DateTime ApplicationDate = DateTime.Now;
                int ApplicationTypeID = -1;
                byte ApplicationStatus = 1;
                DateTime LastStatusDate = DateTime.Now;
                decimal PaidFees = 0;
                int CreatedByUserID = -1;

                if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID,
                    ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus,
                    ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
                {
                    return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                        ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
                }
                else
                {
                    return null;
                }
            }

            private bool _AddNewApplication()
            {
                this.ApplicationID = clsApplicationData.AddNewApplication(
                    this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                    (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

                return (this.ApplicationID != -1);
            }

            private bool _UpdateApplication()
            {
                return clsApplicationData.UpdateApplication(
                    this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                    (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            }

            public bool Save()
            {
                switch (Mode)
                {
                    case enMode.AddNew:
                        if (_AddNewApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:
                        return _UpdateApplication();
                }

                return false;
            }

            public static bool Delete(int ApplicationID)
            {
                return clsApplicationData.DeleteApplication(ApplicationID);
            }

            public static bool IsApplicationExist(int ApplicationID)
            {
                return clsApplicationData.IsApplicationExist(ApplicationID);
            }

            // Helper method to cancel application
            public bool Cancel()
            {
                return clsApplicationData.UpdateStatus(this.ApplicationID, 2); // Assuming 2 is Cancelled
            }

            // Helper method to set complete
            public bool SetComplete()
            {
                return clsApplicationData.UpdateStatus(this.ApplicationID, 3); // Assuming 3 is Completed
            }
        // 3. Generic Status Update (If you need to set it to 1 or anything else manually)
            public static bool UpdateStatus(int ApplicationID , int NewStatus)
            {
                return clsApplicationData.UpdateStatus(ApplicationID, (short)NewStatus);
            }
    }
    
}

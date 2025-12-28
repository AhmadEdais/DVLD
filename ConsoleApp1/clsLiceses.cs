using System;
using System.Data;

public class clsLicense
{
    public enum enMode { AddNew = 0, Update = 1 }
    public enMode Mode = enMode.AddNew;

    public int LicenseID { set; get; }
    public int ApplicationID { set; get; }
    public int DriverID { set; get; }
    public int LicenseClass { set; get; }
    public DateTime IssueDate { set; get; }
    public DateTime ExpirationDate { set; get; }
    public string Notes { set; get; }
    public decimal PaidFees { set; get; }
    public bool IsActive { set; get; }
    public byte IssueReason { set; get; }
    public int CreatedByUserID { set; get; }

    public clsLicense()
    {
        this.LicenseID = -1;
        this.ApplicationID = -1;
        this.DriverID = -1;
        this.LicenseClass = -1;
        this.IssueDate = DateTime.Now;
        this.ExpirationDate = DateTime.Now;
        this.Notes = "";
        this.PaidFees = 0;
        this.IsActive = true;
        this.IssueReason = 1;
        this.CreatedByUserID = -1;

        Mode = enMode.AddNew;
    }

    private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
        DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees,
        bool IsActive, byte IssueReason, int CreatedByUserID)
    {
        this.LicenseID = LicenseID;
        this.ApplicationID = ApplicationID;
        this.DriverID = DriverID;
        this.LicenseClass = LicenseClass;
        this.IssueDate = IssueDate;
        this.ExpirationDate = ExpirationDate;
        this.Notes = Notes;
        this.PaidFees = PaidFees;
        this.IsActive = IsActive;
        this.IssueReason = IssueReason;
        this.CreatedByUserID = CreatedByUserID;

        Mode = enMode.Update;
    }

    public static clsLicense Find(int LicenseID)
    {
        int ApplicationID = -1;
        int DriverID = -1;
        int LicenseClass = -1;
        DateTime IssueDate = DateTime.Now;
        DateTime ExpirationDate = DateTime.Now;
        string Notes = "";
        decimal PaidFees = 0;
        bool IsActive = true;
        byte IssueReason = 1;
        int CreatedByUserID = -1;

        if (clsLicenseDataAccess.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID,
            ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
            ref IsActive, ref IssueReason, ref CreatedByUserID))
        {
            return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
        }
        else
        {
            return null;
        }
    }

    private bool _AddNewLicense()
    {
        this.LicenseID = clsLicenseDataAccess.AddNewLicense(this.ApplicationID, this.DriverID,
            this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
            this.IsActive, this.IssueReason, this.CreatedByUserID);

        return (this.LicenseID != -1);
    }
    public static bool IsLicenseExistByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, int LicenseClassID)
    {
        return clsLicenseDataAccess.IsLicenseExistByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID, LicenseClassID);
    }
    public static DataTable GetDriverLicenses(int DriverID)
    {
        return clsLicenseDataAccess.GetDriverLicenses(DriverID);
    }
    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewLicense())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:
                return false; // Implement _UpdateLicense similar to _AddNewLicense
        }
        return false;
    }
}
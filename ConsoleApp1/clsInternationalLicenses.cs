using System;
using System.Data;
using System.Runtime.CompilerServices;

public class clsInternationalLicense
{
    // Enums to handle the mode of the object (Add New or Update)
    public enum enMode { AddNew = 0, Update = 1 };
    public enMode Mode = enMode.AddNew;

    // Properties corresponding to the Database Fields
    public int InternationalLicenseID { get; set; }
    public int ApplicationID { get; set; }
    public int DriverID { get; set; }
    public int IssuedUsingLocalLicenseID { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public int CreatedByUserID { get; set; }

    // Constructor 1: Default Constructor for Adding New
    public clsInternationalLicense()
    {
        this.InternationalLicenseID = -1;
        this.ApplicationID = -1;
        this.DriverID = -1;
        this.IssuedUsingLocalLicenseID = -1;
        this.IssueDate = DateTime.Now;
        this.ExpirationDate = DateTime.Now;
        this.IsActive = true;
        this.CreatedByUserID = -1;

        this.Mode = enMode.AddNew;
    }

    // Constructor 2: Private Constructor for Loading Data (Find)
    private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
        int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate,
        bool IsActive, int CreatedByUserID)
    {
        this.InternationalLicenseID = InternationalLicenseID;
        this.ApplicationID = ApplicationID;
        this.DriverID = DriverID;
        this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
        this.IssueDate = IssueDate;
        this.ExpirationDate = ExpirationDate;
        this.IsActive = IsActive;
        this.CreatedByUserID = CreatedByUserID;

        this.Mode = enMode.Update;
    }

    // Method to Find an International License by ID
    public static clsInternationalLicense Find(int InternationalLicenseID)
    {
        int ApplicationID = -1;
        int DriverID = -1;
        int IssuedUsingLocalLicenseID = -1;
        DateTime IssueDate = DateTime.Now;
        DateTime ExpirationDate = DateTime.Now;
        bool IsActive = true;
        int CreatedByUserID = -1;

        // Assuming clsInternationalLicenseData.GetInternationalLicenseInfoByID returns true if found
        if (clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID,
            ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate,
            ref IsActive, ref CreatedByUserID))
        {
            return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,
                IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
        }
        else
        {
            return null;
        }
    }

    // Method to get all international licenses
    public static DataTable GetAllInternationalLicenses()
    {
        return clsInternationalLicenseData.GetAllInternationalLicenses();
    }

    // Method to get licenses specifically for a driver (The one you asked for)
    public static DataTable GetDriverInternationalLicenses(int DriverID)
    {
        return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
    }

    // Internal method to Add New License
    private bool _AddNewInternationalLicense()
    {
        // AddNewInternationalLicense should return the new ID if successful
        this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(
            this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
            this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

        return (this.InternationalLicenseID != -1);
    }

    // Internal method to Update License
    private bool _UpdateInternationalLicense()
    {
        return clsInternationalLicenseData.UpdateInternationalLicense(
            this.InternationalLicenseID, this.ApplicationID, this.DriverID,
            this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate,
            this.IsActive, this.CreatedByUserID);
    }

    // Save Method: Decides whether to Add or Update
    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewInternationalLicense())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:
                return _UpdateInternationalLicense();
        }

        return false;
    }

    // Helper methods
    public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
    {
        return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
    }
}
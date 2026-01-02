using System;
using System.Data;

public class clsDetainedLicense
{
    public enum enMode { AddNew = 0, Update = 1 }
    public enMode Mode = enMode.AddNew;

    public int DetainID { set; get; }
    public int LicenseID { set; get; }
    public DateTime DetainDate { set; get; }
    public decimal FineFees { set; get; }
    public int CreatedByUserID { set; get; }
    public bool IsReleased { set; get; }
    public DateTime? ReleaseDate { set; get; }
    public int? ReleasedByUserID { set; get; }
    public int? ReleaseApplicationID { set; get; }

    // Optional: Add Creator/Releaser Names or User Info objects here if needed.

    public clsDetainedLicense()
    {
        this.DetainID = -1;
        this.LicenseID = -1;
        this.DetainDate = DateTime.Now;
        this.FineFees = 0;
        this.CreatedByUserID = -1;
        this.IsReleased = false;
        this.ReleaseDate = null;
        this.ReleasedByUserID = null;
        this.ReleaseApplicationID = null;

        Mode = enMode.AddNew;
    }

    private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
        decimal FineFees, int CreatedByUserID, bool IsReleased,
        DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
    {
        this.DetainID = DetainID;
        this.LicenseID = LicenseID;
        this.DetainDate = DetainDate;
        this.FineFees = FineFees;
        this.CreatedByUserID = CreatedByUserID;
        this.IsReleased = IsReleased;
        this.ReleaseDate = ReleaseDate;
        this.ReleasedByUserID = ReleasedByUserID;
        this.ReleaseApplicationID = ReleaseApplicationID;

        Mode = enMode.Update;
    }

    public static clsDetainedLicense Find(int DetainID)
    {
        int LicenseID = -1;
        DateTime DetainDate = DateTime.Now;
        decimal FineFees = 0;
        int CreatedByUserID = -1;
        bool IsReleased = false;
        DateTime? ReleaseDate = null;
        int? ReleasedByUserID = null;
        int? ReleaseApplicationID = null;

        if (clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID,
            ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
        {
            return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
        }
        else
        {
            return null;
        }
    }

    public static clsDetainedLicense FindByLicenseID(int LicenseID)
    {
        int DetainID = -1;
        DateTime DetainDate = DateTime.Now;
        decimal FineFees = 0;
        int CreatedByUserID = -1;
        bool IsReleased = false;
        DateTime? ReleaseDate = null;
        int? ReleasedByUserID = null;
        int? ReleaseApplicationID = null;

        if (clsDetainedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID,
            ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
        {
            return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
        }
        else
        {
            return null;
        }
    }

    public static DataTable GetAllDetainedLicenses()
    {
        return clsDetainedLicenseData.GetAllDetainedLicenses();
    }

    public static bool IsLicenseDetained(int LicenseID)
    {
        return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
    }

    private bool _AddNewDetainedLicense()
    {
        this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(
            this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

        return (this.DetainID != -1);
    }

    private bool _UpdateDetainedLicense()
    {
        return clsDetainedLicenseData.UpdateDetainedLicense(
            this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewDetainedLicense())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:
                return _UpdateDetainedLicense();
        }

        return false;
    }

    public bool Release(int ReleasedByUserID, int ReleaseApplicationID)
    {
        return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
    }
}
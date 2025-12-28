using System;

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
    public DateTime ReleaseDate { set; get; }
    public int ReleasedByUserID { set; get; }
    public int ReleaseApplicationID { set; get; }

    public clsDetainedLicense()
    {
        this.DetainID = -1;
        this.LicenseID = -1;
        this.DetainDate = DateTime.Now;
        this.FineFees = 0;
        this.CreatedByUserID = -1;
        this.IsReleased = false;
        this.ReleaseDate = DateTime.MinValue;
        this.ReleasedByUserID = -1;
        this.ReleaseApplicationID = -1;

        Mode = enMode.AddNew;
    }

    private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees,
        int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
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
        DateTime ReleaseDate = DateTime.MinValue;
        int ReleasedByUserID = -1;
        int ReleaseApplicationID = -1;

        if (clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate,
            ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
        {
            return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
        }
        else
        {
            return null;
        }
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
                return (this.DetainID != -1);

            case enMode.Update:
                // We typically only update detained licenses when we Release them
                // You can add a general Update method if needed, but 'Release' is usually enough.
                return false;
        }
        return false;
    }

    public bool Release(int ReleasedByUserID, int ReleaseApplicationID)
    {
        return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
    }
    public static bool IsLicenseDetained(int LicenseID)
    {
        return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
    }
}
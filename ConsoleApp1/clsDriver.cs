using DVLDBusinessLayer;
using System;
using System.Data;

public class clsDriver
{
    public enum enMode { AddNew = 0, Update = 1 }
    public enMode Mode = enMode.AddNew;

    public int DriverID { set; get; }
    public int PersonID { set; get; }
    public int CreatedByUserID { set; get; }
    public DateTime CreatedDate { set; get; }

    // Lazy Loading for Person Info
    public clsPerson PersonInfo { set; get; }

    public clsDriver()
    {
        this.DriverID = -1;
        this.PersonID = -1;
        this.CreatedByUserID = -1;
        this.CreatedDate = DateTime.Now;
        Mode = enMode.AddNew;
    }

    private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
    {
        this.DriverID = DriverID;
        this.PersonID = PersonID;
        this.CreatedByUserID = CreatedByUserID;
        this.CreatedDate = CreatedDate;
        this.PersonInfo = clsPerson.Find(PersonID); // Load linked Person data
        Mode = enMode.Update;
    }

    public static clsDriver Find(int DriverID)
    {
        int PersonID = -1;
        int CreatedByUserID = -1;
        DateTime CreatedDate = DateTime.Now;

        if (clsDriverDataAccess.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
        {
            return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
        }
        else
        {
            return null;
        }
    }

    public static clsDriver FindByPersonID(int PersonID)
    {
        int DriverID = -1;
        int CreatedByUserID = -1;
        DateTime CreatedDate = DateTime.Now;

        if (clsDriverDataAccess.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
        {
            return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
        }
        else
        {
            return null;
        }
    }

    public static DataTable GetAllDrivers()
    {
        return clsDriverDataAccess.GetAllDrivers();
    }
    public static int GetDriverIDByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
    {
        return clsDriverDataAccess.GetDriverIDByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);
    }

    private bool _AddNewDriver()
    {
        this.DriverID = clsDriverDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID);
        return (this.DriverID != -1);
    }

    private bool _UpdateDriver()
    {
        return clsDriverDataAccess.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewDriver())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            case enMode.Update:
                return _UpdateDriver();
        }
        return false;
    }
}
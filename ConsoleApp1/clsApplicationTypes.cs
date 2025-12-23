using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationTypeFee { get; set; }
        public clsApplicationTypes()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationTypeFee = 0;
            this.Mode = enMode.AddNew;

        }
        public clsApplicationTypes(int applicationTypeID, string applicationTypeTitle, decimal applicationTypeFee)
        {
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationTypeFee = applicationTypeFee;
            this.Mode = enMode.Update;
        }
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }
        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationTypeFee);
        }
        public static clsApplicationTypes FindByApplicationTypeID(int ApplicationTypeID)
        {
            
            string ApplicationTypeTitle = "";
            decimal ApplicationFees = 0;

            // Call the Data Layer
            if (clsApplicationTypesData.GetApplicationTypeByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees)) 
            {
                // Return the object using the constructor we made above
                return new clsApplicationTypes(ApplicationTypeID,ApplicationTypeTitle,ApplicationFees);
            }
            else
            {
                return null;
            }
        }
        // Inside clsApplicationTypes class

        public static decimal GetApplicationFees(int ApplicationTypeID)
        {
            return clsApplicationTypesData.GetApplicationFees(ApplicationTypeID);
        }
        public static string GetApplicationTypeTitle(int ApplicationTypeID)
        {
            return clsApplicationTypesData.GetApplicationTypeTitle(ApplicationTypeID);
        }
        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    //if (_AddNewUser())
                    //{

                    //    Mode = enMode.Update;
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}

                case enMode.Update:

                    return _UpdateApplicationType();


            }
            return false;

        }
    }
}

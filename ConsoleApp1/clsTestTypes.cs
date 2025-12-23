using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class clsTestTypes
    {
        public enum enTestTypeID { Vision = 1, Written = 2, Road = 3 };
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestTypesID { get; set; }
        public string TestTypesTitle { get; set; }
        public string TestTypesDescription { get; set; }
        public decimal TestTypesFees { get; set; }
        public clsTestTypes()
        {
            TestTypesID = -1;
            TestTypesTitle = "";
            TestTypesDescription = "";
            TestTypesFees = 0;
            this.Mode = enMode.AddNew;
        }
        private clsTestTypes(int TestTypesID, string TestTypesTitle, string TestTypesDescription, decimal TestTypesFees)
        {
            this.TestTypesID = TestTypesID;
            this.TestTypesTitle = TestTypesTitle;
            this.TestTypesDescription = TestTypesDescription;
            this.TestTypesFees = TestTypesFees;
            this.Mode = enMode.Update;
        }
        private bool _UpdateTestTypes()
        {
            return clsTestTypesData.UpdateTestType(this.TestTypesID, this.TestTypesTitle, this.TestTypesDescription, this.TestTypesFees);
        }
        public static clsTestTypes FindByTestTypeID(int TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal TestTypeFee = 0;
            // Call the Data Layer
            if (clsTestTypesData.GetTestTypeByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFee))
            {
                // Return the object using the constructor we made above
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFee);
            }
            else
            {
                return null;
            }
        }
        public  static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
        public static decimal GetTestTypeFees(clsTestTypes.enTestTypeID TestTypeID)
        {
            return clsTestTypesData.GetTestTypeFees((int)TestTypeID);
        }
        public bool Save()
        {
            if (this.Mode == enMode.Update)
            {
                return this._UpdateTestTypes();
            }
            else
            {
                // For now, we only support Update mode
                throw new NotImplementedException("AddNew mode is not implemented yet.");
            }
        }
    }
}

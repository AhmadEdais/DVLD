using DVLD_DataAccessLayer;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int UserID { get; set; }
        public int PersonID { get; set; } // The link
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        // COMPOSITION: The User "has" Person info
        public clsPerson PersonInfo { get; set; }

        // Constructor
        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            this.Mode = enMode.AddNew;
        }
        private clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            this.Mode = enMode.Update;
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            this.PersonInfo = clsPerson.FindPersonByID(PersonID);
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);

        }
        private bool _UpdateUserInfo()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);

        }
        public static bool _DeleteUser(int UserID)
        {
             return clsUserData.DeleteUser(UserID);
        }
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            // Call the Data Layer
            if (clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                // Return the object using the constructor we made above
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByUsername(string UserName)
        {
            int UserID = -1;
            int PersonID = -1;
            string Password = "";
            bool IsActive = false;

            // Call the Data Layer (Using the overload for Username)
            if (clsUserData.GetUserInfoByUsername(UserName, ref UserID, ref PersonID, ref Password, ref IsActive))
            {
                // Return the object
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
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
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUserInfo();


            }
            return false;

        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool isUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }
        public static bool isUserExist(int PersonID)
        {
            return clsUserData.IsUserExist(PersonID);
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                // Return the object using the private constructor for Update mode
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
    }
}

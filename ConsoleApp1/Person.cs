using DVLD_DataAccessLayer;
using Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDBusinessLayer
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0,Update = 1};
        public enMode Mode = enMode.AddNew;
        public int ID {  get; set; }    
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return clsPersonData.GetFullName(this.ID);
            }
        }
        public string NationalNO { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor {  get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public int NatoinalityCountryID { get; set; }
        public string Address {  get; set; }
        public clsPerson()
        {
            ID = -1;
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            NationalNO = "";
            DateOfBirth = DateTime.Now;
            Gendor = -1;
            Email = "";
            Phone = "";
            ImagePath = "";
            Address = "";
            NatoinalityCountryID = -1;
            Mode = enMode.AddNew;
        }
        private clsPerson(int ID,string FirstName,string SecondName
            ,string ThirdName,string LastName
            ,string NationalNO, DateTime DateOfBirth
            ,short Gendor,string Email,string Phone
            ,string ImagePath,int NationalityCountryID,string Address)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNO = NationalNO;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Email = Email;
            this.Phone = Phone;
            this.ImagePath = ImagePath;
            this.NatoinalityCountryID = NationalityCountryID;
            this.Address = Address;
            Mode = enMode.Update;

        }
        private bool _AddNewPerson()
        {
            this.ID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNO
                                                , this.DateOfBirth, this.Gendor, this.Email, this.Phone, this.ImagePath
                                                , this.NatoinalityCountryID, this.Address);
            
            return(this.ID != -1);
        }
        private bool _UpdatePersonInfo()
        {
            return clsPersonData.UpdatePersonInfo(this.ID,this.FirstName,this.SecondName, this.ThirdName, this.LastName, this.NationalNO
                                                , this.DateOfBirth, this.Gendor, this.Email, this.Phone, this.ImagePath
                                                , this.NatoinalityCountryID, this.Address);
        }
        public static clsPerson Find(int ID)
        {
            string FirstName = "",
            SecondName = "",
            ThirdName = "",
            LastName = "",
            NationalNO = "",
            Email = "",
            Phone = "",
            ImagePath = "",
            Address = "";
            int NationalityCountryID = -1;
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = -1;

            if (clsPersonData.GetPersonInfoByID(ID,ref FirstName,ref SecondName,ref ThirdName,ref LastName,ref NationalNO, ref DateOfBirth
                ,ref Gendor,ref Email,ref Phone,ref ImagePath,ref NationalityCountryID, ref Address))
            {
                return new clsPerson(ID, FirstName, SecondName
                                        , ThirdName, LastName
                                        , NationalNO, DateOfBirth
                                        , Gendor, Email, Phone
                                        , ImagePath, NationalityCountryID, Address);
            }
            else
                return null;


        }
        public static clsPerson FindPersonByNationalNO(string NationalNO)
        {
            int ID = -1;
            string FirstName = "",
            SecondName = "",
            ThirdName = "",
            LastName = "",
            

            Email = "",
            Phone = "",
            ImagePath = "",
            Address = "";
            int NationalityCountryID = -1;
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = -1;

            if (clsPersonData.GetPersonInfoByNationalNO(ref ID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,  NationalNO, ref DateOfBirth
                , ref Gendor, ref Email, ref Phone, ref ImagePath, ref NationalityCountryID, ref Address))
            {
                return new clsPerson(ID, FirstName, SecondName
                                        , ThirdName, LastName
                                        , NationalNO, DateOfBirth
                                        , Gendor, Email, Phone
                                        , ImagePath, NationalityCountryID, Address);
            }
            else
                return null;


        }
        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePersonInfo();

                   
            }
            return false;

        }
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();

        }
        public static int GetPeopleCount(clsSettings.enPeopleFilterOptions enFilteredBy, string Text)
        {
            return clsPersonData.GetPeopleCount( enFilteredBy, Text);
        }
        public static DataTable GetAllPeopleInfoForManegePeople()
        {
            return clsPersonData.GetAllPeopleInfoForManagePeople();
        }
        public static DataTable GetFilterPeople(clsSettings.enPeopleFilterOptions enFilteredBy,string Text)
        {
            return clsPersonData.GetFilterdPeople(enFilteredBy, Text);
        }
        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersinExist(ID);
        }
        public static bool isPersonExistsByNationalNO(string NationalNo)
        {
            return clsPersonData.IsPersinExistByNationalNO(NationalNo);
        }
        public static string GetFullName(int PersonID)
        {
            return clsPersonData.GetFullName(PersonID);
        }

    }
}

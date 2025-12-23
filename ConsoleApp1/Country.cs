using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Country
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public Country()

        {
            this.CountryID = -1;
            this.CountryName = "";

            Mode = enMode.AddNew;

        }
        private Country(int ID, string CountryName)

        {
            this.CountryID = ID;
            this.CountryName = CountryName;

            Mode = enMode.Update;

        }
        public static DataTable GetAllCountries()
        {
            return CountriesData.GetAllCountries();

        }
        public static string GetCountryNameByNationalityID(int NationalityID)
        {
            return CountriesData.GetCountryNameByNationalityID(NationalityID);
        }
        public static short GetCountryIdByCountryName(string CountryName)
        {
            return CountriesData.GetCountryIdByCountryName(CountryName);
        }

    }

}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class CountriesData
    {
        public static DataTable GetAllCountries()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries order by CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
        public static string GetCountryNameByNationalityID(int NationalityID)
        {

           string CountryName = "";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT TOP 1 Countries.CountryName\r\nFROM     Countries INNER JOIN\r\n                  People ON Countries.CountryID = @NationalityID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalityID", NationalityID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result.ToString() != "")
                {
                    CountryName = result.ToString();
                }

            }


            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return CountryName;
        }
        public static short GetCountryIdByCountryName(string CountryName)
        {

            short CountryId = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Countries.CountryID From Countries where Countries.CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(),out int ID))
                {
                    CountryId = (short)ID;
                }

            }


            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return CountryId;
        }
    }
}

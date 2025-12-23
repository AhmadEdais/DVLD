using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsApplicationTypesData
    {
        public static DataTable  GetAllApplicationTypes()
        {
            // 1. Initialize the DataTable
            DataTable dt = new DataTable();

            // 2. Define the Query
            string query = "Select ApplicationTypeID AS ID ," +
                " ApplicationTypeTitle AS Title," +
                " ApplicationFees AS Fees" +
                " FROM ApplicationTypes";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        // 3. ExecuteReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 4. Load the DataTable
                            // This function automatically loops through the reader and fills the table.
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            // 5. Return the table (it might be empty, but it won't be null)
            return dt;
        }
        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle,decimal ApplicationFees)
        {
            int rowsAffected = 0;

            // 1. Define the Update Query
            // Note: We usually DO NOT update the PersonID. 
            // If you need to change the person, you typically delete the user and create a new one.
            string query = "UPDATE ApplicationTypes" +
                "   SET ApplicationTypeTitle = @ApplicationTypeTitle" +
                ",ApplicationFees = @ApplicationFees" +
                " WHERE ApplicationTypeID = @ApplicationTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 2. Add Parameters
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
                    command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

                    try
                    {
                        connection.Open();

                        // 3. ExecuteNonQuery
                        // This returns the number of rows that were changed.
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                    // 4. Return True if a row was actually found and updated
                    return (rowsAffected > 0);
                }
            }
        }
        public static bool GetApplicationTypeByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref decimal ApplicationTypeFees)
        {
            bool found = false;
            // 1. Define the Query
            string query = "Select ApplicationTypeTitle, ApplicationFees" +
                " FROM ApplicationTypes" +
                " WHERE ApplicationTypeID = @ApplicationTypeID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 2. Add Parameters
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    try
                    {
                        connection.Open();
                        // 3. ExecuteReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 4. Read the data
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    ApplicationTypeTitle = reader.GetString(0);
                                    ApplicationTypeFees = reader.GetDecimal(1);
                                    found = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return found;
        }
        // Inside clsApplicationTypesData class

        public static decimal GetApplicationFees(int ApplicationTypeID)
        {
            decimal Fees = 0;

            string query = "SELECT ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        // Check if we got a result (not null)
                        if (result != null && decimal.TryParse(result.ToString(), out decimal value))
                        {
                            Fees = value;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log (optional)
                    }
                }
            }
            return Fees;
        }
        public static string GetApplicationTypeTitle(int ApplicationTypeID)
        {
            string Title = "";

            string query = "SELECT ApplicationTypeTitle FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            Title = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log
                    }
                }
            }

            return Title;
        }
    }
}

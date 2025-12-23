using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsTestTypesData
    {
        
            public static DataTable GetAllTestTypes()
            {
                // 1. Initialize the DataTable
                DataTable dt = new DataTable();

                // 2. Define the Query
                string query = "SELECT TestTypeID AS ID, " +
                    "TestTypeTitle AS Title, " +
                    "TestTypeDescription As Description," +
                    " TestTypeFees As Fees " +
                    "FROM TestTypes ";
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
        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle,string TestTypeDescription, decimal TestFees)
        {
            int rowsAffected = 0;

            // 1. Define the Update Query
            // Note: We usually DO NOT update the PersonID. 
            // If you need to change the person, you typically delete the user and create a new one.
            string query = "UPDATE TestTypes" +
                "   SET TestTypeTitle = @TestTypeTitle, " +
                "TestTypeDescription = @TestTypeDescription" +
                ",TestTypeFees = @TestFees" +
                " WHERE TestTypeID = @TestTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 2. Add Parameters
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                    command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                    command.Parameters.AddWithValue("@TestFees", TestFees);

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

        public static bool GetTestTypeByID(int TestTypeID, ref string TestTypeTitle,ref string TestTypeDescription, ref decimal TestTypeFees)
        {
            bool found = false;
            // 1. Define the Query
            string query = "Select TestTypeTitle,TestTypeDescription, TestTypeFees" +
                " FROM TestTypes" +
                " WHERE TestTypeID = @TestTypeID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 2. Add Parameters
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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
                                    TestTypeTitle = reader.GetString(0);
                                    TestTypeDescription = reader.GetString(1);
                                    TestTypeFees = reader.GetDecimal(2);
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
        public static string GetTestTypeTitle(int TestTypeID)
        {
            string Title = "";

            string query = "SELECT TestTypeTitle FROM TestTypes WHERE TestTypeID = @TestTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
        public static decimal GetTestTypeFees(int TestTypeID)
        {
            decimal Fees = 0;

            string query = "SELECT TestTypeFees FROM TestTypes WHERE TestTypeID = @TestTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && decimal.TryParse(result.ToString(), out decimal value))
                        {
                            Fees = value;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log
                    }
                }
            }

            return Fees;
        }

    }
}

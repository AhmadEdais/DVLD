using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsTestData
{
    public static bool GetTestInfoByID(int TestID,
        ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
    {
        bool isFound = false;

        string query = "SELECT * FROM Tests WHERE TestID = @TestID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            TestAppointmentID = (int)reader["TestAppointmentID"];
                            TestResult = (bool)reader["TestResult"];

                            // Check for Null Notes
                            if (reader["Notes"] != DBNull.Value)
                                Notes = (string)reader["Notes"];
                            else
                                Notes = "";

                            CreatedByUserID = (int)reader["CreatedByUserID"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    isFound = false;
                }
            }
        }
        return isFound;
    }

    public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
    {
        int TestID = -1;

        string query = @"INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                         VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                         SELECT SCOPE_IDENTITY();";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);

                if (string.IsNullOrEmpty(Notes))
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Notes", Notes);

                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TestID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log
                }
            }
        }
        return TestID;
    }

    public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
    {
        int rowsAffected = 0;
        string query = @"UPDATE Tests  
                         SET TestAppointmentID = @TestAppointmentID,
                             TestResult = @TestResult,
                             Notes = @Notes,
                             CreatedByUserID = @CreatedByUserID
                         WHERE TestID = @TestID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestID", TestID);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);

                if (string.IsNullOrEmpty(Notes))
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Notes", Notes);

                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        return (rowsAffected > 0);
    }
}
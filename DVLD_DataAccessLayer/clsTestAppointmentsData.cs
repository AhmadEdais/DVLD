using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsTestAppointmentData
{
    public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
        ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
        ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID,
        ref bool IsLocked, ref int RetakeTestApplicationID)
    {
        bool isFound = false;

        string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            TestTypeID = (int)reader["TestTypeID"];
                            LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                            AppointmentDate = (DateTime)reader["AppointmentDate"];
                            PaidFees = (decimal)reader["PaidFees"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            IsLocked = (bool)reader["IsLocked"];

                            // Handle Nullable RetakeTestApplicationID
                            if (reader["RetakeTestApplicationID"] != DBNull.Value)
                                RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                            else
                                RetakeTestApplicationID = -1;
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

    public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
        DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
    {
        int TestAppointmentID = -1;

        string query = @"INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
                         VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked, @RetakeTestApplicationID);
                         SELECT SCOPE_IDENTITY();";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);

                if (RetakeTestApplicationID == -1)
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TestAppointmentID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                }
            }
        }
        return TestAppointmentID;
    }

    public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
        DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
    {
        int rowsAffected = 0;
        string query = @"UPDATE TestAppointments  
                         SET TestTypeID = @TestTypeID,
                             LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                             AppointmentDate = @AppointmentDate,
                             PaidFees = @PaidFees,
                             CreatedByUserID = @CreatedByUserID,
                             IsLocked = @IsLocked,
                             RetakeTestApplicationID = @RetakeTestApplicationID
                         WHERE TestAppointmentID = @TestAppointmentID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);

                if (RetakeTestApplicationID == -1)
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

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
    public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
    {
        DataTable dt = new DataTable();

        string query = @"SELECT TestAppointmentID, AppointmentDate, PaidFees, IsLocked
                     FROM TestAppointments
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                     AND TestTypeID = @TestTypeID
                     ORDER BY AppointmentDate DESC";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle Log
                }
            }
        }

        return dt;
    }
    public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
    {
        bool isFound = false;

        // We check for IsLocked = 0 (Active/Open appointment)
        string query = @"SELECT TOP 1 Found=1 
                     FROM TestAppointments 
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                     AND TestTypeID = @TestTypeID
                     AND IsLocked = 0";

        try
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        isFound = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Ideally, log the error to Event Viewer
            isFound = false;
        }

        return isFound;
    }

}
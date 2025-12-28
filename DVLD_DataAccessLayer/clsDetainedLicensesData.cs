using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsDetainedLicenseData
{
    public static bool GetDetainedLicenseInfoByID(int DetainID, ref int LicenseID, ref DateTime DetainDate,
        ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased,
        ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
    {
        bool isFound = false;

        string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DetainID", DetainID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            LicenseID = (int)reader["LicenseID"];
                            DetainDate = (DateTime)reader["DetainDate"];
                            FineFees = (decimal)reader["FineFees"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            IsReleased = (bool)reader["IsReleased"];

                            // --- HANDLE NULLS for Release Info ---
                            if (reader["ReleaseDate"] == DBNull.Value)
                                ReleaseDate = DateTime.MinValue; // Use MinValue to indicate "Not Released"
                            else
                                ReleaseDate = (DateTime)reader["ReleaseDate"];

                            if (reader["ReleasedByUserID"] == DBNull.Value)
                                ReleasedByUserID = -1;
                            else
                                ReleasedByUserID = (int)reader["ReleasedByUserID"];

                            if (reader["ReleaseApplicationID"] == DBNull.Value)
                                ReleaseApplicationID = -1;
                            else
                                ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
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

    public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID)
    {
        int DetainID = -1;

        // Note: We don't insert Release info here because it's initially NULL
        string query = @"INSERT INTO DetainedLicenses 
                        (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased)
                        VALUES 
                        (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, 0);
                        SELECT SCOPE_IDENTITY();";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        DetainID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                }
            }
        }
        return DetainID;
    }

    public static bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
    {
        int rowsAffected = 0;

        string query = @"UPDATE DetainedLicenses
                         SET IsReleased = 1,
                             ReleaseDate = @ReleaseDate,
                             ReleasedByUserID = @ReleasedByUserID,
                             ReleaseApplicationID = @ReleaseApplicationID
                         WHERE DetainID = @DetainID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);

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
    public static bool IsLicenseDetained(int LicenseID)
    {
        bool isDetained = false;

        // "TOP 1 Found=1" is an efficient way to check existence
        string query = @"SELECT TOP 1 Found=1 
                     FROM DetainedLicenses 
                     WHERE LicenseID = @LicenseID 
                     AND IsReleased = 0";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        isDetained = true;
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                }
            }
        }

        return isDetained;
    }
}
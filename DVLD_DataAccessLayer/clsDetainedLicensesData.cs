using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsDetainedLicenseData
{
    public static bool GetDetainedLicenseInfoByID(int DetainID,
        ref int LicenseID, ref DateTime DetainDate,
        ref decimal FineFees, ref int CreatedByUserID,
        ref bool IsReleased, ref DateTime? ReleaseDate,
        ref int? ReleasedByUserID, ref int? ReleaseApplicationID)
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
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        LicenseID = (int)reader["LicenseID"];
                        DetainDate = (DateTime)reader["DetainDate"];
                        FineFees = (decimal)reader["FineFees"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                        IsReleased = (bool)reader["IsReleased"];

                        // Handle Nullable Fields
                        if (reader["ReleaseDate"] == DBNull.Value)
                            ReleaseDate = null;
                        else
                            ReleaseDate = (DateTime)reader["ReleaseDate"];

                        if (reader["ReleasedByUserID"] == DBNull.Value)
                            ReleasedByUserID = null;
                        else
                            ReleasedByUserID = (int)reader["ReleasedByUserID"];

                        if (reader["ReleaseApplicationID"] == DBNull.Value)
                            ReleaseApplicationID = null;
                        else
                            ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log Error
                    isFound = false;
                }
            }
        }
        return isFound;
    }

    public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID,
       ref int DetainID, ref DateTime DetainDate,
       ref decimal FineFees, ref int CreatedByUserID,
       ref bool IsReleased, ref DateTime? ReleaseDate,
       ref int? ReleasedByUserID, ref int? ReleaseApplicationID)
    {
        bool isFound = false;

        // Get the LAST detention record for this license that is NOT released yet
        string query = @"SELECT TOP 1 * FROM DetainedLicenses 
                         WHERE LicenseID = @LicenseID 
                         ORDER BY DetainID DESC";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        DetainID = (int)reader["DetainID"];
                        DetainDate = (DateTime)reader["DetainDate"];
                        FineFees = (decimal)reader["FineFees"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                        IsReleased = (bool)reader["IsReleased"];

                        if (reader["ReleaseDate"] == DBNull.Value)
                            ReleaseDate = null;
                        else
                            ReleaseDate = (DateTime)reader["ReleaseDate"];

                        if (reader["ReleasedByUserID"] == DBNull.Value)
                            ReleasedByUserID = null;
                        else
                            ReleasedByUserID = (int)reader["ReleasedByUserID"];

                        if (reader["ReleaseApplicationID"] == DBNull.Value)
                            ReleaseApplicationID = null;
                        else
                            ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    isFound = false;
                }
            }
        }
        return isFound;
    }

    public static DataTable GetAllDetainedLicenses()
    {
        DataTable dt = new DataTable();

        string query = @"SELECT DetainedLicenses.DetainID AS [D.ID], 
                        DetainedLicenses.LicenseID AS [L.ID], 
                        DetainedLicenses.DetainDate AS [D.Date], 
                        DetainedLicenses.IsReleased AS [Is Released], 
                        DetainedLicenses.FineFees AS [Fine Fees], 
                        DetainedLicenses.ReleaseDate AS [Release Date], 
                        People.NationalNo AS [N.No.], 
                        People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName AS [Full Name], 
                        DetainedLicenses.ReleaseApplicationID AS [Release App.ID]
                 FROM DetainedLicenses INNER JOIN
                      Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID INNER JOIN
                      Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
                      People ON Drivers.PersonID = People.PersonID
                        ORDER BY DetainedLicenses.DetainID DESC";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
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
                    // Log Error
                }
            }
        }
        return dt;
    }

    public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate,
        decimal FineFees, int CreatedByUserID)
    {
        int DetainID = -1;

        string query = @"INSERT INTO DetainedLicenses (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased)
                         VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, 0);
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
                    // Log Error
                }
            }
        }
        return DetainID;
    }

    public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
        decimal FineFees, int CreatedByUserID)
    {
        // This method updates the basic info only, not the release info.
        int rowsAffected = 0;
        string query = @"UPDATE DetainedLicenses
                         SET LicenseID = @LicenseID, 
                             DetainDate = @DetainDate, 
                             FineFees = @FineFees, 
                             CreatedByUserID = @CreatedByUserID
                         WHERE DetainID = @DetainID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
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
                command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

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

        string query = "SELECT Found=1 FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

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
                    // Log Error
                }
            }
        }
        return isDetained;
    }
}
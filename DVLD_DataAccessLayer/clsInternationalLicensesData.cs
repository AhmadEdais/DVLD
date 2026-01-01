using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsInternationalLicenseData
{
    // Method 1: Get Info by ID
    public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID,
        ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,
        ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
    {
        bool isFound = false;

        string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        ApplicationID = (int)reader["ApplicationID"];
                        DriverID = (int)reader["DriverID"];
                        IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                        IssueDate = (DateTime)reader["IssueDate"];
                        ExpirationDate = (DateTime)reader["ExpirationDate"];
                        IsActive = (bool)reader["IsActive"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // isFound = false;
                }
            }
        }

        return isFound;
    }

    // Method 2: Add New License
    public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
        DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
    {
        int InternationalLicenseID = -1;

        string query = @" UPDATE InternationalLicenses
                          set IsActive = 0
                        WHERE DriverID = @DriverID AND IsActive = 1;    

                                INSERT INTO InternationalLicenses
                               (ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                                IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                         VALUES
                               (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID,
                                @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
                         SELECT SCOPE_IDENTITY();";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        InternationalLicenseID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log Error
                }
            }
        }

        return InternationalLicenseID;
    }

    // Method 3: Update License
    public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
        int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate,
        bool IsActive, int CreatedByUserID)
    {
        int rowsAffected = 0;

        string query = @"UPDATE InternationalLicenses
                           SET ApplicationID = @ApplicationID,
                               DriverID = @DriverID,
                               IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                               IssueDate = @IssueDate,
                               ExpirationDate = @ExpirationDate,
                               IsActive = @IsActive,
                               CreatedByUserID = @CreatedByUserID
                         WHERE InternationalLicenseID = @InternationalLicenseID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@IsActive", IsActive);
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

    // Method 4: Get All Licenses

    // Method 5: Get Licenses by Driver ID
    public static DataTable GetDriverInternationalLicenses(int DriverID)
    {
        DataTable dt = new DataTable();

        string query = @"
            SELECT 
                InternationalLicenses.InternationalLicenseID AS [Int.License ID], 
                InternationalLicenses.ApplicationID AS [App. ID], 
                InternationalLicenses.IssuedUsingLocalLicenseID AS [L.License ID], 
                InternationalLicenses.IssueDate AS [Issue Date], 
                InternationalLicenses.ExpirationDate AS [Expiration Date], 
                InternationalLicenses.IsActive AS [Is Active]
            FROM InternationalLicenses 
            WHERE DriverID = @DriverID
            ORDER BY ExpirationDate DESC";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DriverID", DriverID);

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

    // Method 6: Get Active International License for Driver
    public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
    {
        int ActiveLicenseID = -1;

        string query = @"SELECT TOP 1 InternationalLicenseID 
                         FROM InternationalLicenses 
                         WHERE DriverID = @DriverID 
                           AND GETDATE() BETWEEN IssueDate AND ExpirationDate 
                           AND IsActive = 1;";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DriverID", DriverID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        ActiveLicenseID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log Error
                }
            }
        }

        return ActiveLicenseID;
    }
    public static bool IsInternationalLicenseExistByLocalLicenseID(int LocalLicenseID)
    {
        bool isFound = false;

        string query = @"SELECT Found=1 
                     FROM InternationalLicenses 
                     WHERE IssuedUsingLocalLicenseID = @LocalLicenseID 
                     AND IsActive = 1";
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        isFound = true;
                    }
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
    public static DataTable GetAllInternationalLicenses()
    {
        DataTable dt = new DataTable();

        string query = @"
        SELECT 
            InternationalLicenseID AS [Int.License ID], 
            ApplicationID AS [Application ID], 
            DriverID AS [Driver ID], 
            IssuedUsingLocalLicenseID AS [L.License ID], 
            IssueDate AS [Issue Date], 
            ExpirationDate AS [Expiration Date], 
            IsActive AS [Is Active]
        FROM InternationalLicenses 
        ORDER BY IsActive, ExpirationDate DESC";

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
}
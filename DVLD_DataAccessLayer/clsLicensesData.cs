using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsLicenseDataAccess
{
    public static bool GetLicenseInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID,
        ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate,
        ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason,
        ref int CreatedByUserID)
    {
        bool isFound = false;

        string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            ApplicationID = (int)reader["ApplicationID"];
                            DriverID = (int)reader["DriverID"];
                            LicenseClass = (int)reader["LicenseClass"];
                            IssueDate = (DateTime)reader["IssueDate"];
                            ExpirationDate = (DateTime)reader["ExpirationDate"];
                            PaidFees = (decimal)reader["PaidFees"];
                            IsActive = (bool)reader["IsActive"];
                            IssueReason = (byte)reader["IssueReason"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];

                            // HANDLE NULLABLE NOTES
                            if (reader["Notes"] != DBNull.Value)
                                Notes = (string)reader["Notes"];
                            else
                                Notes = ""; // Or null, depending on your preference
                        }
                    }
                }
                catch (Exception ex)
                {
                    isFound = false;
                    // Log error
                }
            }
        }
        return isFound;
    }

    public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
        DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees,
        bool IsActive, byte IssueReason, int CreatedByUserID)
    {
        int LicenseID = -1;

        string query = @"INSERT INTO Licenses 
                        (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, 
                         Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
                        VALUES 
                        (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, 
                         @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
                        SELECT SCOPE_IDENTITY();";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                // HANDLE INSERTING NULL
                if (string.IsNullOrEmpty(Notes))
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Notes", Notes);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        LicenseID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                }
            }
        }
        return LicenseID;
    }
    public static bool IsLicenseExistByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, int LicenseClassID)
    {
        bool isFound = false;

        // We select Found=1 to optimize performance (we don't need the actual data, just a Yes/No)
        string query = @"
        SELECT TOP 1 Found=1
        FROM Licenses 
        INNER JOIN Applications ON Licenses.ApplicationID = Applications.ApplicationID 
        INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
        WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
        AND LicenseClass = @LicenseClassID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
                    // Log error
                    isFound = false;
                }
            }
        }

        return isFound;
    }

    // You can add UpdateLicense here following the same logic as AddNewLicense

    public static DataTable GetDriverLicenses(int DriverID)
    {
        DataTable dt = new DataTable();

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = @"SELECT 
                            Licenses.LicenseID AS [Lic.ID],
                            Licenses.ApplicationID AS [App.ID],
                            LicenseClasses.ClassName AS [Class Name],
                            Licenses.IssueDate AS [Issue Date],
                            Licenses.ExpirationDate AS [Expiration Date],
                            Licenses.IsActive AS [Is Active]
                        FROM Licenses 
                        INNER JOIN LicenseClasses ON LicenseClasses.LicenseClassID = Licenses.LicenseClass
                        WHERE DriverID = @DriverID";

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
                    // Handle exception (log it)
                }
            }
        }

        return dt;
    }

}
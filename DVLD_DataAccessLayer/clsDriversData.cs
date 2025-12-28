using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsDriverDataAccess
{
    public static bool GetDriverInfoByDriverID(int DriverID, ref int PersonID,
        ref int CreatedByUserID, ref DateTime CreatedDate)
    {
        bool isFound = false;

        string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DriverID", DriverID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            PersonID = (int)reader["PersonID"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error (Event Viewer)
                    isFound = false;
                }
            }
        }
        return isFound;
    }

    public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID,
        ref int CreatedByUserID, ref DateTime CreatedDate)
    {
        bool isFound = false;

        string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            DriverID = (int)reader["DriverID"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];
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

    public static int AddNewDriver(int PersonID, int CreatedByUserID)
    {
        int DriverID = -1;

        string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                         VALUES (@PersonID, @CreatedByUserID, @CreatedDate);
                         SELECT SCOPE_IDENTITY();";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        DriverID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                }
            }
        }
        return DriverID;
    }

    public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
    {
        int rowsAffected = 0;
        string query = @"UPDATE Drivers
                         SET PersonID = @PersonID,
                             CreatedByUserID = @CreatedByUserID
                         WHERE DriverID = @DriverID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@PersonID", PersonID);
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

    public static DataTable GetAllDrivers()
    {
        DataTable dt = new DataTable();

        string query = @"SELECT Drivers.DriverID AS [Driver ID],
                            Drivers.PersonID AS [Person ID],
                            People.NationalNo AS [National No],
                            People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName,'') + ' ' + People.LastName AS [Full Name],
                            Drivers.CreatedDate AS [Date],
                            (SELECT COUNT(*) FROM Licenses WHERE Licenses.DriverID = Drivers.DriverID AND IsActive = 1) AS [Active Licenses]
                     FROM Drivers
                     INNER JOIN People ON Drivers.PersonID = People.PersonID
                     ORDER BY [Full Name]";

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
    public static int GetDriverIDByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
    {
        int DriverID = -1;

        string query = @"
          SELECT Drivers.DriverID
          FROM LocalDrivingLicenseApplications
          INNER JOIN Applications 
              ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
          INNER JOIN Drivers 
              ON Applications.ApplicantPersonID = Drivers.PersonID
          WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int foundID))
                    {
                        DriverID = foundID;
                    }
                }
                catch (Exception ex)
                {
                    // Log Error
                }
            }
        }

        return DriverID;
    }

}
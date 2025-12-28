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
        // This query usually joins with People to be useful in a grid
        string query = @"SELECT Drivers.DriverID, Drivers.PersonID, 
                         People.NationalNo, People.FirstName, People.SecondName, People.LastName, 
                         Drivers.CreatedDate, Drivers.CreatedByUserID 
                         FROM Drivers INNER JOIN People ON Drivers.PersonID = People.PersonID";

        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
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
                    // Log error
                }
            }
        }
        return dt;
    }
    public static int GetDriverIDByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
    {
        int DriverID = -1;

        string query = @"SELECT Licenses.DriverID
                     FROM Applications 
                     INNER JOIN Licenses ON Applications.ApplicationID = Licenses.ApplicationID 
                     INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

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
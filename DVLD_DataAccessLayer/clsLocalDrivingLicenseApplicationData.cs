using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDBusinessLayer // Change to your actual DataAccess namespace if different
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static bool GetLocalDrivingLicenseApplicationInfoByID(int LocalDrivingLicenseApplicationID,
            ref int ApplicationID, ref int LicenseClassID) 
        {
            bool isFound = false;

            string query = @"SELECT * FROM LocalDrivingLicenseApplications 
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                ApplicationID = (int)reader["ApplicationID"];
                                LicenseClassID = (int)reader["LicenseClassID"];
                            }
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

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;

            string query = @"INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
                             VALUES (@ApplicationID, @LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LocalDrivingLicenseApplicationID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error
                    }
                }
            }
            return LocalDrivingLicenseApplicationID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE LocalDrivingLicenseApplications  
                             SET ApplicationID = @ApplicationID, 
                                 LicenseClassID = @LicenseClassID
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;
            // Note: Since this table has a FK to Applications, you usually delete 
            // the record here first, and then delete the base Application record.
            string query = @"DELETE FROM LocalDrivingLicenseApplications 
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS [L.D.LAppID],
                            LicenseClasses.ClassName AS [Driving Class],
                            People.NationalNo AS [National No],
                            (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) AS [Full Name],
                            Applications.ApplicationDate AS [Application Date],
                            
                            (SELECT COUNT(*) 
                            FROM TestAppointments 
                            INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID 
                            WHERE Tests.TestResult = 1 
                            AND TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) AS [Passed Tests],
                            
                            CASE 
                                WHEN Applications.ApplicationStatus = 1 THEN 'New' 
                                WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled' 
                                WHEN Applications.ApplicationStatus = 3 THEN 'Completed' 
                                ELSE 'Unknown' 
                            END AS [Status]

                     FROM Applications 
                     INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID 
                     INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID 
                     INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
                     ORDER BY [Application Date] DESC";

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
                        // Handle Log
                    }
                }
            }

            return dt;
        }
        public static bool IsThereAnActiveScheduledApplication(int ApplicantPersonID, int LicenseClassID, int ApplicationStatus)
        {
            bool isFound = false;

            // We select Found=1 for efficiency. 
            // We match the PersonID and Status from the 'Applications' table
            // and the LicenseClassID from the 'LocalDrivingLicenseApplications' table.
            string query = @"
        SELECT Found=1 
        FROM LocalDrivingLicenseApplications 
        INNER JOIN Applications 
        ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
        WHERE Applications.ApplicantPersonID = @ApplicantPersonID 
        AND Applications.ApplicationStatus = @ApplicationStatus 
        AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Map the C# function parameters to the correct SQL variables
                    command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                    command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
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
                        // Handle Log (optional)
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            string query = @"SELECT count(TestResult)
                     FROM LocalDrivingLicenseApplications 
                     INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                     INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                     WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                     AND TestResult = 1";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && byte.TryParse(result.ToString(), out byte count))
                        {
                            PassedTestCount = count;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log
                    }
                }
            }

            return PassedTestCount;
        }
        public static string GetClassNameByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            string ClassName = "";

            string query = @"SELECT LicenseClasses.ClassName
                     FROM LicenseClasses 
                     INNER JOIN LocalDrivingLicenseApplications ON LicenseClasses.LicenseClassID = LocalDrivingLicenseApplications.LicenseClassID
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

                        if (result != null)
                        {
                            ClassName = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log
                    }
                }
            }

            return ClassName;
        }

        public static decimal GetClassFeesByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            decimal ClassFees = 0;

            string query = @"SELECT LicenseClasses.ClassFees
                     FROM LicenseClasses 
                     INNER JOIN LocalDrivingLicenseApplications ON LicenseClasses.LicenseClassID = LocalDrivingLicenseApplications.LicenseClassID
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

                        if (result != null && decimal.TryParse(result.ToString(), out decimal fees))
                        {
                            ClassFees = fees;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log
                    }
                }
            }

            return ClassFees;
        }
        public static byte GetTestTrials(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte Trials = 0;

            string query = @"SELECT COUNT(*) 
                     FROM TestAppointments
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                     AND TestTypeID = @TestTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && byte.TryParse(result.ToString(), out byte count))
                        {
                            Trials = count;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log
                    }
                }
            }

            return Trials;
        }
        public static string GetApplicantFullNameByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            string FullName = "";

            string query = @"SELECT (People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName, '') + ' ' + People.LastName) AS FullName
                     FROM LocalDrivingLicenseApplications
                     INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                     INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
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

                        if (result != null)
                        {
                            FullName = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log
                    }
                }
            }

            return FullName;
        }
        public static string GetLicenseClassNameByID(int LocalDrivingLicenseApplicationID)
        {
            string ClassName = "";

            string query = @"SELECT ClassName
                     FROM LicenseClasses 
                     INNER JOIN LocalDrivingLicenseApplications 
                     ON LicenseClasses.LicenseClassID = LocalDrivingLicenseApplications.LicenseClassID
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

                        if (result != null)
                        {
                            ClassName = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error
                    }
                }
            }

            return ClassName;
        }
        public static string GetApplicantFullNameByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            string FullName = "";

            // We select the individual names, but you could also concatenate in SQL
            string query = @"SELECT People.FirstName, People.SecondName, People.ThirdName, People.LastName
                     FROM LocalDrivingLicenseApplications 
                     INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID 
                     INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // We construct the full name in C# to safely handle nulls (like ThirdName)
                                string First = (string)reader["FirstName"];
                                string Second = (string)reader["SecondName"];

                                string Third = "";
                                if (reader["ThirdName"] != DBNull.Value)
                                {
                                    Third = (string)reader["ThirdName"];
                                }

                                string Last = (string)reader["LastName"];

                                // Join them with spaces, ignoring empty Third names
                                FullName = First + " " + Second + " " + (Third != "" ? Third + " " : "") + Last;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error
                    }
                }
            }

            return FullName;
        }
    }
}
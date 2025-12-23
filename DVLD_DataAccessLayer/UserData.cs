using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsUserData
    {
        public static int AddNewUser(int personID, string userName, string password, bool isActive)
        { 
            int newUserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Users 
                     (PersonID, UserName, Password, IsActive)
                 VALUES 
                     (@PersonID, @UserName, @Password, @IsActive);
                 SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    newUserID = insertedID;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return newUserID;

        }
        public static bool UpdateUser(int userID, string userName, string password, bool isActive)
        {
            int rowsAffected = 0;

            // 1. Define the Update Query
            // Note: We usually DO NOT update the PersonID. 
            // If you need to change the person, you typically delete the user and create a new one.
            string query = @"UPDATE Users  
                     SET UserName = @UserName, 
                         Password = @Password, 
                         IsActive = @IsActive
                     WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 2. Add Parameters
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@IsActive", isActive);

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
                }
            }

            // 4. Return True if a row was actually found and updated
            return (rowsAffected > 0);
        }
        public static bool DeleteUser(int userID)
        {
            int rowsAffected = 0;

            // 1. Define the Delete Query
            string query = @"DELETE FROM Users
                     WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 2. Add Parameter
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();

                        // 3. ExecuteNonQuery
                        // Returns the number of rows deleted.
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }

            // 4. Return True if the user was successfully deleted
            return (rowsAffected > 0);
        }
        public static DataTable GetAllUsers()
        {
            // 1. Initialize the DataTable
            DataTable dt = new DataTable();

            // 2. Define the Query
            // Tip: In a real app, you might want to join with People to get names instead of just IDs.
            // e.g., "SELECT Users.UserID, People.FirstName, Users.UserName, Users.IsActive..."
            string query = "SELECT UserID As 'User ID' , Users.PersonID As 'Person ID' ,People.FirstName + ' '  + People.SecondName + ' ' + People.ThirdName\r\n           + ' ' + People.LastName As 'Full Name'\r\n           \r\n           ,USERNAME as 'UserName' , IsActive AS 'Is Active' FROM USERS inner join People on People.PersonID = Users.PersonID";

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
        public static bool IsUserExist(int PersonID)
        {
            bool isFound = false;

            // The query returns 1 if found, and nothing (null) if not found.
            string query = "SELECT Found = 1 FROM Users WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();

                        // ExecuteScalar returns the first column of the first row.
                        object result = command.ExecuteScalar();

                        // If result is not null, it means we got the "1" back.
                        if (result != null)
                        {
                            isFound = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;
            string query = "SELECT Found = 1 FROM Users WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);

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
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName,
                                       ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
    ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

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
                                UserID = (int)reader["UserID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle Log
                        isFound = false;
                    }
                }
            }
            return isFound;
        }
        public static bool GetUserInfoByUsername(string UserName, ref int UserID, ref int PersonID,
                                         ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            string query = "SELECT * FROM Users WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
    }
}

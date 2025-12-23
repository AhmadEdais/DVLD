using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Settings;

namespace DVLD_DataAccessLayer
{
    public class clsPersonData
    {
        private static string _SafeColumnName(Settings.clsSettings.enPeopleFilterOptions enFilterBy,ref string Text)
        {
            DataTable dataTable = new DataTable();
            string safeColumnName;

            // This is the C# 7.3 compatible switch STATEMENT
            switch (enFilterBy)
            {
                case clsSettings.enPeopleFilterOptions.PersonID:
                    safeColumnName = "People.PersonID";
                    break;
                case clsSettings.enPeopleFilterOptions.NationalNo:
                    safeColumnName = "People.NationalNo";
                    break;
                case clsSettings.enPeopleFilterOptions.FirstName:
                    safeColumnName = "People.FirstName";
                    break;
                case clsSettings.enPeopleFilterOptions.SecondName:
                    safeColumnName = "People.SecondName";
                    break;
                case clsSettings.enPeopleFilterOptions.ThirdName:
                    safeColumnName = "People.ThirdName";
                    break;
                case clsSettings.enPeopleFilterOptions.LastName:
                    safeColumnName = "People.LastName";
                    break;
                case clsSettings.enPeopleFilterOptions.Nationality:
                    safeColumnName = "Countries.CountryName";
                    break;
                case clsSettings.enPeopleFilterOptions.Gendor:
                    Text = (Text == "Male" || Text == "male") ? "0":((Text == "Female" || Text == "female")? "1":"-1");
                    safeColumnName = "People.Gendor";
                    break;
                case clsSettings.enPeopleFilterOptions.Phone:
                    safeColumnName = "People.Phone";
                    break;
                case clsSettings.enPeopleFilterOptions.Email:
                    safeColumnName = "People.Email";
                    break;
                default:
                    safeColumnName = "People.PersonID";
                    break;  
            }
            return safeColumnName;
        }
        public static bool GetPersonInfoByID(int ID, ref string FirstName, ref string SecondName
            , ref string ThirdName, ref string LastName
            , ref string NatioinalNO, ref DateTime DateOfBirth
            , ref short Gendor, ref string Email, ref string Phone
            , ref string ImagePath, ref int NationalityCountryID,ref string Address)

        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People Where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if ((reader.Read()))
                {
                    isFound = true;
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    Gendor = (byte)reader["Gendor"];
                    NatioinalNO = (string)reader["NationalNO"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    Phone = (string)reader["Phone"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }
                Address = (string)reader["Address"];

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }

            finally
            {
                connection.Close();
            }
            return isFound;

        }
        
        public static bool GetPersonInfoByNationalNO(ref int ID, ref string FirstName, ref string SecondName
            , ref string ThirdName, ref string LastName
            ,  string NationalNO, ref DateTime DateOfBirth
            , ref short Gendor, ref string Email, ref string Phone
            , ref string ImagePath, ref int NationalityCountryID, ref string Address)

        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People Where NationalNO = @NationalNO";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNO", NationalNO);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if ((reader.Read()))
                {
                    isFound = true;
                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    Gendor = (byte)reader["Gendor"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    Phone = (string)reader["Phone"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }
                Address = (string)reader["Address"];

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }

            finally
            {
                connection.Close();
            }
            return isFound;

        }
        public static int AddNewPerson(string FirstName, string SecondName
            , string ThirdName, string LastName
            , string NationalNo, DateTime DateOfBirth
            , short Gendor, string Email, string Phone
            , string ImagePath, int NationalityCountryID,string Address)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int ContactID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People
   ([NationalNo],[FirstName],[SecondName],[ThirdName],[LastName]
   ,[DateOfBirth],[Gendor],[Address],[Phone],[Email]
   ,[NationalityCountryID],[ImagePath])
VALUES
   (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName
   ,@DateOfBirth,@Gendor,@Address,@Phone,@Email
   ,@NationalityCountryID,@ImagePath);SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            // Add required parameters
            command.Parameters.AddWithValue("@NationalNo", NationalNo); // <-- Fixed typo
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (string.IsNullOrEmpty(ThirdName))
            {
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }

            // Email
            if (string.IsNullOrEmpty(Email))
            {
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", Email);
            }

            // ImagePath
            if (string.IsNullOrEmpty(ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ContactID = insertedID;
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


            return ContactID;
        }
        public static bool UpdatePersonInfo(int ID, string FirstName, string SecondName
            , string ThirdName, string LastName
            , string NatioinalNO, DateTime DateOfBirth
            , short Gendor, string Email, string PhoneNumber
            , string ImagePath, int NationalityCountryID, string Address)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE People
                                           SET [NationalNo] = @NationalNo
                                              ,[FirstName] = @FirstName
                                              ,[SecondName] = @SecondName
                                              ,[ThirdName] = @ThirdName
                                              ,[LastName] = @LastName
                                              ,[DateOfBirth] = @DateOfBirth
                                              ,[Gendor] = @Gendor
                                              ,[Address] = @Address
                                              ,[Phone] = @Phone
                                              ,[Email] = @Email
                                              ,[NationalityCountryID] = @NationalityCountryID
                                              ,[ImagePath] = @ImagePath
                                               WHERE PersonID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);



            if (ThirdName != null && ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            command.Parameters.AddWithValue("@LastName", LastName);



            if (Email != null && Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalNo", NatioinalNO);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Phone", PhoneNumber);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static DataTable GetAllPeople()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }
        public static DataTable GetAllPeopleInfoForManagePeople()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT People.PersonID AS 'Person ID', People.NationalNo AS 'National NO.'," +
                " People.FirstName AS 'First Name'\r\n, People.SecondName AS 'Second Name'," +
                " People.ThirdName AS 'Third Name', People.LastName AS 'Last Name'\r\n,CASE\r\n" +
                "        WHEN People.Gendor = 0 THEN 'Male'\r\n   " +
                "     WHEN People.Gendor = 1 THEN 'Female'\r\n    END AS Gendor" +
                " -- Alias the CASE expression as 'Gendor'\r\n, FORMAT(People.DateOfBirth, " +
                "'MM/dd/yyyy hh:mm:ss tt') AS 'Date Of Birth'\r\n, " +
                "Countries.CountryName AS 'Nationality', People.Phone AS 'Phone', " +
                "People.Email AS 'Email' \r\nFROM     People INNER JOIN\r\n      " +
                "            Countries ON People.NationalityCountryID = Countries.CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }
        public static DataTable GetFilterdPeople(clsSettings.enPeopleFilterOptions enFilteredBy,string Text)
        {
            DataTable dataTable = new DataTable();
            string SafeColumnName = _SafeColumnName(enFilteredBy,ref Text);
            
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT People.PersonID AS 'Person ID'," +
                " People.NationalNo AS 'National NO.'," +
                " People.FirstName AS 'First Name'\r\n," +
                " People.SecondName AS 'Second Name'," +
                " People.ThirdName AS 'Third Name'," +
                " People.LastName AS 'Last Name'\r\n," +
                "CASE\r\n        " +
                "WHEN People.Gendor = 0 THEN 'Male'\r\n" +
                "WHEN People.Gendor = 1 THEN 'Female'\r\n " +
                "   END AS Gendor -- Alias the CASE expression as 'Gendor'\r\n," +
                " FORMAT(People.DateOfBirth, 'MM/dd/yyyy hh:mm:ss tt') AS 'Date Of Birth'\r\n," +
                " Countries.CountryName AS 'Nationality', People.Phone AS 'Phone', People.Email AS 'Email' " +
                "\r\nFROM     People INNER JOIN\r\n " +
                "  Countries ON People.NationalityCountryID = Countries.CountryID\r\nWHERE \r\n " +
                SafeColumnName + " LIKE '' + @Text + '%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Text", Text);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        
        }
        public static int  GetPeopleCount(clsSettings.enPeopleFilterOptions enFilteredBy, string Text)
        {

            int PeopleCount = 0;
            string SafeColumnName = _SafeColumnName(enFilteredBy, ref Text);

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT COUNT (PersonID) FROM People WHERE " + 
                SafeColumnName + " LIKE '' + @Text + '%'";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Text", Text);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int Count))
                {
                    PeopleCount = Count;
                }

            }


            catch (Exception ex)
            {
               // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return PeopleCount;
        }
        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"DELETE FROM People
                                 WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
        public static bool IsPersinExist(int PerosnID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PerosnID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PerosnID", PerosnID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

    
    public static bool IsPersinExistByNationalNO(string NationalNO)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNO";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNO", NationalNO);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static string GetFullName(int PersonID)
        {
            string FullName = "";

            // We use ISNULL on ThirdName just in case it is empty, so it doesn't break the whole name.
            string query = @"SELECT (FirstName + ' ' + SecondName + ' ' + ISNULL(ThirdName, '') + ' ' + LastName) AS FullName 
                     FROM People 
                     WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

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
    }
    

    }
    

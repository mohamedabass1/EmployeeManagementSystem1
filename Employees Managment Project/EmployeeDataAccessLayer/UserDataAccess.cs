using System;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDataAccessLayer
{
    public class clsUserDataAccess
    {

        static public bool Find(int ID, ref string UserName, ref string Password)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select * from Users Where UserID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];

                }

                reader.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        static public bool Find(string UserName, ref int ID, ref string Password)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select * from Users Where UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ID = (int)reader["UserID"];
                    Password = (string)reader["Password"];

                }

                reader.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        static public int AddNewUser(string UserName, string Password)
        {
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Insert Into Users (UserName, Password)
                                          Values(@UserName, @Password);
                            Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null & int.TryParse(result.ToString(), out int InsertedID))
                {
                    UserID = InsertedID;
                }

            }
            catch (Exception)
            {

            }

            finally
            {
                connection.Close();
            }

            return UserID;

        }

        static public bool UpdateUser(int ID, string UserName, string Password)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Update Users
                                      SET
                                         UserName = @UserName,
                                         Password = @Password
                                    Where UserID = @ID  ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@ID", ID);

            int rowsAffected = 0;
            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();



            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        static public bool DeleteUser(int ID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Delete Users where UserID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            int rowAffected = 0;

            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }

            finally
            {
                connection.Close();
            }


            return (rowAffected > 0);

        }

        static public bool IsUserExists(int ID)
        {
            bool isExists = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select isFound = 1 From Users Where UserID = @ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isExists = reader.HasRows;

                reader.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return isExists;

        }

        static public bool IsUserExists(string UserName)
        {
            bool isExists = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select isFound = 1 From Users Where UserName = @UserName;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isExists = reader.HasRows;

                reader.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return isExists;

        }

        static public bool IsUserExists(string UserName, string Password)
        {
            bool isExists = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select isFound = 1 From Users Where UserName = @UserName and Password = @Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isExists = reader.HasRows;

                reader.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return isExists;

        }

        static public DataTable GetAllUsers()
        {
            DataTable dtUsers = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);


            string quere = @"Select * from Users";

            SqlCommand command = new SqlCommand(quere, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtUsers.Load(reader);
                }

                reader.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return dtUsers;

        }

        static public int GetTheTotalNumberOfUsers()
        {
            int NumberOfUsers = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select Count(*) from Users;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int TotalNumbeOfUsers))
                {
                    NumberOfUsers = TotalNumbeOfUsers;
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return NumberOfUsers;
        }


    }
}

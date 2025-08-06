using System;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDataAccessLayer
{
    public class clsEmployeeDataAccess
    {
        static public bool Find(int ID, ref string FirstName, ref string LastName,
            ref string Email, ref string Phone, ref string Address,
            ref DateTime DateOfBirth, ref decimal Salary, ref string Country, ref string ImagePath)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select * from Employees Where EmployeeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    Email = (string)reader["Email"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Salary = (decimal)reader["Salary"];
                    Country = (string)reader["Country"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];

                    }
                    else
                        ImagePath = "";

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



        static public int AddNewEmployee(string FirstName, string LastName,
             string Email, string Phone, string Address,
             DateTime DateOfBirth, decimal Salary, string Country, string ImagePath)
        {
            int EmployeeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Insert Into Employees (FirstName,LastName ,Email, Phone , Address, DateOfBirth, Salary, Country, ImagePath) 
                                             Values (@FirstName,@LastName ,@Email, @Phone, @Address, @DateOfBirth, @Salary, @Country, @ImagePath);
                                           Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Salary", Salary);
            command.Parameters.AddWithValue("@Country", Country);



            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    EmployeeID = insertedID;
                }
            }
            catch (Exception)
            {
                EmployeeID = -1;

            }
            finally
            {
                connection.Close();
            }

            return EmployeeID;

        }


        static public bool UpdateEmployee(int ID, string FirstName, string LastName,
             string Email, string Phone, string Address,
             DateTime DateOfBirth, decimal Salary, string Country, string ImagePath)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Update Employees 
                                   SET 
                                       FirstName = @FirstName,
                                       LastName = @LastName,
                                       Email = @Email,
                                       Phone = @Phone,
                                       Address = @Address,
                                       DateOfBirth = @DateOfBirth,
                                       Salary = @Salary,
                                       Country = @Country,
                                       ImagePath = @ImagePath
                                Where EmployeeID = @ID";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Salary", Salary);
            command.Parameters.AddWithValue("@Country", Country);


            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

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


        static public bool DeleteEmployee(int ID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Delete Employees Where EmployeeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

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


        static public DataTable GetAllEmployees()
        {
            DataTable dtEmployees = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select * from Employees";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtEmployees.Load(reader);
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

            return dtEmployees;

        }

        static public bool IsExistsEmployee(int ID)
        {
            bool isExists = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select isFound=1 from Employees Where EmployeeID = @ID;";


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

        static public int GetTheTotalNumberOfEmployees()
        {
            int NumberOfEmployess = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select Count(*) from Employees;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int TotalNumbeOfEmployees))
                {
                    NumberOfEmployess = TotalNumbeOfEmployees;
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return NumberOfEmployess;
        }

        static public decimal GetTheTotalSalaryOfEmployees()
        {
            decimal TotalSlary = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.connectionSetting);

            string query = @"Select Sum(Salary) from Employees;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && decimal.TryParse(result.ToString(), out decimal Salary))
                {
                    TotalSlary = Salary;
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return TotalSlary;
        }

    }
}

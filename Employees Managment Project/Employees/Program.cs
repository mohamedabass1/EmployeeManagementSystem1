using EmployeeBusinessLaye;
using EmployeeBusinessLayer;
using System;
using System.Data;

namespace Employees
{
    internal class Program
    {
        static void _testFindEmployee(int ID)
        {
            clsEmployee Employee = clsEmployee.Find(ID);

            if (Employee != null)
            {
                Console.WriteLine("Employee was found");

                Console.WriteLine(Employee.FirstName);
                Console.WriteLine(Employee.LastName);
                Console.WriteLine(Employee.Email);
                Console.WriteLine(Employee.Phone);
                Console.WriteLine(Employee.Address);
                Console.WriteLine(Employee.DateOfBirth);
                Console.WriteLine(Employee.Salary);
                Console.WriteLine(Employee.Country);

                Console.WriteLine(Employee.ImagePath);
            }
            else
                Console.WriteLine("Employee was not found");



        }

        static void _testAddNewEmployee()
        {
            clsEmployee employee = new clsEmployee();

            employee.FirstName = "Ali";
            employee.LastName = "Abass";
            employee.Email = "Ali@.com";
            employee.Phone = "777663222";
            employee.Address = "Snaaa";
            employee.DateOfBirth = new DateTime(2000, 7, 8);
            employee.Country = "Yemen";
            employee.Salary = 500;


            if (employee.Save())
            {
                Console.WriteLine("Employee Saved Successfully Employee ID: " + employee.ID);
            }
            else
                Console.WriteLine("Save Failed");
        }

        static void _testUpdteEmployee(int ID)
        {
            clsEmployee employee = clsEmployee.Find(ID);

            if (employee != null)
            {
                employee.FirstName = "Lamiaa";
                employee.LastName = "Hassan";
                employee.Email = "Lamyaa@.com";
                employee.Phone = "777663222";
                employee.Address = "IBB";
                employee.DateOfBirth = new DateTime(2005, 7, 8);
                employee.Country = "Yemen";
                employee.Salary = 100;


                if (employee.Save())
                {
                    Console.WriteLine("Employee Saved Successfully Employee ID: " + employee.ID);
                }
                else
                    Console.WriteLine("Save Failed");

            }
            else
                Console.WriteLine("Employee not found");
        }

        static void _testDeleteEmployee(int ID)
        {
            if (clsEmployee.DeleteEmployee(ID))
            {
                Console.WriteLine("Employee Deleted Successfully");
            }
            else
                Console.WriteLine("Employee Deleted Failed");

        }

        static public void GetAllEmployees()
        {
            DataTable dtEmployees = clsEmployee.GetAllEmployees();

            foreach (DataRow row in dtEmployees.Rows)
            {
                Console.WriteLine();
                Console.WriteLine($"EmployeeID: {row["EmployeeID"]}");
                Console.WriteLine($"FullName: {row["FirstName"]} {row["LastName"]}");
                Console.WriteLine($"Email: {row["Email"]} , Phone: {row["Phone"]}");
                Console.WriteLine($"Salary: {row["Salary"]} , Country: {row["Country"]}");
            }
        }

        static public void _testIsExistsEmployee(int ID)
        {
            if (clsEmployee.IsExistsEmployee(ID))
            {
                Console.WriteLine($"Employee with ID [{ID}] Is Exists. ");
            }
            else
                Console.WriteLine($"Employee with ID [{ID}] Is NOT Exists. ");

        }

        //--------------------------------------------

        static void _testAddNewUser(string UserName, string Password)
        {
            clsUser User1 = new clsUser();



            User1.UserName = UserName;
            User1.Password = Password;

            if (User1.Save())
            {
                Console.WriteLine("User Save Successfully ID: " + User1.ID);
            }
            else
                Console.WriteLine("User Save Failed ");

        }

        static void _testUpdateUser(int ID, string UserName, string Password)
        {
            clsUser User1 = clsUser.Find(ID);

            if (User1 != null)
            {
                User1.UserName = UserName;
                User1.Password = Password;

                if (User1.Save())
                {
                    Console.WriteLine("User Updated Successfully : ");
                }
                else
                    Console.WriteLine("User Save Failed ");


            }
        }

        static void _testDeleteUser(int ID)
        {
            if (clsUser.IsUserExists(ID))
            {
                if (clsUser.DeleteUser(ID))
                {
                    Console.WriteLine("User Deleted Successfully");
                }
                else
                    Console.WriteLine("User Deleted Failed");
            }
            else
                Console.WriteLine("User Not Exists");
        }

        static void _testIsExistsUser(int ID)
        {
            if (clsUser.IsUserExists(ID))
            {
                Console.WriteLine($"User with ID [{ID}] Is Exists. ");
            }
            else
                Console.WriteLine($"User with ID [{ID}] Is NOT Exists. ");

        }

        static void _testIsExistsUser(string UserName)
        {
            if (clsUser.IsUserExists(UserName))
            {
                Console.WriteLine($"User with UserName [{UserName}] Is Exists. ");
            }
            else
                Console.WriteLine($"User with UserName [{UserName}] Is NOT Exists. ");

        }



        static void Main(string[] args)
        {

            //_testFindEmployee(9);
            // _testAddNewEmployee();

            //_testUpdteEmployee(10);

            //_testDeleteEmployee(10);

            // GetAllEmployees();

            //_testIsExistsEmployee(9);


            ///--------------------
            ///
            // _testAddNewUser("Mohamed Abass", "1111");

            //_testUpdateUser(2, "Doon", "2222");

            _testDeleteUser(3);

            //_testIsExistsUser(2);

            //_testIsExistsUser("Mod");

            Console.ReadKey();
        }
    }
}

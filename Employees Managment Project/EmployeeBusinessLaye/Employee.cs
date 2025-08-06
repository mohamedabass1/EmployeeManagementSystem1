using EmployeeDataAccessLayer;
using System;
using System.Data;

namespace EmployeeBusinessLayer
{
    public class clsEmployee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string ImagePath { get; set; }


        enum enMode { AddNew = 1, Update = 2 };
        enMode Mode;

        public clsEmployee()
        {

            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.Salary = -1;
            this.Country = "";
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }
        private clsEmployee(int ID, string FirstName, string LastName, string Email, string Phone, string Address,
                                        DateTime DateOfBirth, decimal Salary, string Country, string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.Salary = Salary;
            this.Country = Country;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }

        private bool _AddNewEmployee()
        {
            this.ID = clsEmployeeDataAccess.AddNewEmployee(this.FirstName, this.LastName, Email,
                                           this.Phone, this.Address, this.DateOfBirth, this.Salary, this.Country, this.ImagePath);

            return (this.ID != -1);

        }

        private bool _UpdateEmployee()
        {
            return clsEmployeeDataAccess.UpdateEmployee(this.ID, this.FirstName, this.LastName, Email,
                                           this.Phone, this.Address, this.DateOfBirth, this.Salary, this.Country, this.ImagePath);
        }

        static public clsEmployee Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", Country = "", ImagePath = "";

            decimal Salary = 0;
            DateTime DateOfBirth = DateTime.Now;


            if (clsEmployeeDataAccess.Find(ID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address, ref DateOfBirth, ref Salary, ref Country, ref ImagePath))
            {

                return new clsEmployee(ID, FirstName, LastName, Email, Phone, Address, DateOfBirth, Salary, Country, ImagePath);

            }
            else
                return null;

        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewEmployee())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    {
                        return _UpdateEmployee();

                    }

            }

            return false;

        }


        static public bool DeleteEmployee(int ID)
        {
            return clsEmployeeDataAccess.DeleteEmployee(ID);
        }

        static public DataTable GetAllEmployees()
        {
            return clsEmployeeDataAccess.GetAllEmployees();
        }

        static public bool IsExistsEmployee(int ID)
        {
            return clsEmployeeDataAccess.IsExistsEmployee(ID);
        }


        static public int GetTheTotalNumberOfEmployees()
        {
            return clsEmployeeDataAccess.GetTheTotalNumberOfEmployees();
        }

        static public decimal GetTheTotalSalaryOfEmployees()
        {
            return clsEmployeeDataAccess.GetTheTotalSalaryOfEmployees();
        }

    }
}

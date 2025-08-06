using EmployeeDataAccessLayer;
using System.Data;

namespace EmployeeBusinessLaye
{
    public class clsUser
    {


        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode;
        public clsUser()
        {
            this.ID = -1;
            this.UserName = "";
            this.Password = "";

            Mode = enMode.AddNew;

        }

        private clsUser(int ID, string UserName, string Password)
        {
            this.ID = ID;
            this.UserName = UserName;
            this.Password = Password;

            Mode = enMode.Update;
        }


        static public clsUser Find(int ID)
        {
            string UserName = "", Password = "";
            if (clsUserDataAccess.Find(ID, ref UserName, ref Password))
            {
                return new clsUser(ID, UserName, Password);
            }
            else return null;
        }

        static public clsUser Find(string UserName)
        {
            int ID = 0;
            string Password = "";


            if (clsUserDataAccess.Find(UserName, ref ID, ref Password))
            {
                return new clsUser(ID, UserName, Password);
            }
            else return null;
        }

        private bool _AddNewUser()
        {
            this.ID = clsUserDataAccess.AddNewUser(this.UserName, this.Password);

            return (this.ID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserDataAccess.UpdateUser(this.ID, this.UserName, this.Password);
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (!IsUserExists(this.UserName))
                        {
                            if (_AddNewUser())
                            {
                                Mode = enMode.Update;
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateUser();
                    }
            }

            return false;

        }


        static public bool DeleteUser(int ID)
        {
            if (IsUserExists(ID))
                return clsUserDataAccess.DeleteUser(ID);
            else
                return false;

        }

        static public bool IsUserExists(string UserName)
        {
            return clsUserDataAccess.IsUserExists(UserName);
        }

        static public bool IsUserExists(int ID)
        {
            return clsUserDataAccess.IsUserExists(ID);
        }

        static public bool IsUserExists(string UserName, string Password)
        {
            return clsUserDataAccess.IsUserExists(UserName, Password);
        }
        static public int GetTheTotalNumberOfUsers()
        {
            return clsUserDataAccess.GetTheTotalNumberOfUsers();

        }

        static public DataTable GetAllUsers()
        {
            return clsUserDataAccess.GetAllUsers();
        }

    }
}

using EmployeeBusinessLaye;
using System;
using System.Windows.Forms;

namespace EmployeesWindowsForm
{
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }

        clsUser _User;
        int _UserID = -1;

        enum _enMode { AddNew = 1, Update = 2 };
        _enMode _Mode = _enMode.AddNew;

        private void _RefrishUserList()
        {
            dgvUsersList.DataSource = clsUser.GetAllUsers();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefrishUserList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete this user? ", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser((int)dgvUsersList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Successfully.");
                    _RefrishUserList();
                }
                else
                    MessageBox.Show("Deleted Failed.");
            }
        }

        private void _LoadData()
        {
            // Fill the Form from the user

            lblUserID.Text = _User.ID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _UserID = (int)dgvUsersList.CurrentRow.Cells[0].Value;
            _Mode = _enMode.Update;

            _User = clsUser.Find(_UserID);
            _LoadData();
        }

        private void _Clear()
        {
            _UserID = -1;
            _Mode = _enMode.AddNew;

            lblUserID.Text = "???";
            txtUserName.Clear();
            txtPassword.Clear();

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("User Name or Password can not be empty.");
                return;
            }



            if (_Mode == _enMode.AddNew)
            {
                _User = new clsUser();
            }
            else
                _User = clsUser.Find(_UserID); // in update mode

            // Fill the user data from the Form
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;



            if (_User.Save())
            {

                string message = (_Mode == _enMode.AddNew) ? "User added successfully." : "User updated successfully.";
                MessageBox.Show(message);
                _RefrishUserList();
                _Clear();
            }
            else
            {
                MessageBox.Show("Data Saved Failed.");

            }


        }

        private void _OpenForm(Form form)
        {
            this.Hide();
            form.Show();
        }

        private void btnManageEmployees_Click(object sender, EventArgs e)
        {
            //frmManageEmployees frmManage = new frmManageEmployees();
            //frmManage.Show();
            //this.Hide();

            _OpenForm(new frmManageEmployees());
        }
        private void btnDashoard_Click(object sender, EventArgs e)
        {
            //frmDashBoard frmDashBoard = new frmDashBoard();
            //this.Hide();
            //frmDashBoard.Show();
            _OpenForm(new frmDashBoard());

        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _OpenForm(new frmLogin());

        }
    }
}

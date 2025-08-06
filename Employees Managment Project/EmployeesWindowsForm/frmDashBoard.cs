using EmployeeBusinessLaye;
using EmployeeBusinessLayer;
using System;
using System.Windows.Forms;

namespace EmployeesWindowsForm
{
    public partial class frmDashBoard : Form
    {

        public frmDashBoard()
        {
            InitializeComponent();

        }

        private void frmDashBoard_Load(object sender, EventArgs e)
        {

            lblTotalEmployees.Text = clsEmployee.GetTheTotalNumberOfEmployees().ToString();

            lblTotalSalary.Text = clsEmployee.GetTheTotalSalaryOfEmployees().ToString();

            lblTotalUsers.Text = ((int)clsUser.GetTheTotalNumberOfUsers()).ToString();

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

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            //frmManageUsers frmManage = new frmManageUsers();
            //frmManage.Show();
            //this.Hide();
            _OpenForm(new frmManageUsers());

        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _OpenForm(new frmLogin());

        }
    }
}

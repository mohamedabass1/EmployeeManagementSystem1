using EmployeeBusinessLayer;
using System;
using System.Windows.Forms;

namespace EmployeesWindowsForm
{
    public partial class frmManageEmployees : Form
    {


        clsEmployee _Employee;
        int _EmployeeID = -1;

        enum _enMode { AddNew = 1, Update = 2 }
        _enMode _Mode = _enMode.AddNew;

        public frmManageEmployees()
        {

            InitializeComponent();

        }

        private void _RefeishEmployeesList()
        {
            dgvEmployeesList.DataSource = clsEmployee.GetAllEmployees();
        }


        private void frmManageEmploees_Load(object sender, EventArgs e)
        {
            _RefeishEmployeesList();

        }


        private void _Clear()
        {

            _EmployeeID = -1;
            _Mode = _enMode.AddNew;


            lblEmployeeID.Text = "??";
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            dtpDateOfBirth.Value = DateTime.Now;
            txtSalary.Clear();
            txtCountry.Clear();
            pictureBox1.ImageLocation = null;

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you suer want to delete this employee? ", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (clsEmployee.DeleteEmployee((int)dgvEmployeesList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Successfully.");
                    _RefeishEmployeesList();
                }
                else
                    MessageBox.Show("Deleted Failed.");

            }
        }

        void _LoadData()
        {

            lblEmployeeID.Text = _Employee.ID.ToString();
            txtFirstName.Text = _Employee.FirstName;
            txtLastName.Text = _Employee.LastName;
            txtEmail.Text = _Employee.Email;
            txtPhone.Text = _Employee.Phone;
            txtAddress.Text = _Employee.Address;
            dtpDateOfBirth.Value = _Employee.DateOfBirth;
            txtSalary.Text = _Employee.Salary.ToString();
            txtCountry.Text = _Employee.Country;

            if (_Employee.ImagePath != "")
            {
                pictureBox1.Load(_Employee.ImagePath);
            }

        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            _EmployeeID = (int)dgvEmployeesList.CurrentRow.Cells[0].Value;
            _Mode = _enMode.Update;

            _Employee = clsEmployee.Find(_EmployeeID);

            // Load date from Employee Object to form
            _LoadData();
        }

        private void txtSalary_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(txtSalary.Text, out decimal salary))
            {
                e.Cancel = true;
                txtSalary.Focus();
                errorProvider1.SetError(txtSalary, "Invalid Salary Value.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtSalary, "");

            }
        }
        private void _FillEmployeeFromForm()
        {

            // تعبئة البيانات من الفورم
            // Fill Employee Data from the Form

            _Employee.FirstName = txtFirstName.Text;
            _Employee.LastName = txtLastName.Text;
            _Employee.Email = txtEmail.Text;
            _Employee.Phone = txtPhone.Text;
            _Employee.Address = txtAddress.Text;
            _Employee.DateOfBirth = dtpDateOfBirth.Value;
            _Employee.Country = txtCountry.Text;

            try
            {

                _Employee.Salary = decimal.Parse(txtSalary.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid salary value. Salary is 0 ");

                _Employee.Salary = 0;
            }




            if (pictureBox1.ImageLocation != null)
            {
                _Employee.ImagePath = pictureBox1.ImageLocation;
            }
            else
                _Employee.ImagePath = "";


        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Pleas Enter employee Data");
                return;
            }


            //if (_Mode == _enMode.AddNew)
            //{
            //    _Employee = new clsEmployee();
            //}
            //else
            //    _Employee = clsEmployee.Find(_EmployeeID);

            _Employee = ((_Mode == _enMode.AddNew) ? new clsEmployee() : clsEmployee.Find(_EmployeeID));


            // Fill Employee Data from the Form
            _FillEmployeeFromForm();



            if (_Employee.Save())
            {
                string message = (_Mode == _enMode.AddNew) ? "Employee added successfully." : "Employee update successfully";
                MessageBox.Show(message);

                _RefeishEmployeesList();

                _Clear();

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");


        }
        private void btnImportImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;

                pictureBox1.Load(selectedFilePath);
            }
        }

        private void _OpenForm(Form form)
        {
            this.Hide();
            form.Show();
        }
        private void btnDashBoard_Click(object sender, EventArgs e)
        {

            //frmDashBoard frmDashBoard = new frmDashBoard();
            //this.Hide();
            //frmDashBoard.Show();

            _OpenForm(new frmDashBoard());
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


using System;
using System.Windows.Forms;

namespace EmployeesWindowsForm
{
    public partial class frmMainForm : Form
    {
        public frmMainForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewLayer
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            lbTime.Text = DateTime.Now.ToString("H:M:s");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TimeSystem_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("H:M:s");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable dataRow = BusinessLayer.BStaff.Login(txtUsername.Text, txtPassword.Text);

            if (dataRow.Rows.Count == 0)
            {
                MessageBox.Show("User or Password incorrect", "Business System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            else
            {
                frmMain mainFrm = new frmMain();
                mainFrm.IdStaff = dataRow.Rows[0][0].ToString();
                mainFrm.NameStaff = dataRow.Rows[0][1].ToString();
                mainFrm.SurnameStaff = dataRow.Rows[0][2].ToString();
                mainFrm.AccessStaff = dataRow.Rows[0][3].ToString();

                mainFrm.Show();
                this.Hide();
            }
        }

        
    }
}

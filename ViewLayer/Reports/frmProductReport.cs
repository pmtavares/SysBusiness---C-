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
    public partial class frmProductReport : Form
    {
        public frmProductReport()
        {
            InitializeComponent();
        }

        private void frmProductReport_Load(object sender, EventArgs e)
        {
            
            // TODO: This line of code loads data into the 'dstMain.spshow_product' table. You can move, or remove it, as needed.
            this.spshow_productTableAdapter.Fill(this.dstMain.spshow_product);

            this.reportViewer1.RefreshReport();
        }
    }
}

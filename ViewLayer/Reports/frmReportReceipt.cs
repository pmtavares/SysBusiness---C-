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
    public partial class frmReportReceipt : Form
    {

        private int _Idsale;

        public int Idsale
        {
            get { return _Idsale; }
            set { _Idsale = value; }
        }

      
        public frmReportReceipt()
        {
            InitializeComponent();
        }

        private void frmReportReceipt_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dstMain.spreport_invoice' table. You can move, or remove it, as needed.
            try
            {
                this.spreport_invoiceTableAdapter.Fill(this.dstMain.spreport_invoice, this.Idsale);

                this.reportViewer1.RefreshReport();
            }
            catch(Exception ex)
            {
                this.reportViewer1.RefreshReport();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace ViewLayer.Queries
{
    public partial class frmQuerie_Stock_Products : Form
    {
        public frmQuerie_Stock_Products()
        {
            InitializeComponent();
        }
        private void HideCollumns()
        {
            this.dataList.Columns[0].Visible = false;
            
        }

        private void Show()
        {
            this.dataList.DataSource = BProduct.StockProduct();
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        private void frmQuerie_Stock_Products_Load(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}

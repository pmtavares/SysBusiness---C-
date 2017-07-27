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

namespace ViewLayer
{
    public partial class frmSeeProductInput : Form
    {
        public frmSeeProductInput()
        {
            InitializeComponent();
        }

        private void frmSeeProductInput_Load(object sender, EventArgs e)
        {
            this.ShowValues();
        }

        private void HideCollumns()
        {
            this.dataList.Columns[0].Visible = false;
            this.dataList.Columns[1].Visible = false;
            this.dataList.Columns[5].Visible = false; //Hide the image column
            this.dataList.Columns[6].Visible = false;
            this.dataList.Columns[8].Visible = false;
        }

        private void ShowValues()
        {
            this.dataList.DataSource = BProduct.ShowValues();
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        private void SearchName()
        {
            this.dataList.DataSource = BProduct.SearchName(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.SearchName();
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            frmInput frm = frmInput.GetInstance();

            string par1, par2;

            par1 = Convert.ToString(this.dataList.CurrentRow.Cells["idproduct"].Value);
            par2 = Convert.ToString(this.dataList.CurrentRow.Cells["name"].Value);

            frm.setProduct(par1, par2);
            this.Hide();

        }
    }
}

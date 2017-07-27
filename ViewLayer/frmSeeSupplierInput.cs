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
    public partial class frmSeeSupplierInput : Form
    {
        public frmSeeSupplierInput()
        {
            InitializeComponent();
        }


        private void HideCollumns()
        {
            this.dataList.Columns[0].Visible = false;
            this.dataList.Columns[1].Visible = false;
        }

        private void ShowValues()
        {
            this.dataList.DataSource = BSupplier.ShowValues();
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        private void SearchName()
        {
            this.dataList.DataSource = BSupplier.SearchName(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void SearchDocument()
        {
            this.dataList.DataSource = BSupplier.SearchDocument(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtSearch.Text = string.Empty;
        }

        private void frmSeeSupplierInput_Load(object sender, EventArgs e)
        {
            this.ShowValues();
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            frmInput frm = frmInput.GetInstance();

            string par1, par2;

            par1 = Convert.ToString(this.dataList.CurrentRow.Cells["idsupply"].Value);
            par2 = Convert.ToString(this.dataList.CurrentRow.Cells["company"].Value);

            frm.setSupplier(par1, par2);
            this.Hide();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.SearchName();
        }
       
    }
}

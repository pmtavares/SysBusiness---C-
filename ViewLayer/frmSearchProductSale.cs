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
    public partial class frmSearchProductSale : Form
    {
        public frmSearchProductSale()
        {
            InitializeComponent();
        }

        
        private void HideCollumns()
        {
            this.dataList.Columns[0].Visible = false;
            this.dataList.Columns[1].Visible = false;
            //this.dataList.Columns[5].Visible = false; //Hide the image column
            //this.dataList.Columns[6].Visible = false;
            //this.dataList.Columns[8].Visible = false;
        }

        private void ShowProduct_Sale_Name() //show products by name
        {
            this.dataList.DataSource = BSale.ShowProduct_Sale_Name(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void ShowProduct_Sale_Code() //show products by code
        {
            this.dataList.DataSource = BSale.ShowProduct_Sale_Code(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        
       
       

        private void frmSearchProductSale_Load(object sender, EventArgs e)
        {
            this.ShowProduct_Sale_Name();
            this.cbTypeSearch.SelectedIndex = 0;
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            

            if (this.cbTypeSearch.Text.Equals("Name"))
            {
                this.ShowProduct_Sale_Name();
            }
            else if (this.cbTypeSearch.Text.Equals("Code"))
            {
                this.ShowProduct_Sale_Code();
            }
        }

        private void dataList_DoubleClick_1(object sender, EventArgs e)
        {
            frmSale frm = frmSale.GetInstance();

            string par1, par2;
            decimal par3, par4;
            int par5;
            DateTime par6;

            par1 = Convert.ToString(this.dataList.CurrentRow.Cells["idinput_detail"].Value);
            par2 = Convert.ToString(this.dataList.CurrentRow.Cells["name"].Value);
            par3 = Convert.ToDecimal(this.dataList.CurrentRow.Cells["value_purchased"].Value);
            par4 = Convert.ToDecimal(this.dataList.CurrentRow.Cells["value_sold"].Value);
            par5 = Convert.ToInt32(this.dataList.CurrentRow.Cells["current_stoque"].Value);
            par6 = Convert.ToDateTime(this.dataList.CurrentRow.Cells["expired_date"].Value);

            frm.setProduct(par1, par2,par3, par4, par5, par6);
            this.Hide();
        }

        

    }
}

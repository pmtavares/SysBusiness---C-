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
    public partial class frmSearchClient : Form
    {
        public frmSearchClient()
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
            this.dataList.DataSource = BClient.ShowValues();
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        private void SearchName()
        {
            this.dataList.DataSource = BClient.SearchName(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void SearchDocument()
        {
            this.dataList.DataSource = BClient.SearchDocument(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }


        private void frmSearchClient_Load(object sender, EventArgs e)
        {
            this.cbSearch.SelectedIndex = 0;
            this.ShowValues();
           
            
            
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtSearch.Text = string.Empty;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (this.cbSearch.Text.Equals("Name"))
            {
                this.SearchName();
            }
            else if (this.cbSearch.Text.Equals("Document"))
            {
                this.SearchDocument();
            }
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            frmSale form = frmSale.GetInstance();

            string par1, par2;
            par1 = Convert.ToString(dataList.CurrentRow.Cells["idClient"].Value);
            par2 = Convert.ToString(dataList.CurrentRow.Cells["name"].Value);

            form.setClient(par1, par2);

            this.Hide();
        }
    }
}

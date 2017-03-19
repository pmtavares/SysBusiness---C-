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
    public partial class frmSearchCategory : Form
    {
        public frmSearchCategory()
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
            this.dataList.DataSource = BCategory.ShowValues();
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        private void SearchName()
        {
            this.dataList.DataSource = BCategory.SearchName(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.SearchName();
        }

        private void frmSearchCategory_Load(object sender, EventArgs e)
        {
            this.ShowValues();
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            frmProduct form = frmProduct.GetInstance();

            string par1, par2;
            par1 = Convert.ToString(dataList.CurrentRow.Cells["idcategory"].Value);
            par2 = Convert.ToString(dataList.CurrentRow.Cells["name"].Value);

            form.setCategory(par1, par2);

            this.Hide();
        }
    }
}

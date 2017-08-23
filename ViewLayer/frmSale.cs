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
    public partial class frmSale : Form
    {

        private static frmSale  _Instance;
        public int idstaff;
        //Variables to program the insertion functionality
        private bool isNew;
        private DataTable dtDetails;
        private decimal totalPaid = 0;

         public static frmSale GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new frmSale();
            }
            
            return _Instance;
        }
        public frmSale()
        {
            InitializeComponent();
            this.ttMessage.SetToolTip(this.btnSearchClient, "Search for Client");
            this.ttMessage.SetToolTip(this.btnSearchProduct, "Search for product");
            this.txtidClient.Enabled = false;
            this.txtIdProduct.Enabled = false;
            this.txtClient.Enabled = false;
            this.txtProduct.Enabled = false;
        }

        private void MessageOk(string message)
        {
            MessageBox.Show(message, "Business System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Erro message
        private void MessageError(string message)
        {
            MessageBox.Show(message, "Business System", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //
        private void Clean()
        {
            this.txtidClient.Text = string.Empty;
            this.txtClient.Text = string.Empty;

            this.txtProduct.Text = string.Empty;
            this.txtSerie.Text = string.Empty;
            this.txtCorelativ.Text = string.Empty;
            this.txtCurrentStock.Text = string.Empty;
            this.txtPriceSale.Text = string.Empty;
            this.txtVat.Text = string.Empty;
            
            this.txtPricePurchased.Text = string.Empty;
            this.lblTotalToPay.Text = "0.0";
            this.CreateTable();
            //this.CleanDetails();


        }
        private void CleanDetails()
        {
            this.txtIdProduct.Text = string.Empty;
            this.txtProduct.Text = string.Empty;
            this.txtCurrentStock.Text = string.Empty;
            this.txtPricePurchased.Text = string.Empty;
            this.txtPriceSale.Text = string.Empty;
            this.txtDiscount.Text = string.Empty;
        }

        //enabled boxes
        private void EnabledField(bool value)
        {
            this.dtpDate.Enabled = value;
            this.dtpExpires.Enabled = value;
            this.txtPriceSale.ReadOnly = !value;
            this.txtSerie.ReadOnly = !value;
            this.txtDiscount.ReadOnly = !value;
            this.txtQuantity.ReadOnly = !value;
            this.txtCorelativ.ReadOnly = !value;
            this.txtVat.ReadOnly = !value;
            this.txtPricePurchased.Enabled = value;
            this.txtPriceSale.Enabled = value;
            this.txtCurrentStock.Enabled = value;

            this.cbReceipt.Enabled = value;


            this.btnAdd.Enabled = value;
            this.btnSearchClient.Enabled = value;
            this.btnSearchProduct.Enabled = value;

        }

        private void Enabledbuttons()
        {
            if (this.isNew)
            {
                this.EnabledField(true);
                this.btnNew.Enabled = false;
                this.btnSave.Enabled = true;

                this.btnCancel.Enabled = true;
                this.btnSearchClient.Enabled = true;
                this.btnSearchProduct.Enabled = true;

                this.btnCancelInput.Enabled = false;
                this.btnSearch.Enabled = false;

            }
            else
            {
                this.EnabledField(false);
                this.btnNew.Enabled = true;
                this.btnSave.Enabled = false;

                this.btnCancel.Enabled = false;
                this.btnSearchClient.Enabled = false;
                this.btnSearchProduct.Enabled = false;

                this.btnCancelInput.Enabled = true;
                this.btnSearch.Enabled = true;

            }

        }
        private void HideCollumns()
        {
            this.dataList.Columns[0].Visible = false;
            this.dataList.Columns[1].Visible = false;

        }

        private void Show()
        {
            this.dataList.DataSource = BSale.Show();
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void ShowDate()
        {
            this.dataList.DataSource = BSale.SearchNameDate(this.dtpInitialDate.Value.ToString("yyyy/MM/dd"), this.dtpFinalDate.Value.ToString("yyyy/MM/dd"));
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        //Show details sale
        private void ShowDetailSale()
        {
            this.dataListDetails.DataSource = BSale.ShowDetailsSale(this.txtId.Text);


        }

        public void setClient(string idClient, string name)
        {
            this.txtidClient.Text = idClient;
            this.txtClient.Text = name;
        }

        public void setProduct(string idproduct, string name,
            decimal price_purchase, decimal price_sale, int stock, DateTime expire_date )
        {
            this.txtIdProduct.Text = idproduct;
            this.txtProduct.Text = name;
            this.txtPricePurchased.Text = Convert.ToString(price_purchase);
            this.txtPriceSale.Text = Convert.ToString(price_sale);
            this.dtpExpires.Value = expire_date;
            this.txtCurrentStock.Text = Convert.ToString(stock);
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.EnabledField(false);
            this.Enabledbuttons();
            this.Show();
            //this.HideCollumns();
            this.CreateTable();
            
            //this.ShowInput();
            this.cbReceipt.SelectedIndex = 0;
        }
        private void frmSale_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Instance = null;
       
        }

        private void btnSearchClient_Click(object sender, EventArgs e)
        {
            frmSearchClient frm = new frmSearchClient();
            
            frm.Show();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.isNew = true;
            this.Enabledbuttons();
            this.Clean();
            this.EnabledField(true);
            this.txtSerie.Focus();
            this.CleanDetails();
            this.txtQuantity.Text = "0";
            this.txtVat.Text = "0";
            this.txtQuantity.Text = "1";
                
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.isNew = false;
            this.EnabledField(false);
            this.Enabledbuttons();
            this.Clean();
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            frmSearchProductSale frm = new frmSearchProductSale();

            frm.Show();
        }

        private void CreateTable()
        {
            this.dtDetails = new DataTable("Detail");
            this.dtDetails.Columns.Add("idinput_detail", System.Type.GetType("System.Int32"));
            this.dtDetails.Columns.Add("product", System.Type.GetType("System.String"));
            this.dtDetails.Columns.Add("value", System.Type.GetType("System.Decimal"));
            this.dtDetails.Columns.Add("quantity", System.Type.GetType("System.Int32"));
            
            this.dtDetails.Columns.Add("discount", System.Type.GetType("System.Int32"));
           
            this.dtDetails.Columns.Add("subtotal", System.Type.GetType("System.Decimal"));
            // this.dtDetails.Columns.Add("vat", System.Type.GetType("System.Decimal"));

            this.dataListDetails.DataSource = this.dtDetails;
        }

        private void chkCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDelete.Checked)
            {
                this.dataList.Columns[0].Visible = true;
                this.dataList.Columns[1].Visible = true;
            }
            else
            {
                this.dataList.Columns[0].Visible = false;
                this.dataList.Columns[1].Visible = true;
            }
        }

        private void dataList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataList.Columns["Delete"].Index)
            {
                DataGridViewCheckBoxCell chkDelete= (DataGridViewCheckBoxCell)dataList.Rows[e.RowIndex].Cells["Delete"];
                chkDelete.Value = !Convert.ToBoolean(chkDelete.Value);
            }
        }

        private void btnCancelInput_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult option;
                option = MessageBox.Show("Do you really want to delete?", "SysBusiness", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (option == DialogResult.OK)
                {
                    string Code;
                    string resp = "";

                    foreach (DataGridViewRow row in dataList.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Code = Convert.ToString(row.Cells[1].Value);
                            resp = BSale.Delete(Convert.ToInt32(Code));

                            if (resp.Equals("OK"))
                            {
                                this.MessageOk("Success deleted");
                            }
                            else
                            {
                                this.MessageError(resp);
                            }
                        }
                    }
                    this.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataList.CurrentRow.Cells["idsale"].Value);
            this.txtClient.Text = Convert.ToString(this.dataList.CurrentRow.Cells["client"].Value);
            this.dtpDate.Value = Convert.ToDateTime(this.dataList.CurrentRow.Cells["date"].Value);

            this.cbReceipt.Text = Convert.ToString(this.dataList.CurrentRow.Cells["type_receipt"].Value);
            this.txtSerie.Text = Convert.ToString(this.dataList.CurrentRow.Cells["serie"].Value);
            this.txtCorelativ.Text = Convert.ToString(this.dataList.CurrentRow.Cells["corelation"].Value);
            //this.txtVat.Text = Convert.ToString(this.dataList.CurrentRow.Cells["vat"].Value);
            this.lblTotalToPay.Text = Convert.ToString(this.dataList.CurrentRow.Cells["Total"].Value);
            this.ShowDetailSale();
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (txtSerie.Text == string.Empty)
                {
                    MessageError("Fill the fields");
                    //errorIcon.SetError(txtId, "Insert the supply");
                    
                    errorIcon.SetError(txtSerie, "Insert the serie");
                }
                else
                {


                    if (this.isNew)
                    {
                        resp = BSale.Insert(Convert.ToInt32(this.txtidClient.Text), idstaff, Convert.ToDateTime(this.dtpDate.Text),
                           this.cbReceipt.Text, txtSerie.Text, this.txtCorelativ.Text, this.txtVat.Text, dtDetails);
                    }


                    if (resp.Equals("OK"))
                    {
                        if (this.isNew)
                        {
                            this.MessageOk("Register saved");
                        }

                    }
                    else
                    {
                        this.MessageError(resp);
                    }

                    this.isNew = false;
                    this.Enabledbuttons();
                    this.Clean();
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (txtPricePurchased.Text == string.Empty)
                {
                    MessageError("Fill the fields");
                    //errorIcon.SetError(txtIdProduct, "Insert the product");
                    errorIcon.SetError(txtPricePurchased, "Insert the purchase price");
                }
                else
                {

                    bool save = true;

                    foreach (DataRow row in dtDetails.Rows)
                    {
                        if (Convert.ToInt32(row["idinput_detail"]) == Convert.ToInt32(this.txtIdProduct.Text))
                        {
                            save = false;
                            this.MessageError("Product already added");
                        }
                    }

                    if (save && Convert.ToInt32(txtQuantity.Text) <= Convert.ToInt32(txtCurrentStock.Text))
                    {
                        //code to add to datalist
                        decimal subtotal = Convert.ToDecimal(this.txtQuantity.Text) * Convert.ToDecimal(this.txtPricePurchased.Text);


                        //Calculate the total
                        totalPaid = totalPaid + subtotal;
                        this.lblTotalToPay.Text = totalPaid.ToString(" $ #0.00#");

                        DataRow row = this.dtDetails.NewRow();

                        row["idinput_detail"] = Convert.ToInt32(this.txtIdProduct.Text);
                        row["product"] = this.txtProduct.Text;
                        row["quantity"] = Convert.ToInt32(this.txtQuantity.Text);
                        row["value"] = Convert.ToDecimal(this.txtPricePurchased.Text);
                        
                        row["discount"] = this.txtDiscount.Text;
                        row["subtotal"] = subtotal;

                        this.dtDetails.Rows.Add(row);
                        this.CleanDetails();
                    }
                    else
                    {
                        MessageError("There is no this quantity in stock");
                        this.txtQuantity.Text = string.Empty;
                        this.txtQuantity.Focus();

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.ShowDate();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int indexLine = this.dataListDetails.CurrentCell.RowIndex;

                DataRow row = this.dtDetails.Rows[indexLine];


                this.totalPaid = this.totalPaid - Convert.ToDecimal(row["subtotal"].ToString());
                this.lblTotalToPay.Text = totalPaid.ToString("#0.00#");

                //remove the line from data list
                this.dtDetails.Rows.Remove(row);
            }
            catch (Exception ex)
            {
                MessageError("There is no line to remove");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportReceipt frm = new frmReportReceipt();
            frm.Idsale = Convert.ToInt32(this.dataList.CurrentRow.Cells["idsale"].Value);
            frm.ShowDialog();
        }

        public void HideTab()
        {
            this.tabControl1.Controls.Remove(tabPage2); //remove one tab
            this.btnCancelInput.Visible = false;
            this.chkDelete.Visible = false;
            this.btnPrint.Visible = false;

        }
    }
}

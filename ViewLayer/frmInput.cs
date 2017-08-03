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
    public partial class frmInput : Form
    {
        public int idstaff;
        private static frmInput  _Instance;
       
        //Variables to program the insertion functionality
        private bool isNew;
        private DataTable dtDetails;
        private decimal totalPaid = 0;


        public static frmInput GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new frmInput();
            }
            
            return _Instance;
        }
        public frmInput()
        {
            InitializeComponent();

            this.ttMessage.SetToolTip(this.btnSearchSupplier, "Search for supply");
            this.ttMessage.SetToolTip(this.btnSearchProduct, "Search for product");
            this.txtidSupplier.Enabled = false;
            this.txtIdProduct.Enabled = false;
            this.txtSupplier.Enabled = false;
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
            this.txtidSupplier.Text = string.Empty;
            this.txtSupplier.Text = string.Empty;
            
            this.txtProduct.Text = string.Empty;
            this.txtSerie.Text = string.Empty;
            this.txtCorelativ.Text = string.Empty;
            this.txtInitialStoque.Text = string.Empty;
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
            this.txtInitialStoque.Text = string.Empty;
            this.txtPricePurchased.Text = string.Empty;
            this.txtPriceSale.Text = string.Empty;
        }

        //enabled boxes
        private void EnabledField(bool value)
        {
            this.dtpDate.Enabled = value;
            this.dtpOrdered.Enabled = value;
            this.dtpExpire.Enabled = value;
            this.txtSerie.ReadOnly = !value;
            this.txtCorelativ.ReadOnly = !value;
            this.txtVat.ReadOnly = !value;
            this.txtPricePurchased.Enabled = value;
            this.txtPriceSale.Enabled = value;
            this.txtInitialStoque.Enabled = value;

            this.cbReceipt.Enabled = value;
            

            this.btnAdd.Enabled = value;
           
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
                this.btnSearchSupplier.Enabled = true;
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
                this.btnSearchSupplier.Enabled = false;
                this.btnSearchProduct.Enabled = false;

                this.btnCancelInput.Enabled = true;
                this.btnSearch.Enabled = true;
                
            }

        }
        private void HideCollumns()
        {
            this.dataList.Columns[0].Visible = false;
            //this.dataList.Columns[1].Visible = false;
           
        }

        private void ShowDate()
        {
            this.dataList.DataSource = BInput.SearchNameDate(this.dtpInitialDate.Value.ToString("yyyy/MM/dd"),this.dtpFinalDate.Value.ToString("yyyy/MM/dd") );
            //this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        
        //Show details input
        private void ShowDetailInput()
        {
            this.dataListDetails.DataSource = BInput.ShowDetails(this.txtId.Text);
            
           
        }

        private void ShowInput()
        {
            
            this.dataList.DataSource = BInput.Show();
            

        }

        
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

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
            catch(Exception ex)
            {
                MessageError("There is no line to remove");
            }

        }

        private void frmInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Instance = null;
        }

        public void setSupplier(string idsupplier, string name)
        {
            this.txtidSupplier.Text = idsupplier;
            this.txtSupplier.Text = name;
        }

        public void setProduct(string idproduct, string name)
        {
            this.txtIdProduct.Text = idproduct;
            this.txtProduct.Text = name;
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            frmSeeProductInput frm = new frmSeeProductInput();
            frm.Show();
        }

        private void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            frmSeeSupplierInput frm = new frmSeeSupplierInput();
            frm.Show();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            this.ShowDate();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult option;
                option = MessageBox.Show("Do you really want to cancel?", "SysBusiness", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (option == DialogResult.OK)
                {
                    string Code;
                    string resp = "";

                    foreach (DataGridViewRow row in dataList.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Code = Convert.ToString(row.Cells[1].Value);
                            resp = BInput.Cancel(Convert.ToInt32(Code));

                            if (resp.Equals("OK"))
                            {
                                this.MessageOk("Success canceled");
                            }
                            else
                            {
                                this.MessageError(resp);
                            }
                        }
                    }
                    this.ShowDetailInput();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void chkCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCancel.Checked)
            {
                this.dataList.Columns[0].Visible = true;
            }
            else
            {
                this.dataList.Columns[0].Visible = false;
            }
        }

        private void CreateTable()
        {
            this.dtDetails = new DataTable("Detail");
            this.dtDetails.Columns.Add("idproduct", System.Type.GetType("System.Int32"));
            this.dtDetails.Columns.Add("product", System.Type.GetType("System.String"));
            this.dtDetails.Columns.Add("value_purchased", System.Type.GetType("System.Decimal"));
            this.dtDetails.Columns.Add("value_sold", System.Type.GetType("System.Decimal"));
            this.dtDetails.Columns.Add("initial_stoque", System.Type.GetType("System.Int32"));
            this.dtDetails.Columns.Add("current_stoque", System.Type.GetType("System.Int32"));
            this.dtDetails.Columns.Add("produced_date", System.Type.GetType("System.DateTime"));
            this.dtDetails.Columns.Add("expired_date", System.Type.GetType("System.DateTime"));
            this.dtDetails.Columns.Add("subtotal", System.Type.GetType("System.Decimal"));
           // this.dtDetails.Columns.Add("vat", System.Type.GetType("System.Decimal"));

            this.dataListDetails.DataSource = this.dtDetails;
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.EnabledField(false);
            this.Enabledbuttons();
            this.HideCollumns();
            this.CreateTable();
            //this.ShowDetailInput();
            this.ShowInput();
            this.cbReceipt.SelectedIndex = 0;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.isNew = true;
            
            this.Enabledbuttons();
            this.Clean();
            this.EnabledField(true);
            this.txtSerie.Focus();
            //this.txtIdProduct.Enabled = false;
            this.totalPaid = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.isNew = false;
            this.EnabledField(false);
            this.Enabledbuttons();
            this.Clean();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (txtidSupplier.Text == string.Empty || txtSerie.Text == string.Empty)
                {
                    MessageError("Fill the fields");
                    errorIcon.SetError(txtidSupplier, "Insert the supply");
                    errorIcon.SetError(txtIdProduct, "Insert the product id");
                    errorIcon.SetError(txtSerie, "Insert the serie");
                }
                else
                {
                    

                    if (this.isNew)
                    {
                        resp = BInput.Insert(idstaff, Convert.ToInt32(this.txtidSupplier.Text), Convert.ToDateTime(this.dtpDate.Text),
                           this.cbReceipt.Text, txtSerie.Text, txtCorelativ.Text, Convert.ToDecimal(txtVat.Text),
                           "ISSUED", dtDetails);
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
                    this.ShowDetailInput();
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
                if (txtIdProduct.Text == string.Empty || txtInitialStoque.Text == string.Empty || txtPricePurchased.Text == string.Empty)
                {
                    MessageError("Fill the fields");
                    errorIcon.SetError(txtIdProduct, "Insert the product");
                    errorIcon.SetError(txtInitialStoque, "Insert the iniital stock");
                    errorIcon.SetError(txtPricePurchased, "Insert the purchase price");
                }
                else
                {

                    bool save = true;

                    foreach(DataRow row in dtDetails.Rows)
                    {
                        if(Convert.ToInt32(row["idproduct"]) == Convert.ToInt32(this.txtIdProduct.Text))
                        {
                            save = false;
                            this.MessageError("Product already added");
                        }
                    }

                    if(save)
                    {
                        //code to add to datalist
                        decimal subtotal = Convert.ToDecimal(this.txtInitialStoque.Text) * Convert.ToDecimal(this.txtPricePurchased.Text);


                        //Calculate the total
                        totalPaid = totalPaid + subtotal;
                        this.lblTotalToPay.Text = totalPaid.ToString("#0.00#");

                        DataRow row = this.dtDetails.NewRow();

                        row["idproduct"] = Convert.ToInt32(this.txtIdProduct.Text);
                        row["product"] = this.txtProduct.Text;
                        row["value_purchased"] = Convert.ToDecimal(this.txtPricePurchased.Text);
                        row["value_sold"] = Convert.ToDecimal(this.txtPricePurchased.Text);
                        row["initial_stoque"] = Convert.ToInt32(this.txtInitialStoque.Text);
                        row["produced_date"] = Convert.ToDateTime(this.dtpOrdered.Value);
                        row["expired_date"] = this.dtpExpire.Value;
                        row["subtotal"] = subtotal;

                        this.dtDetails.Rows.Add(row);
                        this.CleanDetails();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataList.Columns["Cancel"].Index)
            {
                DataGridViewCheckBoxCell chkCancel = (DataGridViewCheckBoxCell)dataList.Rows[e.RowIndex].Cells["Cancel"];
                chkCancel.Value = !Convert.ToBoolean(chkCancel.Value);
            }
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            this.txtId.Text= Convert.ToString(this.dataList.CurrentRow.Cells["idinput"].Value);
            this.txtSupplier.Text = Convert.ToString(this.dataList.CurrentRow.Cells["supplier"].Value);
            this.dtpDate.Value = Convert.ToDateTime(this.dataList.CurrentRow.Cells["date"].Value);
            
            this.cbReceipt.Text = Convert.ToString(this.dataList.CurrentRow.Cells["type"].Value);
            this.txtSerie.Text = Convert.ToString(this.dataList.CurrentRow.Cells["serie"].Value);
            this.txtCorelativ.Text = Convert.ToString(this.dataList.CurrentRow.Cells["corelativ"].Value);
            //this.txtVat.Text = Convert.ToString(this.dataList.CurrentRow.Cells["vat"].Value);
            this.lblTotalToPay.Text = Convert.ToString(this.dataList.CurrentRow.Cells["Total"].Value);
            this.ShowDetailInput();
            this.tabControl1.SelectedIndex = 1;



        }


      
    }
}

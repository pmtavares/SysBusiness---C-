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
            this.CleanDetails();

            
        }
        private void CleanDetails()
        {
            this.txtIdProduct.Text = string.Empty;
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
            this.txtPricePurchased.Enabled = !value;
            this.txtPriceSale.Enabled = !value;
            this.txtInitialStoque.Enabled = !value;

            this.cbReceipt.Enabled = value;
            this.btnSearch.Enabled = value;

            this.btnAdd.Enabled = value;
            this.btnDelete.Enabled = value;

            this.btnSearchSupplier.Enabled = value;
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

            }
            else
            {
                this.EnabledField(false);
                this.btnNew.Enabled = true;
                this.btnSave.Enabled = false;
                
                this.btnCancel.Enabled = false;
                this.btnSearchSupplier.Enabled = false;
                this.btnSearchProduct.Enabled = false;
            }

        }
        private void HideCollumns()
        {
            this.dataList.Columns[0].Visible = false;
            this.dataList.Columns[1].Visible = false;
           
        }

        private void ShowDate()
        {
            this.dataList.DataSource = BInput.SearchNameDate(this.dtpInitialDate.Value.ToString("dd/MM/yyyy"),this.dtpFinalDate.Value.ToString("dd/MM/yyyy") );
            //this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        
        //Show details input
        private void ShowDetailInput()
        {
            this.dataListDetails.DataSource = BInput.ShowDetails(this.txtId.Text);
           
        }

        
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

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
            this.dtDetails.Columns.Add("idproduct", System.Type.GetType("system.Int32"));
            
            this.dtDetails.Columns.Add("product", System.Type.GetType("system.String"));
            this.dtDetails.Columns.Add("value_purchased", System.Type.GetType("system.Decimal"));
            this.dtDetails.Columns.Add("value_sold", System.Type.GetType("system.Decimal"));
            this.dtDetails.Columns.Add("initial_stoque", System.Type.GetType("system.Int32"));
            this.dtDetails.Columns.Add("produced_date", System.Type.GetType("system.Int32"));
            this.dtDetails.Columns.Add("initial_stoque", System.Type.GetType("system.DateTime"));
            this.dtDetails.Columns.Add("expired_date", System.Type.GetType("system.DateTime"));
            this.dtDetails.Columns.Add("subtotal", System.Type.GetType("system.Decimal"));

            this.dataListDetails.DataSource = this.dtDetails;
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.EnabledField(false);
            this.Enabledbuttons();
            this.CreateTable();
            this.ShowDetailInput();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.isNew = true;
            
            this.Enabledbuttons();
            this.Clean();
            this.EnabledField(true);
            this.txtSerie.Focus();
            //this.txtIdProduct.Enabled = false;
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
                if (txtidSupplier.Text == string.Empty || txtIdProduct.Text == string.Empty || txtSerie.Text == string.Empty)
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
                           "ISSUED", this.dtDetails);
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

                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


      
    }
}

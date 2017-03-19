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
    public partial class frmSupplier : Form
    {

        private bool eNew = false;
        private bool eEdit = false;
        public frmSupplier()
        {
            InitializeComponent();
            this.ttMessage.SetToolTip(this.txtCompany, "Insert the name of company");
        }

        //Show confirmation message
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
            this.txtSearch.Text = string.Empty;
            this.txtId.Text = string.Empty;
            this.txtAddress.Text = string.Empty;
            this.txtCompany.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtDocNumber.Text = string.Empty;
            this.txtUrl.Text = string.Empty;
            this.txtDocNumber.Text = string.Empty;
            this.cbComercialSector.Text = string.Empty; ;
            this.cbDocumentType.Text = string.Empty;
        }


        //enabled boxes
        private void EnabledField(bool value)
        {
            this.txtAddress.ReadOnly = !value;
            this.txtDocNumber.ReadOnly = !value;
            this.txtUrl.ReadOnly = !value;
            this.txtDocNumber.ReadOnly = !value;
            this.cbComercialSector.Enabled = value;
            this.cbDocumentType.Enabled = value;
            this.txtCompany.ReadOnly = !value;
            this.txtEmail.ReadOnly = !value;
        }

        private void Enabledbuttons()
        {
            if (this.eNew || this.eEdit)
            {
                this.EnabledField(true);
                this.btnNew.Enabled = false;
                this.btnSave.Enabled = true;
                this.btnEdit.Enabled = false;
                this.btnCancel.Enabled = true;

            }
            else
            {
                this.EnabledField(false);
                this.btnNew.Enabled = true;
                this.btnSave.Enabled = false;
                this.btnEdit.Enabled = true;
                this.btnCancel.Enabled = false;
            }

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

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            this.categoryTableAdapter.Fill(this.dbBusinessDataSet.category);
            this.Top = 0;
            this.Left = 0;
            this.ShowValues();
            this.EnabledField(false);
            this.Enabledbuttons();
            this.cbSearch.SelectedIndex = 0;
            this.cbComercialSector.SelectedIndex = 0;
            this.cbDocumentType.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SearchName();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.SearchName();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.eNew = true;
            this.eEdit = false;
            this.Enabledbuttons();
            this.Clean();
            this.EnabledField(true);
            this.txtCompany.Focus();
            this.txtId.Enabled = false;
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            if(this.cbSearch.Text.Equals("Company"))
            {
                this.SearchName();
            }
            else if(this.cbSearch.Text.Equals("Document"))
            {
                this.SearchDocument();
            }
           
            //
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            //this.SearchName();
            this.SearchDocument();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (txtCompany.Text == string.Empty)
                {
                    MessageError("Fill the fields");
                    errorIcon.SetError(txtCompany, "Insert the Company's name");

                }
                else
                {
                    if (this.eNew)
                    {
                        resp = BSupplier.Insert(this.txtCompany.Text, this.cbComercialSector.Text,
            this.cbDocumentType.Text, this.txtDocNumber.Text, this.txtAddress.Text, "1234", this.txtEmail.Text, this.txtUrl.Text
                            );
                    }
                    else
                    {
                        resp = BSupplier.Edit(Convert.ToInt32(this.txtId.Text),
                            this.txtCompany.Text, this.cbComercialSector.Text,
            this.cbDocumentType.Text, this.txtDocNumber.Text.Trim(), this.txtAddress.Text, "1234", this.txtEmail.Text, this.txtUrl.Text);
                    }

                    if (resp.Equals("OK"))
                    {
                        if (this.eNew)
                        {
                            this.MessageOk("Register saved");
                        }
                        else
                        {
                            this.MessageOk("Register Edited");
                        }
                    }
                    else
                    {
                        this.MessageError(resp);
                    }

                    this.eNew = false;
                    this.eEdit = false;
                    this.Enabledbuttons();
                    this.Clean();
                    this.ShowValues();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataList.CurrentRow.Cells["idsupply"].Value);
            this.txtCompany.Text = Convert.ToString(this.dataList.CurrentRow.Cells["company"].Value);
            this.txtAddress.Text = Convert.ToString(this.dataList.CurrentRow.Cells["address"].Value);
            this.txtUrl.Text = Convert.ToString(this.dataList.CurrentRow.Cells["url"].Value);
            this.txtEmail.Text = Convert.ToString(this.dataList.CurrentRow.Cells["email"].Value);
            this.txtDocNumber.Text = Convert.ToString(this.dataList.CurrentRow.Cells["document_number"].Value);
            this.cbDocumentType.Text = Convert.ToString(this.dataList.CurrentRow.Cells["document_type"].Value);
            this.cbComercialSector.Text = Convert.ToString(this.dataList.CurrentRow.Cells["business_department"].Value);
            
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Equals(""))
            {
                this.MessageError("Select a record to edit");
            }
            else
            {
                this.eEdit = true;
                this.Enabledbuttons();
                this.EnabledField(true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.eNew = false;
            this.eEdit = false;
            this.Enabledbuttons();
            this.EnabledField(false);
            this.Clean();
        }

        private void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDelete.Checked)
            {
                this.dataList.Columns[0].Visible = true;
            }
            else
            {
                this.dataList.Columns[0].Visible = false;
            }
        }

        private void dataList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataList.Columns["Delete"].Index)
            {
                DataGridViewCheckBoxCell chkDelete = (DataGridViewCheckBoxCell)dataList.Rows[e.RowIndex].Cells["Delete"];
                chkDelete.Value = !Convert.ToBoolean(chkDelete.Value);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                            resp = BSupplier.Delete(Convert.ToInt32(Code));

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
                    this.ShowValues();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtSearch.Text = string.Empty;
        }
    }
}

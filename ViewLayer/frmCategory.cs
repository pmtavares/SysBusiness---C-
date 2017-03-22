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
    public partial class frmCategory : Form
    {
        private bool eNew = false;
        private bool eEdit = false;

        public frmCategory()
        {
            InitializeComponent();
            this.ttMessage.SetToolTip(this.txtSearch, "Insert the name of category");
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
            this.txtIdCategory.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
            this.txtNameConfig.Text = string.Empty;
        }


        //enabled boxes
        private void EnabledField(bool value)
        {
            this.txtNameConfig.ReadOnly = !value;
            this.txtDescription.ReadOnly = !value;
            this.txtIdCategory.ReadOnly = !value;
        }

        private void Enabledbuttons()
        {
            if(this.eNew || this.eEdit)
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

        private void frmCategory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbBusinessDataSet.category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.dbBusinessDataSet.category);
            this.Top = 0;
            this.Left = 0;
            this.ShowValues();
            this.EnabledField(false);
            this.Enabledbuttons();
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
            this.txtNameConfig.Focus();
            this.txtIdCategory.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if(txtNameConfig.Text ==  string.Empty)
                {
                    MessageError("Fill the fields");
                    errorIcon.SetError(txtNameConfig, "Insert the name");

                }
                else
                {
                    if(this.eNew)
                    {
                        resp = BCategory.Insert(this.txtNameConfig.Text.Trim().ToUpper(), 
                            txtDescription.Text.Trim());
                    }
                    else
                    {
                        resp = BCategory.Edit(Convert.ToInt32(this.txtIdCategory.Text),
                            this.txtNameConfig.Text.Trim().ToUpper(), txtDescription.Text.Trim());
                    }

                    if(resp.Equals("OK"))
                    {
                        if(this.eNew)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdCategory.Text = Convert.ToString(this.dataList.CurrentRow.Cells["idcategory"].Value);
            this.txtNameConfig.Text = Convert.ToString(this.dataList.CurrentRow.Cells["name"].Value);
            this.txtDescription.Text = Convert.ToString(this.dataList.CurrentRow.Cells["description"].Value);
            this.tabControl1.SelectedIndex = 1;
        
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(txtIdCategory.Text.Equals(""))
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
            if(chkDelete.Checked)
            {
                this.dataList.Columns[0].Visible = true;
            }
            else
            {
                this.dataList.Columns[0].Visible = false;
            }
        }


        //Content clicke in the cell
        private void dataList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataList.Columns["Delete"].Index)
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
                if(option == DialogResult.OK)
                {
                    string Code;
                    string resp = "";

                    foreach(DataGridViewRow row in dataList.Rows)
                    {
                        if(Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Code = Convert.ToString(row.Cells[1].Value);
                            resp = BCategory.Delete(Convert.ToInt32(Code));

                            if(resp.Equals("OK"))
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
            catch(Exception ex)
            {

            }
        }
    }
}

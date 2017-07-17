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
    public partial class frmStaff : Form
    {
        private bool eNew = false;
        private bool eEdit = false;
        public frmStaff()
        {
            InitializeComponent();
            this.ttMessage.SetToolTip(this.txtName, "Insert the name of staff");
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

        private void Clean()
        {
            this.txtSearch.Text = string.Empty;
            this.txtId.Text = string.Empty;
            this.txtAddress.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtDocNumber.Text = string.Empty;
            this.txtPhone.Text = string.Empty;
            this.txtDocNumber.Text = string.Empty;
            this.txtSurname.Text = string.Empty;
            this.cbGender.Text = string.Empty; ;
            this.cbAccess.Text = string.Empty;
            this.txtUsername.Text = string.Empty;
            this.txtPassword.Text = string.Empty;
        }


        //enabled boxes
        private void EnabledField(bool value)
        {
            this.txtAddress.ReadOnly = !value;
            this.txtDocNumber.ReadOnly = !value;
            this.txtPhone.ReadOnly = !value;
            this.txtDocNumber.ReadOnly = !value;
            this.txtPassword.ReadOnly = !value;
            this.cbAccess.Enabled = value;
            this.cbGender.Enabled = value;
            this.txtName.ReadOnly = !value;
            this.txtSurname.ReadOnly = !value;
            this.txtEmail.ReadOnly = !value;
            this.dtDate.Enabled = value;
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
            this.dataList.DataSource = BStaff.ShowValues();
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        private void SearchName()
        {
            this.dataList.DataSource = BStaff.SearchName(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void SearchDocument()
        {
            this.dataList.DataSource = BStaff.SearchDocument(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }

        private void frmStaff_Load(object sender, EventArgs e)
        {
            //this.categoryTableAdapter.Fill(this.dbBusinessDataSet.category);
            this.Top = 0;
            this.Left = 0;
            this.ShowValues();
            this.EnabledField(false);
            this.Enabledbuttons();
            this.cbSearch.SelectedIndex = 0;
            this.cbSearch.SelectedIndex = 0;
            this.cbAccess.SelectedIndex = 0;
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

        private void btnSearch_Click(object sender, EventArgs e)
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.eNew = true;
            this.eEdit = false;
            this.Enabledbuttons();
            this.Clean();
            this.EnabledField(true);
            this.txtName.Focus();
            this.txtId.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (txtName.Text == string.Empty)
                {
                    MessageError("Fill the fields");
                    errorIcon.SetError(txtName, "Insert the staff's name");

                }
                else
                {
                    if (this.eNew)
                    {
                        resp = BStaff.Insert(
                            this.txtName.Text.Trim().ToUpper(), 
                            this.txtSurname.Text, 
                            this.cbGender.Text, 
                            this.dtDate.Value,
                            this.txtDocNumber.Text, 
                            this.txtAddress.Text, 
                            this.txtPhone.Text, 
                            this.txtEmail.Text, 
                            this.cbAccess.Text,
                            this.txtUsername.Text, 
                            this.txtPassword.Text
                            );
                    }
                    else
                    {
                        resp = BStaff.Edit(Convert.ToInt32(this.txtId.Text),
                            this.txtName.Text.Trim().ToUpper(), this.txtSurname.Text, this.cbGender.Text, this.dtDate.Value,
             this.txtDocNumber.Text, this.txtAddress.Text, this.txtPhone.Text, this.txtEmail.Text, this.cbAccess.Text,
             this.txtUsername.Text, this.txtPassword.Text);
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

        private void dataList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataList.CurrentRow.Cells["idstaff"].Value);
            this.txtName.Text = Convert.ToString(this.dataList.CurrentRow.Cells["name"].Value);
            this.txtSurname.Text = Convert.ToString(this.dataList.CurrentRow.Cells["surname"].Value);
            this.cbGender.Text = Convert.ToString(this.dataList.CurrentRow.Cells["gender"].Value);
            this.dtDate.Value = Convert.ToDateTime(this.dataList.CurrentRow.Cells["dob"].Value);
            this.txtEmail.Text = Convert.ToString(this.dataList.CurrentRow.Cells["email"].Value);
            this.txtAddress.Text = Convert.ToString(this.dataList.CurrentRow.Cells["address"].Value);
            this.txtPhone.Text = Convert.ToString(this.dataList.CurrentRow.Cells["phone"].Value);
            this.cbAccess.Text = Convert.ToString(this.dataList.CurrentRow.Cells["access"].Value);
            this.txtDocNumber.Text = Convert.ToString(this.dataList.CurrentRow.Cells["document_number"].Value);
            this.txtUsername.Text = Convert.ToString(this.dataList.CurrentRow.Cells["username"].Value);
            this.txtPassword.Text = Convert.ToString(this.dataList.CurrentRow.Cells["password"].Value);
            
            
            

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
                            resp = BStaff.Delete(Convert.ToInt32(Code));

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

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
    public partial class frmProduct : Form
    {

        private bool eNew = false;
        private bool eEdit = false;

        private static frmProduct _Instance;

        public static frmProduct GetInstance()
        {
            if(_Instance == null)
            {
                _Instance = new frmProduct();
            }

            return _Instance;
        }

        public void setCategory(string idcategory, string name)
        {
            this.txtidcategory.Text = idcategory;
            this.txtnamecategory.Text = name;
        }
        public frmProduct()
        {
            InitializeComponent();
            this.ttMessage.SetToolTip(this.txtSearch, "Insert the name of product");
            this.ttMessage.SetToolTip(this.pxImage, "Insert the image of product");
            this.ttMessage.SetToolTip(this.cbPresentation, "Select a presentation of product");
            this.ttMessage.SetToolTip(this.txtnamecategory, "Select a category");
            this.txtidcategory.Enabled = false;
            this.txtnamecategory.Enabled = false;
            this.CombPresentation();
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
            this.txtSearch.Text = string.Empty;
            this.txtCode.Text = string.Empty;
            this.txtIdProduct.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
            this.txtNameConfig.Text = string.Empty;
            this.txtidcategory.Text = string.Empty;
            this.pxImage.Image = global::ViewLayer.Properties.Resources.noImage;
        }


        //enabled boxes
        private void EnabledField(bool value)
        {
            this.txtNameConfig.ReadOnly = !value;
            this.txtDescription.ReadOnly = !value;
            //this.txtIdProduct.ReadOnly = !value;
            this.txtCode.ReadOnly = !value;
            this.txtIdProduct.ReadOnly = !value;
            this.cbPresentation.Enabled = value;
            this.btnClean.Enabled = value;
            this.btnSearch.Enabled = value;
            this.btnLoad.Enabled = value;
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
                this.btnSearchCategory.Enabled = true;

            }
            else
            {
                this.EnabledField(false);
                this.btnNew.Enabled = true;
                this.btnSave.Enabled = false;
                this.btnEdit.Enabled = true;
                this.btnCancel.Enabled = false;
                this.btnSearchCategory.Enabled = false;
            }

        }
        private void HideCollumns()
        {
            this.dataList.Columns[0].Visible = false;
            this.dataList.Columns[1].Visible = false;
            this.dataList.Columns[5].Visible = false; //Hide the image column
            this.dataList.Columns[6].Visible = false;
            this.dataList.Columns[8].Visible = false;
        }

        private void ShowValues()
        {
            this.dataList.DataSource = BProduct.ShowValues();
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }
        private void SearchName()
        {
            this.dataList.DataSource = BProduct.SearchName(this.txtSearch.Text);
            this.HideCollumns();
            lblTotal.Text = Convert.ToString(dataList.Rows.Count) + " registers found";
        }


        private  void CombPresentation()
        {
            cbPresentation.DataSource = BPresentation.ShowValues();
            cbPresentation.ValueMember = "idpresentation";
            cbPresentation.DisplayMember = "name";
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.ShowValues();
            this.EnabledField(false);
            this.Enabledbuttons();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                this.pxImage.SizeMode = PictureBoxSizeMode.StretchImage; //to adjust the image in the box
                this.pxImage.Image = Image.FromFile(dialog.FileName);

            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            this.pxImage.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pxImage.Image = global::ViewLayer.Properties.Resources.noImage;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.SearchName();
        }

        private void btnSearch_Click(object sender, EventArgs e)
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
            this.txtIdProduct.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (txtNameConfig.Text == string.Empty || txtidcategory.Text == string.Empty || txtCode.Text == string.Empty)
                {
                    MessageError("Fill the fields");
                    errorIcon.SetError(txtNameConfig, "Insert the name");
                    errorIcon.SetError(txtidcategory, "Insert the category id");
                    errorIcon.SetError(txtCode, "Insert the code");
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pxImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] image = ms.GetBuffer();
                    
                    if (this.eNew)
                    {
                        resp = BProduct.Insert(this.txtCode.Text, this.txtNameConfig.Text,
                            this.txtDescription.Text, image, Convert.ToInt32(this.txtidcategory.Text), 
                            Convert.ToInt32(this.cbPresentation.SelectedValue));
                    }
                    else
                    {
                        resp = BProduct.Edit(Convert.ToInt32(this.txtIdProduct.Text), this.txtCode.Text, this.txtNameConfig.Text,
                            this.txtDescription.Text, image, Convert.ToInt32(this.txtidcategory.Text),
                            Convert.ToInt32(this.cbPresentation.SelectedValue));
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtIdProduct.Text.Equals(""))
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
            this.EnabledField(false);
            this.Enabledbuttons();
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

        private void dataList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtIdProduct.Text = Convert.ToString(this.dataList.CurrentRow.Cells["idproduct"].Value);
            this.txtCode.Text = Convert.ToString(this.dataList.CurrentRow.Cells["code"].Value);
            this.txtNameConfig.Text = Convert.ToString(this.dataList.CurrentRow.Cells["name"].Value);
            this.txtDescription.Text = Convert.ToString(this.dataList.CurrentRow.Cells["description"].Value);

            byte[] imageBuffer =(byte[])this.dataList.CurrentRow.Cells["image"].Value;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
            this.pxImage.Image = Image.FromStream(ms);
            this.pxImage.SizeMode = PictureBoxSizeMode.StretchImage;

            this.txtidcategory.Text = Convert.ToString(this.dataList.CurrentRow.Cells["idcategory"].Value);
            this.txtnamecategory.Text = Convert.ToString(this.dataList.CurrentRow.Cells["Category"].Value);
            this.cbPresentation.SelectedValue = Convert.ToString(this.dataList.CurrentRow.Cells["idpresentation"].Value);

            this.tabControl1.SelectedIndex = 1;
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
                            resp = BProduct.Delete(Convert.ToInt32(Code));

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

            }
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            frmSearchCategory form = new frmSearchCategory();
            form.ShowDialog();
        }

        private void frmProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Instance = null;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmProductReport frm = new frmProductReport();
            //frm.ShowDialog();
            frm.Show();
        }


    }
}

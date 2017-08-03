using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewLayer
{
    public partial class frmMain : Form
    {
        private int childFormNumber = 0;

        private string _IdStaff = "";
        private string _NameStaff = "";
        private string _SurnameStaff = "";
        private string _AccessStaff = "";

        public string IdStaff
        {
            get { return _IdStaff; }
            set { _IdStaff = value; }
        }
        

        public string NameStaff
        {
            get { return _NameStaff; }
            set { _NameStaff = value; }
        }
        

        public string SurnameStaff
        {
            get { return _SurnameStaff; }
            set { _SurnameStaff = value; }
        }
        

        public string AccessStaff
        {
            get { return _AccessStaff; }
            set { _AccessStaff = value; }
        }




        public frmMain()
        {
            InitializeComponent();
        }  

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategory frm = frmCategory.GetInstance();
            frm.MdiParent = this;
            frm.Show();

        }

        private void presentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPresentation frm = new frmPresentation();
            frm.MdiParent = this;
            frm.Show();
        }

        private void productsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmProduct frm = frmProduct.GetInstance();
            frm.MdiParent = this;
            frm.Show();

        }


        private void ManagerAccess()
        {
            if(AccessStaff == "Admin")
            {
                
            }
            else if(AccessStaff == "Manager")
            {
                
                menuPurchases.Visible = false;
                menuSettings.Visible = false;
                menuTools.Visible = false;
                
            }
            else{
                
                menuSettings.Visible = false;
                menuTools.Visible = false;
            }
        }

        private void ShowLabels()
        {
            lbShowName.Text = this.NameStaff;
            lbShowPermission.Text = this.AccessStaff;
            this.Text = "Business System - Username: " + this.NameStaff.ToLower() + " - Permission: " + this.AccessStaff;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ManagerAccess();
            ShowLabels();
        }

        private void incomingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInput frm = frmInput.GetInstance();
            frm.MdiParent = this;
            frm.Show();
            frm.idstaff = Convert.ToInt32(this.IdStaff);
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSupplier frm = frmSupplier.GetInstance();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}

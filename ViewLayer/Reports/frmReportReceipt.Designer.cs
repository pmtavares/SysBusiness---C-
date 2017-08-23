namespace ViewLayer
{
    partial class frmReportReceipt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dstMain = new ViewLayer.dstMain();
            this.spreport_invoiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spreport_invoiceTableAdapter = new ViewLayer.dstMainTableAdapters.spreport_invoiceTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dstMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreport_invoiceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.spreport_invoiceBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ViewLayer.Reports.rptReceiptSale.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(743, 399);
            this.reportViewer1.TabIndex = 0;
            // 
            // dstMain
            // 
            this.dstMain.DataSetName = "dstMain";
            this.dstMain.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spreport_invoiceBindingSource
            // 
            this.spreport_invoiceBindingSource.DataMember = "spreport_invoice";
            this.spreport_invoiceBindingSource.DataSource = this.dstMain;
            // 
            // spreport_invoiceTableAdapter
            // 
            this.spreport_invoiceTableAdapter.ClearBeforeFill = true;
            // 
            // frmReportReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 399);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReportReceipt";
            this.Text = "frmReportReceipt";
            this.Load += new System.EventHandler(this.frmReportReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dstMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreport_invoiceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource spreport_invoiceBindingSource;
        private dstMain dstMain;
        private dstMainTableAdapters.spreport_invoiceTableAdapter spreport_invoiceTableAdapter;
    }
}
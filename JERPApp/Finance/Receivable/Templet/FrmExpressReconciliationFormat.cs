using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace JERPApp.Finance.Receivable.Templet
{
    public partial class FrmExpressReconciliationFormat : Form
    {
        public FrmExpressReconciliationFormat()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accFormat = new JERPData.Product.ExpressReconciliationFormat();
            this.fileHelper = new JCommon.ServerFileHelper();   
            this.SetPermit();
            
        }
        JERPData.Product.ExpressReconciliationFormat accFormat;
        FrmExpressReconciliationFormatOper frmOper;
        FrmExpressReconciliationFormatCopy frmCopy;
        private DataTable dtblFormat;
        private JCommon.ServerFileHelper fileHelper;
       
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����          
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(731);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(732);
            if (this.enableBrowse)
            {              
                this.LoadData();
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);

            }
            this.lnkNew.Enabled = this.enableSave;
            this.ColumnBtnCopy.Visible = this.enableSave;
            this.ColumnBtnEdit.Visible = this.enableSave;
            this.lnkDownload.Enabled = this.enableSave;
            this.lnkUpload.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.lnkNew.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkNew_LinkClicked);
                this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
                this.lnkDownload.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkDowload_LinkClicked);
                this.lnkUpload.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkUpload_LinkClicked);
            }
        }
        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        private void LoadData()
        {  
            this.dtblFormat = this.accFormat.GetDataExpressReconciliationFormat().Tables[0];
            this.dgrdv.DataSource = this.dtblFormat;
        }
        void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmOper == null)
            {
                frmOper = new FrmExpressReconciliationFormatOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave += new FrmExpressReconciliationFormatOper.AffterSaveDelegate(frmOper_AffterSave);
            }
            frmOper.NewFormat();
            frmOper.ShowDialog();
        }

        void frmOper_AffterSave()
        {
            this.LoadData();
        }
        void lnkDowload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmMsg.Show("���������У����Ժ�.....");
            this.fileHelper.DownloadFile(JERPData.ServerParameter.TempletFolder + "ExpressReconciliation.xlt",
                "���ն��˵���ʽ.xlt");
            FrmMsg.Hide();

        }

        void lnkUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "ģ���ļ�(*.xlt)|*.xlt";
            DialogResult rlg = dlg.ShowDialog();
            if (rlg == DialogResult.Cancel) return;
            string ClientFilePath = dlg.FileName;
            FrmMsg.Show("���������У����Ժ�.....");
            this.fileHelper.UploadFile(JERPData.ServerParameter.TempletFolder + "ExpressExpressReconciliation.xlt", ClientFilePath);
            FrmMsg.Hide();

        }

     
        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            int FormatID = (int)this.dtblFormat.DefaultView[irow]["FormatID"];
            if (this.dgrdv.Columns[icol].Name == this.ColumnBtnEdit.Name)
            {
              
                if (frmOper == null)
                {
                    frmOper = new FrmExpressReconciliationFormatOper();
                    new FrmStyle(frmOper).SetPopFrmStyle(this);
                    frmOper.AffterSave += new FrmExpressReconciliationFormatOper.AffterSaveDelegate(frmOper_AffterSave);
                }
                frmOper.EditFormat(FormatID);
                frmOper.ShowDialog();
            } 
            if (this.dgrdv.Columns[icol].Name == this.ColumnBtnCopy.Name)
            {
                if (frmCopy == null)
                {
                    frmCopy = new FrmExpressReconciliationFormatCopy();
                    new FrmStyle(frmCopy).SetPopFrmStyle(this);
                    frmCopy.AffterSave += this.LoadData;
                }
                frmCopy.NewFromCopy(FormatID);
                frmCopy.ShowDialog();
            }
        }
      
    }
}
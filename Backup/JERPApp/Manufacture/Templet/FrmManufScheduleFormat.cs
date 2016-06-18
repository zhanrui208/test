using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace JERPApp.Manufacture.Templet
{
    public partial class FrmManufScheduleFormat : Form
    {
        public FrmManufScheduleFormat()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accFormat = new JERPData.Manufacture.ManufScheduleFormat();
            this.fileHelper = new JCommon.ServerFileHelper();
            this.SetPermit();
        }
        JERPData.Manufacture.ManufScheduleFormat accFormat;
        FrmManufScheduleFormatOper frmOper;
        FrmManufScheduleFormatCopy frmCopy;
        private JCommon .ServerFileHelper  fileHelper;
        private DataTable dtblFormat;
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����          
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(784);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(785);
            if (this.enableBrowse)
            {
                this.LoadData();
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
            }
            this.lnkNew.Enabled = enableSave;
            this.ColumnBtnEdit.Visible = enableSave;
            this.ColumnBtnCopy.Visible = enableSave;
            this.lnkDownload.Enabled = enableSave;
            this.lnkUpload.Enabled = enableSave;
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
            this.dtblFormat = this.accFormat.GetDataManufScheduleFormat().Tables[0];
            this.dgrdv.DataSource = this.dtblFormat;
        }
        void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmOper == null)
            {
                frmOper = new FrmManufScheduleFormatOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave += new FrmManufScheduleFormatOper.AffterSaveDelegate(frmOper_AffterSave);
            }
            frmOper.NewFormat();
            frmOper.ShowDialog();
        }

        void frmOper_AffterSave()
        {
            this.LoadData();
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
                    frmOper = new FrmManufScheduleFormatOper();
                    new FrmStyle(frmOper).SetPopFrmStyle(this);
                    frmOper.AffterSave += new FrmManufScheduleFormatOper.AffterSaveDelegate(frmOper_AffterSave);
                }
                frmOper.EditFormat(FormatID);
                frmOper.ShowDialog();
            }
            if (this.dgrdv.Columns[icol].Name == this.ColumnBtnCopy.Name)
            {
                if (frmCopy == null)
                {
                    frmCopy = new FrmManufScheduleFormatCopy();
                    new FrmStyle(frmCopy).SetPopFrmStyle(this);
                    frmCopy.AffterSave += this.LoadData;
                }
                frmCopy.NewFromCopy(FormatID);
                frmCopy.ShowDialog();
            }
        }
       
        void lnkDowload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            FrmMsg.Show("���������У����Ժ�.....");
            this.fileHelper.DownloadFile(JERPData.ServerParameter.TempletFolder + "ManufSchedule.xlt",
                "���򵥸�ʽ.xlt");
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
            this.fileHelper.UploadFile(JERPData.ServerParameter.TempletFolder + "ManufSchedule.xlt", ClientFilePath);
           FrmMsg.Hide();

        }
    }
}
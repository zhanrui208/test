using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace JERPApp.Dispatcher
{
    public partial class FrmExpress : Form
    {
        public FrmExpress()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accExpress = new JERPData.General.Express();
            this.fileHelper = new JCommon.ServerFileHelper();
            this.SetPermit();
        }
        JERPData.General .Express  accExpress;     
        private JCommon .ServerFileHelper  fileHelper;
        private DataTable dtblExpress;
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����          
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(110);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(111);
            if (this.enableBrowse)
            {
                this.LoadData();
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
            }          
            this.lnkDownload.Enabled = enableSave;
            this.lnkUpload.Enabled = enableSave;
            if (this.enableSave)
            {
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
                this.dgrdv.RowValidated += new DataGridViewCellEventHandler(dgrdv_RowValidated);
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
            this.dtblExpress = this.accExpress.GetDataExpress().Tables[0];
            this.dgrdv.DataSource = this.dtblExpress;
        }
        void dgrdv_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            bool flag = true;
            DataRow drow = null;
            try
            {
                drow = this.dtblExpress.DefaultView[irow].Row;
            }
            catch
            {
                return;
            }
            string errormsg = string.Empty;
            if (drow["CompanyID"] == DBNull.Value)
            {
                object objCompanyID = 0;
                flag = this.accExpress .InsertExpress (
                        ref errormsg,
                        ref objCompanyID,
                        drow["CompanyName"],
                        drow["ReceiveCompanyNameCellName"],
                        drow["ReceiveAddressCellName"],
                        drow["LinkmanCellName"],
                        drow["TelephoneCellName"]);
                if (flag)
                {
                    drow["CompanyID"] = objCompanyID;
                }
                else
                {
                    MessageBox.Show(errormsg);

                }
            }
            else
            {
                flag = this.accExpress.UpdateExpress(
                        ref errormsg,
                        drow["CompanyID"],
                        drow["CompanyName"],
                        drow["ReceiveCompanyNameCellName"],
                        drow["ReceiveAddressCellName"],
                        drow["LinkmanCellName"],
                        drow["TelephoneCellName"]);
                if (!flag)
                {
                   
                    MessageBox.Show(errormsg);

                }

            }
        }
        private void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblExpress.DefaultView[irow].Row;
            if (drow["CompanyID"] == DBNull.Value)
            {
                e.Cancel = true;
                return;
            }
            int ExpressBillID = (int)drow["ExpressBillID"];
            string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {

                flag = this.accExpress.DeleteExpress(ref errormsg, ExpressBillID);
                if (!flag)
                {
                    MessageBox.Show(errormsg);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        void lnkDowload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            FrmMsg.Show("���������У������һ��򿪣��༭�ϴ�����.....");
            this.fileHelper.DownloadFile(JERPData.ServerParameter.TempletFolder + "ExpressBill.xlt",
                "��ݵ���ʽ.xlt");
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
            this.fileHelper.UploadFile(JERPData.ServerParameter.TempletFolder + "ExpressBill.xlt", ClientFilePath);
           FrmMsg.Hide();

        }
    }
}
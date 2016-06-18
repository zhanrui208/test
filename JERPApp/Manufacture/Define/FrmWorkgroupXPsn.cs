using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture.Define
{
    public partial class FrmWorkgroupXPsn : Form
    {
        public FrmWorkgroupXPsn()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accXPsn = new JERPData.Manufacture.WorkgroupXPsn();
            this.SetPermit();
        }
        private JERPData.Manufacture.WorkgroupXPsn accXPsn;
        private DataTable dtblXPsn;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(252);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(253);
            if (this.enableBrowse)
            {
                this.LoadData();
                this.ctrlWorkgroupID.AffterSelected += this.LoadData;
                this.ctrlShiftID.AffterSelected += this.LoadData;
            }
            this.btnAddPsn.Enabled = this.enableSave;
            this.dgrdv.AllowUserToDeleteRows = this.enableSave;
            if (this.enableSave)
            {
               
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
                this.btnAddPsn.Click += new EventHandler(btnAddPsn_Click);
            }
        }

        private void LoadData()
        {
            this.dtblXPsn = this.accXPsn.GetDataWorkgroupXPsnForDailyWorking (this.ctrlWorkgroupID.WorkgroupID,
                this.ctrlShiftID .ShiftID ).Tables[0];
            this.dgrdv.DataSource = this.dtblXPsn;
        }

        void btnAddPsn_Click(object sender, EventArgs e)
        {
            if (this.ctrlPsnID .PsnID  == -1)
            {
                MessageBox.Show("δѡ���κ���Ա");
                return;
            }
            if (this.ctrlWorkgroupID.WorkgroupID == -1)
            {
                MessageBox.Show("δѡ���κ���������");
                return;
            }
            if (this.ctrlShiftID .ShiftID == -1)
            {
                MessageBox.Show("δѡ���κΰ��");
                return;
            }
            DataRow[] drows = this.dtblXPsn.Select("PsnID=" + this.ctrlPsnID.PsnID.ToString(), "", DataViewRowState.Deleted);
            if (drows.Length > 0)
            {
                MessageBox.Show("�����Ѵ���");
                return;
            }
            DataRow drowNew = this.dtblXPsn.NewRow();
            drowNew["PsnID"] = this.ctrlPsnID.PsnID;
            drowNew["PsnCode"] = this.ctrlPsnID.PsnCode;
            drowNew["PsnName"] = this.ctrlPsnID.PsnName ;
            this.dtblXPsn.Rows.Add(drowNew);
            string errormsg=string.Empty ;
            this.accXPsn.SaveWorkgroupXPsn(ref errormsg,
                this.ctrlWorkgroupID.WorkgroupID,
                this.ctrlShiftID .ShiftID,
                this.ctrlPsnID.PsnID);
        }

        void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;          
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;          
            string errormsg = string.Empty;
            int PsnID = (int)this.dtblXPsn.DefaultView[irow]["PsnID"];
            bool flag=this.accXPsn.DeleteWorkgroupXPsn(ref errormsg,
                this.ctrlWorkgroupID.WorkgroupID,
                this.ctrlShiftID .ShiftID ,
                PsnID);
            if (flag)
            {
                MessageBox.Show("�ɹ�ɾ����ǰ��Ա");
            }
            else
            {
                e.Cancel = true;
            }
           
        }
    }
}
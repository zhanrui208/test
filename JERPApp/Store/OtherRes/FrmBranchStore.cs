using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.OtherRes
{
    public partial class FrmBranchStore : Form
    {
        private DataTable dtblBranchStore;
        private JERPData.OtherRes.BranchStore accBranchStore;
        public FrmBranchStore()
        {
            InitializeComponent();
            this.accBranchStore = new JERPData.OtherRes .BranchStore ();
            this.dgrdv.AutoGenerateColumns = false;
            this.SetPermit();
        }
        private void LoadData()
        {
            
            this.dtblBranchStore = this.accBranchStore.GetDataBranchStore ().Tables[0];
            this.dtblBranchStore.Columns["BranchStoreName"].AllowDBNull = false;
            this.dtblBranchStore.Columns["BranchStoreName"].Unique = true;
            this.dtblBranchStore.Columns["ValidFlag"].DefaultValue = true;
            this.dgrdv.DataSource = this.dtblBranchStore;
        }
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(562);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(563);
            if (this.enableBrowse)
            { 
                this.LoadData();
            }         
            if (this.enableSave)
            {
                this.dgrdv.RowValidated += new DataGridViewCellEventHandler(dgrdv_RowValidated);
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
            }
        }
     
        void dgrdv_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            bool flag = false;
            DataRow drow = null;
            try
            {
                drow = this.dtblBranchStore .DefaultView[irow].Row;
            }
            catch
            {
                return;
            }
            string errormsg = string.Empty;
            if (drow["BranchStoreID"] == DBNull.Value)
            {
                object objBranchStoreID = DBNull.Value;
                flag = this.accBranchStore.InsertBranchStore (ref errormsg ,ref objBranchStoreID,
                             drow["BranchStoreName"],
                             drow["ValidFlag"]);
                if (flag)
                {
                    drow["BranchStoreID"] = objBranchStoreID;
                    if (this.affterSave != null) this.affterSave();
                }
                else
                {
                    MessageBox.Show("��ǰ�����ʧ�ܣ�����������Ѵ���");
                }
            }
            else
            {
                flag = this.accBranchStore.UpdateBranchStore(ref errormsg, drow["BranchStoreID"],
                            drow["BranchStoreName"],
                             drow["ValidFlag"]);

                if (flag)
                {
                    if (this.affterSave != null) this.affterSave();                   
                }
                else
                {
                    MessageBox.Show("��ǰ�и���ʧ�ܣ�������ͬ���������");
                }

            }
        }     
        private void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblBranchStore  .DefaultView[irow].Row;
            if (drow["BranchStoreID"] == DBNull.Value)
            {
                e.Cancel = true;
                return;
            }
            string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {

                flag = this.accBranchStore.DeleteBranchStore(ref errormsg, drow["BranchStoreID"]);
                if (flag)
                {
                    if (this.affterSave != null) this.affterSave();
                }
                else
                {
                   MessageBox .Show (errormsg);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        public delegate void AffterSaveDelegate();
        private AffterSaveDelegate affterSave;
        public event AffterSaveDelegate AffterSave
        {
            add
            {
                affterSave += value;
            }
            remove
            {
                affterSave -= value;
            }
        }
    }
}
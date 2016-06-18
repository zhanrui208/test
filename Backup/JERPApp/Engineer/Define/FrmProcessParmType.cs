using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer.Define
{
    public partial class FrmProcessParmType : Form
    {

        public FrmProcessParmType()
        {
            InitializeComponent();
            this.accParmType = new JERPData.Manufacture.ProcessParmType();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.SetPermit();
        }
        private DataTable dtbParmTypes;
        private JERPData.Manufacture .ProcessParmType  accParmType;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����           

        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(393);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(394);
            if (this.enableBrowse)
            {
                this.LoadData();
                this.ctrlProcessTypeID.AffterSelected += this.LoadData;
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
            }
            this.dgrdv.ReadOnly = !enableSave;
            this.dgrdv.AllowUserToAddRows = enableSave;
            this.dgrdv.AllowUserToDeleteRows = enableSave;
            this.btnSave.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
                this.btnSave.Click += new EventHandler(btnSave_Click);
            }

        }


        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        private void LoadData()
        {
          
            this.dtbParmTypes =  this.accParmType.GetDataProcessParmTypeByProcessTypeID(this.ctrlProcessTypeID .ProcessTypeID).Tables[0];       
            this.dtbParmTypes.Columns["ParmTypeName"].AllowDBNull = false;
            this.dtbParmTypes.Columns["ParmTypeName"].Unique = true;
            this.dgrdv.DataSource = this.dtbParmTypes;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ctrlProcessTypeID.ProcessTypeID == -1) return;
            string errormsg = string.Empty;
            bool flag = false;
            foreach (DataRow drow in this.dtbParmTypes.Rows)
            {
                if (drow.RowState == DataRowState.Deleted ) continue ;
                if (drow.RowState == DataRowState.Unchanged) continue ;
               
                if (drow["ParmTypeID"] == DBNull.Value)
                {
                    object objParmTypeID = DBNull.Value;
                    flag = this.accParmType.InsertProcessParmType(
                        ref errormsg,
                        ref objParmTypeID,
                        this.ctrlProcessTypeID.ProcessTypeID,
                        drow["ParmTypeName"],
                        drow["DefaultValue"],
                        drow["ItemValues"]);
                    if (flag)
                    {
                        drow["ParmTypeID"] = objParmTypeID;
                    }
                   
                }
                else
                {
                    flag = this.accParmType.UpdateProcessParmType(
                        ref errormsg,
                        drow["ParmTypeID"],
                        drow["ParmTypeName"],
                        drow["DefaultValue"],
                        drow["ItemValues"]);                 
                }
            }
            if (this.affterSave != null) this.affterSave();
            MessageBox.Show("�ɹ����浱ǰ����");
        }
        private void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtbParmTypes  .DefaultView[irow].Row;
            string ErrorMsg = string.Empty;
            if (drow["ParmTypeID"] == DBNull.Value)
            {           
                return;
            }         
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accParmType.DeleteProcessParmType  (ref ErrorMsg,
                    drow["ParmTypeID"]);
                if (flag)
                {
                    if (this.affterSave != null)
                    {
                        this.affterSave();
                    }
                }
                else
                {
                   MessageBox .Show (ErrorMsg +"/n�˼�¼�ѱ�����ҵ�����ã����ܴ����ݿ���ɾ��");
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
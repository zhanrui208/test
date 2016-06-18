using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer.Define
{
    public partial class FrmDevelopProcessType : Form
    {
        private DataTable dtblProcessType;
        private JERPData.Technology  .ProcessType accProcessType;
        public FrmDevelopProcessType()
        {
            InitializeComponent();
            this.accProcessType = new JERPData.Technology  .ProcessType();
            this.dgrdv.AutoGenerateColumns = false;
            this.SetPermit();

        }
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(312);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(313);
            if (this.enableBrowse)
            {
                //��������
                LoadData();
            }
            this.dgrdv.AllowUserToAddRows = enableSave;
            this.dgrdv.AllowUserToDeleteRows = enableSave;
            this.dgrdv.ReadOnly = !enableSave;
            if (this.enableSave)
            {
                this.dgrdv.RowValidated += new DataGridViewCellEventHandler(dgrdv_RowValidated);
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);

            }


        }
       
        private void LoadData()
        {
            this.dtblProcessType = this.accProcessType.GetDataProcessType().Tables[0];           
            this.dtblProcessType.Columns["ProcessTypeCode"].AllowDBNull = false;
            this.dtblProcessType.Columns["ProcessTypeCode"].Unique  = true;
            this.dtblProcessType.Columns["ProcessTypeName"].AllowDBNull = false;
            this.dtblProcessType.Columns["ProcessTypeName"].Unique = true;
            this.dgrdv.DataSource = this.dtblProcessType;
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
                drow = this.dtblProcessType.DefaultView[irow].Row;
            }
            catch 
            {
                return;
            }
            if (drow == null) return;
            if (drow.RowState == DataRowState.Unchanged) return;
            string errormsg = string.Empty;
            if (drow["ProcessTypeID"] == DBNull.Value)
            {
                object objProcessTypeID =DBNull .Value ;
                flag = this.accProcessType.InsertProcessType(ref errormsg, ref objProcessTypeID,
                        drow["ProcessTypeCode"],
                        drow["ProcessTypeName"]);
                if (flag)
                {
                    drow["ProcessTypeID"] = objProcessTypeID;
                    if(this.affterSave !=null) this.affterSave ();
                }
                else
                {
                    MessageBox.Show(errormsg);
                    this.LoadData();
                }
            }
            else
            {
                flag=this.accProcessType.UpdateProcessType(ref errormsg,
                             drow["ProcessTypeID"],
                             drow["ProcessTypeCode"],
                             drow["ProcessTypeName"]);
                if (flag)
                {
                    if (this.affterSave != null) this.affterSave();
                }
                else
                {
                    MessageBox.Show(errormsg);
                    this.LoadData();
                }

            }
        }     

        private void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblProcessType  .DefaultView[irow].Row;
            string ErrorMsg = string.Empty;
            if (drow["ProcessTypeID"] == DBNull.Value)
            {
                e.Cancel = true;
                return;
            }

            int  ProcessTypeID = (int)drow["ProcessTypeID"];
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accProcessType.DeleteProcessType(ref ErrorMsg,
                    ProcessTypeID);
                if (flag)
                {
                    if (this.affterSave != null)
                    {
                        this.affterSave();
                    }
                }
                else
                {
                   MessageBox .Show ("�˼�¼�ѱ�����ҵ�����ã����ܴ����ݿ���ɾ���˵�λ");
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
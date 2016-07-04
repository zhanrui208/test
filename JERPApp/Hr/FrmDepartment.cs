using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Hr
{
    public partial class FrmDepartment : Form
    {
        private DataTable dtblDepts;
        private JERPData.Hr .Department  accDept;
        public FrmDepartment()
        {
            InitializeComponent();
            this.accDept = new JERPData.Hr .Department ();
            this.dgrdv.AutoGenerateColumns = false;
            this.SetPermit();

        }
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(7);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(8);
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
            this.dtblDepts = this.accDept.GetDataDepartment().Tables[0];
            this.dtblDepts.Columns["DeptCode"].AllowDBNull = false;
            this.dtblDepts.Columns["DeptCode"].Unique = true; 
            this.dtblDepts.Columns["DeptName"].AllowDBNull = false;
            this.dtblDepts.Columns["DeptName"].Unique = true;
            this.dgrdv.DataSource = this.dtblDepts;
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
                drow = this.dtblDepts.DefaultView[irow].Row;
            }
            catch 
            {
                return;
            }
            if (drow == null) return;
            if (drow.RowState == DataRowState.Unchanged) return;
            string errormsg = string.Empty;
            if (drow["DeptID"] == DBNull.Value)
            {
                object objDeptID =DBNull .Value ;
                flag = this.accDept.InsertDepartment (ref errormsg, 
                        ref objDeptID,
                        drow["DeptCode"],
                        drow["DeptName"]);
                if (flag)
                {
                    drow["DeptID"] = objDeptID;
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
                flag=this.accDept.UpdateDepartment (ref errormsg,
                             drow["DeptID"],
                             drow["DeptCode"],
                             drow["DeptName"]);
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
            DataRow drow = this.dtblDepts  .DefaultView[irow].Row;
            string ErrorMsg = string.Empty;
            if (drow["DeptID"] == DBNull.Value)
            {
                e.Cancel = true;
                return;
            }

            int  DeptID = (int)drow["DeptID"];
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accDept.DeleteDepartment (ref ErrorMsg,
                    DeptID);
                if (flag)
                {
                    if (this.affterSave != null)
                    {
                        this.affterSave();
                    }
                }
                else
                {
                   MessageBox .Show (ErrorMsg);
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
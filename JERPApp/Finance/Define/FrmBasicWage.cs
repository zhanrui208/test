using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Define
{
    public partial class FrmBasicWage : Form
    {
      
        public FrmBasicWage()
        {
            InitializeComponent();
            this.accBasicWage = new JERPData.Finance.BasicWages();
            this.accPsn = new JERPData.Hr.Personnel();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.SetPermit();
        }
        private DataTable dtblBasicWage,dtblPsns;
        private JERPData.Finance.BasicWages accBasicWage;
        private JERPData.Hr.Personnel accPsn;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����     
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(306);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(307);
            if (this.enableBrowse)
            {
                this.SetColumnSrc();
                this.LoadData();               
            }
            this.btnSave.Enabled = this.enableSave;
            this.dgrdv.AllowUserToDeleteRows = this.enableSave;
            this.dgrdv.ReadOnly = !this.enableSave;
            if (this.enableSave)
            {
                this.dgrdv .UserDeletingRow +=new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
                this.btnSave.Click += new EventHandler(btnSave_Click);              
            }
        }
        private void SetColumnSrc()
        {
            this.dtblPsns = this.accPsn.GetDataPersonnelOnjob().Tables[0];
            this.dtblPsns.Columns.Add("exp", typeof(string), "ISNULL(PsnName,'')+ISNULL(PsnCode,'')");
            this.ColumnPsnID.DataSource = this.dtblPsns;
            this.ColumnPsnID.ValueMember = "PsnID";
            this.ColumnPsnID.DisplayMember = "exp";

        }
        private void LoadData()
        {
            this.dtblBasicWage = this.accBasicWage.GetDataBasicWages ().Tables[0];
            this.dtblBasicWage.Columns["PsnID"].AllowDBNull = false;
            this.dtblBasicWage.Columns["PsnID"].Unique  = true;
            this.dgrdv.DataSource = this.dtblBasicWage;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string errormsg = string.Empty;
            bool flag = false;
            foreach (DataRow drow in this.dtblBasicWage.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                object objWageID = drow["WageID"];
                if (objWageID == DBNull.Value)
                {
                    flag=this.accBasicWage.InsertBasicWages(
                        ref errormsg,
                        ref objWageID,
                        drow["PsnID"],
                        drow["StaticWage"],
                        drow["PositionWage"]);
                    if (flag)
                    {
                        drow["WageID"] = objWageID;
                    }
                }
                else
                {
                    this.accBasicWage.UpdateBasicWages (
                        ref errormsg,
                        objWageID,
                        drow["PsnID"],
                        drow["StaticWage"],
                        drow["PositionWage"]);

                }
            }
            MessageBox.Show("�ɹ����浱ǰ����");
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
        private void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblBasicWage  .DefaultView[irow].Row;
            if (drow["WageID"] == DBNull.Value)
            {
                return;
            }          
            string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {

                flag = this.accBasicWage.DeleteBasicWages(ref errormsg, drow["WageID"]);
                if (!flag)
                {
                   MessageBox .Show (errormsg );
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    
    }
}
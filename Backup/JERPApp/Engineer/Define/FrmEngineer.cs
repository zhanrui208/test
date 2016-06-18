using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer.Define
{
    public partial class FrmEngineer : Form
    {
        public FrmEngineer()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accPsn = new JERPData.Hr.Personnel();
            this.SetPermit();

        }
        private JERPData.Hr.Personnel accPsn;
        private DataTable dtblPsns;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(316);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(317);
            if (this.enableBrowse)
            {
                //��������
                LoadData();
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
            }
           
            this.dgrdv.AllowUserToDeleteRows = enableSave;
            this.btnAdd.Enabled = enableSave;
            if (this.enableSave)
            {
                this.btnAdd.Click += new EventHandler(btnAdd_Click);
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
            }


        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.ctrlPsnID.PsnID == -1) return;
            DataRow[] drows = this.dtblPsns.Select("PsnID=" + this.ctrlPsnID .PsnID.ToString(), "", DataViewRowState.CurrentRows);
            if (drows.Length > 0)
            {
                MessageBox.Show("�Ѽ���");
                return;
            }
            DataRow drowNew = this.dtblPsns.NewRow();
            drowNew["PsnID"] = this.ctrlPsnID .PsnID;
            drowNew["PsnCode"] = this.ctrlPsnID.PsnCode;
            drowNew["PsnName"] = this.ctrlPsnID.PsnName;
            this.dtblPsns.Rows.Add(drowNew);
            string errormsg = string.Empty;
            this.accPsn.UpdatePersonnelForEngineer(ref errormsg, this.ctrlPsnID.PsnID, true);
        }

        private void LoadData()
        {
            this.dtblPsns = this.accPsn.GetDataPersonnelForEngineer().Tables[0];
            this.dgrdv.DataSource = this.dtblPsns;
        }
        private void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;          
            DataRow drow = this.dtblPsns .DefaultView[irow].Row;            
            string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("��Ĳ�����Ҫ������Ա�Ƴ�����ʦ��", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                this.accPsn .UpdatePersonnelForEngineer (ref errormsg, drow["PsnID"],false);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
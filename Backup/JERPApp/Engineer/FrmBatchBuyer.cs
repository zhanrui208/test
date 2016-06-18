using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer
{
    public partial class FrmBatchBuyer : Form
    {
        public FrmBatchBuyer()
        {
            InitializeComponent();
            this.accPsns = new JERPData.Hr.Personnel();
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.SetColumnSrc();
        }
        private JERPData.Hr.Personnel accPsns;
        private DataTable dtblBuyer, dtblPsns;
        private void SetColumnSrc()
        {
            this.dtblPsns  = this.accPsns.GetDataPersonnel ().Tables[0];
            this.dtblPsns.Columns.Add("Exp", typeof(string), "ISNULL(AssistantCode,'')+ISNULL(PsnName,'')");
            this.ColumnPsnID.DataSource = this.dtblPsns;
            this.ColumnPsnID.ValueMember = "PsnID";
            this.ColumnPsnID.DisplayMember = "Exp";
        }
        public delegate void AffterSaveDelegate(DataRow[] drowPsns,bool BuyFlag);
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
        public void BatchBuyer()
        {
            this.dtblBuyer  = new DataTable();
            this.dtblBuyer.Columns.Add("PsnID", typeof(int));
            this.dtblBuyer.Columns["PsnID"].AllowDBNull = false;
            this.dtblBuyer.Columns["PsnID"].Unique = true;
            this.dgrdv.DataSource = this.dtblBuyer;
        }
        bool ValidateData()
        {
            if (this.dtblBuyer .Select ("","",DataViewRowState.CurrentRows).Length ==0)
            {
                MessageBox.Show("�Բ�����Աδѡ��");
                return false;
            }
            return true;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("���������趨��ѡ��Ʒ�ɹ�Ա����ȷ�ϣ�", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            DataRow[] drows = this.dtblBuyer .Select("", "", DataViewRowState.CurrentRows);
            this.affterSave(drows, true);
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("��������ȡ����ѡ��Ʒ�ɹ�Ա����ȷ�ϣ�", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            DataRow[] drows = this.dtblBuyer.Select("", "", DataViewRowState.CurrentRows);
            this.affterSave(drows, false);
        }

    }
}
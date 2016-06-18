using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture
{
    public partial class FrmManufPlanAlter : Form
    {
        public FrmManufPlanAlter()
        {
            InitializeComponent();
            this.accManufPlans = new JERPData.Manufacture.ManufPlans  ();
            this.ManufPlanEntity = new JERPBiz.Manufacture.ManufPlanEntity  ();
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.ctrlCompanyID.AppendAll();
            this.ctrlPrdID.AffterSelected +=ctrlPrdID_AffterSelected;
           
            
           
        }

        private JERPData.Manufacture.ManufPlans  accManufPlans;
        private JERPBiz.Manufacture.ManufPlanEntity  ManufPlanEntity;

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
        private long manufplanid = -1;
        private long ManufPlanID
        {
            get
            {
                return this.manufplanid;
            }
            set
            {
                this.manufplanid = value;
                this.ctrlPrdID.Enabled = (value == -1);
            }
        }
        public void Edit(long ManufPlanID)
        {
            this.ManufPlanID = ManufPlanID;
            this.ManufPlanEntity.LoadData(ManufPlanID); 
            this.txtDateNote.Text = this.ManufPlanEntity.DateNote.ToShortDateString();
            this.ctrlCompanyID.CompanyID = this.ManufPlanEntity.CompanyID;
            this.ctrlPrdID.PrdID = this.ManufPlanEntity.PrdID;
            this.txtQuantity.Text = string.Format("{0:0.####}", this.ManufPlanEntity.Quantity);
            this.lblUnitName.Text = this.ManufPlanEntity.UnitName; 
            this.radPrdStore.Checked = this.ManufPlanEntity.PrdStoreFlag;
            this.radMtrStore.Checked = this.ManufPlanEntity.MtrStoreFlag;
            if (this.ManufPlanEntity.DateTarget == DateTime.MinValue)
            {
                this.dtpDateTarget.Value = DateTime.Now.AddDays(3);
            }
            else
            {
                this.dtpDateTarget.Value = this.ManufPlanEntity.DateTarget;
            }
           
            this.rchMemo .Text =this.ManufPlanEntity .Memo ;
            this.ckbBOMPlanFlag.Checked = this.ManufPlanEntity.BOMPlanFlag ; 
            
            //����ؼ����ÿɲ���
            bool BomPlanFlag = (this.ManufPlanEntity.BOMPlanFlag); 
            this.btnSave.Enabled = !BomPlanFlag;
            this.btnDelete.Enabled = !BomPlanFlag; 
           
        }
     
        void ctrlPrdID_AffterSelected()
        {
         
            this.lblUnitName.Text = this.ctrlPrdID.PrdEntity.UnitName;
            this.radPrdStore.Checked = this.ctrlPrdID.PrdEntity.SaleFlag; 
            this.radMtrStore.Checked = !this.radPrdStore.Checked;

        }
        bool ValidateData()
        {
            if (this.ctrlPrdID.PrdID == -1)
            {
                MessageBox.Show("δѡ���κβ�Ʒ");
                return false;
            }
            decimal d;
            if (!decimal.TryParse(this.txtQuantity.Text, out d))
            {
                MessageBox.Show("������ʽ����!");
                return false;
            }
            if (d <= 0)
            {
                MessageBox.Show("��������С�ڻ����0!");
                return false;
            }
            return true;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            DialogResult rul = MessageBox.Show("��Ҫ���б��棬��ȷ���������룡", "����ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rul == DialogResult.Cancel) return;
            bool flag = false;
            string errormsg = string.Empty;
            flag = this.accManufPlans.UpdateManufPlans  (ref errormsg,
                this.ManufPlanID,
                this.ctrlCompanyID.CompanyID,
                this.txtQuantity.Text,
                this.dtpDateTarget.Value,
                this.radPrdStore.Checked,
                this.radMtrStore .Checked , 
                JERPBiz.Frame.UserBiz.PsnID,
                this.rchMemo.Text);
            if (flag)
            {
                MessageBox.Show("�ɹ������ǰ�����ƻ�");
                if (this.affterSave != null) this.affterSave();
            }
            else
            {
                MessageBox.Show(errormsg );
                if (this.affterSave != null) this.affterSave();
                this.Close();
            }
        }

       

        void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult rul = MessageBox.Show("�������мƻ�ɾ����һ��ɾ�����ָܻ���", "����ȷ��", MessageBoxButtons.OKCancel , MessageBoxIcon.Question);
            if (rul == DialogResult.Cancel ) return;
            bool flag = false;
            string errormsg = string.Empty;
            flag = this.accManufPlans.DeleteManufPlans  (ref errormsg, this.ManufPlanID);
            if (flag)
            {
                MessageBox.Show("�ɹ�ɾ����ǰ�����ƻ�");
            }
            else
            {
                MessageBox.Show(errormsg);
            }
            if (this.affterSave != null) this.affterSave();
            this.Close();
        }
    }
}
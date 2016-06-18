using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture
{
    public partial class FrmManufPlanFromMtrBackupOrder : Form
    {
        public FrmManufPlanFromMtrBackupOrder()
        {
            InitializeComponent();
            this.accBuyPlan = new JERPData.Product.BuyPlans();
            this.accManufPlans = new JERPData.Manufacture.ManufPlans  ();
            this.dtpDateTarget.Value = DateTime.Now.AddDays(15);
            this.ctrlCompanyID.AppendAll();
            this.ctrlPrdID.AffterSelected += ctrlPrdID_AffterSelected;
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

       
     
        private JERPData.Product.BuyPlans accBuyPlan;
        private JERPData.Manufacture.ManufPlans   accManufPlans;
        public void BackupOper()
        {
            this.ctrlPrdID.PrdID = -1;
            this.ctrlPrdID.Enabled = true;
            this.ctrlCompanyID.CompanyID = -1;
            this.txtQuantity.Text = string.Empty;
            this.rchMemo.Text = string.Empty;
        }
        public void BackupOper(int PrdID,decimal RequireQty)
        {
            this.ctrlPrdID.PrdID = PrdID;
            this.ctrlPrdID.Enabled = false;
            this.ctrlCompanyID.CompanyID = -1;
            this.txtQuantity.Text = string.Format("{0:0.####}", RequireQty);
            this.lblUnitName.Text = this.ctrlPrdID.PrdEntity.UnitName;
            this.rchMemo.Text = string.Empty;
        }
        void ctrlPrdID_AffterSelected()
        {
            this.lblUnitName.Text = this.ctrlPrdID.PrdEntity.UnitName;
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
        bool ValidateData()
        {
            if (this.ctrlPrdID.PrdID == -1)
            {
                MessageBox.Show("δѡ���κβ�Ʒ");
                return false;
            }
            decimal d;
            if (!decimal.TryParse(this.txtQuantity .Text, out d))
            {
                MessageBox.Show("������ʽ����!");
                return false;
            }
            if (d <= 0)
            {
                MessageBox.Show("��������<=0!");
                return false;
            }
            return true;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            string errormsg = string.Empty; 
            //������������
            DialogResult rul = MessageBox.Show("�������б����µ�һ���´ﲻ�ܱ������ȷ�ϣ�", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            decimal Quantity = decimal.Parse(this.txtQuantity.Text);
            bool PrdStoreFlag=false;
            if (Quantity > 0)
            {             
                if (this.radManufFlag.Checked)
                { 
                    this.accManufPlans.InsertManufPlans (ref errormsg, 
                       DateTime.Now .Date,
                       DBNull .Value ,
                       this.ctrlCompanyID .CompanyID ,
                       this.ctrlPrdID .PrdID,
                       Quantity,
                       PrdStoreFlag,
                       true,
                       this.dtpDateTarget .Value,
                       this.rchMemo .Text ,
                       true ,
                       JERPBiz .Frame .UserBiz .PsnID 
                       );

                }
                if (this.radBuyFlag.Checked)
                {
                    this.accBuyPlan.SaveBuyPlans (ref errormsg,
                        this.ctrlPrdID.PrdID,
                        Quantity );
                }
            }
            if (this.affterSave != null) this.affterSave();
            MessageBox.Show("�ɹ����е�ǰ�����µ���");
            this.Close();
        }

    }
}
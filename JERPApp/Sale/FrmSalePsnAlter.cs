using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Sale
{
    public partial class FrmSalePsnAlter : Form
    {
        public FrmSalePsnAlter()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accCustomers = new JERPData.General.Customer ();
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.ctrlAll.SeachGridView = this.dgrdv;
            this.btnExport.Click += new EventHandler(btnExport_Click);
            this.ctrlFromPsnID.AffterSelected += new JERPApp.Define.Hr.CtrlPersonnel.AffterSelectedDelegate(ctrlFromPsnID_AffterSelected);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.FormClosed += new FormClosedEventHandler(FrmSalePsnAlter_FormClosed);
        }
        private JERPData.General .Customer accCustomers;
        private DataTable dtblCustomer;   
        void ctrlFromPsnID_AffterSelected(int PsnID)
        {
            this.LoadData();
        }
        public void SalePsnAlter()
        {
            this.LoadData();
        }
        void FrmSalePsnAlter_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.affterSave != null) this.affterSave();
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

        private void LoadData()
        {
            this.dtblCustomer = this.accCustomers.GetDataCustomerBySalePsnID (this.ctrlFromPsnID.PsnID).Tables[0];
            this.dtblCustomer.Columns.Add("CheckedFlag", typeof(bool));
            this.dgrdv.DataSource = this.dtblCustomer;
        }
        bool ValidateData()
        {
            if (this.ctrlToPsnID.PsnID == -1)
            {
                MessageBox.Show("�Բ���ת��ҵ��Ա����Ϊ��");
                return false;
            }
            if (this.dtblCustomer.Select("CheckedFlag=1").Length == 0)
            {
                MessageBox.Show("�Բ���δѡ���κοͻ�");
                return false;
            }
            return true;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;
            bool flag = false;
            string errormsg = string.Empty;
             DialogResult rul = MessageBox.Show("��������ҵ��Աת��!\n��ȷ�ϣ�", "ת��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (rul == DialogResult.Yes)
             {
                 foreach (DataRow drow in this.dtblCustomer.Select("CheckedFlag=1"))
                 {
                     flag = this.accCustomers.UpdateCustomerForSalePsn(
                         ref errormsg,
                         drow["CompanyID"],
                         this.ctrlToPsnID.PsnID);

                 }
                 MessageBox.Show("�ɹ����пͻ�ҵ��Ա���");
                 this.LoadData();
             }
           
        }
   
        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("��������Excel�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "�ͻ�");
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.SetRangeAutoFit(3, 1, rowIndex, colIndex, true, true);
            FrmMsg.Hide();
            excel.Show();
        }
    }
}
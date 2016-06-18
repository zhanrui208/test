using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Receivable
{
    public partial class FrmAdvanceAMTOper : Form
    {
        public FrmAdvanceAMTOper()
        {
            InitializeComponent(); 
            this.accSaleOrderNotes = new JERPData.Product.SaleOrderNotes(); 
            this.SaleOrderNoteEntity = new JERPBiz.Product.SaleOrderNoteEntity();
            this.accAdvanceAMT = new JERPData.Finance.AdvanceReceiveAccount();
            this.accCashAccount = new JERPData.Finance.CashAccount();
            this.accBankAccount = new JERPData.Finance.BankAccount();
            this.accReceiptNotes = new JERPData.Product.SaleReceiptNotes();
            this.accRecords = new JERPData.Product.SaleAdvanceAMTRecords();
            this.printhelper = new JERPBiz.Product.SaleReceiptPrintHelper();
            new ToolTip().SetToolTip(this.btnSaleOrder, "����Ԥ��");
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnSaleOrder.Click += new EventHandler(btnSaleOrder_Click);
            this.ctrlCompanyID.AffterSelected +=ctrlCompanyID_AffterSelected;
            this.btnNew.Click += new EventHandler(btnNew_Click);
        }


        private JERPData.Product.SaleOrderNotes accSaleOrderNotes; 
        private JERPBiz.Product.SaleOrderNoteEntity SaleOrderNoteEntity;
        private JERPData.Product.SaleAdvanceAMTRecords accRecords;
        private JERPData.Finance.AdvanceReceiveAccount accAdvanceAMT;
        private JERPData.Finance.CashAccount accCashAccount;
        private JERPData.Finance.BankAccount accBankAccount;
        private JERPData.Product.SaleReceiptNotes accReceiptNotes;
        private JERPBiz.Product.SaleReceiptPrintHelper printhelper;
        private FrmAdvanceAMTFromOrderNote frmFromOrder; 
        private long SaleOrderNoteID = -1;
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
        void btnSaleOrder_Click(object sender, EventArgs e)
        {
            if (frmFromOrder == null)
            {
                frmFromOrder = new FrmAdvanceAMTFromOrderNote();
                new FrmStyle(frmFromOrder).SetPopFrmStyle(this);
                frmFromOrder.AffterSelect += new FrmAdvanceAMTFromOrderNote.AffterSelectDelegate(frmFromOrder_AffterSelect);
            }
            frmFromOrder.LoadData();
            frmFromOrder.ShowDialog();
        }

        void frmFromOrder_AffterSelect(long NoteID)
        {
            this.SaleOrderNoteID = NoteID;
            this.SaleOrderNoteEntity.LoadData(NoteID);
            this.txtPONo.Text = this.SaleOrderNoteEntity.PONo;
            this.txtDateNote.Text = this.SaleOrderNoteEntity.DateNote.ToShortDateString();
            this.ctrlCompanyID.CompanyID = this.SaleOrderNoteEntity.CompanyID;
            this.ctrlFinanceAddressID.LoadData(this.ctrlCompanyID.CompanyID);
            this.ctrlFinanceAddressID.FinanceAddressID = this.SaleOrderNoteEntity.FinanceAddressID;
            this.ctrlMoneyTypeID.MoneyTypeID = this.SaleOrderNoteEntity.MoneyTypeID;
            this.ctrlCompanyID.Enabled = false;
            this.ctrlMoneyTypeID.Enabled = false;         
        }

        public void New()
        {
            this.SaleOrderNoteID = -1;
            this.txtReceiptNoteCode.Text = string.Empty;
            this.txtPONo.Text = string.Empty;
            this.txtDateNote.Text = string.Empty;
            this.dtpDateRecord.Value = DateTime.Now.Date;
            this.txtAmount.Text = string.Empty;
            this.rchMemo.Text = string.Empty;
            this.ctrlCompanyID.CompanyID = -1;
            this.ctrlFinanceAddressID.LoadData(-1);
            this.ctrlCompanyID.Enabled =true ;
            this.ctrlMoneyTypeID.Enabled = true;
        }

        void ctrlCompanyID_AffterSelected()
        {
            this.ctrlFinanceAddressID.LoadData(this.ctrlCompanyID.CompanyID);
        }

        void btnNew_Click(object sender, EventArgs e)
        {
            this.New();
        }
        bool ValidateData()
        {
            if (this.ctrlCompanyID.CompanyID == -1)
            {
                MessageBox.Show("�Բ��𣬿ͻ�����Ϊ��");
                return false;
            }
            if (this.ctrlMoneyTypeID .MoneyTypeID  == -1)
            {
                MessageBox.Show("�Բ��𣬱��ֲ���Ϊ��");
                return false;
            }
            decimal Amount;
            if ((decimal.TryParse(this.txtAmount.Text, out Amount) == false)
                || (Amount <= 0))
            {
                MessageBox.Show("���������0!");
                return false;
            }
            return true;
        }


      
        private void SaveReceiptNote(decimal Amount,int BankID)
        {
            object objReceiptNoteID = DBNull.Value;
            string errormsg = string.Empty;
            bool flag = false;
            object objNoteCode = DBNull.Value;            
            if (this.txtReceiptNoteCode.Text.Length > 0)
            {
                objNoteCode=this.txtReceiptNoteCode .Text ;
            }
            flag=this.accReceiptNotes.InsertSaleReceiptNotes(
                ref errormsg,
                ref objReceiptNoteID,
                ref objNoteCode ,
                DateTime.Now.Date,
                this.ctrlCompanyID .CompanyID,
                this.ctrlFinanceAddressID .FinanceAddressID,
                DBNull .Value ,
                this.ctrlMoneyTypeID .MoneyTypeID,
                Amount,
                0,
                BankID ,
                -1,
                JERPBiz.Frame.UserBiz.PsnID); 
            if (!flag)
            {
                MessageBox .Show(errormsg );
                return;
            }
            if (this.txtReceiptNoteCode.Text.Length == 0)
            {
                DialogResult rut = MessageBox.Show("�Ƿ���Ҫ��ӡ�վ�?", "�վݴ�ӡ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rut == DialogResult.OK)
                {
                    this.printhelper.ExportToExcel((long)objReceiptNoteID);
                }
                this.txtReceiptNoteCode.Text = objNoteCode.ToString();
            }
             
            
        }
       
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;          
            DialogResult rut = MessageBox.Show("����Ԥ�տ���б��棬һ������,���ܱ��,ȷ�Ϸ�?", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rut == DialogResult.No) return;        
            string errormsg = string.Empty;
            bool flag = false;
            decimal Amount = decimal.Parse(this.txtAmount.Text);
            flag=this.accRecords.InsertSaleAdvanceAMTRecords(ref errormsg,
                this.dtpDateRecord.Value,
                this.SaleOrderNoteID,
                this.ctrlCompanyID.CompanyID,
                this.ctrlMoneyTypeID.MoneyTypeID,
                Amount,
                JERPBiz.Frame.UserBiz.PsnID,
                this.rchMemo.Text);
            if (this.SaleOrderNoteID > -1)
            {
                flag = this.accSaleOrderNotes.UpdateSaleOrderNotesForAdvanceAMT(
                    ref errormsg,
                    this.SaleOrderNoteID ,
                    Amount);
            }            
            int BankID = JERPApp.Define.Finance.FrmSettleAMTType.GetBankID(); 
            //Ūһ������֮�վ�
            this.SaveReceiptNote(Amount,BankID);
            //���Ӷ���
            this.accAdvanceAMT.InsertAdvanceReceiveAccountForCredit(ref errormsg,
               "�վݺ�[" + this.txtReceiptNoteCode.Text + "]",
               this.ctrlCompanyID .CompanyID,
               this.ctrlMoneyTypeID.MoneyTypeID,
               Amount ,
               JERPBiz.Frame.UserBiz.PsnID);
            if (BankID == -1)
            {
                //�ֽ���
                this.accCashAccount.InsertCashAccountForDebit(
                    ref errormsg,
                    "��ȡ�ͻ�[" + this.ctrlCompanyID .CompanyAbbName + "]Ԥ�տ�,�վݺ�[" + this.txtReceiptNoteCode.Text + "]",
                    this.ctrlMoneyTypeID .MoneyTypeID,
                    Amount ,
                    JERPBiz.Frame.UserBiz.PsnID);
            }
            else
            {
                //���д��
                this.accBankAccount.InsertBankAccountForDebit(
                    ref errormsg,
                    "��ȡ�ͻ�[" + this.ctrlCompanyID.CompanyAbbName + "]Ԥ�տ�,�վݺ�[" + this.txtReceiptNoteCode.Text + "]",
                    BankID,
                    this.ctrlMoneyTypeID .MoneyTypeID,
                    Amount,
                    JERPBiz.Frame.UserBiz.PsnID);
            }            
            if (this.affterSave != null) this.affterSave();
            MessageBox.Show("�ɹ����˵�ǰԤ�տ�");
            this.Close();
        }
    }
}
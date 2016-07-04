using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.Product
{
    public partial class FrmAdvanceAMTOper : Form
    {
        public FrmAdvanceAMTOper()
        {
            InitializeComponent(); 
            this.accBuyOrderNotes = new JERPData.Product.BuyOrderNotes(); 
            this.BuyOrderNoteEntity = new JERPBiz.Product.BuyOrderNoteEntity();
            this.accAdvanceAMT = new JERPData.Finance.AdvancePayingAccount();
            this.accCashAccount = new JERPData.Finance.CashAccount();
            this.accBankAccount = new JERPData.Finance.BankAccount();
            this.accReceiptNotes = new JERPData.Product.BuyReceiptNotes();
            this.accRecords = new JERPData.Product.BuyAdvanceAMTRecords(); 
            new ToolTip().SetToolTip(this.btnBuyOrder, "����Ԥ��");
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnBuyOrder.Click += new EventHandler(btnBuyOrder_Click); 
            this.btnNew.Click += new EventHandler(btnNew_Click);
        }


        private JERPData.Product.BuyOrderNotes accBuyOrderNotes; 
        private JERPBiz.Product.BuyOrderNoteEntity BuyOrderNoteEntity;
        private JERPData.Product.BuyAdvanceAMTRecords accRecords;
        private JERPData.Finance.AdvancePayingAccount accAdvanceAMT;
        private JERPData.Finance.CashAccount accCashAccount;
        private JERPData.Finance.BankAccount accBankAccount;
        private JERPData.Product.BuyReceiptNotes accReceiptNotes; 
        private FrmAdvanceAMTFromOrderNote frmFromOrder; 
        private long BuyOrderNoteID = -1;
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
        void btnBuyOrder_Click(object sender, EventArgs e)
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
            this.BuyOrderNoteID = NoteID;
            this.BuyOrderNoteEntity.LoadData(NoteID);
            this.txtPONo.Text = this.BuyOrderNoteEntity.NoteCode;
            this.txtDateNote.Text = this.BuyOrderNoteEntity.DateNote.ToShortDateString();
            this.ctrlSupplierID.CompanyID = this.BuyOrderNoteEntity.CompanyID;  
            this.ctrlMoneyTypeID.MoneyTypeID = this.BuyOrderNoteEntity.MoneyTypeID;
            this.ctrlSupplierID.Enabled = false;
            this.ctrlMoneyTypeID.Enabled = false;         
        }

        public void New()
        {
            this.BuyOrderNoteID = -1;
            this.txtReceiptNoteCode.Text = string.Empty;
            this.txtPONo.Text = string.Empty;
            this.txtDateNote.Text = string.Empty;
            this.dtpDateRecord.Value = DateTime.Now.Date;
            this.txtAmount.Text = string.Empty;
            this.rchMemo.Text = string.Empty;
            this.ctrlSupplierID.CompanyID = -1; 
            this.ctrlSupplierID.Enabled =true ;
            this.ctrlMoneyTypeID.Enabled = true;
        }

   

        void btnNew_Click(object sender, EventArgs e)
        {
            this.New();
        }
        bool ValidateData()
        {
            if (this.ctrlSupplierID.CompanyID == -1)
            {
                MessageBox.Show("�Բ��𣬹�Ӧ�̲���Ϊ��");
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
            flag=this.accReceiptNotes.InsertBuyReceiptNotes(
                ref errormsg,
                ref objReceiptNoteID,
                this.txtReceiptNoteCode .Text ,
                DateTime.Now.Date,
                DBNull .Value ,  
                this.ctrlSupplierID .CompanyID ,
                this.ctrlMoneyTypeID .MoneyTypeID,
                Amount,
                0,
                BankID,
                JERPBiz.Frame.UserBiz.PsnID);
            if (!flag)
            {
                MessageBox .Show(errormsg );
                return;
            }
            
           
        }
       
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;          
            DialogResult rut = MessageBox.Show("����Ԥ������б��棬һ������,���ܱ��,ȷ�Ϸ�?", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rut == DialogResult.No) return;        
            string errormsg = string.Empty;
            bool flag = false;
            decimal Amount = decimal.Parse(this.txtAmount.Text);
            flag=this.accRecords.InsertBuyAdvanceAMTRecords(ref errormsg,
                this.dtpDateRecord.Value,
                this.BuyOrderNoteID,
                this.ctrlSupplierID.CompanyID,
                this.ctrlMoneyTypeID.MoneyTypeID,
                Amount,
                JERPBiz.Frame.UserBiz.PsnID,
                this.rchMemo.Text);
            if (this.BuyOrderNoteID > -1)
            {
                flag = this.accBuyOrderNotes.UpdateBuyOrderNotesForAdvanceAMT(
                    ref errormsg,
                    this.BuyOrderNoteID ,
                    Amount); 
               

            }            
            int BankID = JERPApp.Define.Finance.FrmSettleAMTType.GetBankID();  
            //Ūһ������֮����
            this.SaveReceiptNote(Amount,BankID);
            //���Ӷ���
            this.accAdvanceAMT.InsertAdvancePayingAccountForDebit  (ref errormsg,
               "��Ʒ�ɹ�Ԥ���վݺ�[" + this.txtReceiptNoteCode.Text + "]",
               this.ctrlSupplierID .CompanyID,
               this.ctrlMoneyTypeID.MoneyTypeID,
               Amount ,
               JERPBiz.Frame.UserBiz.PsnID);
            if (BankID == -1)
            {
                //�ֽ���
                this.accCashAccount.InsertCashAccountForCredit(
                    ref errormsg,
                    "֧����Ʒ��Ӧ��[" + this.ctrlSupplierID .CompanyAbbName + "]Ԥ����,�վݺ�[" + this.txtReceiptNoteCode.Text + "]",
                    this.ctrlMoneyTypeID .MoneyTypeID,
                    Amount ,
                    JERPBiz.Frame.UserBiz.PsnID);
            }
            else
            {
                //���д��
                this.accBankAccount.InsertBankAccountForCredit(
                    ref errormsg,
                    "֧����Ʒ��Ӧ��[" + this.ctrlSupplierID.CompanyAbbName + "]Ԥ����,�վݺ�[" + this.txtReceiptNoteCode.Text + "]",
                    BankID,
                    this.ctrlMoneyTypeID .MoneyTypeID,
                    Amount,
                    JERPBiz.Frame.UserBiz.PsnID);
            }            
            if (this.affterSave != null) this.affterSave();
            MessageBox.Show("�ɹ����˵�ǰԤ����");
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace JERPApp.Finance.Payable.Postage
{
    public partial class FrmSettleAMTOper : Form
    {
        public FrmSettleAMTOper()
        {
            InitializeComponent();
            this.accReconciliations = new JERPData.Finance.PostageReconciliations();
            this.accPostageNotes  = new JERPData.Finance.PostageNotes ();
            this.accReceiptNote = new JERPData.Finance.PostageReceiptNotes();
            this.ReconciliationEntity = new JERPBiz.Finance.PostageReconciliationEntity();
            this.accCashAccount = new JERPData.Finance.CashAccount();
            this.accBankAccount = new JERPData.Finance.BankAccount();
            this.accPayableAccount = new JERPData.Finance.ExpressPayableAccount();
            this.MoneyTypeParm = new JERPBiz.Finance.MoneyTypeParm();
            this.dgrdv.AutoGenerateColumns = false;            
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

        private JERPData.Finance.PostageReconciliations accReconciliations;
        private JERPData.Finance.PostageReceiptNotes accReceiptNote;
        private JERPData.Finance.PostageNotes  accPostageNotes;
        private JERPData.Finance.CashAccount accCashAccount;
        private JERPData.Finance.BankAccount accBankAccount;
        private JERPData.Finance.ExpressPayableAccount accPayableAccount;
        private JERPBiz.Finance.MoneyTypeParm MoneyTypeParm;
        private JERPBiz.Finance.PostageReconciliationEntity ReconciliationEntity;
        private DataTable dtblPostageNotes;
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
        private long ReconciliationID = -1;
        private long noteID = -1;
        private long NoteID
        {
            get
            {
                return this.noteID;
            }
            set
            {
                this.noteID = value;
            }
        }
        
     
        public void SettleAMTOper(long ReconciliationID)
        {
            this.NoteID = -1;
            this.txtReceiptNoteCode.Text = string.Empty;
            this.ReconciliationID = ReconciliationID;
            this.ReconciliationEntity.LoadData(ReconciliationID);
            this.txtReconciliationCode.Text = this.ReconciliationEntity.ReconciliationCode;
            this.txtYear.Text = this.ReconciliationEntity.Year.ToString();
            this.txtMonth.Text = this.ReconciliationEntity.Month.ToString();
            this.txtCompanyName .Text = this.ReconciliationEntity.CompanyName ;
            this.txtDateNote.Text = this.ReconciliationEntity.DateNote.ToShortDateString(); 
            this.txtDateSettle.Text = this.ReconciliationEntity.DateSettle.ToShortDateString();
            this.txtNonFinishedAMT.Text = string.Format("{0:#,##0.####}", this.ReconciliationEntity.NonFinishedAMT);
            this.txtAmount .Text = string.Format("{0:0.####}", this.ReconciliationEntity.NonFinishedAMT);
            this.LoadItems();
        }
        private void LoadItems()
        {
            this.dtblPostageNotes  = this.accPostageNotes .GetDataPostageNotesByReconciliationID(this.ReconciliationID).Tables[0];
            this.dgrdv.DataSource = this.dtblPostageNotes;

        }

    
        private bool ValidateData()
        {
            if (this.txtReceiptNoteCode.Text.Length == 0)
            {
                MessageBox.Show("�Բ���,�վݱ�Ų���Ϊ��");
                return false;
            }
      
            decimal Amount;
            if (!decimal.TryParse(this.txtAmount .Text, out Amount))
            {
                MessageBox.Show("�Բ���֧������ʽ����ȷ");
                return false;
            }
            if (Amount > this.ReconciliationEntity .NonFinishedAMT)
            {
                MessageBox.Show("֧�����ܴ���δ�����");
                return false;
            }
            return true;
        }
   
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;
            DialogResult rlt = MessageBox.Show("�㽫�����վݽ�һ�������޸ĵ��ݣ�\n�ǣ�ȷ�ϣ���ȡ��!", "����ȷ��",
MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.No) return;
            string errormsg = string.Empty;
            object objNoteID = DBNull.Value;
            decimal Amount = decimal.Parse(this.txtAmount.Text);
            int BankID = JERPApp.Define.Finance.FrmSettleAMTType.GetBankID();
            bool flag = this.accReceiptNote .InsertPostageReceiptNotes (ref errormsg,
                ref objNoteID,
                this.txtReceiptNoteCode .Text,
                DateTime.Now.Date,
                this.ReconciliationID,
                Amount,
                BankID ,
                JERPBiz.Frame.UserBiz.PsnID);
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }
            this.NoteID = (long)objNoteID;
            this.accReconciliations.UpdatePostageReconciliationsForAppendFinishedAMT(ref errormsg, this.ReconciliationID, Amount);
            int MainMoneyTypeID = this.MoneyTypeParm.GetMainMoneyTypeID();
       
            if (BankID == -1)
            {
                //�ֽ���
                this.accCashAccount.InsertCashAccountForCredit(ref errormsg,
                    "֧��������˾[" + this.txtCompanyName .Text  + "]�˷��վ�[" +
                    this.txtReceiptNoteCode.Text + "]�˷�",
                    MainMoneyTypeID ,
                    Amount,
                    JERPBiz.Frame.UserBiz.PsnID);
            }
            else
            {
                //���д��
                this.accBankAccount.InsertBankAccountForCredit(ref errormsg,
                 "֧��������˾[" + this.txtCompanyName.Text + "]�˷��վ�[" +
                    this.txtReceiptNoteCode.Text + "]�˷�",
                    BankID,
                    MainMoneyTypeID,
                    Amount,
                    JERPBiz.Frame.UserBiz.PsnID);
            }
            //����Ӧ���˿�
            this.accPayableAccount.InsertExpressPayableAccountForDebit(
                ref errormsg,
               "֧��������˾[" + this.txtCompanyName.Text + "]�˷��վ�[" +
                    this.txtReceiptNoteCode.Text + "]�˷�",
                this.ReconciliationEntity.CompanyID,
                MainMoneyTypeID ,
                Amount,
                JERPBiz.Frame.UserBiz.PsnID); 
         
     
            MessageBox.Show("�ɹ������˷ѽ��");
            if (this.affterSave != null) this.affterSave();
            this.Close();  
          
        }
    }
}
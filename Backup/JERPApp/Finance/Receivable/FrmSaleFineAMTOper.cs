using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Receivable
{
    public partial class FrmSaleFineAMTOper : Form
    {
        public FrmSaleFineAMTOper()
        {
            InitializeComponent();
            this.accCashAccount = new JERPData.Finance.CashAccount();
            this.accBankAccount = new JERPData.Finance.BankAccount();
            this.accReceivableAccount = new JERPData.Finance.ReceivableAccount();
            this.accNotes = new JERPData.Product.SaleFineAMTNotes();
            this.accExpenseAccount = new JERPData.Finance.ExpenseAccount();
            this.btnSave .Click +=new EventHandler(btnSave_Click);
        }

        private JERPData.Product.SaleFineAMTNotes accNotes;
        private JERPData.Finance.CashAccount accCashAccount;
        private JERPData.Finance.BankAccount accBankAccount;
        private JERPData.Finance.ExpenseAccount accExpenseAccount;
        private JERPData.Finance.ReceivableAccount accReceivableAccount;
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
        public  void New()
        {

            this.txtNoteCode.Text = "�µ���";
            this.dtpDateNote.Value = DateTime.Now.Date;
            this.ctrlCustomerID.CompanyID = -1;
            this.txtAmount.Text = string.Empty;
            this.txtSummary.Text = string.Empty;
          
        }
  
        private bool ValidateData()
        {
            int CompanyID = this.ctrlCustomerID.CompanyID;
            int MoneyTypeID = this.ctrlMoneyTypeID.MoneyTypeID;
            if (
                (CompanyID == -1) ||
                (MoneyTypeID == -1))
            {
                MessageBox.Show("�Բ���ͻ������ֲ���Ϊ�գ�");
                return false;
            }
            decimal Amount;
            if (!decimal.TryParse(this.txtAmount.Text, out Amount))
            {
                MessageBox.Show("�Բ���,�ۿ���ʽ����!");
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;
            bool flag = false;
            string errormsg = string.Empty;
            DialogResult rut = MessageBox.Show("�㽫��ӵ�ǰ�ۿ��ݣ���ע���ʽ!\n��,ȷ��;��,��ȡ��",
      "������ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rut == DialogResult.No) return;
            object objNoteID = -1;
         
            flag = this.accNotes.InsertSaleFineAMTNotes(
                ref errormsg,
                ref objNoteID,
                this.txtNoteCode.Text,
                this.dtpDateNote.Value.Date,
                this.ctrlCustomerID.CompanyID,
                this.ctrlMoneyTypeID.MoneyTypeID,
                this.radCashSettleFlag.Checked,
                this.txtAmount .Text ,
                this.txtSummary.Text,
                JERPBiz.Frame.UserBiz.PsnID);
            if (!flag)
            {
                MessageBox .Show (errormsg );
                return;
            }
            if (this.radCashSettleFlag.Checked)
            {
               int BankID = JERPApp.Define.Finance.FrmSettleAMTType.GetBankID();
               if (BankID == -1)
               {
                   //�ֽ�
                   this.accCashAccount.InsertCashAccountForCredit(
                       ref errormsg,
                       "���ۿۿ[" + this.txtNoteCode.Text + "]�ۿ�",
                       this.ctrlMoneyTypeID.MoneyTypeID,
                       this.txtAmount.Text,
                       JERPBiz.Frame.UserBiz.PsnID);
               }
               else
               {
                   //���д��
                   this.accBankAccount.InsertBankAccountForCredit (
                       ref errormsg,
                       "���ۿۿ[" + this.txtNoteCode.Text + "]�ۿ�",
                       BankID ,
                       this.ctrlMoneyTypeID.MoneyTypeID,
                       this.txtAmount.Text,
                       JERPBiz.Frame.UserBiz.PsnID);
               }
              
                //Ū�ɶ���
                this.accNotes.UpdateSaleFineAMTNotesForReconciliation(ref errormsg,
                   objNoteID,-1);
            }
            else
            {
                //Ӧ���˿�
                this.accReceivableAccount.InsertReceivableAccountForCredit(
                    ref errormsg,
                    "���ۿۿ[" + this.txtNoteCode.Text + "]���",
                    this.ctrlCustomerID.CompanyID,
                    this.ctrlMoneyTypeID.MoneyTypeID,
                    this.txtAmount .Text ,
                    JERPBiz.Frame.UserBiz.PsnID);

            }
            //�ۿ�͵��ɷ�����
            this.accExpenseAccount.InsertExpenseAccountForDebit(
                ref errormsg,
                "���ۿۿ[" + this.txtNoteCode.Text + "]�ۿ�",
                JERPBiz.Finance.ExpenseTypeParm.SaleFineExpense,
                decimal.Parse(this.txtAmount.Text) * this.ctrlMoneyTypeID.ExchangeRate,
                JERPBiz.Frame.UserBiz.PsnID);

            MessageBox.Show("�ɹ������˵�ǰ�ۿ���");
            if (this.affterSave != null) this.affterSave();
            this.New();
          
        }

   
    }
}
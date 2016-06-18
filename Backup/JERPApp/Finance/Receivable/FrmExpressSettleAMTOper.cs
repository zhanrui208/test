using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace JERPApp.Finance.Receivable
{
    public partial class FrmExpressSettleAMTOper : Form
    {
        public FrmExpressSettleAMTOper()
        {
            InitializeComponent();
            this.accReceiptNote = new JERPData.Product.ExpressReceiptNotes();
            this.ReconciliationEntity = new JERPBiz.Product.ExpressReconciliationEntity();
            this.printhelper = new JERPBiz.Product.ExpressReceiptPrintHelper();
            this.accReconciliation = new JERPData.Product.ExpressReconciliations();
            this.accReceivableAccount = new JERPData.Finance.ExpressReceivableAccount();
            this.accCashAccount = new JERPData.Finance.CashAccount();
            this.accBankAccount = new JERPData.Finance.BankAccount();
            this.accSaleReceiptNotes = new JERPData.Product.SaleReceiptNotes();
            this.dgrdvReceipt .AutoGenerateColumns = false;
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.txtSettleAMT.TextChanged += new EventHandler(txtSettleAMT_TextChanged);
        }

        private JERPData.Product.ExpressReceiptNotes accReceiptNote;
        private JERPBiz.Product.ExpressReconciliationEntity ReconciliationEntity;
        private JERPBiz.Product.ExpressReceiptPrintHelper printhelper;
        private JERPData.Product.ExpressReconciliations accReconciliation;
        private JERPData.Finance .ExpressReceivableAccount  accReceivableAccount;
        private JERPData.Product.SaleReceiptNotes accSaleReceiptNotes;

        private JERPData.Finance.CashAccount accCashAccount;
        private JERPData.Finance.BankAccount accBankAccount;
        private DataTable dtblReceiptNotes;
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
        public void ReceiptOper(long ReconciliationID)
        {
            this.ReconciliationID = ReconciliationID;
            this.txtReceiptCode.Text = string.Empty;
            this.ReconciliationEntity.LoadData(ReconciliationID);
            this.txtReconciliationCode.Text = this.ReconciliationEntity.ReconciliationCode;
            this.txtReconciliationName.Text = this.ReconciliationEntity.ReconciliationName;
            this.txtYear.Text = this.ReconciliationEntity.Year.ToString();
            this.txtMonth.Text = this.ReconciliationEntity.Month.ToString();
            this.txtCompanyAbbName.Text = this.ReconciliationEntity.CompanyName;
        
            this.txtMoneyTypeName.Text = this.ReconciliationEntity.MoneyTypeName;
            this.txtDateNote.Text  = this.ReconciliationEntity.DateNote .ToShortDateString ();
            decimal NonFinishedAMT = this.ReconciliationEntity.NonFinishedAMT;
            this.txtNonFinishedAMT.Text = string.Format("{0:0.####}", NonFinishedAMT);
            this.txtSettleAMT.Text = string.Format("{0:0.####}", NonFinishedAMT);
            this.dtblReceiptNotes  = this.accSaleReceiptNotes.GetDataSaleReceiptNotesByExpressReconciliationID (this.ReconciliationID).Tables[0];
            this.dgrdvReceipt.DataSource = this.dtblReceiptNotes;
        }
        void txtSettleAMT_TextChanged(object sender, EventArgs e)
        {
            decimal NonFinishedAMT = decimal.Parse(this.txtNonFinishedAMT.Text);
            decimal SettleAMT;
            if ((!decimal.TryParse(this.txtSettleAMT.Text, out SettleAMT)) || (SettleAMT > NonFinishedAMT))
            {
                MessageBox.Show("�����ʽ����ȷ���������δ����!");
                this.txtSettleAMT.Text = string.Format("{0:0.#####}", NonFinishedAMT);
            }
        }
   
        private bool ValidateData()
        {
         
            decimal SettleAMT;
            if (!decimal.TryParse(this.txtSettleAMT.Text, out SettleAMT))
            {
                MessageBox.Show("�Բ��𣬽�������������!");
                return false;
            }

            return true;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;
          
            DialogResult rlt = MessageBox.Show("�㽫�����վݿ�ȡ��һ�����治�ܱ����\n�ǣ�ȷ�ϣ���ȡ��!", "����ȷ��",
     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.No) return;
            string errormsg = string.Empty;
            object objNoteID = DBNull.Value;
            object objNoteCode = DBNull.Value;
            decimal SettleAMT = decimal.Parse(this.txtSettleAMT.Text);
        
            bool flag=false;
            if (this.txtReceiptCode.Text.Length > 0)
            {
                objNoteCode = this.txtReceiptCode.Text;
            }
            bool printflag = (txtReceiptCode.Text.Length == 0);
            int BankID = -1;
            if (SettleAMT > 0)
            {
               BankID= JERPApp.Define.Finance.FrmSettleAMTType.GetBankID();
            }
            flag = this.accReceiptNote.InsertExpressReceiptNotes  (ref errormsg,
                ref objNoteID,
                 ref objNoteCode ,
                 DateTime.Now.Date,
                 this.ReconciliationID,
                 SettleAMT,
                 BankID ,
                 JERPBiz.Frame.UserBiz.PsnID);
            if (flag)
            {
                this.txtReceiptCode.Text = objNoteCode.ToString();
            }
            
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }
            string ReceiptCode = objNoteCode.ToString();
            //���ö��˵��������
            this.accReconciliation.UpdateExpressReconciliationsForAppendFinishedAMT(
                ref errormsg,
                this.ReconciliationID,
                SettleAMT); 
            if (SettleAMT > 0)
            {
              
                //�����Լ��յĿ����Ǵ���,����Ǵ��յĻ�,�ݶ����ݽ��
                if (BankID == -1)
                {
                    //�ֽ���
                    this.accCashAccount.InsertCashAccountForDebit(ref errormsg,
                        "��ȡ������˾[" + this.ReconciliationEntity .CompanyName + "]�վ�[" +
                        ReceiptCode  + "]���ջ���",
                        this.ReconciliationEntity.MoneyTypeID,
                        SettleAMT,
                        JERPBiz.Frame.UserBiz.PsnID);
                }
                else
                {

                    //���д��
                    this.accBankAccount.InsertBankAccountForDebit(ref errormsg,
                             "��ȡ������˾[" + this.ReconciliationEntity.CompanyName + "]�վ�[" +
                        ReceiptCode + "]���ջ���",
                        BankID,
                        this.ReconciliationEntity.MoneyTypeID,
                        SettleAMT,
                        JERPBiz.Frame.UserBiz.PsnID);
                }
                //�����˿�
                this.accReceivableAccount .InsertExpressReceivableAccountForCredit(
                    ref errormsg,
                   "��ȡ������˾[" + this.ReconciliationEntity.CompanyName + "]�վ�[" +
                        ReceiptCode + "]���ջ���",
                    this.ReconciliationEntity.CompanyID,
                    this.ReconciliationEntity.MoneyTypeID,
                    SettleAMT,
                    JERPBiz.Frame.UserBiz.PsnID);
            } 
            MessageBox.Show("�ɹ������վݿ�ȡ!");
            if (printflag)
            {
                rlt = MessageBox.Show("��Ҫ���е���ӡ��?", "��ӡȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rlt == DialogResult.Yes)
                {
                    this.printhelper.ExportToExcel((long)objNoteID);
                }
            }
            if (this.affterSave != null) this.affterSave();
            this.Close();
        }
    }
}
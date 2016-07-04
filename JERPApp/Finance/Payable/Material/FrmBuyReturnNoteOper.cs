using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.Material
{
    public partial class FrmBuyReturnNoteOper : Form
    {
        public FrmBuyReturnNoteOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accNotes = new JERPData.Material.BuyReturnNotes();
            this.accItems = new JERPData.Material.BuyReturnItems();
            this.NoteEntity = new JERPBiz.Material.BuyReturnNoteEntity();
            this.accCashAccount = new JERPData.Finance.CashAccount();
            this.accBankAccount = new JERPData.Finance.BankAccount();
            this.accPayableAccount = new JERPData.Finance.PayableAccount();
            this.accRevenueAccount = new JERPData.Finance.RevenueAccount();
            this.MoneyTypeParm = new JERPBiz.Finance.MoneyTypeParm();      
            this.btnSave.Click += new EventHandler(btnSave_Click);

        }
        private JERPData.Material.BuyReturnNotes accNotes;
        private JERPData.Material.BuyReturnItems accItems;
        private JERPBiz.Material.BuyReturnNoteEntity NoteEntity;
        private JERPData.Finance.CashAccount accCashAccount;
        private JERPData.Finance.BankAccount accBankAccount;
        private JERPData.Finance.PayableAccount accPayableAccount;
        private JERPData.Finance.RevenueAccount accRevenueAccount;
        private JERPBiz.Finance.MoneyTypeParm MoneyTypeParm;
        private DataTable dtblItems;     
        private long noteid = -1;
        private long NoteID
        {
            get
            {
                return this.noteid;
            }
            set
            {
                this.noteid = value;
            }
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
        public  void ConfirmOper(long NoteID)
        {
            this.NoteID = NoteID;
            this.NoteEntity.LoadData(NoteID);
            this.txtNoteCode.Text = this.NoteEntity.NoteCode;
            this.txtCompanyAbbName.Text = this.NoteEntity.CompanyAbbName;
            this.txtMoneyTypeName.Text = this.NoteEntity.MoneyTypeName;
            this.txtDateNote.Text = this.NoteEntity.DateNote.ToShortDateString();
            this.txtMakerPsn.Text = this.NoteEntity.MakerPsn;
            this.rchMemo.Text = this.NoteEntity.Memo;
            this.txtTotalAMT.Text = string.Format("{0:0.####}", this.NoteEntity.TotalAMT);
                  
            this.dtblItems = this.accItems.GetDataBuyReturnItemsByNoteID(this.NoteID).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;

        }
        
        void btnSave_Click(object sender, EventArgs e)
        {
            string errormsg = string.Empty;
            bool flag = false;
         
            flag=this.accNotes.UpdateBuyReturnNotesForFinance (ref errormsg,
                this.NoteID,
                this.radCashFlag .Checked ,
                JERPBiz.Frame.UserBiz.PsnID);
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }

            if (this.radCashFlag.Checked)
            { 
                int BankID = JERPApp.Define.Finance.FrmSettleAMTType.GetBankID();
                if (BankID == -1)
                {
                    //�ֽ�
                    this.accCashAccount.InsertCashAccountForDebit (
                        ref errormsg,
                        "ԭ�ϲɹ��˻���[" + this.NoteEntity.NoteCode + "]�˿�",
                        this.NoteEntity.MoneyTypeID,
                        this.NoteEntity.TotalAMT,
                        JERPBiz.Frame.UserBiz.PsnID);
                }
                else
                {
                    //���д��
                    this.accBankAccount.InsertBankAccountForDebit  (
                          ref errormsg,
                          "ԭ�ϲɹ��˻���[" + this.NoteEntity.NoteCode + "]�˿�",
                          BankID,
                          this.NoteEntity.MoneyTypeID,
                          this.NoteEntity.TotalAMT,
                          JERPBiz.Frame.UserBiz.PsnID);
                }
                foreach (DataRow drow in this.dtblItems.Rows)
                {
                    this.accItems.UpdateBuyReturnItemsForReconciliation(
                        ref errormsg,
                        drow["ItemID"],
                        -1);
                }
            }
            else
            {
                //Ӧ���˿�
                this.accPayableAccount.InsertPayableAccountForDebit(
                    ref errormsg,
                    "ԭ�ϲɹ��˻���[" + this.NoteEntity.NoteCode + "]���",
                    this.NoteEntity.CompanyID,
                    this.NoteEntity.MoneyTypeID,
                    this.NoteEntity.TotalAMT,
                    JERPBiz.Frame.UserBiz.PsnID);
            }
            //Ū�ɿۿ�����
            this.accRevenueAccount.InsertRevenueAccountForCredit(
                     ref errormsg,
                      "ԭ�ϲɹ��˻���[" + this.txtNoteCode.Text + "]",
                      JERPBiz.Finance.RevenueTypeParm.FineRevenue,
                     this.NoteEntity.TotalAMT * this.MoneyTypeParm.GetExchangeRate(this.NoteEntity.MoneyTypeID),
                     JERPBiz.Frame.UserBiz.PsnID);
            MessageBox.Show("�ɹ���ɵ�ǰ�˻�������");
           
            if (this.affterSave != null) this.affterSave();
            this.Close();
           
        }
    }
}
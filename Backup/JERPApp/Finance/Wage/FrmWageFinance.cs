using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Wage
{
    public partial class FrmWageFinance : Form
    {
        public FrmWageFinance()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accItems = new JERPData.Finance.WageItems();
            this.accNotes = new JERPData.Finance.WageNotes();
            this.NoteEntity = new JERPBiz.Finance.WageNoteEntity();
            this.accWageType = new JERPData.Finance.OtherWageType();
            this.accCashAccount = new JERPData.Finance.CashAccount();
            this.accBankAccount = new JERPData.Finance.BankAccount();
            this.accExpenseAccount = new JERPData.Finance.ExpenseAccount();
            this.MoneyTypeParm = new JERPBiz.Finance.MoneyTypeParm();
            this.SetPermit();
        }        
        private JERPData.Finance.WageNotes accNotes;
        private JERPData.Finance.WageItems accItems;
        private JERPBiz.Finance.WageNoteEntity NoteEntity;
        private JERPData.Finance.OtherWageType accWageType;
        private JERPData.Finance.CashAccount accCashAccount;
        private JERPData.Finance.BankAccount accBankAccount;
        private JERPData.Finance.ExpenseAccount accExpenseAccount;
        private JERPBiz.Finance.MoneyTypeParm MoneyTypeParm;
        private DataTable dtblItems,dtblWageType;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����     
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(621);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(622);
            if (this.enableBrowse)
            {
                this.CreateColumn();
                this.LoadData();
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
            }
            this.ColumnbtnFinance.Visible = this.enableSave;     
            if (this.enableSave)
            {
                this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
            }
        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
      
      
        private void CreateColumn()
        {
            this.dtblWageType = this.accWageType.GetDataOtherWageType().Tables[0];
            DataGridViewTextBoxColumn txtcol;          
            foreach (DataRow drow in this.dtblWageType.Rows)
            {
                txtcol = new DataGridViewTextBoxColumn();
                txtcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                txtcol.DataPropertyName = drow["WageTypeName"].ToString();
                txtcol.HeaderText = drow["WageTypeName"].ToString();
                txtcol.DefaultCellStyle.Format = "0.#####"; 
                this.dgrdv.Columns.Add(txtcol);
            }
            txtcol = new DataGridViewTextBoxColumn();
            txtcol.Width = 60;
            txtcol.DataPropertyName = "TotalWage";
            txtcol.HeaderText = "�ϼ�";
            txtcol.DefaultCellStyle.Format  = "0.#####";      
            this.dgrdv.Columns.Add(txtcol);
            this.ctrlQFind.SeachGridView = this.dgrdv;
        }
       
        public void LoadData()
        {  
            this.dtblItems = this.accItems.GetDataWageItemsPivotWageTypeForFinance().Tables[0];         
            this.dgrdv.DataSource = this.dtblItems;
           
        }
        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnbtnFinance.Name)
            {
                DialogResult rut = MessageBox.Show("������ǰԱ�����й��ʽ���,��ע����㷽ʽ��ȷ�Ϸ�?", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rut == DialogResult.No) return;
                DataRow drow = this.dtblItems.DefaultView[irow].Row;
                int BankID = JERPApp.Define.Finance.FrmSettleAMTType.GetBankID();
                int MainMoneyTypeID = this.MoneyTypeParm.GetMainMoneyTypeID();
                string errormsg=string.Empty ;
                this.accItems.UpdateWageItemsForFinance(ref errormsg,
                    drow["ItemID"],
                    BankID,
                    JERPBiz.Frame.UserBiz.PsnID);
                if (BankID == -1)
                {
                    //�ֽ���
                    this.accCashAccount.InsertCashAccountForCredit(
                        ref errormsg,
                        "֧��Ա��[" + drow["PsnName"].ToString () + "]����",
                        MainMoneyTypeID,
                        drow["TotalWage"],
                        JERPBiz.Frame.UserBiz.PsnID);
                }
                else
                {
                    //���д��
                    this.accBankAccount.InsertBankAccountForCredit(
                        ref errormsg,
                        "֧��Ա��[" + drow["PsnName"].ToString() + "]����",
                        BankID,
                        MainMoneyTypeID,
                        drow["TotalWage"],
                        JERPBiz.Frame.UserBiz.PsnID);
                }
                //Ա��н��
                this.accExpenseAccount.InsertExpenseAccountForDebit(
                    ref errormsg,
                   "֧��Ա��[" + drow["PsnName"].ToString() + "]����",
                    JERPBiz.Finance.ExpenseTypeParm.WageExpense,
                   drow["TotalWage"],
                    JERPBiz.Frame.UserBiz.PsnID);
     
                MessageBox.Show("�ɹ����浱ǰ�ͻ���");
                this.dtblItems.Rows.Remove(drow);
            }
        }
      
    
    
      
    }
}
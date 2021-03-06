using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Report
{
    public partial class FrmBankAccountRecord : Form
    {
        public FrmBankAccountRecord()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accBankAccount = new JERPData.Finance.BankAccount();
            this.dtpDateBegin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                   1);
            this.dtpDateEnd.Value = this.dtpDateBegin.Value.AddMonths(1).AddDays(-1);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnExport.Click += new EventHandler(btnExport_Click);

        }

        private JERPData.Finance.BankAccount  accBankAccount;
        private DataTable dtblRecords;
      
        public void LoadRecord(int BankID,int MoneyTypeID)
        {
            this.ctrlBankID.BankID = BankID;
            this.ctrlMoneyTypeID.MoneyTypeID = MoneyTypeID;
            this.LoadData();
        }
        private void LoadData()
        {
            this.dtblRecords = this.accBankAccount.GetDataBankAccountRecord(
                 this.ctrlBankID.BankID ,
                 this.ctrlMoneyTypeID.MoneyTypeID,
                 this.dtpDateBegin.Value,
                 this.dtpDateEnd.Value).Tables[0];
            this.dgrdv.DataSource = this.dtblRecords;
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();

        }    

        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("正在生成打印文档，请稍候......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1","银行存款日记账记录");
            excel.SetCellVal("A2", "起止时间:"+this.dtpDateBegin .Value .ToString ()
                +" - "+this.dtpDateEnd .Value .ToString ()
                +" 币种:"+this.ctrlMoneyTypeID .MoneyTypeName );
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(this.dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.SetRangeAutoFit(3, 1, rowIndex, rowIndex, true, false);
            excel.Show();
            FrmMsg.Hide();
        }
    }
}
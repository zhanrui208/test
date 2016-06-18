using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Report
{
    public partial class FrmExpressPayableAccountRecord : Form
    {
        public FrmExpressPayableAccountRecord()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accAccount = new JERPData.Finance.ExpressPayableAccount();
            this.dtpDateBegin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
             1);
            this.dtpDateEnd.Value = this.dtpDateBegin.Value.AddMonths(1).AddDays(-1);
            this.btnExport.Click += new EventHandler(btnExport_Click);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);

        }

        private JERPData.Finance.ExpressPayableAccount accAccount;
        private DataTable dtblRecords;
        public void LoadRecord(int CompanyID, int MoneyTypeID)
        {
            this.ctrlExpressID.CompanyID = CompanyID;
            this.ctrlMoneyTypeID.MoneyTypeID = MoneyTypeID;
            this.LoadData();
        }
        private void LoadData()
        {
            this.dtblRecords = this.accAccount.GetDataExpressPayableAccountRecord(this.ctrlExpressID.CompanyID,
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
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1","Ӧ���˿��¼");
            excel.SetCellVal("A2", "��ֹʱ��:"+this.dtpDateBegin .Value .ToString ()
                +" - "+this.dtpDateEnd .Value .ToString ()
                +" ������:"+this.ctrlExpressID .CompanyName
                +" ����:"+this.ctrlMoneyTypeID .MoneyTypeName );
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
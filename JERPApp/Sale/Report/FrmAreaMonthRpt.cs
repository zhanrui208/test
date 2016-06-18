using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Sale.Report
{
    public partial class FrmAreaMonthRpt : Form
    {
        public FrmAreaMonthRpt()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accDeliverItems = new JERPData.Product.SaleDeliverItems();
            this.SetPermit();
        }
        private JERPData.Product.SaleDeliverItems accDeliverItems;
        private DataTable dtblItems;
        //Ȩ����
        private bool enableBrowse = false;//���
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(236);
            if (this.enableBrowse)
            {
                this.ctrlQFind.SeachGridView = this.dgrdv;
                this.ctrlSpot.SeachGridView = this.dgrdv;
                this.ctrlYear.Year = DateTime.Now.Year;
                //��������               
                this.ctrlYear.OnSelected += this.LoadData;
                this.ctrlMoneyTypeID.AffterSelected += this.LoadData;
                this.LoadData();
                this.btnExplore.Click += new EventHandler(btnExplore_Click);                
            }
        }

   
        private void LoadData()
        {
            this.dtblItems = this.accDeliverItems.GetDataSaleDeliverItemsAreaAMTPivotMonth (this.ctrlYear.Year,
                this.ctrlMoneyTypeID .MoneyTypeID ).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;          
            DataRow drowNew;
            string exp = string.Empty;
            if (dtblItems.Rows.Count > 0)
            {
                drowNew = this.dtblItems.NewRow();              
                drowNew["AreaName"] = "�ϼ�";
                for (int m = 1; m < 13; m++)
                {
                    drowNew[m.ToString()] = this.dtblItems.Compute("SUM([" + m.ToString() + "])","");
                    exp += "+ISNULL([" + m.ToString() + "],0)";
                }
                this.dtblItems.Rows.Add(drowNew);
            }
            this.dtblItems.Columns.Add("TotalAMT", typeof(decimal));
        }
        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "�������۱���");
            excel.SetCellVal("A2", "���:" + this.ctrlYear .Year .ToString ());
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            FrmMsg.Hide();
            excel.Show();
        }
    }
}
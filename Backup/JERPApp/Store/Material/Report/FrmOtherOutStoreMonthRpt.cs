using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Material.Report
{
    public partial class FrmOtherOutStoreMonthRpt : Form
    {
        public FrmOtherOutStoreMonthRpt()
        {
            InitializeComponent();
            this.accOutStoreItems = new JERPData.Material.OtherOutStoreItems();
            this.SetPermit();
        }
        private JERPData.Material.OtherOutStoreItems accOutStoreItems;
        private DataTable dtblRpt;
        //Ȩ����
        private bool enableBrowse = false;//���
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(331);
            if (this.enableBrowse)
            {
                this.ctrlYear.Year = DateTime.Now.Year;
                this.ctrlYear.OnSelected += this.LoadData;
                this.ctrlDeptID.AffterSelected += this.LoadData;
                this.CreateGridColumn();
                this.ctrlQFind.SeachGridView = this.dgrdv;
                this.LoadData();
                this.btnExport .Click += new EventHandler(btnExport_Click);               
            }
        } 
        private void CreateGridColumn()
        {
            this.dgrdv.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn txtcolumn;
            txtcolumn = new DataGridViewTextBoxColumn();
            txtcolumn.DataPropertyName = "PrdCode";
            txtcolumn.HeaderText = "���ϱ��";
            txtcolumn.Width = 100;
            txtcolumn.Frozen = true;
            this.dgrdv.Columns.Add(txtcolumn);

            txtcolumn = new DataGridViewTextBoxColumn();
            txtcolumn.DataPropertyName = "PrdName";
            txtcolumn.HeaderText = "��������";
            txtcolumn.Width = 80;
            txtcolumn.Frozen = true;
            this.dgrdv.Columns.Add(txtcolumn);
         
            txtcolumn = new DataGridViewTextBoxColumn();
            txtcolumn.DataPropertyName = "PrdName";
            txtcolumn.HeaderText = "���Ϲ��";
            txtcolumn.Width = 100;
            txtcolumn.Frozen = true;
            this.dgrdv.Columns.Add(txtcolumn);

            txtcolumn = new DataGridViewTextBoxColumn();
            txtcolumn.DataPropertyName = "Model";
            txtcolumn.HeaderText = "����";
            txtcolumn.Width = 66;
            txtcolumn.Frozen = true;
            this.dgrdv.Columns.Add(txtcolumn);
       
            txtcolumn = new DataGridViewTextBoxColumn();
            txtcolumn.DataPropertyName = "Manufacturer";
            txtcolumn.HeaderText = "Ʒ��";
            txtcolumn.Frozen = true;
            txtcolumn.Width = 66;
            this.dgrdv.Columns.Add(txtcolumn);

            for (int j = 1; j < 13; j++)
            {
                txtcolumn = new DataGridViewTextBoxColumn();
                txtcolumn.DataPropertyName = j.ToString();
                txtcolumn.HeaderText = j.ToString ()+"��";
                txtcolumn.Width = 66;
                this.dgrdv.Columns.Add(txtcolumn);
            }
            txtcolumn = new DataGridViewTextBoxColumn();
            txtcolumn.DataPropertyName = "Total";
            txtcolumn.HeaderText = "�ϼ�";
            txtcolumn.Width = 70;
            this.dgrdv.Columns.Add(txtcolumn);
        }
        private void LoadData()
        {
            string totalexp = "";
            for (int j = 1; j < 13; j++)
            {
                totalexp += "+ISNULL([" + j.ToString() + "],0)";
            }
          
            this.dtblRpt = this.accOutStoreItems.GetDataOtherOutStoreItemsPivotMonth  (this.ctrlYear.Year,this.ctrlDeptID .DeptID ).Tables[0];
            this.dtblRpt.Columns.Add("Total", typeof(decimal), totalexp); 
            if (this.dtblRpt.Rows.Count > 0)
            {
                DataRow drowNew = this.dtblRpt.NewRow();
                drowNew["Manufacturer"] = "�ϼ�";
                for (int j = 1; j < 13; j++)
                {
                    drowNew[j.ToString()] = this.dtblRpt.Compute("SUM([" + j.ToString() + "])", "");
                }
                this.dtblRpt.Rows.Add(drowNew);
            }
            this.dgrdv.DataSource = this.dtblRpt;
        }
     


        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "���������±�");        
            excel.SetCellVal("A2", "���:" + this.ctrlYear.Year .ToString ()+" ����:"+this.ctrlDeptID .DeptName );
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(this.dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            FrmMsg.Hide();
            excel.Show();
        }
    }
}
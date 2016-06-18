using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Supply.OutSrc.Report
{
    public partial class FrmPrdNonFinished : Form
    {
        public FrmPrdNonFinished()
        {
            InitializeComponent();
            this.accOrderItems = new JERPData.Manufacture.OutSrcOrderItems();
            this.SetPermit();
        }

        private JERPData.Manufacture.OutSrcOrderItems accOrderItems;
        private DataTable dtblItems;

        //Ȩ����
        private bool enableBrowse = false;//���
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(208);
            if (this.enableBrowse)
            {
                this.LoadData();
                this.dgrdv.ContextMenuStrip = this.cMenu; ;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.btnExplore.Click += new EventHandler(btnExplore_Click);
            }
        }

        private void LoadData()
        {
            this.dtblItems = this.accOrderItems.GetDataOutSrcOrderItemsPrdNonFinishedQtyPivotCompany ().Tables[0];
            while (this.dgrdv.ColumnCount > 9)
            {
                this.dgrdv.Columns.RemoveAt(9);
            }
            this.dgrdv.DataSource = this.dtblItems;
            this.ctrlGridQFind.SeachGridView = this.dgrdv;
            this.ctrlSpot.SeachGridView = this.dgrdv;
        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "ί��Ƿ��һ��");           
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.SetRangeAutoFit(3, 1, rowIndex, colIndex, true, true);
            FrmMsg.Hide();
            excel.Show();
        }
    }
}
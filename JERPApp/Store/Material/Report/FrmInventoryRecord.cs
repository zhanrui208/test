using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Material.Report
{
    public partial class FrmInventoryRecord : Form
    {
        public FrmInventoryRecord()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlGridQuickQFind.SeachGridView = this.dgrdv;
            this.accStore = new JERPData.Material.Store();
            this.dtpDateBegin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
            this.dtpDateEnd.Value = DateTime.Now;
            this.ctrlPrdID.AffterSelected += this.LoadData;
            this.dtpDateBegin.ValueChanged += new EventHandler(dtpDateBegin_ValueChanged);
            this.dtpDateEnd.ValueChanged += new EventHandler(dtpDateEnd_ValueChanged);
            this.btnExplore.Click += new EventHandler(btnExplore_Click);
        }

       
        void dtpDateEnd_ValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        void dtpDateBegin_ValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }      
        private JERPData.Material.Store accStore;
        private DataTable dtblStore;
        private JERPApp.Store.Material.Report.Bill.FrmBizBill frmBill;
        public void StoreRecord(int PrdID,int BranchStoreID,DateTime DateSpot)
        {
            this.dtpDateEnd.Value = DateSpot;
            this.ctrlPrdID.PrdID = PrdID;
            this.ctrlBranchStoreID.BranchStoreID = BranchStoreID;
            this.LoadData();
        }
        private void LoadData()
        {
            this.dtblStore = this.accStore.GetDataStoreRecord(this.ctrlPrdID .PrdID,
                this.ctrlBranchStoreID .BranchStoreID,
             this.dtpDateBegin.Value, this.dtpDateEnd.Value).Tables [0];
            this.dgrdv.DataSource = this.dtblStore;
        }
        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnNoteName .Name)
            {
                DataRow drow = this.dtblStore.DefaultView[irow].Row;
                frmBill = JERPApp.Store.Material.Report.Bill.FrmBizBill.Create((int)drow["NoteNameID"]);
                new FrmStyle(frmBill).SetPopFrmStyle(this);
                frmBill.DetailNote((long)drow["NoteID"]);
                frmBill.ShowDialog();
            }
        }
        void btnExplore_Click(object sender, EventArgs e)
        {
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "���Ͽ���¼");
            excel.SetCellVal("A2", "ԭ�ϱ��:" + this.ctrlPrdID .PrdEntity .PrdCode 
                +" ԭ������:"+this.ctrlPrdID .PrdEntity .PrdName +" ԭ�Ϲ��:"+this.ctrlPrdID .PrdEntity .PrdSpec );
            excel.SetCellVal("A3", "��ʼ����:" +this.dtpDateBegin.Value .ToString ()
               + " ��ֹ����:" +this.dtpDateEnd.Value.ToString() + "  ��λ:" +this.ctrlBranchStoreID .BranchStoreName);
            int rowIndex = 4;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.Show();
        }

    }
}
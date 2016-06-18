using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.MaterialOEM.Report
{
    public partial class FrmSafeStore : Form
    {
        public FrmSafeStore()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accStore = new JERPData.Material.OEMStore();
            this.SetPermit();
        }
        private JERPData.Material .OEMStore accStore;
        private DataTable dtblInventory;
        //Ȩ����
        private bool enableBrowse = false;//���
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(666);
            if (this.enableBrowse)
            {
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                //��������
                LoadData();
                this.btnExplore.Click += new EventHandler(btnExplore_Click);
            }
        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            this.dtblInventory = this.accStore.GetDataOEMStoreSafeInventory  ().Tables[0];
            int customerid = -1;
            int lastcustomerid = -1;
            foreach (DataRow drow in this.dtblInventory.Rows)
            {
                customerid = (int)drow["CustomerID"];
                if (customerid == lastcustomerid)
                {
                    drow["Customer"] = DBNull.Value;
                }
                lastcustomerid = customerid;
            }
            this.dgrdv.DataSource = this.dtblInventory;
        }
        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "�͹����ϰ�ȫ��汨��");
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            FrmMsg.Hide();
            excel.Show();
        }


    }
}
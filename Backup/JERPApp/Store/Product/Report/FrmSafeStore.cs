using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Product.Report
{
    public partial class FrmSafeStore : Form
    {
        public FrmSafeStore()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accStore = new JERPData.Product.Store();
            this.SetPermit();
        }
        private JERPData.Product.Store accStore;
        private DataTable dtblInventory;
        //Ȩ����
        private bool enableBrowse = false;//���
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(195);
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
            this.dtblInventory = this.accStore.GetDataStoreSafeInventory  ().Tables[0];            
            this.dgrdv.DataSource = this.dtblInventory;
        }
        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "��Ʒ��ȫ��汨��");
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            FrmMsg.Hide();
            excel.Show();
        }


    }
}
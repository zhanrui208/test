using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Product
{
    public partial class FrmRoadStoreReserveCheck : Form
    {
        public FrmRoadStoreReserveCheck()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accStoreReserve = new JERPData.Product.RoadStoreReserve ();
            this.SetPermit();
        }
        private JERPData.Product.RoadStoreReserve  accStoreReserve;
        private DataTable dtblReserve;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(772);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(773);
            if (this.enableBrowse)
            {
                this.LoadData();
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);              
            }           
            this.btnSave.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.btnSave.Click += new EventHandler(btnSave_Click);
            }
        }
        private void LoadData()
        {
            this.dtblReserve = this.accStoreReserve.GetDataRoadStoreReserve ().Tables[0];
            this.dgrdv.DataSource = this.dtblReserve;
        }
        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        } 
        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
             
                grow.ErrorText = string.Empty;
                object objQuantity = grow.Cells[this.ColumnQuantity.Name].Value;
                if (objQuantity == DBNull.Value) continue;
                object objInventoryQty = grow.Cells[this.ColumnInventoryQty.Name].Value;
                if (objInventoryQty == DBNull.Value) continue;
                if ((decimal)objQuantity > (decimal)objInventoryQty)
                {
                    grow.ErrorText = "Ԥ�������ܴ�����Ч��;��";
                }

            }
        }
 
        private bool ValidateData()
        {
            DataRow[]  drows = this.dtblReserve.Select("Quantity<0", "", DataViewRowState.CurrentRows);
            if (drows.Length > 0)
            {
                MessageBox.Show("Ԥ��������<0");
                return false;
            }
            drows = this.dtblReserve.Select("InventoryQty<Quantity", "", DataViewRowState.CurrentRows);
            if (drows.Length> 0)
            {
                MessageBox.Show("Ԥ�������ܴ�����Ч��;��");
                return false;
            }
            return true;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            DialogResult rsl = MessageBox.Show("�����б��沢���ˣ�\n��ע�����ݵ�׼ȷ?",
             "ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rsl == DialogResult.No) return;
            string errormsg=string.Empty ;
            foreach (DataRow drow in this.dtblReserve.Rows)
            {
                if (drow.RowState == DataRowState.Unchanged) continue;
                decimal Quantity = 0;
                if (drow["Quantity"] != DBNull.Value)
                {
                    Quantity = (decimal)drow["Quantity"];
                }
                this.accStoreReserve.SaveRoadStoreReserveForCheck (ref errormsg,
                    drow["PrdID"],
                    Quantity);
                drow.AcceptChanges();

            }
            MessageBox.Show("�ɹ����浱ǰԤ��");
            
        }
        
    }
}
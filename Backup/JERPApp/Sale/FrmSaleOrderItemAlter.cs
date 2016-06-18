using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Sale
{
    public partial class FrmSaleOrderItemAlter : Form
    {
        public FrmSaleOrderItemAlter()
        {
            InitializeComponent();
            this.accItems = new JERPData.Product.SaleOrderItems();
            this.dgrdv.AutoGenerateColumns = false;
            this.SetPermit();
        }
        private JERPData.Product.SaleOrderItems accItems;
        private JERPApp.Define.Product .FrmSaleOrderItemSearchItem frmSearch = null;
        private DataTable dtblItems;
        private string whereclause = string.Empty;
        private string initwherelause = string.Empty;
        private void LoadData()
        {
        
            this.dtblItems = this.accItems.GetDataSaleOrderItemsFreeSearch   (
                this.whereclause ).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
        }
        private bool enableBrowse = false;       
        private bool enableEdit = false;
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(59);
            this.enableEdit = JERPBiz.Frame.PermitHelper.EnableFunction(60);
            this.lnkSearch.Enabled = this.enableEdit;
            if (this.enableEdit)
            {
                this.initwherelause  = " and (MakerPsnID=" + JERPBiz.Frame.UserBiz.PsnID.ToString() + ")";
                this.whereclause = this.initwherelause ;
                this.ctrlQFind.SetSearchElement(this.dgrdv);
                this.lnkSearch.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkSearch_LinkClicked);
                this.btnSave.Click += new EventHandler(btnSave_Click);
                this.btnExport.Click += new EventHandler(btnExport_Click);
                this.FormClosed += new FormClosedEventHandler(FrmSaleOrderItemUpdate_FormClosed);
                this.txtPONo.KeyDown += new KeyEventHandler(txtPONo_KeyDown);
            }
          
        }

    
        void txtPONo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==Keys .Enter)
            {
                this.whereclause = this.initwherelause  +" and (PONo like '" + this.txtPONo.Text + "%')";
                this.LoadData();
            }
        }

       
        public delegate void AffterSaveDelegate();
        private AffterSaveDelegate affterSave;
        public event AffterSaveDelegate AffterSave
        {
            add
            {
                affterSave += value;
            }
            remove
            {
                affterSave -= value;
            }
        }

        void FrmSaleOrderItemUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.affterSave != null) this.affterSave();
        }
        void lnkSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmSearch == null)
            {
                frmSearch = new JERPApp.Define.Product .FrmSaleOrderItemSearchItem();
                new FrmStyle(frmSearch).SetPopFrmStyle(this);
                frmSearch.AffterSearch +=new JERPApp.Define.Product.FrmSaleOrderItemSearchItem.AffterSearchDelegate(frmSearch_AffterSearch);
            }
            frmSearch.ShowDialog();
        }

        void frmSearch_AffterSearch(string whereclasue)
        {
            this.whereclause =this.initwherelause + whereclasue;
            this.LoadData();
        }

     
        void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataRow drow in this.dtblItems.Select("", "", DataViewRowState.CurrentRows))
            {
                if (drow.RowState == DataRowState.Unchanged) continue;
                bool flag = false;
                string errormsg = string.Empty;
                flag = this.accItems.UpdateSaleOrderItemsAdjustNoFinishedQty (
                        ref errormsg,
                        drow["ItemID"],
                        drow["NonFinishedQty"]);
                flag = this.accItems.UpdateSaleOrderItemsForPrice(ref errormsg,
                    drow["ItemID"],
                    drow["Price"]);
                if (!flag)
                {
                    MessageBox.Show(errormsg);
                }
            }
            MessageBox.Show("�ɹ����浱ǰ���");
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("C1", "P/O�е�����");
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(this.dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.SetRangeAutoFit(3, 1, rowIndex, colIndex, true, false);
            FrmMsg.Hide();
            excel.Show();
        }

    }
}
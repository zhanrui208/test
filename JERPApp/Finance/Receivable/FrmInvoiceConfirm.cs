using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Receivable
{
    public partial class FrmInvoiceConfirm : Form
    {
        public FrmInvoiceConfirm()
        {
            InitializeComponent(); 
            this.SetPermit();
        } 
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����  
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(124);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(125);
            if (this.enableBrowse)
            {

                this.ctrlRepairInvoiceConfirm.AffterRefresh += new CtrlRepairInvoiceConfirm.AffterRefreshDelegate(ctrlRepairInvoiceConfirm_AffterRefresh);
                this.ctrlSaleInvoiceConfirm.AffterRefresh += new CtrlSaleInvoiceConfirm.AffterRefreshDelegate(ctrlSaleInvoiceConfirm_AffterRefresh);
                this.LoadData();
                this.lnkRefreshAll.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkRefreshAll_LinkClicked);
            }
            this.ctrlSaleInvoiceConfirm.Enabled = this.enableSave;
            this.ctrlRepairInvoiceConfirm.Enabled = this.enableSave;
            
        }

        void lnkRefreshAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            this.ctrlRepairInvoiceConfirm.LoadData();
            this.ctrlSaleInvoiceConfirm.LoadData();
        }
        void ctrlSaleInvoiceConfirm_AffterRefresh(int count)
        {
            this.pageSale.Text = "���۷�Ʊ[" + count.ToString() + "]";
        }

        void ctrlRepairInvoiceConfirm_AffterRefresh(int count)
        {
            this.pageRepair.Text = "ά�޷�Ʊ[" + count.ToString() + "]";
        }

     
    }
}
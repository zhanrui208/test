using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.OutSrc
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
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(140);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(141);
            if (this.enableBrowse)
            {
                this.lnkRefreshAll.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkRefreshAll_LinkClicked);
                this.ctrlOutSrcInvoice.AffterRefresh += this.ctrlOutSrcInvoice_AffterRefresh;
                this.ctrlOutSrcLossSupplyInvoice.AffterRefresh +=ctrlOutSrcLossSupplyInvoice_AffterRefresh ;

                this.LoadData();
            }
            this.ctrlOutSrcInvoice.Enabled = this.enableSave;
            this.ctrlOutSrcLossSupplyInvoice.Enabled = this.enableSave;
        }

        void lnkRefreshAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.LoadData();
        }


        private void LoadData()
        {
            this.ctrlOutSrcInvoice.LoadData();
            this.ctrlOutSrcLossSupplyInvoice.LoadData();

        }
        void ctrlOutSrcInvoice_AffterRefresh(int count)
        {
            this.pageOutSrc.Text = "�ӹ���Ʊ[" + count.ToString() + "]";
        }
        void ctrlOutSrcLossSupplyInvoice_AffterRefresh(int count)
        {
            this.pageLossSupply.Text = "�����Ϸ�Ʊ[" + count.ToString() + "]";
        }

     
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.OutSrc
{
    public partial class FrmInvoice : Form
    {
        public FrmInvoice()
        {
            InitializeComponent(); 
            this.SetPermit();
        }  
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����  
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(138);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(139); 
            if (this.enableBrowse)
            {
                this.lnkRefreshAll.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkRefreshAll_LinkClicked);
                this.ctrlOutSrcInvoice.AffterRefresh += new CtrlOutSrcInvoice.AffterRefreshDelegate(ctrlOutSrcInvoice_AffterRefresh);
                this.ctrlOutSrcLossSupplyInvoice.AffterRefresh += new CtrlOutSrcLossSupplyInvoice.AffterRefreshDelegate(ctrlOutSrcLossSupplyInvoice_AffterRefresh);

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

        void ctrlOutSrcLossSupplyInvoice_AffterRefresh(int count)
        {
            this.pageLossSupply .Text = "�����Ϸ�Ʊ[" + count.ToString() + "]";
        }

        void ctrlOutSrcInvoice_AffterRefresh(int count)
        {
            this.pageOutSrc.Text = "�ӹ���Ʊ[" + count.ToString() + "]";
        }

    }
}
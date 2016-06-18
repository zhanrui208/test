using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.OutSrc
{
    public partial class FrmLossSupplyInvoiceConfirmOper : Form
    {
        public FrmLossSupplyInvoiceConfirmOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accInvoices = new JERPData.Material.OutSrcLossSupplyInvoices();
            this.accSupplyItems = new JERPData.Material.OutSrcLossSupplyItems();
            this.InvoiceEntity = new JERPBiz.Material.OutSrcLossSupplyInvoiceEntity();
            this.accPayableAccount = new JERPData.Finance.PayableAccount();
            this.accRevenueAccount = new JERPData.Finance.RevenueAccount();
            this.MoneyTypeParm = new JERPBiz.Finance.MoneyTypeParm();
            this.dgrdv.AutoGenerateColumns = false;
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

      
        private JERPData.Material .OutSrcLossSupplyInvoices accInvoices;
        private JERPData.Material.OutSrcLossSupplyItems accSupplyItems;
        private JERPBiz.Material.OutSrcLossSupplyInvoiceEntity InvoiceEntity;
        private JERPData.Finance.PayableAccount accPayableAccount;
        private JERPData.Finance.RevenueAccount accRevenueAccount;
        private JERPBiz.Finance.MoneyTypeParm MoneyTypeParm;
        private DataTable dtblItems;
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

        private long invoiceID = -1;
        private long InvoiceID
        {
            get
            {
                return this.invoiceID;
            }
            set
            {
                this.invoiceID = value;
                this.btnSave.Enabled = (value > -1);
            }
        }
        public void ConfirmOper(long InvoiceID)
        {
            this.InvoiceID = InvoiceID;
            this.InvoiceEntity.LoadData(InvoiceID);
            this.txtInvoiceCode.Text = this.InvoiceEntity.InvoiceCode;
            this.txtInvoiceName.Text = this.InvoiceEntity.InvoiceName;
            this.txtDateNote.Text = this.InvoiceEntity.DateNote.ToShortDateString();
            this.txtCompanyAbbName.Text = this.InvoiceEntity.CompanyAbbName; 
            this.txtMoneyTypeName.Text = this.InvoiceEntity.MoneyTypeName;
            this.txtTotalAMT.Text = string.Format("{0:0.####}", this.InvoiceEntity.TotalAMT); 
            this.txtYear.Text = this.InvoiceEntity.Year.ToString();
            this.txtMonth.Text = this.InvoiceEntity.Month.ToString();
            this.dtblItems = this.accSupplyItems.GetDataOutSrcLossSupplyItemsByInvoiceID (this.InvoiceID).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
        }
        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.btnSave.Enabled = true;
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                grow.ErrorText = string.Empty;
                if (grow.Cells[this.ColumnPrice.Name].Value == DBNull.Value )
                {
                    grow.ErrorText = "δ�赥��!";
                    this.btnSave.Enabled = false;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           
            DialogResult rul = MessageBox.Show("�����б�������һ��ȷ�ϲ��ܱ����", "���ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            bool flag = false;
            string errormsg = string.Empty;
            flag = this.accInvoices.UpdateOutSrcLossSupplyInvoicesForConfirm (ref errormsg,
                this.InvoiceID ,
                JERPBiz.Frame.UserBiz.PsnID);
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }
            decimal TotalAMT = this.InvoiceEntity.TotalAMT;
            decimal CostAMT = TotalAMT*this.MoneyTypeParm.GetExchangeRate(this.InvoiceEntity.MoneyTypeID);
            //���ﲻȥ��˰��
            if (TotalAMT > 0)
            {
                //Ӧ���˿�(��)
                this.accPayableAccount.InsertPayableAccountForDebit (
                         ref errormsg,
                        "��Ӧ��["+this.txtCompanyAbbName .Text +"]�����Ϸ�Ʊ[" + this.txtInvoiceCode .Text + "]",
                        this.InvoiceEntity .CompanyID,
                        this.InvoiceEntity.MoneyTypeID,
                        TotalAMT,
                        JERPBiz.Frame.UserBiz.PsnID);
            }            
            if(CostAMT >0)
            {
                //�ӹ�����
                this.accRevenueAccount.InsertRevenueAccountForCredit (ref errormsg,
                        "��Ӧ��[" + this.txtCompanyAbbName.Text + "]�����ϿۿƱ[" + this.txtInvoiceCode.Text + "]",
                        JERPBiz.Finance.RevenueTypeParm .FineRevenue ,
                        CostAMT,
                        JERPBiz.Frame.UserBiz.PsnID);
            }
            MessageBox.Show("�ɹ���˵�ǰί�ⳬ���Ϸ�Ʊ");
            if (this.affterSave != null) this.affterSave();
            this.Close();

        }

    }
}
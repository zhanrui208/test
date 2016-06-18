using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.Material
{
    public partial class FrmInvoiceConfirmOper : Form
    {
        public FrmInvoiceConfirmOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accInvoices = new JERPData.Material.BuyInvoices();
            this.accReceiveItems = new JERPData.Material.BuyReceiveItems();
            this.InvoiceEntity = new JERPBiz.Material.BuyInvoiceEntity();
            this.accPayableAccount = new JERPData.Finance.PayableAccount();
            this.MoneyTypeParm = new JERPBiz.Finance.MoneyTypeParm();
            this.accStore = new JERPData.Material.Store();
            this.accPrd = new JERPData.Product.Product(); 
            this.dgrdv.AutoGenerateColumns = false;
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

      
        private JERPData.Material.BuyInvoices accInvoices;
        private JERPData.Material.BuyReceiveItems accReceiveItems;
        private JERPBiz.Material.BuyInvoiceEntity InvoiceEntity;
        private JERPData.Finance.PayableAccount accPayableAccount;
        private JERPBiz.Finance.MoneyTypeParm MoneyTypeParm;
        private JERPData.Material.Store accStore;
        private JERPData.Product.Product accPrd;
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
            this.txtInvoiceTypeName.Text = this.InvoiceEntity.InvoiceTypeName;
            this.txtMoneyTypeName.Text = this.InvoiceEntity.MoneyTypeName;
            this.txtTotalAMT.Text = string.Format("{0:0.####}", this.InvoiceEntity.TotalAMT);
            this.txtTaxAMT.Text = string.Format("{0:0.####}", this.InvoiceEntity.TaxAMT);
            this.txtYear.Text = this.InvoiceEntity.Year.ToString();
            this.txtMonth.Text = this.InvoiceEntity.Month.ToString();
            this.dtblItems = this.accReceiveItems.GetDataBuyReceiveItemsByInvoiceID(this.InvoiceID).Tables[0];
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
        private bool GetPrdSetFlag(int PrdID)
        {
            bool flag = false;
            this.accPrd.GetParmProductPrdSetFlag(PrdID, ref flag);
            return flag;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           
            DialogResult rul = MessageBox.Show("�����б�������һ��ȷ�ϲ��ܱ����", "���ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            bool flag = false;
            string errormsg = string.Empty;
            flag = this.accInvoices.UpdateBuyInvoicesForConfirm(ref errormsg,
                this.InvoiceID,
                JERPBiz.Frame.UserBiz.PsnID);
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }
            //����ǲ��Ӧ����
            decimal AppendAMT = 0;
            foreach (DataRow drow in this.dtblItems.Rows)
            {
                if ((drow["ItemAMT"]!= DBNull.Value)
                    && (drow["ReceiveItemAMT"] != DBNull.Value))
                {
                    AppendAMT += (decimal)drow["ItemAMT"] - (decimal)drow["ReceiveItemAMT"];
                }
                if (this.GetPrdSetFlag((int)drow["PrdID"]) == false)
                {
                    //����������Ͼ͵���ô��
                    if ((drow["CostAMT"] != DBNull.Value)
                        && (drow["ReceiveCostAMT"] != DBNull.Value))
                    {
                        //���֮���
                        this.accStore.UpdateStoreForAppendInventoryAMT(
                            ref errormsg,
                            drow["PrdID"],
                            drow["BranchStoreID"],
                           (decimal)drow["CostAMT"] - (decimal)drow["ReceiveCostAMT"]);

                    }
                }
            }
         
            //���ﲻȥ��˰��
            if (AppendAMT!=0)
            {
                //Ӧ���˿�
                this.accPayableAccount.InsertPayableAccountForCredit(
                         ref errormsg,
                        "��Ӧ��["+this.txtCompanyAbbName .Text +"]ԭ�ϲɹ���Ʊ[" + this.txtInvoiceCode .Text + "]",
                        this.InvoiceEntity .CompanyID,
                        this.InvoiceEntity.MoneyTypeID,
                        AppendAMT,
                        JERPBiz.Frame.UserBiz.PsnID);
            }            
           
            MessageBox.Show("�ɹ���˵�ǰ�ɹ���Ʊ");
            if (this.affterSave != null) this.affterSave();
            this.Close();

        }

    }
}
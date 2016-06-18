using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Sale
{
    public partial class FrmSaleOrderConfirmOper : Form
    {
        public FrmSaleOrderConfirmOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accItems = new JERPData.Product.SaleOrderItems();
            this.NoteEntity = new JERPBiz.Product.SaleOrderNoteEntity();
            this.accCustomer = new JERPData.General.Customer();
            this.lnkCredit.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkCredit_LinkClicked);
            this.btnSave .Click +=new EventHandler(btnSave_Click);
        }

        private JERPData.Product.SaleOrderItems accItems;
        private JERPData.General.Customer accCustomer;
        private JERPBiz.Product.SaleOrderNoteEntity NoteEntity;
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
        public void ConfirmOper(long NoteID)
        {
            this.NoteEntity.LoadData(NoteID);
            this.txtPONo.Text = this.NoteEntity.PONo;
            this.txtCompanyAbbName.Text = this.NoteEntity.CompanyAbbName;
            this.txtMakerPsn.Text = this.NoteEntity.MakerPsn;
            this.txtSalePsn.Text = this.NoteEntity.SalePsn; 
            this.txtDateNote.Text = this.NoteEntity.DateNote.ToShortDateString();
            this.txtDeliverAddress.Text = this.NoteEntity.DeliverAddress;
            this.txtDeliverTypeName.Text = this.NoteEntity.DeliverTypeName;
            this.txtExpressRequire.Text = this.NoteEntity.ExpressRequire;
            this.txtFinanceAddress.Text = this.NoteEntity.FinanceAddress;
            this.txtMemo.Text = this.NoteEntity.Memo;
            this.txtMoneyTypeName.Text = this.NoteEntity.MoneyTypeName;
            this.txtPriceTypeName.Text = this.NoteEntity.PriceTypeName;
            this.txtSettleTypeName.Text = this.NoteEntity.SettleTypeName;
            this.dtblItems = this.accItems.GetDataSaleOrderItemsByNoteID  (NoteID).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
         
        }

        void lnkCredit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            decimal CreditAMT = 0;
            decimal ReceivableAMT = 0;
            this.accCustomer.GetParmCustomerCreditAMT(this.NoteEntity .CompanyID,
                ref CreditAMT, ref ReceivableAMT);
            MessageBox.Show("���ö��:" + string.Format("{0:0.####}", CreditAMT) + "\n��ǰǷ��:" +
                string.Format("{0:0.####}", ReceivableAMT));
        }
        void btnSave_Click(object sender, EventArgs e)
        {
         
            DialogResult rul = MessageBox.Show("��Ҫ�����ѡ�񶩵�,һ����˽�����ƻ����ڣ���ȷ�ϣ�", "���ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            string errormsg=string.Empty ;         
            foreach (DataRow drow in this.dtblItems.Select("ConfirmPsnID is null"))
            {
                this.accItems.UpdateSaleOrderItemsForConfirm(ref errormsg,
                    drow["ItemID"],
                    JERPBiz.Frame.UserBiz.PsnID);
            }
            MessageBox.Show("��ϲ���ɹ���˵�ǰ����");
            if (this.affterSave != null) this.affterSave();
            this.Close();
        }  
    
    }
}
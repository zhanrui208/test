using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.OutSrc
{
    public partial class FrmLossSupplyInvoiceNew : Form
    {
        public FrmLossSupplyInvoiceNew()
        {
            InitializeComponent();
            this.dtpDateNote.Value = DateTime.Now.Date;
            this.ctrlYear.Year = DateTime.Now.Year;
            this.ctrlMonth.Month = DateTime.Now.Month;
            this.ctrlYear.OnSelected += this.SetCaption;
            this.ctrlMonth.OnSelected += this.SetCaption;
            this.SetCaption();
            this.accInvoice = new JERPData.Material.OutSrcLossSupplyInvoices();
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }
        private JERPData.Material .OutSrcLossSupplyInvoices  accInvoice;
        public delegate void AffterSaveDelegate(long InvoiceID);
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
        public void New()
        {
            this.txtInvoiceCode.Text = string.Empty;
        }
        private void SetCaption()
        {
            this.txtInvoiceName.Text = this.ctrlYear.Year.ToString() + "��" + this.ctrlMonth.Month.ToString() + "�·�Ʊ";
        }

        void btnSave_Click(object sender, EventArgs e)
        {
             DialogResult rut = MessageBox.Show("�㽫������Ʊ����һ�����棬��Ӧ�̡����֡��ꡢ�¡���Ʊ���Ͳ��ܱ����ȷ�Ϸ�?", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (rut == DialogResult.Yes)
             {
                 object objInvoiceID = DBNull.Value;
                 string errormsg=string.Empty ;
                 bool flag=this.accInvoice.InsertOutSrcLossSupplyInvoices (
                     ref errormsg,
                     ref objInvoiceID,
                     this.txtInvoiceCode.Text,
                     this.txtInvoiceName.Text,
                     this.ctrlYear.Year,
                     this.ctrlMonth.Month,
                     this.dtpDateNote.Value.Date,
                     this.ctrlSupplierID.CompanyID,
                     this.ctrlMoneyTypeID.MoneyTypeID,
                     JERPBiz.Frame.UserBiz.PsnID);
                 if (flag)
                 {
                     MessageBox.Show("�ɹ�������Ʊ��");
                     if (this.affterSave != null) this.affterSave((long)objInvoiceID);
                     this.Close();
                 }
             }
              
        }
    }
}
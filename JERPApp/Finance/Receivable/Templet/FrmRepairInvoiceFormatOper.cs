using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Receivable.Templet
{
    public partial class FrmRepairInvoiceFormatOper : Form
    {
        public FrmRepairInvoiceFormatOper()
        {
            InitializeComponent();
            ToolTip tip = new ToolTip();
            tip.SetToolTip(this.btnAdd, "����ϸ");
            this.dgrdv.AutoGenerateColumns = false;
            this.accFormat = new JERPData.Product.RepairInvoiceFormat();
            this.accFieldTitle = new JERPData.Product.RepairInvoiceFieldTitle();
            this.FormatEntity = new JERPBiz.Product.RepairInvoiceFormatEntity();
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnNew.Click += new EventHandler(btnNew_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
            this.dgrdv.RowValidated += new DataGridViewCellEventHandler(dgrdv_RowValidated);
        }


        JERPData.Product.RepairInvoiceFormat accFormat;
        JERPData.Product.RepairInvoiceFieldTitle accFieldTitle;
        JERPBiz.Product.RepairInvoiceFormatEntity FormatEntity;
        FrmRepairInvoiceFieldTitle frmTitle;
        private DataTable dtblFieldTitles;
      
        private int formatID = -1;
        private int FormatID
        {
            get
            {
                return formatID;
            }
            set
            {
                this.formatID = value;
                this.btnAdd.Enabled = (value > -1);
                this.btnDelete.Enabled = (value > -1);
            }
        }
        public void NewFormat()
        {
            this.FormatID = -1;

            this.txtDateNoteCellName.Text = string.Empty;
            this.txtFinanceAddressCellName.Text = string.Empty;          
            this.txtItemBeginRowIndex.Text = string.Empty;
            this.txtInvoiceCodeCellName.Text = string.Empty;
            this.txtInvoiceNameCellName.Text = string.Empty;
            this.txtTmpSheetName.Text = string.Empty;
            this.txtCompanyNameCellName.Text = string.Empty;
            this.txtLinkmanCellName.Text = string.Empty;
            this.txtTelephoneCellName.Text = string.Empty;
            this.txtFaxCellName.Text = string.Empty;
            this.txtMoneyTypeCellName.Text = string.Empty;
            this.txtInvoiceTypeCellName.Text = string.Empty;
            this.txtTotalQtyCellName.Text = string.Empty;
            this.txtTaxAMTCellName.Text = string.Empty;
            this.txtTotalAMTCellName.Text = string.Empty;
            this.LoadItem();
        }
        public void EditFormat(int  FormatID)
        {
            this.FormatID = FormatID;
            this.FormatEntity.LoadData(FormatID);
            this.txtDateNoteCellName.Text = this.FormatEntity.DateNoteCellName;
            this.txtTmpSheetName.Text = this.FormatEntity.TmpSheetName;
            this.txtCompanyNameCellName.Text = this.FormatEntity.CompanyNameCellName;
            this.txtFinanceAddressCellName.Text = this.FormatEntity.FinanceAddressCellName;
            this.txtItemBeginRowIndex.Text = (this.FormatEntity.ItemBeginRowIndex > 0) ? this.FormatEntity.ItemBeginRowIndex.ToString() : string.Empty ;
            this.txtInvoiceCodeCellName.Text = this.FormatEntity.InvoiceCodeCellName;
            this.txtInvoiceNameCellName.Text = this.FormatEntity.InvoiceNameCellName;
            this.txtMoneyTypeCellName.Text = this.FormatEntity.MoneyTypeCellName;
            this.txtInvoiceTypeCellName.Text = this.FormatEntity.InvoiceTypeCellName;
            this.txtLinkmanCellName.Text = this.FormatEntity.LinkmanCellName;
            this.txtTelephoneCellName.Text = this.FormatEntity.TelephoneCellName;
            this.txtFaxCellName.Text = this.FormatEntity.FaxCellName ;
            this.txtTotalQtyCellName.Text = this.FormatEntity.TotalQtyCellName;
            this.txtTotalAMTCellName.Text = this.FormatEntity.TotalAMTCellName;
            this.txtTaxAMTCellName.Text = this.FormatEntity.TaxAMTCellName;
           
            this.LoadItem();
        }
        private void LoadItem()
        {
            this.dtblFieldTitles = this.accFieldTitle.GetDataRepairInvoiceFieldTitleByFormatID(this.FormatID).Tables[0];
            this.dgrdv.DataSource = this.dtblFieldTitles;
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

        void dgrdv_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            DataRow drow = null;
            try
            {
                drow = this.dtblFieldTitles.DefaultView[irow].Row;
            }
            catch
            {
                return;
            }
            if (drow == null) return;
            string errormsg=string.Empty ;
            this.accFieldTitle.UpdateRepairInvoiceFieldTitle(ref errormsg,
                drow["FieldTitleID"], drow["FieldTitle"], drow["ColumnName"], drow["SerialNoFlag"],
                drow["GroupFlag"],drow["SumFlag"]);
            this.accFieldTitle.UpdateRepairInvoiceFieldTitleForSort(ref errormsg, drow["FieldTitleID"],
                drow["SortNo"], drow["SortName"]);
        }

    
        void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string errormsg = string.Empty;         
            if (this.FormatID == -1)
            {
                object objFormatID = DBNull.Value;
                flag = this.accFormat.InsertRepairInvoiceFormat(ref errormsg,
                    ref objFormatID,
                    this.txtTmpSheetName.Text,
                    this.txtInvoiceCodeCellName .Text ,
                    this.txtInvoiceNameCellName .Text ,
                    this.txtCompanyNameCellName.Text,
                    this.txtFinanceAddressCellName .Text ,
                    this.txtMoneyTypeCellName .Text ,
                    this.txtInvoiceTypeCellName.Text,
                    this.txtLinkmanCellName.Text,
                    this.txtTelephoneCellName.Text,
                    this.txtFaxCellName.Text,
                    this.txtDateNoteCellName.Text,
                    this.txtTotalQtyCellName .Text ,
                    this.txtTotalAMTCellName.Text,
                    this.txtTaxAMTCellName .Text ,
                    this.txtItemBeginRowIndex.Text);
                if (flag)
                {
                    this.FormatID = (int)objFormatID;
                }
            }
            else
            {
                flag = this.accFormat.UpdateRepairInvoiceFormat(ref errormsg,                  
                    this.FormatID,
                    this.txtTmpSheetName.Text,
                    this.txtInvoiceCodeCellName.Text,
                    this.txtInvoiceNameCellName.Text,
                    this.txtCompanyNameCellName.Text,
                    this.txtFinanceAddressCellName .Text ,
                    this.txtMoneyTypeCellName .Text ,
                    this.txtInvoiceTypeCellName .Text ,
                    this.txtLinkmanCellName.Text,
                    this.txtTelephoneCellName.Text,
                    this.txtFaxCellName.Text,
                    this.txtDateNoteCellName.Text,
                    this.txtTotalQtyCellName .Text ,
                    this.txtTotalAMTCellName.Text,
                    this.txtTaxAMTCellName .Text ,
                    this.txtItemBeginRowIndex.Text);

            }
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }
            else
            {
                MessageBox.Show("�ɹ����浱ǰϵͳ��¼");
                if (this.affterSave != null) this.affterSave();
               
            }

        }
        void btnNew_Click(object sender, EventArgs e)
        {
            this.NewFormat();
        }
        void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.frmTitle == null)
            {
                this.frmTitle = new FrmRepairInvoiceFieldTitle();
                new FrmStyle(this.frmTitle).SetPopFrmStyle(this);
                this.frmTitle.AffterSave += new FrmRepairInvoiceFieldTitle.AffterSaveDelegate(frmTitle_AffterSave);
            }
            this.frmTitle.NewFieldTitle(this.FormatID);
            this.frmTitle.ShowDialog();
        }

        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnBtnEdit.Name)
            {
                long FieldTitleID = (long)this.dtblFieldTitles.DefaultView[irow]["FieldTitleID"];
                if (this.frmTitle == null)
                {
                    this.frmTitle = new FrmRepairInvoiceFieldTitle();
                    new FrmStyle(this.frmTitle).SetPopFrmStyle(this);
                    this.frmTitle.AffterSave += new FrmRepairInvoiceFieldTitle.AffterSaveDelegate(frmTitle_AffterSave);
                }
                this.frmTitle.EditFieldTitle(FieldTitleID);
                this.frmTitle.ShowDialog();
            }
        }
        void frmTitle_AffterSave()
        {
            this.LoadItem();
        }
        void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult rul = MessageBox.Show("����ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            bool flag = false;
            string errormsg = string.Empty;
            flag = this.accFormat.DeleteRepairInvoiceFormat(ref errormsg, this.FormatID);
            if (flag)
            {
                MessageBox.Show("�ɹ�ɾ����ǰ��ʽ��");
            }
            else
            {
                MessageBox.Show(errormsg);
            }
            if (this.affterSave != null) this.affterSave();
            this.NewFormat();
        }

    

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Product.Templet
{
    public partial class FrmRepairDeliverFormatOper : Form
    {
        public FrmRepairDeliverFormatOper()
        {
            InitializeComponent();
            ToolTip tip = new ToolTip();
            tip.SetToolTip(this.btnAdd, "����ϸ");
            this.dgrdv.AutoGenerateColumns = false;
            this.accFormat = new JERPData.Product.RepairDeliverFormat();
            this.accFieldTitle = new JERPData.Product.RepairDeliverFieldTitle();
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnNew.Click += new EventHandler(btnNew_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
            this.dgrdv.RowValidated += new DataGridViewCellEventHandler(dgrdv_RowValidated);
        }

        JERPData.Product.RepairDeliverFormat accFormat;
        JERPData.Product.RepairDeliverFieldTitle accFieldTitle;
        FrmRepairDeliverFieldTitle frmTitle;
       
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
            this.txtTmpSheetName.Text = string.Empty;
            this.txtNoteCodeCellName.Text = string.Empty;
            this.txtDateNoteCellName.Text = string.Empty; 
            this.txtCompanyNameCellName.Text = string.Empty;
            this.txtLinkmanCellName.Text = string.Empty;
            this.txtTelephoneCellName.Text = string.Empty;          
            this.txtDeliverAddressCellName.Text = string.Empty;
            this.txtDeliverPsnCellName.Text = string.Empty;

            this.txtMoneyTypeCellName.Text = string.Empty;
            this.txtSettleTypeCellName.Text = string.Empty;
            this.txtPriceTypeCellName.Text = string.Empty;
            this.txtTotalAMTCellName.Text = string.Empty;

            this.txtMakerPsnCellName.Text = string.Empty;
            this.txtMemoCellName.Text = string.Empty;
            this.txtItemRowIndex.Text = string.Empty;
            this.txtItemRowCount.Text = string.Empty; 
            this.LoadItem();
        }
        public void EditFormat(int FormatID)
        {
            this.FormatID = FormatID;
            DataTable dtbl = this.accFormat.GetDataRepairDeliverFormatByFormatID(FormatID).Tables[0];
            this.txtTmpSheetName.Text = string.Empty;
            this.txtNoteCodeCellName.Text = string.Empty;
            this.txtCompanyNameCellName.Text = string.Empty;
            this.txtTelephoneCellName.Text = string.Empty;
            this.txtLinkmanCellName.Text = string.Empty;
            this.txtDateNoteCellName.Text = string.Empty;
            this.txtDeliverAddressCellName.Text = string.Empty;
            this.txtMoneyTypeCellName.Text = string.Empty;
            this.txtSettleTypeCellName.Text = string.Empty;
            this.txtPriceTypeCellName.Text = string.Empty;
            this.txtTotalAMTCellName.Text = string.Empty;
            this.txtDeliverPsnCellName.Text = string.Empty;
            this.txtMakerPsnCellName.Text = string.Empty;    
          
            this.txtMemoCellName.Text = string.Empty;
            this.txtItemRowIndex.Text = string.Empty;
            this.txtItemRowCount.Text = string.Empty; 
       
         
           
            if (dtbl.Rows.Count > 0)
            {
                DataRow drow = dtbl.Rows[0];
                this.txtTmpSheetName.Text = drow["TmpSheetName"].ToString();
                this.txtNoteCodeCellName.Text = drow["NoteCodeCellName"].ToString();
                this.txtDateNoteCellName.Text = drow["DateNoteCellName"].ToString();
                this.txtCompanyNameCellName.Text = drow["CompanyNameCellName"].ToString();
                this.txtLinkmanCellName .Text = drow["LinkmanCellName"].ToString();
                this.txtTelephoneCellName .Text = drow["TelephoneCellName"].ToString();
                this.txtDeliverAddressCellName.Text = drow["DeliverAddressCellName"].ToString();
                this.txtDeliverPsnCellName.Text = drow["DeliverPsnCellName"].ToString();
                this.txtMoneyTypeCellName.Text = drow["MoneyTypeCellName"].ToString();
                this.txtSettleTypeCellName.Text = drow["SettleTypeCellName"].ToString();
                this.txtPriceTypeCellName.Text = drow["PriceTypeCellName"].ToString();
                this.txtTotalAMTCellName.Text = drow["TotalAMTCellName"].ToString();
                this.txtMakerPsnCellName.Text = drow["MakerPsnCellName"].ToString();
                this.txtMemoCellName.Text = drow["MemoCellName"].ToString();
                this.txtItemRowIndex.Text = drow["ItemRowIndex"].ToString();
                this.txtItemRowCount.Text = drow["ItemRowCount"].ToString(); 
            }
            this.LoadItem();
        }
        private void LoadItem()
        {
            this.dtblFieldTitles = this.accFieldTitle.GetDataRepairDeliverFieldTitleByFormatID(this.FormatID).Tables[0];
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
                drow=this.dtblFieldTitles.DefaultView[irow].Row;
            }
            catch 
            { 
                return; 
            }
            if (drow == null) return;
            string errormsg=string.Empty ;
            this.accFieldTitle.UpdateRepairDeliverFieldTitle(ref errormsg,
                drow["FieldTitleID"], drow["FieldTitle"], drow["ColumnName"], drow["SerialNoFlag"]);
         
        }

    
        void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string errormsg = string.Empty;

            if (this.FormatID == -1)
            {
                object objFormatID = DBNull.Value;
                flag = this.accFormat.InsertRepairDeliverFormat(ref errormsg, ref objFormatID,
                    this.txtTmpSheetName.Text,
                    this.txtNoteCodeCellName.Text,
                    this.txtCompanyNameCellName.Text,
                    this.txtDateNoteCellName.Text,
                    this.txtDeliverAddressCellName.Text,
                    this.txtLinkmanCellName.Text,
                    this.txtTelephoneCellName.Text,
                    this.txtDeliverPsnCellName.Text,
                    this.txtMoneyTypeCellName .Text ,
                    this.txtSettleTypeCellName .Text ,
                    this.txtPriceTypeCellName.Text,
                    this.txtTotalAMTCellName.Text,
                    this.txtMakerPsnCellName.Text,
                    this.txtMemoCellName.Text,
                    this.txtItemRowIndex.Text,
                    this.txtItemRowCount.Text );

                if (flag)
                {
                    this.FormatID = (int)objFormatID;
                }
            }
            else
            {
                flag = this.accFormat.UpdateRepairDeliverFormat(ref errormsg, this.FormatID,
                    this.txtTmpSheetName.Text,
                    this.txtNoteCodeCellName.Text,
                    this.txtCompanyNameCellName.Text,
                    this.txtDateNoteCellName.Text,
                    this.txtDeliverAddressCellName.Text,
                    this.txtLinkmanCellName.Text,
                    this.txtTelephoneCellName.Text,
                    this.txtDeliverPsnCellName.Text,
                    this.txtMoneyTypeCellName.Text,
                    this.txtSettleTypeCellName.Text,
                    this.txtPriceTypeCellName.Text,
                    this.txtTotalAMTCellName.Text,
                    this.txtMakerPsnCellName.Text,
                    this.txtMemoCellName.Text,
                    this.txtItemRowIndex.Text,
                    this.txtItemRowCount.Text);

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
                this.frmTitle = new FrmRepairDeliverFieldTitle();
                new FrmStyle(this.frmTitle).SetPopFrmStyle(this);
                this.frmTitle.AffterSave += new FrmRepairDeliverFieldTitle.AffterSaveDelegate(frmTitle_AffterSave);
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
                    this.frmTitle = new FrmRepairDeliverFieldTitle();
                    new FrmStyle(this.frmTitle).SetPopFrmStyle(this);
                    this.frmTitle.AffterSave += new FrmRepairDeliverFieldTitle.AffterSaveDelegate(frmTitle_AffterSave);
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
            flag = this.accFormat.DeleteRepairDeliverFormat(ref errormsg, this.FormatID);
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
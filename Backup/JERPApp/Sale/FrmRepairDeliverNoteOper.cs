using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Sale
{
    public partial class FrmRepairDeliverNoteOper : Form
    {
        public FrmRepairDeliverNoteOper()
        {
            InitializeComponent();
            this.accNotes = new JERPData.Product.RepairDeliverNotes();
            this.accItems = new JERPData.Product.RepairDeliverItems();
            this.accRepairItems = new JERPData.Product.RepairItems();
            this.NoteEntity = new JERPBiz.Product.RepairDeliverNoteEntity();
            this.dgrdvItem.AutoGenerateColumns = false;  
            this.ctrlCustomerID.AffterSelected += ctrlCustomerID_AffterSelected; 
            this.btnSave .Click +=new EventHandler(btnSave_Click);
            this.dgrdvItem.MouseClick += new MouseEventHandler(dgrdvItem_MouseClick);
            this.btnAppend.Click += new EventHandler(btnAppend_Click);
            this.dgrdvItem.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvItem_UserDeletingRow);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
        }

   

        private JERPData.Product.RepairDeliverNotes accNotes;
        private JERPData.Product.RepairDeliverItems accItems;
        private JERPBiz.Product.RepairDeliverNoteEntity NoteEntity;
        private JERPData.Product.RepairItems accRepairItems;
        private FrmRepairDeliverItemAppend frmAppend;
        private DataTable dtblItems; 
        void ctrlCustomerID_AffterSelected()
        {
            this.ctrlDeliverAddressID.LoadData(this.ctrlCustomerID.CompanyID);
            this.ctrlFinanceAddressID.LoadData(this.ctrlCustomerID.CompanyID);
           
        }
        private long noteid = -1;
        private long NoteID
        {
            get
            {
                return this.noteid;
            }
            set
            {
                this.noteid = value;
                this.btnDelete.Enabled = (value> -1); 
            }
        }
        private void LoadItems()
        {
            this.dtblItems = this.accItems .GetDataRepairDeliverItemsByNoteID (this.NoteID).Tables[0]; 
            this.dgrdvItem.DataSource = this.dtblItems;
        }
        public void New(DataRow drowRepairItem)
        {
            this.NoteID = -1;
            this.txtNoteCode .Text =string.Empty ;
            this.ctrlCustomerID.CompanyID = (int)drowRepairItem["CompanyID"];
            this.ctrlCustomerID_AffterSelected ();  
            this.txtExpressRequire.Text = string.Empty;
            this.rchMemo.Text = string.Empty;
            this.LoadItems();
            this.dtblItems.ImportRow(drowRepairItem);

        }
        public void Edit(long NoteID)
        {
            this.NoteID = NoteID;
            this.NoteEntity.LoadData(NoteID);
            this.txtNoteCode.Text = this.NoteEntity.NoteCode;
            this.ctrlCustomerID.CompanyID = this.NoteEntity.CompanyID;

            this.ctrlCustomerID_AffterSelected();
            this.ctrlDeliverAddressID.DeliverAddressID = this.NoteEntity.DeliverAddressID;
            this.ctrlFinanceAddressID.FinanceAddressID = this.NoteEntity.FinanceAddressID;
            this.ctrlMoneyTypeID.MoneyTypeID = this.NoteEntity.MoneyTypeID;
            this.ctrlSettleTypeID.SettleTypeID = this.NoteEntity.SettleTypeID;
            this.ctrlPriceTypeID.PriceTypeID = this.NoteEntity.PriceTypeID;
            this.txtExpressRequire.Text = this.NoteEntity.ExpressRequire;
            this.rchMemo.Text = this.NoteEntity.Memo;
            this.LoadItems();
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
        void dgrdvItem_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblItems.DefaultView[irow].Row;
            string ErrorMsg = string.Empty;
            if (drow["ItemID"] == DBNull.Value)
            {
                return;
            }
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accItems.DeleteRepairDeliverItems (ref ErrorMsg,
                    drow["ItemID"]);
                if (!flag)
                {

                    MessageBox.Show(ErrorMsg);
                    if (this.affterSave != null) this.affterSave();
                } 
            }
            else
            {
                e.Cancel = true;
            }
        }
        void btnDelete_Click(object sender, EventArgs e)
        {
               DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (rul == DialogResult.Yes)
               {
                   string errormsg = string.Empty;
                   bool flag = this.accNotes.DeleteRepairDeliverNotes(ref errormsg,
                       this.NoteID);
                   if (flag)
                   {
                       MessageBox.Show("�ɹ�ɾ����ǰ����");
                   }
                   else
                   { 
                       MessageBox.Show(errormsg);
                   }
                   if (this.affterSave != null) this.affterSave();
                   this.Close();
               }
        }
        private void AppendItem()
        {
            if (this.ctrlCustomerID.CompanyID == -1)
            {
                MessageBox.Show("δѡ���κοͻ�");
                return;
            }
            if (frmAppend == null)
            {
                frmAppend = new FrmRepairDeliverItemAppend();
                new FrmStyle(frmAppend).SetPopFrmStyle(this);
                frmAppend.AffterAppend += new FrmRepairDeliverItemAppend.AffterAppendDelegate(frmAppend_AffterAppend);
            }
            frmAppend.DeliverItemAppend(this.ctrlCustomerID.CompanyID,
                this.dtblItems.Select("ItemID is null", "", DataViewRowState.CurrentRows));
            frmAppend.ShowDialog();
        }

        void frmAppend_AffterAppend(DataRow drow)
        {
            this.ctrlCustomerID.Enabled = false;
            DataRow[] drowItems = this.dtblItems.Select("RepairItemID=" + drow["RepairItemID"].ToString(), "", DataViewRowState.CurrentRows);
            if (drowItems.Length > 0)
            {
                return;
            }
            this.dtblItems.ImportRow(drow);
        }
        void dgrdvItem_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.AppendItem();
            }
        }

        void btnAppend_Click(object sender, EventArgs e)
        {
            this.AppendItem();
        } 
        private bool ValidateData()
        { 
            if (this.ctrlCustomerID.CompanyID == -1)
            {
                MessageBox.Show("�Բ���,δѡ���κοͻ�!");
                return false;
            }
            if (this.ctrlMoneyTypeID .MoneyTypeID  == -1)
            {
                MessageBox.Show("�Բ���,δѡ�����!");
                return false;
            }
            if (this.ctrlSettleTypeID.SettleTypeID  == -1)
            {
                MessageBox.Show("�Բ���,δѡ����㷽ʽ!");
                return false;
            }
            if (this.ctrlPriceTypeID.PriceTypeID  == -1)
            {
                MessageBox.Show("�Բ���,δѡ�񵥼�����!");
                return false;
            }
            return true;
        } 
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            DialogResult rul = MessageBox.Show("������ά���ͻ���һ�������ͻ����Ų��ܱ����", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            bool flag = false;
            string errormsg = string.Empty;
            object objTotalAMT=this.dtblItems .Compute("SUM(Amount)","");
            if (this.NoteID == -1)
            {
                object objNoteCode = DBNull.Value;
                object objNoteID = DBNull.Value;
                flag = this.accNotes.InsertRepairDeliverNotes(
                   ref errormsg,
                   ref objNoteID,
                   ref objNoteCode,
                   DateTime.Now.Date,
                   this.ctrlCustomerID.CompanyID,
                   this.ctrlMoneyTypeID.MoneyTypeID,
                   this.ctrlSettleTypeID.SettleTypeID,
                   this.ctrlPriceTypeID.PriceTypeID,
                   this.ctrlDeliverAddressID.DeliverAddressID,
                   this.ctrlFinanceAddressID.FinanceAddressID,
                   this.txtExpressRequire.Text,
                   this.ctrlSettleTypeID.CashSettleFlag,
                   objTotalAMT,
                   JERPBiz.Frame.UserBiz.PsnID,
                   this.rchMemo.Text);
                if (flag)
                {
                    this.NoteID = (long)objNoteID;
                    this.txtNoteCode.Text = objNoteCode.ToString();
                }
            }
            else
            {
                flag=this.accNotes.UpdateRepairDeliverNotes(ref errormsg,
                    this.NoteID,
                    this.ctrlMoneyTypeID.MoneyTypeID,
                    this.ctrlSettleTypeID.SettleTypeID,
                    this.ctrlPriceTypeID.PriceTypeID,
                    this.ctrlDeliverAddressID.DeliverAddressID,
                    this.ctrlFinanceAddressID.FinanceAddressID,
                    this.txtExpressRequire.Text,
                    this.ctrlSettleTypeID.CashSettleFlag,
                    objTotalAMT,
                    JERPBiz.Frame.UserBiz.PsnID,
                    this.rchMemo.Text);
                    
            }
            if (!flag)
            {
                MessageBox.Show(errormsg,"������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }       
            //��ϸ 
            object objItemID = DBNull.Value;
            foreach (DataRow drow in this.dtblItems.Rows)
            { 
                if (drow.RowState == DataRowState.Deleted) continue;
                objItemID = drow["ItemID"];
                if (objItemID == DBNull.Value)
                {
                    flag = this.accItems.InsertRepairDeliverItems(
                        ref errormsg,
                        ref objItemID,
                        this.NoteID,
                        drow["RepairItemID"],
                        drow["Amount"],
                        drow["Memo"]);
                    if (flag)
                    {
                        drow["ItemID"] = objItemID;
                    }
                }
                else
                {
                    flag = this.accItems.UpdateRepairDeliverItems(
                        ref errormsg,
                        objItemID,
                        drow["Amount"],
                        drow["Memo"]); 
                }
               
            }          
            MessageBox.Show("�ɹ����浱ǰ����");
            if (this.affterSave != null) this.affterSave();
            
        }

   
      
    }
}
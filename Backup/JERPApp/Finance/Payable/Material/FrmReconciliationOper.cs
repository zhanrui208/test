using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.Material
{
    public partial class FrmReconciliationOper : Form
    {
        public FrmReconciliationOper()
        {
            InitializeComponent();
            this.accReconciliations = new JERPData.Material.BuyReconciliations();
            this.accReceiveItems = new JERPData.Material.BuyReceiveItems();
            this.accReturnItems = new JERPData.Material.BuyReturnItems();
            this.accFineAMT = new JERPData.Material.BuyFineAMTNotes();
            this.ReconciliationEntity = new JERPBiz.Material.BuyReconciliationEntity();

            this.dgrdvReceive.AutoGenerateColumns = false;
            this.dgrdvReceive.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvReceive_UserDeletingRow);
            this.dgrdvReceive.UserDeletedRow += new DataGridViewRowEventHandler(dgrdvReceive_UserDeletedRow);
            this.dgrdvReceive.MouseClick += new MouseEventHandler(dgrdvReceive_MouseClick);

            this.dgrdvReturn.AutoGenerateColumns = false;
            this.dgrdvReturn.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvReturn_UserDeletingRow);
            this.dgrdvReturn.MouseClick += new MouseEventHandler(dgrdvReturn_MouseClick);
            this.dgrdvReturn.UserDeletedRow += new DataGridViewRowEventHandler(dgrdvReturn_UserDeletedRow);


            this.dgrdvFine.AutoGenerateColumns = false;
            this.dgrdvFine.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvFine_UserDeletingRow);
            this.dgrdvFine.UserDeletedRow += new DataGridViewRowEventHandler(dgrdvFine_UserDeletedRow);
            this.dgrdvFine.MouseClick += new MouseEventHandler(dgrdvFine_MouseClick);


            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.FormClosed += new FormClosedEventHandler(FrmReconciliationOper_FormClosed);
            this.btnNew.Click += new EventHandler(btnNew_Click);
            this.Shown += new EventHandler(FrmReconciliationOper_Shown);
        }

       

        private JERPData.Material.BuyReconciliations accReconciliations;
        private JERPData.Material.BuyReceiveItems accReceiveItems;
        private JERPData.Material.BuyReturnItems accReturnItems;
        private JERPData.Material.BuyFineAMTNotes accFineAMT;
        private JERPBiz.Material.BuyReconciliationEntity ReconciliationEntity;
        private FrmReconciliationNew frmNew;
        private JERPApp.Define.Material.FrmBuyReceiveItemForReconciliation frmReceiveItem;
        private JERPApp.Define.Material.FrmBuyFineAMTForReconciliation frmFine;
        private JERPApp.Define.Material.FrmBuyReturnItemForReconciliation  frmReturnItem;
        private DataTable dtblReceiveItems, dtblReturnItems, dtblFineAMT;
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
        private long reconciliationid = 0;
        private long ReconciliationID
        {
            get
            {
                return this.reconciliationid;
            }
            set
            {
                this.reconciliationid = value;
                this.btnDelete.Enabled = (value > 0);
            }
        }
        public void New()
        {
            this.ReconciliationID =0;
            this.txtReconciliationCode.Text = string.Empty ;
            this.txtReconciliationName.Text = string.Empty;
            this.txtCompanyAbbName.Text = string.Empty;
            this.txtYear.Text = string.Empty;
            this.txtMonth.Text = string.Empty;
            this.txtMoneyTypeName.Text = string.Empty ;
            this.txtSettleTypeName.Text = string.Empty;
            this.txtDeliverAMT.Text = string.Empty;
            this.txtReturnAMT.Text = string.Empty;
            this.txtFineAMT.Text = string.Empty;
            this.txtTotalAMT.Text =string.Empty;

            this.SetRemindText();
            this.LoadReceiveItems();
            this.LoadReturnItems();
            this.LoadFine();
        }
     
        public void Edit(long ReconciliationID)
        {
            this.ReconciliationID = ReconciliationID;
            this.ReconciliationEntity.LoadData(ReconciliationID);
            this.txtReconciliationCode.Text = this.ReconciliationEntity.ReconciliationCode;
            this.txtReconciliationName.Text = this.ReconciliationEntity.ReconciliationName;
            this.txtCompanyAbbName.Text = this.ReconciliationEntity.CompanyAbbName;
            this.txtYear.Text = this.ReconciliationEntity.Year.ToString();
            this.txtMonth.Text = this.ReconciliationEntity.Month.ToString();
            this.dtpDateNote .Value  = this.ReconciliationEntity.DateNote;
            this.dtpDateSettle.Value = this.ReconciliationEntity.DateSettle;
            this.txtMoneyTypeName.Text = this.ReconciliationEntity.MoneyTypeName;
            this.txtSettleTypeName.Text = this.ReconciliationEntity.SettleTypeName;
            this.txtDeliverAMT.Text = (this.ReconciliationEntity.DeliverAMT > 0) ? string.Format("{0:0.####}", this.ReconciliationEntity.DeliverAMT) : string.Empty;
            this.txtReturnAMT.Text = (this.ReconciliationEntity.ReturnAMT > 0) ? string.Format("{0:0.####}", this.ReconciliationEntity.ReturnAMT) : string.Empty;
            this.txtFineAMT.Text = (this.ReconciliationEntity.FineAMT > 0) ? string.Format("{0:0.####}", this.ReconciliationEntity.FineAMT) : string.Empty;
            this.txtTotalAMT.Text = (this.ReconciliationEntity.TotalAMT > 0) ? string.Format("{0:#,##0.####}", this.ReconciliationEntity.TotalAMT) : string.Empty;

            this.SetRemindText();
            this.LoadReturnItems();
            this.LoadReceiveItems();
            this.LoadFine();
        }
        private void SetRemindText()
        {
            int cnt=0;
            this.accReceiveItems.GetParmBuyReceiveItemsCountForReconciliation(this.ReconciliationID, ref cnt);
            this.tabReceive.Text = "送货:" + cnt.ToString();

            cnt = 0;
            this.accReturnItems.GetParmBuyReturnItemsCountForReconciliation(this.ReconciliationID, ref cnt);
            this.tabReturn.Text = "退货:" + cnt.ToString();
         
            this.accFineAMT.GetParmBuyFineAMTNotesCountForReconciliation (this.ReconciliationID, ref cnt);
            this.pageFine .Text = "扣款:" + cnt.ToString();
          
        }
        private void SetAMT(DataTable dtblItems, string fieldname, TextBox txt)
        {
            decimal AMT = 0;
            foreach (DataRow drow in dtblItems.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                AMT += (decimal)drow[fieldname];
            }
            txt.Text = string.Format("{0:0.####}", AMT);
            this.SetToatalAMT();
        }
        private void SetToatalAMT()
        {
            decimal TotalAMT = 0;
            decimal tmp = 0;
            foreach (DataRow drow in this.dtblReceiveItems.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                tmp += (decimal)drow["ItemAMT"];
            }
            this.txtDeliverAMT.Text = string.Format("{0:0.####}", tmp);
            TotalAMT += tmp;

            tmp = 0;
            foreach (DataRow drow in this.dtblReturnItems.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                tmp += (decimal)drow["ItemAMT"];
            }
            this.txtReturnAMT.Text = string.Format("{0:0.####}", tmp);
            TotalAMT += -tmp;

            tmp = 0;
            foreach (DataRow drow in this.dtblFineAMT.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                tmp += (decimal)drow["Amount"];
            }
            this.txtFineAMT.Text = string.Format("{0:0.####}", tmp);
            TotalAMT += -tmp;
            this.txtTotalAMT.Text = string.Format("{0:0.####}", TotalAMT);
        }
        private void LoadReceiveItems()
        {
            this.dtblReceiveItems = this.accReceiveItems.GetDataBuyReceiveItemsByReconciliationID(this.ReconciliationID).Tables[0];
            this.dgrdvReceive.DataSource = this.dtblReceiveItems;          
            
        }
        private void LoadReturnItems()
        {
            this.dtblReturnItems = this.accReturnItems.GetDataBuyReturnItemsByReconciliationID(this.ReconciliationID).Tables[0];
            this.dgrdvReturn.DataSource = this.dtblReturnItems;

        }
        private void LoadFine()
        {
            this.dtblFineAMT = this.accFineAMT.GetDataBuyFineAMTNotesByReconciliationID  (this.ReconciliationID).Tables[0];
            this.dgrdvFine.DataSource = this.dtblFineAMT;
        }
        private void SaveAMT()
        {
            string errormsg = string.Empty;
            this.accReconciliations.UpdateBuyReconciliationsForAMT(ref errormsg, this.ReconciliationID);
        }

        void dgrdvReceive_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            object objItemID = this.dtblReceiveItems.DefaultView[irow]["ItemID"];
            if (objItemID == DBNull.Value)
            {
                return;
            }
            string errormsg = string.Empty;
            bool flag = this.accReceiveItems.UpdateBuyReceiveItemsCancelReconciliation(
                ref errormsg, objItemID);
            if (!flag)
            {

                MessageBox.Show(errormsg);
                e.Cancel = true;
            }

        }
        void dgrdvReceive_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.SetAMT(this.dtblReceiveItems, "ItemAMT", this.txtDeliverAMT);
            this.SetRemindText();
        }
        void dgrdvReceive_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.ReconciliationID == 0) return;
            if (e.Button == MouseButtons.Right)
            {
                if (this.ReconciliationID > 0)
                {
                    if (frmReceiveItem == null)
                    {
                        frmReceiveItem = new JERPApp.Define.Material.FrmBuyReceiveItemForReconciliation();
                        new FrmStyle(frmReceiveItem).SetPopFrmStyle(this);
                        frmReceiveItem.AffterEnter += frmReceiveItem_AffterEnter;
                    }
                    frmReceiveItem.ReconciliationItem(this.ReconciliationID, this.ReconciliationEntity.Year, this.ReconciliationEntity.Month);
                    frmReceiveItem.ShowDialog();
                }
            }
        }
        void frmReceiveItem_AffterEnter()
        {
            this.LoadReceiveItems();
            this.SetAMT(this.dtblReceiveItems, "ItemAMT", this.txtDeliverAMT);
            this.SetRemindText();
        }


        void dgrdvReturn_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            object objItemID = this.dtblReturnItems.DefaultView[irow]["ItemID"];
            if (objItemID == DBNull.Value)
            {
                return;
            }
            string errormsg = string.Empty;
            bool flag = this.accReturnItems.UpdateBuyReturnItemsCancelReconciliation(
                ref errormsg, objItemID);
            if (!flag)
            {

                MessageBox.Show(errormsg);
                e.Cancel = true;
            }

        }
        void dgrdvReturn_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

            this.SetAMT(this.dtblReturnItems , "ItemAMT", this.txtReturnAMT);
            this.SetRemindText();
        }
        void dgrdvReturn_MouseClick(object sender, MouseEventArgs e)
        {
         
            if (e.Button == MouseButtons.Right)
            {
                if (this.ReconciliationID > -1)
                {
                    if (frmReturnItem == null)
                    {
                        frmReturnItem = new JERPApp.Define.Material.FrmBuyReturnItemForReconciliation();
                        new FrmStyle(frmReturnItem).SetPopFrmStyle(this);
                        frmReturnItem.AffterEnter += frmReturnItem_AffterEnter;
                    }
                    frmReturnItem.ReconciliationItem(this.ReconciliationID, this.ReconciliationEntity.Year, this.ReconciliationEntity.Month);
                    frmReturnItem.ShowDialog();
                }
            }
        }
        void frmReturnItem_AffterEnter()
        {
           

            this.LoadReturnItems();
            this.SetAMT(this.dtblReturnItems , "ItemAMT", this.txtDeliverAMT);
            this.SetRemindText();
        }

        void dgrdvFine_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            object objNoteID = this.dtblFineAMT.DefaultView[irow]["NoteID"];
            if (objNoteID == DBNull.Value)
            {
                return;
            }
            string errormsg = string.Empty;
            bool flag = this.accFineAMT.UpdateBuyFineAMTNotesCancelReconciliation (
                ref errormsg, objNoteID);
            if (!flag)
            {

                MessageBox.Show(errormsg);
                e.Cancel = true;
            }

        }
        void dgrdvFine_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.SetAMT(this.dtblFineAMT, "Amount", this.txtFineAMT);
            this.SetRemindText();
        }
        void dgrdvFine_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.ReconciliationID == 0) return;
            if (e.Button == MouseButtons.Right)
            {
                if (this.ReconciliationID > 0)
                {
                    if (frmFine == null)
                    {
                        frmFine = new JERPApp.Define.Material.FrmBuyFineAMTForReconciliation();
                        new FrmStyle(frmFine).SetPopFrmStyle(this);
                        frmFine.AffterConfirm += frmFine_AffterEnter;
                    }
                    frmFine.LoadReconciliaion(this.ReconciliationID);
                    frmFine.ShowDialog();
                }
            }
        }
        void frmFine_AffterEnter()
        {
            this.LoadFine();
            this.SetAMT(this.dtblFineAMT, "Amount", this.txtFineAMT);
            this.SetRemindText();
        }
        private void ShowFrmNew()
        {
            if (this.frmNew == null)
            {
                this.frmNew = new FrmReconciliationNew();
                new FrmStyle(frmNew).SetPopFrmStyle(this);
                this.frmNew.AffterSave += this.Edit;
            }
            frmNew.New();
            frmNew.ShowDialog();
        }
        void btnNew_Click(object sender, EventArgs e)
        {
            this.New();
            this.ShowFrmNew();
        }

        void FrmReconciliationOper_Shown(object sender, EventArgs e)
        {
            if (this.ReconciliationID == 0)
            {
                this.ShowFrmNew();
            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            string errormsg = string.Empty;
            bool flag = false;
            flag=this.accReconciliations.UpdateBuyReconciliations(ref errormsg, this.ReconciliationID,
                this.txtReconciliationCode.Text,
                this.txtReconciliationName .Text ,
                this.dtpDateNote.Value.Date,
                this.dtpDateSettle .Value .Date ,
                JERPBiz.Frame.UserBiz.PsnID);
            if (flag)
            {
                MessageBox.Show("成功进行单据的保存");
            }
            else
            {
                MessageBox.Show(errormsg);
            }
        }
        void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult rut = MessageBox.Show("你将删除当前对账单及明细，你的删除将不能恢复，确认否?", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rut == DialogResult.Yes)
            {
                string errormsg = string.Empty;
                bool flag = false;
                flag = this.accReconciliations.DeleteBuyReconciliations(ref errormsg, this.ReconciliationID);
                if (flag)
                {
                    MessageBox.Show("删除当前对账单及明细！");
                }
                else
                {
                    MessageBox.Show(errormsg);
                }
              
                this.Close();
            }
        }      
        void FrmReconciliationOper_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.ReconciliationID > 0)
            {
                this.SaveAMT();
               if (this.affterSave != null) this.affterSave();
            }
        }
    }
}
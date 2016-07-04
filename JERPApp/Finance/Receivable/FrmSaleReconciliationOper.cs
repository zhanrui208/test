using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Receivable
{
    public partial class FrmSaleReconciliationOper : Form
    {
        public FrmSaleReconciliationOper()
        {
            InitializeComponent();
            this.accReconciliations = new JERPData.Product.SaleReconciliations();
            this.accDeliverItems = new JERPData.Product.SaleDeliverItems();
            this.accRepairItems = new JERPData.Product.RepairDeliverItems();
            this.accReturnItems = new JERPData.Product.SaleReturnItems();
            this.accFineAMT = new JERPData.Product.SaleFineAMTNotes();
            this.accReplaceExpressAMT = new JERPData.Product.SaleReplaceExpressAMTRecord();
            this.ReconciliationEntity = new JERPBiz.Product.SaleReconciliationEntity();
            this.printerhelper = new JERPBiz.Product.SaleReconciliationPrintHelper();
            this.dgrdvDeliver.AutoGenerateColumns = false;
            this.dgrdvRepair.AutoGenerateColumns = false;
            this.dgrdvReturn.AutoGenerateColumns = false;
            this.dgrdvFine.AutoGenerateColumns = false;
            this.dgrdvReplaceExpressAMT.AutoGenerateColumns = false;
          
            this.dgrdvDeliver.UserDeletedRow += new DataGridViewRowEventHandler(dgrdvDeliver_UserDeletedRow);
            this.dgrdvDeliver.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvDeliver_UserDeletingRow);
            this.dgrdvDeliver.MouseClick += new MouseEventHandler(dgrdvDeliver_MouseClick);


            this.dgrdvRepair.UserDeletedRow += new DataGridViewRowEventHandler(dgrdvRepair_UserDeletedRow);
            this.dgrdvRepair.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvRepair_UserDeletingRow);
            this.dgrdvRepair.MouseClick += new MouseEventHandler(dgrdvRepair_MouseClick);


            this.dgrdvReturn.UserDeletedRow += new DataGridViewRowEventHandler(dgrdvReturn_UserDeletedRow);
            this.dgrdvReturn.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvReturn_UserDeletingRow);
            this.dgrdvReturn.MouseClick += new MouseEventHandler(dgrdvReturn_MouseClick);


            this.dgrdvFine.UserDeletedRow += new DataGridViewRowEventHandler(dgrdvFine_UserDeletedRow);
            this.dgrdvFine.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvFine_UserDeletingRow);
            this.dgrdvFine.MouseClick += new MouseEventHandler(dgrdvFine_MouseClick);


            this.dgrdvReplaceExpressAMT.UserDeletedRow += new DataGridViewRowEventHandler(dgrdvReplaceExpressAMT_UserDeletedRow);
            this.dgrdvReplaceExpressAMT.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvReplaceExpressAMT_UserDeletingRow);
            this.dgrdvReplaceExpressAMT.MouseClick += new MouseEventHandler(dgrdvReplaceExpressAMT_MouseClick);


            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.FormClosed += new FormClosedEventHandler(FrmSaleReconciliationOper_FormClosed);
            this.btnExport.Click += this.btnExplore_Click;
        }
        private JERPData.Product.SaleReconciliations accReconciliations;
        private JERPData.Product.SaleDeliverItems accDeliverItems;
        private JERPData.Product.RepairDeliverItems accRepairItems;
        private JERPData.Product.SaleReturnItems accReturnItems;
        private JERPData.Product.SaleFineAMTNotes accFineAMT;
        private JERPData.Product.SaleReplaceExpressAMTRecord accReplaceExpressAMT;
        private JERPBiz.Product.SaleReconciliationPrintHelper printerhelper;
        private JERPBiz.Product.SaleReconciliationEntity ReconciliationEntity;
        private JERPApp .Define .Product .FrmSaleDeliverItemForReconciliation  frmDeliverItem;
        private JERPApp.Define.Product.FrmRepairDeliverItemForReconciliation frmRepairItem;
        private JERPApp.Define.Product.FrmSaleReturnItemForReconciliation frmReturnItem;
        private JERPApp.Define.Product.FrmSaleFineAMTForReconciliation frmFine;
        private JERPApp.Define.Product.FrmSaleReplaceExpressAMTRecordForReconciliation frmReplaceExpressAMT;
        private DataTable dtblDeliverItems, dtblRepairItems,dtblReturnItems,dtblFineAMT,dtblReplaceAMT;
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
        private long reconciliationid = -1;
        private long ReconciliationID
        {
            get
            {
                return this.reconciliationid;
            }
            set
            {
                this.reconciliationid = value;
                this.btnDelete.Enabled = (value > -1);
            }
        }
        
        public void Edit(long ReconciliationID)
        {
            this.ReconciliationID = ReconciliationID;
            this.ReconciliationEntity.LoadData(ReconciliationID);
            this.txtReconciliationName.Text = this.ReconciliationEntity.ReconciliationName;
            this.txtReconciliationCode.Text = this.ReconciliationEntity.ReconciliationCode;
            this.txtCompanyAbbName.Text = this.ReconciliationEntity.CompanyAbbName;
            this.txtFinanceAddress.Text = this.ReconciliationEntity.FinanceAddress;
            this.txtYear.Text = this.ReconciliationEntity.Year.ToString();
            this.txtMonth.Text = this.ReconciliationEntity.Month.ToString();
            this.txtDateNote.Text = this.ReconciliationEntity.DateNote.ToShortDateString();
            this.txtMoneyTypeName.Text = this.ReconciliationEntity.MoneyTypeName;
            this.txtSettleTypeName.Text = this.ReconciliationEntity.SettleTypeName;
            if (this.ReconciliationEntity.DateSettle != DateTime.MinValue)
            {
                this.dtpDateSettle.Value = this.ReconciliationEntity.DateSettle;
            }
            this.txtDeliverAMT.Text = (this.ReconciliationEntity.DeliverAMT > 0)?string.Format("{0:0.####}", this.ReconciliationEntity.DeliverAMT):string.Empty;
            this.txtRepairAMT.Text = (this.ReconciliationEntity.RepairAMT  > 0) ? string.Format("{0:0.####}", this.ReconciliationEntity.RepairAMT) : string.Empty;
            this.txtReplaceExpressAMT.Text=(this.ReconciliationEntity.ReplaceExpressAMT  > 0) ? string.Format("{0:0.####}", this.ReconciliationEntity.ReplaceExpressAMT) : string.Empty;
            this.txtReturnAMT.Text = (this.ReconciliationEntity.ReturnAMT > 0) ? string.Format("{0:0.####}", this.ReconciliationEntity.ReturnAMT) : string.Empty;
            this.txtFineAMT .Text = (this.ReconciliationEntity.FineAMT  > 0) ? string.Format("{0:0.####}", this.ReconciliationEntity.FineAMT ) : string.Empty;
            this.txtTotalAMT.Text = (this.ReconciliationEntity.TotalAMT > 0) ? string.Format("{0:0.####}", this.ReconciliationEntity.TotalAMT) : string.Empty;
            this.SetRemindText();
            this.LoadDeliverItems();
            this.LoadRepairItems();
            this.LoadReplaceExpressAMT();
            this.LoadReturnItems();
            this.LoadFine();
        }
        private void SetRemindText()
        {
            int cnt=0;
            this.accDeliverItems.GetParmSaleDeliverItemsCountForReconciliation(this.ReconciliationID, ref cnt);
            this.tabDeliver.Text = "�ͻ�:" + cnt.ToString();

            this.accRepairItems .GetParmRepairDeliverItemsCountForReconciliation (this.ReconciliationID, ref cnt);
            this.tabRepair.Text = "ά��:" + cnt.ToString();

            this.accReturnItems.GetParmSaleReturnItemsCountForReconciliation(this.ReconciliationID, ref cnt);
            this.tabReturn.Text = "�˻�:" + cnt.ToString();

            this.accFineAMT.GetParmSaleFineAMTNotesCountForReconciliation (this.ReconciliationID, ref cnt);
            this.pageFine .Text = "�ۿ�:" + cnt.ToString();

            this.accReplaceExpressAMT.GetParmSaleReplaceExpressAMTRecordCountForReconciliation(this.ReconciliationID, ref cnt);
            this.pageReplaceExpressAMT.Text = "�����˷�:" + cnt.ToString();


        }
        private void SetAMT(DataTable dtblItems,string fieldname,TextBox  txt)
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
            decimal deliveramt;
            if (!decimal.TryParse(this.txtDeliverAMT.Text, out deliveramt))
            {
                deliveramt = 0;
            } 
            decimal repairamt;
            if (!decimal.TryParse(this.txtRepairAMT .Text, out repairamt))
            {
                repairamt = 0;
            }
            decimal replaceexpressamt;
            if (!decimal.TryParse(this.txtReplaceExpressAMT.Text, out replaceexpressamt))
            {
                replaceexpressamt = 0;
            }
            decimal returnamt;
            if (!decimal.TryParse(this.txtReturnAMT.Text, out returnamt))
            {
                returnamt = 0;
            }
       

            decimal fineamt;
            if (!decimal.TryParse(this.txtFineAMT.Text, out fineamt ))
            {
                fineamt = 0;
            }
            this.txtTotalAMT.Text = string.Format("{0:0.####}", deliveramt+repairamt+replaceexpressamt  - returnamt-fineamt);
        }
        private void LoadDeliverItems()
        {
            this.dtblDeliverItems = this.accDeliverItems.GetDataSaleDeliverItemsByReconciliationID(this.ReconciliationID).Tables[0];
            this.dgrdvDeliver.DataSource = this.dtblDeliverItems;
           
        }
        private void LoadRepairItems()
        {
            this.dtblRepairItems  = this.accRepairItems.GetDataRepairDeliverItemsByReconciliationID (this.ReconciliationID).Tables[0];
            this.dgrdvRepair.DataSource = this.dtblRepairItems;

        }
        private void LoadReturnItems()
        {
            this.dtblReturnItems  = this.accReturnItems .GetDataSaleReturnItemsByReconciliationID (this.ReconciliationID).Tables[0];
            this.dgrdvReturn.DataSource = this.dtblReturnItems;

        }
      
        private void LoadFine()
        {
            this.dtblFineAMT  = this.accFineAMT.GetDataSaleFineAMTNotesByReconciliationID (this.ReconciliationID).Tables[0];
            this.dgrdvFine .DataSource = this.dtblFineAMT;

        }
        private void LoadReplaceExpressAMT()
        {
            this.dtblReplaceAMT  = this.accReplaceExpressAMT .GetDataSaleReplaceExpressAMTRecordByReconciliationID (this.ReconciliationID).Tables[0];
            this.dgrdvReplaceExpressAMT.DataSource = this.dtblReplaceAMT;

        }
        private void SaveAMT()
        {
            string errormsg=string.Empty ;
            this.accReconciliations.UpdateSaleReconciliationsForAMT  (ref errormsg, this.ReconciliationID );
          

        }
        void dgrdvDeliver_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            object objItemID = this.dtblDeliverItems.DefaultView[irow]["ItemID"];
            if (objItemID == DBNull.Value)
            {
                return;
            }
            string errormsg = string.Empty;
            bool flag = this.accDeliverItems .UpdateSaleDeliverItemsCancelReconciliation (
                ref errormsg, objItemID);
            if (!flag)
            {

                MessageBox.Show(errormsg);
                e.Cancel = true;
            }
           
        }
        void dgrdvDeliver_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.SetAMT(this.dtblDeliverItems , "ItemAMT", this.txtDeliverAMT);
            this.SetRemindText();
        }
        void dgrdvDeliver_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                if (this.ReconciliationID > -1)
                {
                    if (frmDeliverItem == null)
                    {
                        frmDeliverItem = new JERPApp.Define.Product.FrmSaleDeliverItemForReconciliation();
                        new FrmStyle(frmDeliverItem).SetPopFrmStyle(this);
                        frmDeliverItem.AffterEnter += frmDeliverItem_AffterEnter;
                    }
                    frmDeliverItem.ReconciliationItem(this.ReconciliationID, this.ReconciliationEntity.Year, this.ReconciliationEntity.Month);
                    frmDeliverItem.ShowDialog();
                }
            }
        }
        void frmDeliverItem_AffterEnter()
        {
            this.LoadDeliverItems();
            this.SetAMT(this.dtblDeliverItems, "ItemAMT", this.txtDeliverAMT);
            this.SetRemindText();
        }

        void dgrdvRepair_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            object objItemID = this.dtblRepairItems.DefaultView[irow]["ItemID"];
            if (objItemID == DBNull.Value)
            {
                return;
            }
            string errormsg = string.Empty;
            bool flag = this.accRepairItems.UpdateRepairDeliverItemsCancelReconciliation (
                ref errormsg, objItemID);
            if (!flag)
            {

                MessageBox.Show(errormsg);
                e.Cancel = true;
            }

        }
        void dgrdvRepair_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.SetAMT(this.dtblRepairItems, "Amount", this.txtRepairAMT);
            this.SetRemindText();
        }
        void dgrdvRepair_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                if (this.ReconciliationID > -1)
                {
                    if (frmRepairItem == null)
                    {
                        frmRepairItem = new JERPApp.Define.Product.FrmRepairDeliverItemForReconciliation();
                        new FrmStyle(frmRepairItem).SetPopFrmStyle(this);
                        frmRepairItem.AffterEnter += frmRepairItem_AffterEnter;
                    }
                    frmRepairItem.ReconciliationItem(this.ReconciliationID, this.ReconciliationEntity.Year, this.ReconciliationEntity.Month);
                    frmRepairItem.ShowDialog();
                }
            }
        }
        void frmRepairItem_AffterEnter()
        {
            this.LoadRepairItems();
            this.SetAMT(this.dtblRepairItems, "ItemAMT", this.txtRepairAMT);
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
            bool flag = this.accReturnItems.UpdateSaleReturnItemsCancelReconciliation(
                ref errormsg, objItemID);
            if (!flag)
            {

                MessageBox.Show(errormsg);
                e.Cancel = true;
            }

        }
        void dgrdvReturn_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.SetAMT(this.dtblReturnItems, "ItemAMT", this.txtReturnAMT);
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
                        frmReturnItem = new JERPApp.Define.Product.FrmSaleReturnItemForReconciliation();
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
            this.SetAMT(this.dtblReturnItems, "ItemAMT", this.txtReturnAMT);
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
            bool flag = this.accFineAMT.UpdateSaleFineAMTNotesCancelReconciliation (
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

            if (e.Button == MouseButtons.Right)
            {
                if (this.ReconciliationID > -1)
                {
                    if (frmFine == null)
                    {
                        frmFine = new JERPApp.Define.Product.FrmSaleFineAMTForReconciliation ();
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


        void dgrdvReplaceExpressAMT_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            object objRecordID = this.dtblReplaceAMT .DefaultView[irow]["RecordID"];
            if (objRecordID == DBNull.Value)
            {
                return;
            }
            string errormsg = string.Empty;
            bool flag = this.accReplaceExpressAMT.UpdateSaleReplaceExpressAMTRecordCancelReconciliation (
                ref errormsg, objRecordID);
            if (!flag)
            {

                MessageBox.Show(errormsg);
                e.Cancel = true;
            }

        }
        void dgrdvReplaceExpressAMT_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

            this.SetAMT(this.dtblReplaceAMT , "Amount", this.txtReplaceExpressAMT );
            this.SetRemindText();
        }
        void dgrdvReplaceExpressAMT_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                if (this.ReconciliationID > -1)
                {
                    if (frmReplaceExpressAMT  == null)
                    {
                        frmReplaceExpressAMT = new JERPApp.Define.Product.FrmSaleReplaceExpressAMTRecordForReconciliation();
                        new FrmStyle(frmReplaceExpressAMT).SetPopFrmStyle(this);
                        frmReplaceExpressAMT.AffterConfirm += this.frmReplaceExpressAMT_AffterEnter;
                    }
                    frmReplaceExpressAMT.LoadReconciliaion(this.ReconciliationID);
                    frmReplaceExpressAMT.ShowDialog();
                }
            }
        }
        void frmReplaceExpressAMT_AffterEnter()
        {
            this.LoadReplaceExpressAMT ();
            this.SetAMT(this.dtblReplaceAMT , "Amount", this.txtReplaceExpressAMT );
            this.SetRemindText();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string errormsg = string.Empty;
            bool flag = false;
            flag = this.accReconciliations.UpdateSaleReconciliations(ref errormsg, this.ReconciliationID,this.txtReconciliationName.Text,
                this.dtpDateSettle.Value.Date);
            if (flag)
            {
                MessageBox.Show("�ɹ�������˵���Ϣ");
            }
            else
            {
                MessageBox.Show(errormsg);
            }
        }
        void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult rut = MessageBox.Show("�㽫ɾ����ǰ���˵�����ϸ�����ɾ�������ָܻ���ȷ�Ϸ�?", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rut == DialogResult.Yes)
            {
                string errormsg = string.Empty;
                bool flag = false;
                flag = this.accReconciliations.DeleteSaleReconciliations(ref errormsg, this.ReconciliationID);
                if (flag)
                {
                    MessageBox.Show("ɾ����ǰ���˵�����ϸ��");
                }
                else
                {
                    MessageBox.Show(errormsg);
                }
              
                this.Close();
            }
        }
        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            this.SaveAMT();
            long[] IDList = new long[] { this.ReconciliationID };
            this.printerhelper.ExportToExcel(IDList);
            FrmMsg.Hide();
        }
    
        void FrmSaleReconciliationOper_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.ReconciliationID > -1)
            {
                this.SaveAMT  ();
               if (this.affterSave != null) this.affterSave();
            }
        }
    }
}
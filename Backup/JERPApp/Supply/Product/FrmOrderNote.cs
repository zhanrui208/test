using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Supply.Product
{
    public partial class FrmOrderNote : Form
    {
        public FrmOrderNote()
        {
            InitializeComponent();
            this.dgrdvNonConfirm.AutoGenerateColumns = false;
            this.dgrdvPlan.AutoGenerateColumns = false;
            this.dgrdvNonFinished.AutoGenerateColumns = false;
            this.dgrdvFinished.AutoGenerateColumns = false;
            this.ctrlQPlan.SeachGridView = this.dgrdvPlan;
            this.ctrlQNonConfirm  .SeachGridView = this.dgrdvNonConfirm;
            this.ctrlQNonPrint.SeachGridView = this.dgrdvNonFinished;
            this.accNotes = new JERPData.Product.BuyOrderNotes();
            this.accBuyPlans = new JERPData.Product.BuyPlans();
            this.printer = new JERPBiz.Product.BuyOrderNotePrintHelper();
            this.SetPermit();
        }
        private JERPData.Product.BuyOrderNotes accNotes;
        private JERPData.Product.BuyPlans accBuyPlans;
        private JERPBiz.Product.BuyOrderNotePrintHelper printer;
        private FrmOrderNoteOper frmOper;
        private Report.Bill.FrmBuyOrderNote frmDetail;
        private DataTable dtblNonConfirms, dtblPlans, dtblNonFinisheds, dtblFinisheds;
        private string iniwhereclause = " and (NoteID not in(select NoteID from prd.BuyOrderItems where NonFinishedQty>0))";
        private string whereclause = string.Empty;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(34);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(35);
            if (this.enableBrowse)
            {
                //��������
                this.whereclause = this.iniwhereclause;
                LoadData();
                this.dgrdvNonConfirm.ContextMenuStrip = this.cMenu;
                this.dgrdvPlan.ContextMenuStrip = this.cMenu;
                this.dgrdvNonFinished.ContextMenuStrip = this.cMenu;
                this.dgrdvFinished.ContextMenuStrip = this.cMenu;
                this.dtpDateBegin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                this.dtpDateEnd.Value = this.dtpDateBegin.Value.AddMonths(1).AddDays(-1).Date;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.lnkRefreshAll.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkRefreshAll_LinkClicked);
                this.btnSearch.Click += new EventHandler(btnSearch_Click);
                this.pbar.OnPageIndexChanged += new EventHandler(pbar_OnPageIndexChanged);
            }
            this.lnkNew.Enabled = this.enableSave;
            this.ColumnbtnEdit.Visible = this.enableSave;
            this.ColumnbtnPrint.Visible = this.enableSave;
            if (this.enableSave)
            {
                this.lnkNew.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkNew_LinkClicked);
                this.dgrdvNonConfirm.CellContentClick += new DataGridViewCellEventHandler(dgrdvNonConfirm_CellContentClick);
                this.dgrdvNonFinished.CellContentClick += new DataGridViewCellEventHandler(dgrdvNonFinished_CellContentClick);
                this.dgrdvFinished.CellContentClick += new DataGridViewCellEventHandler(dgrdvFinished_CellContentClick);
            }


        }

     
        void dgrdvNonConfirm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdvNonConfirm.Columns[icol].Name == this.ColumnbtnEdit.Name)
            {
            
                long NoteID = (long)this.dtblNonConfirms.DefaultView[irow]["NoteID"];             
                if (this.frmOper == null)
                {
                    this.frmOper = new FrmOrderNoteOper();
                    new FrmStyle(frmOper).SetPopFrmStyle(this);
                    frmOper.AffterSave += this.LoadData;
                }
                frmOper.EditNote(NoteID);
                frmOper.ShowDialog();
            } 
           
        }

        void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.frmOper == null)
            {
                this.frmOper = new FrmOrderNoteOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave += this.LoadData;
            }
            frmOper.NewNote();
            frmOper.ShowDialog();
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            this.whereclause = this.iniwhereclause;
            if (this.ckbSupplier.Checked)
            {
                this.whereclause += " and (CompanyID=" + this.ctrlSupplierID.CompanyID.ToString() + ")";
            }
            if (this.ckbNoteCode.Checked)
            {
                this.whereclause += " and (NoteCode like '%" + this.txtNoteCode.Text + "%')";
            }
            if (this.ckbDateNote.Checked)
            {
                this.whereclause += " and (DateNote>='" + this.dtpDateBegin.Value.ToShortDateString()
                    + "' and DateNote<='" + this.dtpDateEnd.Value.ToShortDateString() + "')";

            }
            this.LoadFinished();
        }

        void pbar_OnPageIndexChanged(object sender, EventArgs e)
        {
            int cnt = 0;
            this.dtblFinisheds = this.accNotes.GetDataBuyOrderNotesDescPagesFreeSearch(this.pbar.PageIndex,
                this.pbar.PageSize, ref cnt, this.whereclause).Tables[0];
            this.dgrdvFinished.DataSource = this.dtblFinisheds;
        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            if (this.cMenu.SourceControl == this.dgrdvPlan)
            {
                this.LoadPlan();
            }
            if (this.cMenu.SourceControl == this.dgrdvNonConfirm)
            {
                this.LoadNonConfirm();
            }
            if (this.cMenu.SourceControl == this.dgrdvNonFinished)
            {
                this.LoadNonFinished();
            } 
            if (this.cMenu.SourceControl == this.dgrdvFinished)
            {
                this.whereclause = this.iniwhereclause;
                this.LoadFinished();
            }
        }
        void lnkRefreshAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
            this.whereclause = this.iniwhereclause;
            LoadData();
        }
        private void LoadPlan()
        {
            this.dtblPlans = this.accBuyPlans.GetDataBuyPlansForOrder().Tables[0];
            this.dgrdvPlan.DataSource = this.dtblPlans;
            this.pagePlan.Text = "�ɹ��ƻ�[" + this.dtblPlans.Rows.Count.ToString() + "]";
        }
        private void LoadNonConfirm()
        {
            this.dtblNonConfirms = this.accNotes.GetDataBuyOrderNotesNonConfirm().Tables[0];
            this.dgrdvNonConfirm.DataSource = this.dtblNonConfirms;
            this.pageNonConfirm.Text = "δ���[" + this.dtblNonConfirms.Rows.Count.ToString() + "]";
        }
        private void LoadNonFinished()
        {
            this.dtblNonFinisheds = this.accNotes.GetDataBuyOrderNotesNonFinished ().Tables[0];
            this.dgrdvNonFinished.DataSource = this.dtblNonFinisheds;
            this.pageNonFinished.Text = "δ���[" + this.dtblNonFinisheds.Rows.Count.ToString() + "]";
        }
        private void LoadFinished()
        {
            int cnt = 0;
            this.dtblFinisheds = this.accNotes.GetDataBuyOrderNotesDescPagesFreeSearch(1, this.pbar.PageSize,
                ref cnt, this.whereclause).Tables[0];
            this.dgrdvFinished.DataSource = this.dtblFinisheds;
            this.pbar.Init(1, cnt);
        }
        private void LoadData()
        {
            this.LoadPlan();
            this.LoadNonConfirm();
            this.LoadNonFinished();
            this.LoadFinished();
        }
        void ShowFrmDetail(long NoteID)
        {
            if (frmDetail == null)
            {
                frmDetail = new JERPApp.Supply.Product .Report.Bill.FrmBuyOrderNote();
                new FrmStyle(frmDetail).SetPopFrmStyle(this);
            }
            frmDetail.Detail(NoteID);
            frmDetail.ShowDialog();
        }
        void dgrdvNonFinished_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            long NoteID = (long)this.dtblNonFinisheds.DefaultView[irow]["NoteID"];
            if (this.dgrdvNonFinished.Columns[icol].Name == this.ColumnbtnPrint.Name)
            {

                FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
                this.printer.ExportToExcel(NoteID);
                FrmMsg.Hide();

                string errormsg = string.Empty;
                this.accNotes.UpdateBuyOrderNotesForPrint(ref errormsg,
                    NoteID);
                this.LoadNonFinished();
                this.whereclause = this.iniwhereclause;
                this.LoadFinished();
            }
            if (this.dgrdvNonFinished.Columns[icol].DataPropertyName == "NoteCode")
            {
                this.ShowFrmDetail(NoteID);
            }
        }

        void dgrdvFinished_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdvFinished.Columns[icol].DataPropertyName == "NoteCode")
            {
                long NoteID = (long)this.dtblFinisheds.DefaultView[irow]["NoteID"];
                this.ShowFrmDetail(NoteID);
            }
        }

    
    }
}
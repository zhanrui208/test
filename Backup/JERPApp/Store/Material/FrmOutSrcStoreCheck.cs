using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Material
{
    public partial class FrmOutSrcStoreCheck : Form
    {
        public FrmOutSrcStoreCheck()
        {
            InitializeComponent();
            this.dgrdvConfirm.AutoGenerateColumns = false;
            this.dgrdvNonConfirm.AutoGenerateColumns = false;
            this.accNotes = new JERPData.Material .OutSrcStoreCheckNotes();
            this.SetPermit();
        }
        private JERPData.Material.OutSrcStoreCheckNotes accNotes;
        private FrmOutSrcStoreCheckOper frmOper;
        private FrmOutSrcStoreCheckConfirm frmConfirm;
        private  JERPApp.Store.Material.Report.Bill .FrmOutSrcStoreCheckNote frmDetail;
        private string whereclause = string.Empty;
        private string initwhereclause = " and (ConfirmPsnID is not null)";
        private DataTable dtblNonConfirms,dtblConfirms;
       
        //权限码
        private bool enableBrowse = false;//浏览
        private bool enableSave = false;//保存
        private void SetPermit()
        {

            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(649);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(650);
            if (this.enableBrowse)
            {
                //加载数据
                this.whereclause = this.initwhereclause;
                this.LoadConfirmData();
                this.LoadNonConfirmData();
                this.dgrdvNonConfirm.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.ctrlNoteSearch.AffterSearch += new JCommon.ctrlNoteSearch.AffterSearchDelegate(ctrlNoteSearch_AffterSearch);
                this.dgrdvConfirm.CellContentClick += new DataGridViewCellEventHandler(dgrdvConfirm_CellContentClick);
                this.pbar.OnPageIndexChanged += new EventHandler(pbar_OnPageIndexChanged);
            }
            this.lnkNew.Enabled = this.enableSave;
            this.ColumnbtnEdit.Visible = this.enableSave;
            this.ColumnbtnConfirm.Visible = this.enableSave;
            if (this.enableSave)
            {
                this.lnkNew.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkNew_LinkClicked);
                this.dgrdvNonConfirm.CellContentClick += new DataGridViewCellEventHandler(dgrdvNonConfirm_CellContentClick);
            }

        }

    
        void mItemRefresh_Click(object sender, EventArgs e)
        { 
            this.LoadNonConfirmData();
        }
        void ctrlNoteSearch_AffterSearch(string whereclause)
        {
            this.whereclause =this.initwhereclause + whereclause;
            this.LoadConfirmData();
        }

        void dgrdvNonConfirm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            long NoteID = (long)this.dtblNonConfirms.DefaultView[irow]["NoteID"];
            if (this.dgrdvNonConfirm .Columns[icol].Name == this.ColumnbtnEdit .Name)
            {
                if (this.frmOper == null)
                {
                    this.frmOper = new FrmOutSrcStoreCheckOper();
                    new FrmStyle(frmOper).SetPopFrmStyle(this);
                    frmOper.AffterSave += this.LoadNonConfirmData;
                }
                frmOper.Edit (NoteID);
                frmOper.ShowDialog();
            }
            if (this.dgrdvNonConfirm .Columns[icol].Name == this.ColumnbtnConfirm .Name)
            {
                if (this.frmConfirm == null)
                {
                    this.frmConfirm = new FrmOutSrcStoreCheckConfirm();
                    new FrmStyle(frmConfirm).SetPopFrmStyle(this);
                    this.frmConfirm.AffterSave +=frmConfirm_AffterSave;
                }
                this.frmConfirm.Confirm(NoteID);
                this.frmConfirm.ShowDialog();
            }
        }

        void frmConfirm_AffterSave()
        {
            this.LoadNonConfirmData();
            this.whereclause = this.initwhereclause;
            this.LoadConfirmData();
        }

        void dgrdvConfirm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdvConfirm.Columns[icol].Name == this.ColumnbtnDetail .Name)
            {
                long NoteID =(long) this.dtblConfirms.DefaultView[irow]["NoteID"];
                if (this.frmDetail  == null)
                {
                    this.frmDetail = new JERPApp.Store.Material.Report.Bill.FrmOutSrcStoreCheckNote();
                    new FrmStyle(frmDetail).SetPopFrmStyle(this);                   
                }
                frmDetail.DetailNote (NoteID);
                frmDetail.ShowDialog();
            }
        }

        void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.frmOper == null)
            {
                this.frmOper = new FrmOutSrcStoreCheckOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave += this.LoadNonConfirmData;
            }
            frmOper.New ();
            frmOper.ShowDialog();
        }
 
        private void LoadNonConfirmData()
        {
            this.dtblNonConfirms = this.accNotes.GetDataOutSrcStoreCheckNotesNonConfirm().Tables[0];
            this.dgrdvNonConfirm.DataSource = this.dtblNonConfirms;
        }
        private void LoadConfirmData()
        {
            int cnt = 0;
            this.dtblConfirms = this.accNotes.GetDataOutSrcStoreCheckNotesDescPagesFreeSearch  (1,this.pbar .PageSize ,ref cnt,this.whereclause ).Tables [0];
            this.dgrdvConfirm.DataSource = this.dtblConfirms;
            this.pbar.Init(1, cnt);
        }
        void pbar_OnPageIndexChanged(object sender, EventArgs e)
        {
            int cnt = 0;
            this.dtblConfirms = this.accNotes.GetDataOutSrcStoreCheckNotesDescPagesFreeSearch(this.pbar.PageIndex, this.pbar.PageSize, ref cnt, this.whereclause).Tables[0];
            this.dgrdvConfirm.DataSource = this.dtblConfirms;
        }
    }
}
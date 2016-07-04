using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Material
{
    public partial class FrmOtherOutStoreNote : Form
    {
        public FrmOtherOutStoreNote()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accNotes = new JERPData.Material.OtherOutStoreNotes();
            this.SetPermit();
        }
        private JERPData.Material.OtherOutStoreNotes accNotes;
        private FrmOtherOutStoreNoteOper frmOper;
        private JERPApp.Store.Material.Report.Bill.FrmOtherOutStoreNote frmDetail;
        private string whereclause = string.Empty;
        private DataTable dtblNotes;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(230);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(231);
            if (this.enableBrowse)
            {
                //��������
                LoadData();
                this.ctrlNoteSearch.AffterSearch += new JCommon.ctrlNoteSearch.AffterSearchDelegate(ctrlNoteSearch_AffterSearch);
                this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
                this.pbar.OnPageIndexChanged += new EventHandler(pbar_OnPageIndexChanged);
            }
            this.lnkNew.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.lnkNew.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkNew_LinkClicked);
               
            }

        }
        void ctrlNoteSearch_AffterSearch(string whereclause)
        {
            this.whereclause = whereclause;
            this.LoadData();
        }
        void pbar_OnPageIndexChanged(object sender, EventArgs e)
        {
            int cnt = 0;
            this.dtblNotes = this.accNotes.GetDataOtherOutStoreNotesDescPagesFreeSearch (this.pbar .PageIndex , this.pbar.PageSize, ref cnt,this.whereclause ).Tables[0];
            this.dgrdv.DataSource = this.dtblNotes;
        }

        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnbtnDetail .Name)
            {
                long NoteID =(long) this.dtblNotes.DefaultView[irow]["NoteID"];
                if (this.frmDetail  == null)
                {
                    this.frmDetail = new JERPApp.Store.Material.Report.Bill.FrmOtherOutStoreNote();
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
                this.frmOper = new FrmOtherOutStoreNoteOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave += new FrmOtherOutStoreNoteOper.AffterSaveDelegate(frmOper_AffterSave);
            }
            frmOper.NewNote();
            frmOper.ShowDialog();
        }

        void frmOper_AffterSave()
        {
            this.whereclause = string.Empty;
            this.LoadData();
        }

        private void LoadData()
        {
            int cnt = 0;
            this.dtblNotes = this.accNotes.GetDataOtherOutStoreNotesDescPagesFreeSearch  (1,this.pbar .PageSize ,ref cnt,this.whereclause ).Tables [0];
            this.dgrdv.DataSource = this.dtblNotes;
            this.pbar.Init(1, cnt);
        }

    }
}
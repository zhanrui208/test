﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Test
{
    public partial class FrmOtherInStore : Form
    {
        private FrmOtherInStoreOper frmOper;


        private JERPData.Product.StoreNotesOther accNotes;
       
        private JERPApp.Store.OtherRes.Report.Bill.FrmOutStoreNote frmDetail;
        private string whereclause = string.Empty;
        private DataTable dtblNotes;

        //权限码
        private bool enableBrowse = false;//浏览
        private bool enableSave = false;//保存

        public FrmOtherInStore()
        {
            InitializeComponent();
            this.mdgrdv.AutoGenerateColumns = false;
            this.accNotes = new JERPData.Product.StoreNotesOther();
            this.SetPermit();
        }
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(574);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(575);
            if (this.enableBrowse)
            {
                //加载数据
                LoadData();
                this.ctrlNoteSearch.AffterSearch += new JCommon.ctrlNoteSearch.AffterSearchDelegate(ctrlNoteSearch_AffterSearch);
                this.mdgrdv.CellContentClick += new DataGridViewCellEventHandler(mdgrdv_CellContentClick);
                this.pbar.OnPageIndexChanged += new EventHandler(pbar_OnPageIndexChanged);
            }
            this.linkLabel1.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
               
            }

        }
        void ctrlNoteSearch_AffterSearch(string whereclause)
        {
            this.whereclause = whereclause;
            this.LoadData();
        }

        void mdgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.mdgrdv.Columns[icol].Name == this.ColumnbtnDetail.Name)
            {
                long NoteID = (long)this.dtblNotes.DefaultView[irow]["NoteID"];
                if (this.frmDetail == null)
                {
                    this.frmDetail = new JERPApp.Store.OtherRes.Report.Bill.FrmOutStoreNote();
                    new FrmStyle(frmDetail).SetPopFrmStyle(this);
                }
                frmDetail.DetailNote(NoteID);
                frmDetail.ShowDialog();
            }
        }
        void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmOper == null)
            {
                frmOper = new FrmOtherInStoreOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave += new FrmOtherInStoreOper.AffterSaveDelegate(frmOper_AffterSave);
            }
            frmOper.NewNote();
            frmOper.ShowDialog();
        }

        void pbar_OnPageIndexChanged(object sender, EventArgs e)
        {
            int cnt = 0;
            this.dtblNotes = this.accNotes.GetDataStoreNotesOtherDescPagesFreeSearch(1, this.pbar.PageSize, ref cnt, this.whereclause).Tables[0];
            this.mdgrdv.DataSource = this.dtblNotes;
        }


        void frmOper_AffterSave()
        {
            this.whereclause = string.Empty;
            this.LoadData();
        }

        private void LoadData()
        {
            int cnt = 0;
            this.dtblNotes = this.accNotes.GetDataStoreNotesOtherDescPagesFreeSearch(1, this.pbar.PageSize, ref cnt, this.whereclause).Tables[0];
            this.mdgrdv.DataSource = this.dtblNotes;
            this.pbar.Init(1, cnt);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Payable.Material
{
    public partial class FrmBuyReturnNote : Form
    {
        public FrmBuyReturnNote()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accNotes = new JERPData.Material.BuyReturnNotes();
            this.SetPermit();
        }
        private JERPData.Material.BuyReturnNotes accNotes;
        private DataTable dtblNotes;
        private FrmBuyReturnNoteOper frmOper;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(489);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(490);
            if (this.enableBrowse)
            {
                //��������
                LoadData();
                this.dgrdv .ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
               
            }
            this.ColumnbtnConfirm .Visible = this.enableSave;
            if (this.enableSave)
            {
                this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
            }
        }

        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnbtnConfirm.Name)
            {
                long NoteID = (long)this.dtblNotes.DefaultView[irow]["NoteID"];
                if (frmOper == null)
                {
                    frmOper = new FrmBuyReturnNoteOper();
                    new FrmStyle(frmOper).SetPopFrmStyle(this);
                    frmOper.AffterSave += this.LoadData;
                }
                this.frmOper.ConfirmOper(NoteID);
                this.frmOper.ShowDialog();
            }
        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        private void LoadData()
        {
            this.dtblNotes = this.accNotes.GetDataBuyReturnNotesForFinance ().Tables[0];
            this.dgrdv.DataSource = this.dtblNotes;
        }

    }
}
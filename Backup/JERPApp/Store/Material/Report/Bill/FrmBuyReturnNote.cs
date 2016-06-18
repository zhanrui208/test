using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Material.Report.Bill
{
    public partial class FrmBuyReturnNote : FrmBizBill 
    {
        public FrmBuyReturnNote()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.NoteEntity = new JERPBiz.Material.BuyReturnNoteEntity();
            this.accItems = new JERPData.Material.BuyReturnItems();
            this.printerhelper = new JERPBiz.Material.BuyReturnNotePrintHelper();
            this.btnExplore .Click  +=new EventHandler(btnExplore_Click);
        }
        private JERPBiz.Material.BuyReturnNoteEntity NoteEntity;
        private JERPData.Material.BuyReturnItems accItems;
        private JERPBiz.Material.BuyReturnNotePrintHelper printerhelper;
        private DataTable dtblItems;            
        public override void DetailNote(long NoteID)
        {
          
            this.NoteEntity.LoadData(NoteID);
            this.txtCompanyAbbName.Text = this.NoteEntity.CompanyAbbName;
            this.txtDateNote.Text = this.NoteEntity.DateNote.ToShortDateString();
            this.txtNoteCode.Text = this.NoteEntity.NoteCode;
            this.txtMakerPsn.Text = this.NoteEntity.MakerPsn;
            this.txtReturnHandleTypeName.Text = this.NoteEntity.ReturnHandleTypeName;
            this.rchMemo.Text = this.NoteEntity.Memo;
            this.dtblItems = this.accItems.GetDataBuyReturnItemsByNoteID(NoteID).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
        }

        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            this.printerhelper.ExportToExcel(this.NoteEntity.NoteID);
            FrmMsg.Hide();
        }
    }
}
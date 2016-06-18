using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Product.Report.Bill
{
    public partial class FrmOutStoreNote : FrmBizBill 
    {
        public FrmOutStoreNote()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.NoteEntity = new JERPBiz.Product.OutStoreNoteEntity();
            this.accItems = new JERPData.Product.OutStoreItems();
            this.btnExplore .Click  +=new EventHandler(btnExplore_Click);
        }
        private JERPBiz.Product.OutStoreNoteEntity NoteEntity;
        private JERPData.Product.OutStoreItems accItems;
        private DataTable dtblItems;
     
        public override void DetailNote(long NoteID)
        {
            this.NoteEntity.LoadData(NoteID);
            this.txtDateNote.Text = this.NoteEntity.DateNote.ToShortDateString();
            this.txtNoteCode.Text = this.NoteEntity.NoteCode;
            this.txtMakerPsn.Text = this.NoteEntity.MakerPsn;
            this.rchMemo.Text = this.NoteEntity.Memo;
            this.dtblItems = this.accItems.GetDataOutStoreItemsByNoteID(NoteID).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
        }

        void btnExplore_Click(object sender, EventArgs e)
        {

            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "��Ʒ���ⵥ");
            excel.SetCellVal("A2", "���ⵥ��:" + this.txtNoteCode.Text + "   �Ƶ�����:" + this.txtDateNote.Text + " �Ƶ�:" + this.txtMakerPsn.Text);
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            FrmMsg.Hide();
            excel.Show();
           
        }
    }
}
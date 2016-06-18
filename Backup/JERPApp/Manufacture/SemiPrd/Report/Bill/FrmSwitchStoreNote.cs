using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture.SemiPrd.Report.Bill
{
    public partial class FrmSwitchStoreNote : Form  
    {
        public FrmSwitchStoreNote()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.NoteEntity = new JERPBiz.SemiPrd .SwitchStoreNoteEntity();
            this.accItems = new JERPData.SemiPrd.SwitchStoreItems();
            this.btnExplore .Click  +=new EventHandler(btnExplore_Click);
        }
        private JERPBiz.SemiPrd.SwitchStoreNoteEntity NoteEntity;
        private JERPData.SemiPrd.SwitchStoreItems accItems;  
     
        private DataTable dtblItems;            
        public  void DetailNote(long NoteID)
        {
            this.NoteEntity.LoadData(NoteID);
            this.txtDateNote.Text = this.NoteEntity.DateNote.ToShortDateString();
            this.txtNoteCode.Text = this.NoteEntity.NoteCode;
            this.txtMakerPsn.Text = this.NoteEntity.MakerPsn;
            this.rchMemo.Text = this.NoteEntity.Memo;
            this.dtblItems = this.accItems.GetDataSwitchStoreItemsByNoteID(NoteID).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
        }

        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "�������͵�");
            excel.SetCellVal("A2", "���͵���:" + this.txtNoteCode.Text +"   �Ƶ�����:" + this.txtDateNote.Text + " �Ƶ�:" + this.txtMakerPsn.Text);
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            FrmMsg.Hide();
            excel.Show();
        }
    }
}
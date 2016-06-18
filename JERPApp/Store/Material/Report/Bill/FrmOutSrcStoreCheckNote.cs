using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Material.Report.Bill
{
    public partial class FrmOutSrcStoreCheckNote : Form  
    {
        public FrmOutSrcStoreCheckNote()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.NoteEntity = new JERPBiz.Material.OutSrcStoreCheckNoteEntity();
            this.accItems = new JERPData.Material.OutSrcStoreCheckItems();
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.btnExplore .Click +=new EventHandler(btnExplore_Click);
        }
        private JERPBiz.Material.OutSrcStoreCheckNoteEntity NoteEntity;
        private JERPData.Material.OutSrcStoreCheckItems accItems;
        private DataTable dtblItems;      
     
        public  void  DetailNote(long NoteID)
        {
            this.NoteEntity.LoadData(NoteID);
            this.txtNoteCode.Text = this.NoteEntity.NoteCode;
            this.txtCheckPersons.Text = this.NoteEntity.CheckPersons;
            this.txtCompanyAbbName.Text = this.NoteEntity.CompanyAbbName ;
            this.txtDateNote.Text = this.NoteEntity .DateNote .ToShortDateString ();
            this.rchMemo.Text = this.NoteEntity.Memo;        
            this.dtblItems = this.accItems.GetDataOutSrcStoreCheckItemsByNoteID(NoteID ).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
        }


        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "�����̵㵥");
            excel.SetCellVal("A2", "�̵㵥��:" + this.txtNoteCode.Text +"  ί����:" + this.txtCompanyAbbName.Text+
                "   �̵���:" + this.txtCheckPersons .Text  + "   �̵�����:" + this.txtDateNote .Text );
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            rowIndex ++;
            excel.SetCellVal("A" + rowIndex.ToString(), "��ע:" + this.rchMemo.Text);
            FrmMsg.Hide();
            excel.Show();
        }
    }
}
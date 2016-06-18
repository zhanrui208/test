using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.MaterialOEM.Report.Bill
{
    public partial class FrmReceiveNote : FrmBizBill 
    {
        public FrmReceiveNote()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.NoteEntity = new JERPBiz.Material.OEMReceiveNoteEntity();
            this.accItems = new JERPData.Material.OEMReceiveItems();
            this.btnExplore .Click  +=new EventHandler(btnExplore_Click);
        }
        private JERPBiz.Material.OEMReceiveNoteEntity NoteEntity;
        private JERPData.Material.OEMReceiveItems accItems;  
     
        private DataTable dtblItems;            
        public override void DetailNote(long NoteID)
        {
          
            this.NoteEntity.LoadData(NoteID);
            this.txtCompanyAbbName.Text = this.NoteEntity.CompanyAbbName;
            this.txtDateNote.Text = this.NoteEntity.DateNote.ToShortDateString();
            this.txtPONo.Text = this.NoteEntity.PONo;
            this.txtNoteCode.Text = this.NoteEntity.NoteCode;
            this.txtMakerPsn.Text = this.NoteEntity.MakerPsn;
            this.rchMemo.Text = this.NoteEntity.Memo;
            this.dtblItems = this.accItems.GetDataOEMReceiveItemsByNoteID(NoteID).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
        }

        void btnExplore_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "ԭ�Ͽ͹��ջ���");
            excel.SetCellVal("A2", "�ͻ�����:" + this.txtNoteCode.Text +
                "   �ͻ�:" + this.NoteEntity.CompanyAbbName + "   �ͻ�����:" + this.txtDateNote.Text + " �Ƶ�:" + this.txtMakerPsn.Text);
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            FrmMsg.Hide();
            excel.Show();
        }
    }
}